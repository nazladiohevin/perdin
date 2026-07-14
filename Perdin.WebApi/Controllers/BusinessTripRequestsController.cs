using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perdin.WebApi.DTOs;
using Perdin.WebApi.Features.BusinessTripRequests;
using Perdin.WebApi.Features.BusinessTripRequests.Approval;
using Perdin.WebApi.Features.BusinessTripRequests.Create;
using Perdin.WebApi.Features.BusinessTripRequests.Delete;
using Perdin.WebApi.Features.BusinessTripRequests.GetAll;
using Perdin.WebApi.Features.BusinessTripRequests.GetById;
using Perdin.WebApi.Features.BusinessTripRequests.Update;

namespace Perdin.WebApi.Controllers;

[ApiController]
[Route("api/business-trip-requests")]
[Authorize]
public class BusinessTripRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessTripRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? User.FindFirstValue("sub");
        return int.TryParse(userIdStr, out var userId) ? userId : 0;
    }

    private bool IsAdminOrSdm()
    {
        return User.IsInRole("ADMIN") || User.IsInRole("SDM");
    }

    private ObjectResult HandleBusinessTripRequestException(BusinessTripRequestException ex)
    {
        return StatusCode(ex.StatusCode, ApiResponse<object>.ErrorResponse(ex.Message));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBusinessTripRequestsRequest request)
    {
        try
        {
            var query = new GetAllBusinessTripRequestsQuery
            {
                Status = request.Status,
                SortBy = request.SortBy,
                CurrentUserId = GetCurrentUserId(),
                IsAdminOrSdm = IsAdminOrSdm()
            };

            var result = await _mediator.Send(query);
            return Ok(ApiResponse<List<GetAllBusinessTripRequestsResponse>>.SuccessResponse(result, "Pengajuan perjalanan dinas berhasil diambil"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var query = new GetBusinessTripRequestByIdQuery
            {
                Id = id,
                CurrentUserId = GetCurrentUserId(),
                IsAdminOrSdm = IsAdminOrSdm()
            };

            var result = await _mediator.Send(query);
            return Ok(ApiResponse<GetBusinessTripRequestByIdResponse>.SuccessResponse(result, "Pengajuan perjalanan dinas ditemukan"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessTripRequestRequest request)
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
            var command = new CreateBusinessTripRequestCommand
            {
                Request = request,
                CurrentUserId = GetCurrentUserId(),
                IsAdminOrSdm = IsAdminOrSdm()
            };

            await _mediator.Send(command);
            return StatusCode(201, ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil dibuat"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBusinessTripRequestRequest request)
    {
        try
        {
            var command = new UpdateBusinessTripRequestCommand
            {
                Id = id,
                Request = request,
                CurrentUserId = GetCurrentUserId(),
                IsAdminOrSdm = IsAdminOrSdm()
            };

            await _mediator.Send(command);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil diupdate"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteBusinessTripRequestCommand
            {
                Id = id,
                CurrentUserId = GetCurrentUserId(),
                IsAdminOrSdm = IsAdminOrSdm()
            };

            await _mediator.Send(command);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Pengajuan perjalanan dinas berhasil dihapus"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }

    [HttpPatch("{id}/approval")]
    [Authorize(Roles = "ADMIN,SDM")]
    public async Task<IActionResult> Approval(int id, [FromBody] ApprovalBusinessTripRequestRequest request)
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
            var command = new ApprovalBusinessTripRequestCommand
            {
                Id = id,
                Request = request,
                CurrentUserId = GetCurrentUserId()
            };

            await _mediator.Send(command);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Status pengajuan berhasil diupdate"));
        }
        catch (BusinessTripRequestException ex)
        {
            return HandleBusinessTripRequestException(ex);
        }
    }
}
