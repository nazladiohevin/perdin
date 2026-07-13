using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.User;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,SDM")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Username sudah dipakai"));
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Email sudah dipakai"));
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = new User
                {
                    Name = request.Name,
                    Username = request.Username,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    var roles = await _context.Roles.Where(r => request.RoleIds.Contains(r.Id)).ToListAsync();
                    
                    if (roles.Count != request.RoleIds.Count)
                    {
                        return BadRequest(ApiResponse<object>.ErrorResponse("Satu atau lebih role id tidak ada"));
                    }

                    user.Roles = roles;
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return StatusCode(201, ApiResponse<object?>.SuccessResponse(null, "Akun berhasil dibuat"));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Terjadi error ketika membuat akun baru"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Global Query Filter (DeletedAt == null) applies automatically
            var users = await _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Roles = u.Roles.Select(r => new RoleItem { Id = r.Id, Name = r.Name }).ToList()
                })
                .ToListAsync();

            return Ok(ApiResponse<List<UserResponse>>.SuccessResponse(users, "Akun berhasil diambil"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == id)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Roles = u.Roles.Select(r => new RoleItem { Id = r.Id, Name = r.Name }).ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Akun tidak ditemukan"));
            }

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user, "Akun ditemukan"));
        }

        [HttpGet("/api/roles")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles
                .Select(r => new RoleItem { Id = r.Id, Name = r.Name })
                .ToListAsync();

            return Ok(ApiResponse<List<RoleItem>>.SuccessResponse(roles, "Roles berhasil diambil"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("User tidak ditemukan"));
            }

            if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.Id != id))
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Username sudah dipakai"));
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.Id != id))
            {
                return BadRequest(ApiResponse<object>.ErrorResponse("Email sudah dipakai"));
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.Name = request.Name;
                user.Username = request.Username;
                user.Email = request.Email;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }
                user.UpdatedAt = DateTime.UtcNow;

                user.Roles.Clear();
                
                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    var roles = await _context.Roles.Where(r => request.RoleIds.Contains(r.Id)).ToListAsync();
                    
                    if (roles.Count != request.RoleIds.Count)
                    {
                        return BadRequest(ApiResponse<object>.ErrorResponse("Satu atau lebih role id tidak ada"));
                    }

                    user.Roles.AddRange(roles);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<object?>.SuccessResponse(null, "Akun berhasil di ubah"));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Terjadi error ketika mengubah akun"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Akun tidak ditemukan"));
            }

            user.DeletedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Akun berhasil dihapus"));
        }
    }
}
