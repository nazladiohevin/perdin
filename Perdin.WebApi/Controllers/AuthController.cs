using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.DTOs.Auth;
using Perdin.WebApi.Services;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly ILoginAttemptService _loginAttemptService;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public AuthController(
            ILogger<AuthController> logger,
            AppDbContext context,
            IJwtService jwtService,
            ILoginAttemptService loginAttemptService,
            ITokenBlacklistService tokenBlacklistService)
        {
            _logger = logger;
            _context = context;
            _jwtService = jwtService;
            _loginAttemptService = loginAttemptService;
            _tokenBlacklistService = tokenBlacklistService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            // Rate limiting
            if (_loginAttemptService.IsBlocked(ipAddress))
            {
                _logger.LogWarning("Login attempt blocked for IP: {IpAddress}", ipAddress);
                return StatusCode(429, ApiResponse<object>.ErrorResponse(
                    "Terlalu banyak percobaan login. Silakan coba lagi nanti."));
            }

            // Validate input
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<object>.ErrorResponse(
                    string.Join("; ", errors)));
            }

            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Username == request.Username);
                
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                _loginAttemptService.RecordFailedAttempt(ipAddress);
                _logger.LogWarning("Failed login attempt for username: {Username} from IP: {IpAddress}",
                    request.Username, ipAddress);

                return Unauthorized(ApiResponse<object>.ErrorResponse(
                    "Username atau password salah!"));
            }

            _loginAttemptService.ResetAttempts(ipAddress);

            var roles = user.Roles.Select(r => r.Name).ToList();
            var accessToken = _jwtService.GenerateAccessToken(user, roles);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                ExpiresIn = 86400, // 1 day in seconds
                TokenType = "Bearer",
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Roles = roles
                }
            };

            _logger.LogInformation("User {Username} logged in successfully from IP: {IpAddress}",
                user.Username, ipAddress);

            return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "Login successful"));
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            _tokenBlacklistService.BlacklistToken(token, expiry);

            _logger.LogInformation("User logged out successfully");

            return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil keluar!"));
        }
    }
}
