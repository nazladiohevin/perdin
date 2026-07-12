using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.BusinessTripRequest;
using Perdin.WebApi.Helpers;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/business-trip-requests")]
    [Authorize]
    public class BusinessTripRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusinessTripRequestsController(AppDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                ?? User.FindFirstValue("sub");
            return int.TryParse(userIdStr, out var userId) ? userId : 0;
        }

        private bool IsAdminOrSdm()
        {
            return User.IsInRole("ADMIN") || User.IsInRole("SDM");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string sortBy = "newest")
        {
            var query = _context.BusinessTripRequests
                .Include(r => r.User)
                .Include(r => r.OriginCity)
                .Include(r => r.DestinationCity)
                .Include(r => r.DestinationCountry)
                .AsQueryable();

            if (!IsAdminOrSdm())
            {
                var userId = GetCurrentUserId();
                query = query.Where(r => r.UserId == userId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                var allowedStatuses = new[] { "reviewed", "rejected", "approved" };
                if (allowedStatuses.Contains(status.ToLower()))
                {
                    query = query.Where(r => r.Status == status.ToLower());
                }
            }

            if (sortBy.ToLower() == "oldest")
            {
                query = query.OrderBy(r => r.CreatedAt);
            }
            else
            {
                query = query.OrderByDescending(r => r.CreatedAt);
            }

            var requests = await query.Select(r => new BusinessTripRequestListResponse
            {
                Id = r.Id,
                RequestNumber = r.RequestNumber,
                UserName = IsAdminOrSdm() ? r.User.Name : null,
                DepartureDate = r.DepartureDate,
                ReturnDate = r.ReturnDate,
                OriginCity = r.OriginCity.Name,
                DestinationCity = r.DestinationCity != null ? r.DestinationCity.Name : null,
                DestinationCountry = r.DestinationCountry.Name,
                Status = r.Status,
                IsForeign = r.DestinationCountry.IsForeign,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToListAsync();

            return Ok(ApiResponse<List<BusinessTripRequestListResponse>>.SuccessResponse(requests, "Pengajuan perjalanan dinas berhasil diambil"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _context.BusinessTripRequests
                .Include(r => r.User)
                .Include(r => r.Approver)
                .Include(r => r.OriginCity)
                    .ThenInclude(c => c.Province)
                .Include(r => r.DestinationCity)
                    .ThenInclude(c => c.Province)
                .Include(r => r.DestinationCountry)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Pengajuan perjalanan dinas tidak ditemukan"));
            }

            if (!IsAdminOrSdm() && request.UserId != GetCurrentUserId())
            {
                return StatusCode(403, ApiResponse<object>.ErrorResponse("Anda tidak memiliki akses ke pengajuan ini"));
            }

            double? distance = null;
            if (request.DestinationCity != null)
            {
                distance = DistanceHelper.CalculateDistance(
                    request.OriginCity.Latitude, request.OriginCity.Longitude,
                    request.DestinationCity.Latitude, request.DestinationCity.Longitude);
            }

            var response = new BusinessTripRequestDetailResponse
            {
                Id = request.Id,
                RequestNumber = request.RequestNumber,
                User = new BusinessTripRequestDetailResponse.UserInfo
                {
                    Id = request.User.Id,
                    Name = request.User.Name
                },
                DepartureDate = request.DepartureDate,
                ReturnDate = request.ReturnDate,
                OriginCityId = request.OriginCityId,
                OriginCityName = request.OriginCity.Name,
                DestinationCityId = request.DestinationCityId,
                DestinationCityName = request.DestinationCity?.Name,
                DestinationCountryId = request.DestinationCountryId,
                DestinationCountryName = request.DestinationCountry.Name,
                Status = request.Status,
                ApproverId = request.ApproverId,
                ApproverName = request.Approver?.Name,
                Purpose = request.Purpose,
                PocketMoney = request.PocketMoney,
                Distance = distance,
                TotalDays = request.DurationInDays,
                ApprovedAt = request.ApprovedAt,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            };

            return Ok(ApiResponse<BusinessTripRequestDetailResponse>.SuccessResponse(response, "Pengajuan perjalanan dinas ditemukan"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BusinessTripRequestCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            if (request.ReturnDate.Value < request.DepartureDate.Value)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Tanggal kepulangan tidak boleh lebih awal dari keberangkatan"));
            }

            var originCity = await _context.Cities.Include(c => c.Province).FirstOrDefaultAsync(c => c.Id == request.OriginCityId);
            if (originCity == null) return BadRequest(ApiResponse<object>.ErrorResponse("Kota asal tidak valid"));

            var destinationCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == request.DestinationCountryId);
            City? destinationCity = null;

            if (destinationCountry == null || !destinationCountry.IsForeign)
            {
                if (request.DestinationCityId != null)
                {
                    destinationCity = await _context.Cities.Include(c => c.Province).FirstOrDefaultAsync(c => c.Id == request.DestinationCityId);
                    if (destinationCity == null) return BadRequest(ApiResponse<object>.ErrorResponse("Kota tujuan tidak valid"));
                }
                
                if (request.DestinationCountryId == null)
                {
                    request.DestinationCountryId = 1; // Default Indonesia
                }
                destinationCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == request.DestinationCountryId);
            }

            int userId;
            if (IsAdminOrSdm())
            {
                if (!request.UserId.HasValue) return BadRequest(ApiResponse<object>.ErrorResponse("UserId wajib diisi oleh Admin/SDM"));
                userId = request.UserId.Value;
                if (!await _context.Users.AnyAsync(u => u.Id == userId)) return BadRequest(ApiResponse<object>.ErrorResponse("User tidak ditemukan"));
            }
            else
            {
                userId = GetCurrentUserId();
            }

            int durationInDays = request.ReturnDate.Value.DayNumber - request.DepartureDate.Value.DayNumber;

            double distance = 0;
            bool isForeign = destinationCountry?.IsForeign ?? false;
            bool isSameProvince = false;
            bool isSameIsland = false;

            if (!isForeign && destinationCity != null)
            {
                distance = DistanceHelper.CalculateDistance(
                    originCity.Latitude, originCity.Longitude,
                    destinationCity.Latitude, destinationCity.Longitude);
                
                isSameProvince = originCity.ProvinceId == destinationCity.ProvinceId;
                isSameIsland = originCity.Province.Island == destinationCity.Province.Island;
            }

            int pocketMoney = PocketMoneyHelper.CalculatePocketMoney(distance, isForeign, isSameProvince, isSameIsland, durationInDays);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var requestNumber = await RequestNumberHelper.GenerateRequestNumberAsync(_context);

                var tripRequest = new BusinessTripRequest
                {
                    RequestNumber = requestNumber,
                    UserId = userId,
                    DepartureDate = request.DepartureDate.Value,
                    ReturnDate = request.ReturnDate.Value,
                    OriginCityId = request.OriginCityId,
                    DestinationCityId = request.DestinationCityId,
                    DestinationCountryId = request.DestinationCountryId.Value,
                    DurationInDays = durationInDays,
                    Purpose = request.Purpose,
                    PocketMoney = pocketMoney,
                    Status = "reviewed"
                };

                _context.BusinessTripRequests.Add(tripRequest);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return StatusCode(201, ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil dibuat"));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Terjadi kesalahan saat membuat pengajuan"));
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BusinessTripRequestUpdateRequest request)
        {
            var tripRequest = await _context.BusinessTripRequests
                .Include(r => r.OriginCity).ThenInclude(c => c.Province)
                .Include(r => r.DestinationCity).ThenInclude(c => c.Province)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (tripRequest == null) return NotFound(ApiResponse<object>.ErrorResponse("Pengajuan tidak ditemukan"));

            if (!IsAdminOrSdm() && tripRequest.UserId != GetCurrentUserId())
            {
                return StatusCode(403, ApiResponse<object>.ErrorResponse("Anda tidak memiliki akses"));
            }

            bool needsRecalculateDuration = false;
            bool needsRecalculatePocketMoney = false;

            if (request.DepartureDate.HasValue)
            {
                tripRequest.DepartureDate = request.DepartureDate.Value;
                needsRecalculateDuration = true;
            }
            if (request.ReturnDate.HasValue)
            {
                tripRequest.ReturnDate = request.ReturnDate.Value;
                needsRecalculateDuration = true;
            }

            if (needsRecalculateDuration)
            {
                if (tripRequest.ReturnDate < tripRequest.DepartureDate)
                    return BadRequest(ApiResponse<object>.ErrorResponse("Tanggal kepulangan tidak boleh lebih awal dari keberangkatan"));
                
                tripRequest.DurationInDays = tripRequest.ReturnDate.DayNumber - tripRequest.DepartureDate.DayNumber;
                needsRecalculatePocketMoney = true;
            }

            if (request.OriginCityId.HasValue)
            {
                tripRequest.OriginCityId = request.OriginCityId.Value;
                needsRecalculatePocketMoney = true;
            }
            if (request.DestinationCityId.HasValue)
            {
                tripRequest.DestinationCityId = request.DestinationCityId.Value;
                tripRequest.DestinationCountryId = 1;
                needsRecalculatePocketMoney = true;
            }
            if (request.DestinationCountryId.HasValue)
            {
                tripRequest.DestinationCountryId = request.DestinationCountryId.Value;
                if (tripRequest.DestinationCountryId != 1)
                {
                    tripRequest.DestinationCityId = null;
                }
                needsRecalculatePocketMoney = true;
            }
            
            if (request.Purpose != null)
            {
                tripRequest.Purpose = request.Purpose;
            }

            if (request.UserId.HasValue && IsAdminOrSdm())
            {
                if (!await _context.Users.AnyAsync(u => u.Id == request.UserId.Value))
                    return BadRequest(ApiResponse<object>.ErrorResponse("User tidak ditemukan"));
                tripRequest.UserId = request.UserId.Value;
            }

            if (needsRecalculatePocketMoney)
            {
                var originCity = await _context.Cities.Include(c => c.Province).FirstOrDefaultAsync(c => c.Id == tripRequest.OriginCityId);
                var destinationCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == tripRequest.DestinationCountryId);
                City? destinationCity = tripRequest.DestinationCityId.HasValue 
                    ? await _context.Cities.Include(c => c.Province).FirstOrDefaultAsync(c => c.Id == tripRequest.DestinationCityId)
                    : null;

                if (originCity == null) return BadRequest(ApiResponse<object>.ErrorResponse("Kota asal tidak valid"));

                double distance = 0;
                bool isForeign = destinationCountry?.IsForeign ?? false;
                bool isSameProvince = false;
                bool isSameIsland = false;

                if (!isForeign && destinationCity != null)
                {
                    distance = DistanceHelper.CalculateDistance(
                        originCity.Latitude, originCity.Longitude,
                        destinationCity.Latitude, destinationCity.Longitude);
                    
                    isSameProvince = originCity.ProvinceId == destinationCity.ProvinceId;
                    isSameIsland = originCity.Province.Island == destinationCity.Province.Island;
                }

                tripRequest.PocketMoney = PocketMoneyHelper.CalculatePocketMoney(distance, isForeign, isSameProvince, isSameIsland, tripRequest.DurationInDays);
            }

            tripRequest.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil diupdate"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tripRequest = await _context.BusinessTripRequests.FirstOrDefaultAsync(r => r.Id == id);
            
            if (tripRequest == null) return NotFound(ApiResponse<object>.ErrorResponse("Pengajuan tidak ditemukan"));

            if (!IsAdminOrSdm() && tripRequest.UserId != GetCurrentUserId())
            {
                return StatusCode(403, ApiResponse<object>.ErrorResponse("Anda tidak memiliki akses"));
            }

            _context.BusinessTripRequests.Remove(tripRequest);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil dihapus"));
        }

        [HttpPatch("{id}/approval")]
        [Authorize(Roles = "ADMIN,SDM")]
        public async Task<IActionResult> Approval(int id, [FromBody] BusinessTripRequestApprovalRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var tripRequest = await _context.BusinessTripRequests.FirstOrDefaultAsync(r => r.Id == id);
            if (tripRequest == null) return NotFound(ApiResponse<object>.ErrorResponse("Pengajuan tidak ditemukan"));

            tripRequest.Status = request.Status.ToLower();
            tripRequest.ApproverId = GetCurrentUserId();
            tripRequest.ApprovedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Status pengajuan berhasil diupdate"));
        }
    }
}
