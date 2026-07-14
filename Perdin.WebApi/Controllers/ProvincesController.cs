using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.Provinces.Create;
using Perdin.WebApi.Features.Provinces.Delete;
using Perdin.WebApi.Features.Provinces.GetAll;
using Perdin.WebApi.Features.Provinces.GetById;
using Perdin.WebApi.Features.Provinces.Update;

namespace Perdin.WebApi.Controllers
{
    [ApiController]
    [Route("api/provinces")]
    [Authorize(Roles = "ADMIN")]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvinces()
        {
            try
            {
                var query = new GetAllProvincesQuery();
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetAllProvincesResponse>>.SuccessResponse(result, "Berhasil mengambil data provinsi."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvince(int id)
        {
            try
            {
                var query = new GetProvinceByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<GetProvinceByIdResponse>.SuccessResponse(result, "Berhasil mengambil data."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvince([FromBody] CreateProvinceRequest request)
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
                var command = new CreateProvinceCommand { Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetProvinceByIdResponse>.SuccessResponse(result, "Berhasil menambahkan provinsi."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvince(int id, [FromBody] UpdateProvinceRequest request)
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
                var command = new UpdateProvinceCommand { Id = id, Request = request };
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<GetProvinceByIdResponse>.SuccessResponse(result, "Berhasil mengubah data provinsi."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            try
            {
                var command = new DeleteProvinceCommand { Id = id };
                await _mediator.Send(command);
                return Ok(ApiResponse<object>.SuccessResponse(null!, "Berhasil menghapus provinsi."));
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
