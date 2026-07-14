using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.Users.Create;
using Perdin.WebApi.Features.Users.Delete;
using Perdin.WebApi.Features.Users.GetAll;
using Perdin.WebApi.Features.Users.GetById;
using Perdin.WebApi.Features.Users.GetRoles;
using Perdin.WebApi.Features.Users.Update;

namespace Perdin.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,SDM")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            try
            {
                var command = new CreateUserCommand { Request = request };
                await _mediator.Send(command);
                return StatusCode(201, ApiResponse<object?>.SuccessResponse(null, "Akun berhasil dibuat"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetAllUsersResponse>>.SuccessResponse(result, "Akun berhasil diambil"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetUserByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<GetAllUsersResponse>.SuccessResponse(result, "Akun ditemukan"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("/api/roles")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var query = new GetRolesQuery();
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetRolesResponse>>.SuccessResponse(result, "Roles berhasil diambil"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
            }

            try
            {
                var command = new UpdateUserCommand { Id = id, Request = request };
                await _mediator.Send(command);
                return Ok(ApiResponse<object?>.SuccessResponse(null, "Akun berhasil di ubah"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteUserCommand { Id = id };
                await _mediator.Send(command);
                return Ok(ApiResponse<object?>.SuccessResponse(null, "Akun berhasil dihapus"));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
