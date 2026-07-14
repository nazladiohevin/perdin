using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.Countries.Create;
using Perdin.WebApi.Features.Countries.Delete;
using Perdin.WebApi.Features.Countries.GetAll;
using Perdin.WebApi.Features.Countries.GetById;
using Perdin.WebApi.Features.Countries.Update;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/countries")]
    [Authorize]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var query = new GetAllCountriesQuery();
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetAllCountriesResponse>>.SuccessResponse(result, "Berhasil mengambil data negara."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var query = new GetCountryByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<GetCountryByIdResponse>.SuccessResponse(result, "Berhasil mengambil data."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest request)
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
                var command = new CreateCountryCommand { Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetCountryByIdResponse>.SuccessResponse(result, "Berhasil menambahkan negara."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryRequest request)
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
                var command = new UpdateCountryCommand { Id = id, Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetCountryByIdResponse>.SuccessResponse(result, "Berhasil mengubah data negara."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var command = new DeleteCountryCommand { Id = id };
                await _mediator.Send(command);
                return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus negara."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
