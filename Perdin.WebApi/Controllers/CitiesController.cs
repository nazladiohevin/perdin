using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.City;
using Perdin.WebApi.DTOs.Province;
using Perdin.WebApi.DTOs.Country;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [Authorize(Roles = "ADMIN")]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _context.Cities
                .Select(c => new CityResponse
                {
                    Id = c.Id,
                    ProvinceId = c.ProvinceId,
                    Name = c.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<CityResponse>>.SuccessResponse(cities, "Berhasil mengambil data kota."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id)
        {
            var city = await _context.Cities
                .Include(c => c.Province)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data kota tidak ditemukan."));
            }

            var response = MapToCityDetailResponse(city);
            return Ok(ApiResponse<CityDetailResponse>.SuccessResponse(response, "Berhasil mengambil data kota."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var city = new Models.City
            {
                Name = request.Name,
                ProvinceId = request.ProvinceId!.Value,
                Latitude = request.Latitude!.Value,
                Longitude = request.Longitude!.Value,
                CreatedAt = DateTime.UtcNow
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            // Load Province and Country relation for the response
            await _context.Entry(city)
                .Reference(c => c.Province)
                .Query()
                .Include(p => p.Country)
                .LoadAsync();

            var response = MapToCityDetailResponse(city);
            return Ok(ApiResponse<CityDetailResponse>.SuccessResponse(response, "Berhasil menambahkan kota."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var city = await _context.Cities
                .Include(c => c.Province)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data kota tidak ditemukan."));
            }

            city.Name = request.Name;
            city.ProvinceId = request.ProvinceId!.Value;
            city.Latitude = request.Latitude!.Value;
            city.Longitude = request.Longitude!.Value;
            city.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload Province if it was changed
            if (city.Province == null || city.Province.Id != city.ProvinceId)
            {
                await _context.Entry(city)
                    .Reference(c => c.Province)
                    .Query()
                    .Include(p => p.Country)
                    .LoadAsync();
            }

            var response = MapToCityDetailResponse(city);
            return Ok(ApiResponse<CityDetailResponse>.SuccessResponse(response, "Berhasil mengubah data kota."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data kota tidak ditemukan."));
            }

            var isUsedInBusinessTrips = await _context.BusinessTripRequests
                .AnyAsync(b => b.OriginCityId == id || b.DestinationCityId == id);
            
            if (isUsedInBusinessTrips)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Gagal menghapus! Kota ini sedang digunakan pada data Perjalanan Dinas."));
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus kota."));
        }

        private CityDetailResponse MapToCityDetailResponse(Models.City city)
        {
            return new CityDetailResponse
            {
                Id = city.Id,
                ProvinceId = city.ProvinceId,
                Name = city.Name,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
                CreatedAt = city.CreatedAt,
                UpdatedAt = city.UpdatedAt,
                Province = city.Province != null ? new ProvinceDetailResponse
                {
                    Id = city.Province.Id,
                    CountryId = city.Province.CountryId,
                    Name = city.Province.Name,
                    CreatedAt = city.Province.CreatedAt,
                    UpdatedAt = city.Province.UpdatedAt,
                    Country = city.Province.Country != null ? new CountryResponse
                    {
                        Id = city.Province.Country.Id,
                        Name = city.Province.Country.Name,
                        IsForeign = city.Province.Country.IsForeign,
                        CreatedAt = city.Province.Country.CreatedAt,
                        UpdatedAt = city.Province.Country.UpdatedAt
                    } : null!
                } : null!
            };
        }
    }
}
