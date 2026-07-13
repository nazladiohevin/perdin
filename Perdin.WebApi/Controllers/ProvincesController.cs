using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.Province;
using Perdin.WebApi.DTOs.Country;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/provinces")]
    [Authorize(Roles = "ADMIN")]
    public class ProvincesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProvincesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvinces()
        {
            var provinces = await _context.Provinces
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProvinceResponse
                {
                    Id = p.Id,
                    CountryId = p.CountryId,
                    Name = p.Name,
                    Island = p.Island,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<ProvinceResponse>>.SuccessResponse(provinces, "Berhasil mengambil data provinsi."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvince(int id)
        {
            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (province == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data provinsi tidak ditemukan."));
            }

            var response = new ProvinceDetailResponse
            {
                Id = province.Id,
                CountryId = province.CountryId,
                Name = province.Name,
                Island = province.Island,
                CreatedAt = province.CreatedAt,
                UpdatedAt = province.UpdatedAt,
                Country = province.Country != null ? new CountryResponse
                {
                    Id = province.Country.Id,
                    Name = province.Country.Name,
                    IsForeign = province.Country.IsForeign,
                    CreatedAt = province.Country.CreatedAt,
                    UpdatedAt = province.Country.UpdatedAt
                } : null!
            };

            return Ok(ApiResponse<ProvinceDetailResponse>.SuccessResponse(response, "Berhasil mengambil data."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvince([FromBody] ProvinceCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var province = new Models.Province
            {
                Name = request.Name,
                Island = request.Island,
                CountryId = request.CountryId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            // Load Country relation
            await _context.Entry(province).Reference(p => p.Country).LoadAsync();

            var response = new ProvinceDetailResponse
            {
                Id = province.Id,
                CountryId = province.CountryId,
                Name = province.Name,
                Island = province.Island,
                CreatedAt = province.CreatedAt,
                UpdatedAt = province.UpdatedAt,
                Country = province.Country != null ? new CountryResponse
                {
                    Id = province.Country.Id,
                    Name = province.Country.Name,
                    IsForeign = province.Country.IsForeign,
                    CreatedAt = province.Country.CreatedAt,
                    UpdatedAt = province.Country.UpdatedAt
                } : null!
            };

            return Ok(ApiResponse<ProvinceDetailResponse>.SuccessResponse(response, "Berhasil menambahkan provinsi."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvince(int id, [FromBody] ProvinceUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (province == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data provinsi tidak ditemukan."));
            }

            province.Name = request.Name;
            province.Island = request.Island;
            province.CountryId = request.CountryId;
            province.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload country if it was changed
            if (province.Country == null || province.Country.Id != province.CountryId)
            {
                await _context.Entry(province).Reference(p => p.Country).LoadAsync();
            }

            var response = new ProvinceDetailResponse
            {
                Id = province.Id,
                CountryId = province.CountryId,
                Name = province.Name,
                Island = province.Island,
                CreatedAt = province.CreatedAt,
                UpdatedAt = province.UpdatedAt,
                Country = province.Country != null ? new CountryResponse
                {
                    Id = province.Country.Id,
                    Name = province.Country.Name,
                    IsForeign = province.Country.IsForeign,
                    CreatedAt = province.Country.CreatedAt,
                    UpdatedAt = province.Country.UpdatedAt
                } : null!
            };

            return Ok(ApiResponse<ProvinceDetailResponse>.SuccessResponse(response, "Berhasil mengubah data provinsi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data provinsi tidak ditemukan."));
            }

            var isUsedInCities = await _context.Cities.AnyAsync(c => c.ProvinceId == id);
            if (isUsedInCities)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Gagal menghapus! Provinsi ini sedang digunakan pada data Kota."));
            }

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus provinsi."));
        }
    }
}
