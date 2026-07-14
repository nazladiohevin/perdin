using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.Cities.Create;
using Perdin.WebApi.Features.Cities.Delete;
using Perdin.WebApi.Features.Cities.GetAll;
using Perdin.WebApi.Features.Cities.GetById;
using Perdin.WebApi.Features.Cities.Update;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCities([FromQuery] string? include = null)
        {
            try
            {
                var query = new GetAllCitiesQuery { Include = include };
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetAllCitiesResponse>>.SuccessResponse(result, "Berhasil mengambil data kota."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCity(int id)
        {
            try
            {
                var query = new GetCityByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<GetCityByIdResponse>.SuccessResponse(result, "Berhasil mengambil data kota."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityRequest request)
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
                var command = new CreateCityCommand { Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetCityByIdResponse>.SuccessResponse(result, "Berhasil menambahkan kota."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityRequest request)
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
                var command = new UpdateCityCommand { Id = id, Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetCityByIdResponse>.SuccessResponse(result, "Berhasil mengubah data kota."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                var command = new DeleteCityCommand { Id = id };
                await _mediator.Send(command);
                return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus kota."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
