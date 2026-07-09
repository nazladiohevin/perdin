using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.Country;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/countries")]
    [Authorize(Roles = "ADMIN")]
    public class CountriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Countries
                .Select(c => new CountryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsForeign = c.IsForeign,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<CountryResponse>>.SuccessResponse(countries, "Berhasil mengambil data negara."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data negara tidak ditemukan."));
            }

            var response = new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
                IsForeign = country.IsForeign,
                CreatedAt = country.CreatedAt,
                UpdatedAt = country.UpdatedAt
            };

            return Ok(ApiResponse<CountryResponse>.SuccessResponse(response, "Berhasil mengambil data."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var country = new Models.Country
            {
                Name = request.Name,
                IsForeign = request.IsForeign ?? false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            var response = new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
                IsForeign = country.IsForeign,
                CreatedAt = country.CreatedAt,
                UpdatedAt = country.UpdatedAt
            };

            return Ok(ApiResponse<CountryResponse>.SuccessResponse(response, "Berhasil menambahkan negara."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data negara tidak ditemukan."));
            }

            country.Name = request.Name;
            country.IsForeign = request.IsForeign ?? false;
            country.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var response = new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
                IsForeign = country.IsForeign,
                CreatedAt = country.CreatedAt,
                UpdatedAt = country.UpdatedAt
            };

            return Ok(ApiResponse<CountryResponse>.SuccessResponse(response, "Berhasil mengubah data negara."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Data negara tidak ditemukan."));
            }

            var isUsedInProvinces = await _context.Provinces.AnyAsync(p => p.CountryId == id);
            if (isUsedInProvinces)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Gagal menghapus! Negara ini sedang digunakan pada data Provinsi."));
            }

            var isUsedInTrips = await _context.BusinessTripRequests.AnyAsync(t => t.DestinationCountryId == id);
            if (isUsedInTrips)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Gagal menghapus! Negara ini sedang digunakan pada data Perjalanan Dinas."));
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus negara."));
        }
    }
}
