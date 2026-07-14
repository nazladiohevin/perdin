using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.Auth.Login;
using Perdin.WebApi.Features.Auth.Logout;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<object>.ErrorResponse(
                    string.Join("; ", errors)));
            }

            try
            {
                var command = new LoginCommand
                {
                    Username = request.Username,
                    Password = request.Password,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
                };

                var result = await _mediator.Send(command);
                return Ok(ApiResponse<LoginResponse>.SuccessResponse(result, "Login successful"));
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("429:"))
                    return StatusCode(429, ApiResponse<object>.ErrorResponse(ex.Message.Replace("429:", "")));

                if (ex.Message.StartsWith("401:"))
                    return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message.Replace("401:", "")));

                return StatusCode(500, ApiResponse<object>.ErrorResponse("Terjadi kesalahan pada server."));
            }

        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");

            var command = new LogoutCommand
            {
                Token = token
            };
            _mediator.Send(command);

            return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil keluar!"));
        }
    }
}
