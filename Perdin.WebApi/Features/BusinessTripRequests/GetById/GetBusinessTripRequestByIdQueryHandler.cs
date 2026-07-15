using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Helpers;

namespace Perdin.WebApi.Features.BusinessTripRequests.GetById;

public class GetBusinessTripRequestByIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetBusinessTripRequestByIdQuery, GetBusinessTripRequestByIdResponse>
{
    public async Task<GetBusinessTripRequestByIdResponse> Handle(GetBusinessTripRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var tripRequest = await dbContext.BusinessTripRequests
            .Include(r => r.User)
            .Include(r => r.Approver)
            .Include(r => r.OriginCity)
                .ThenInclude(c => c.Province)
            .Include(r => r.DestinationCity)
                .ThenInclude(c => c!.Province)
            .Include(r => r.DestinationCountry)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new BusinessTripRequestException(404, "Pengajuan perjalanan dinas tidak ditemukan");

        if (!request.IsAdminOrSdm && tripRequest.UserId != request.CurrentUserId)
        {
            throw new BusinessTripRequestException(403, "Anda tidak memiliki akses ke pengajuan ini");
        }

        double? distance = null;
        if (tripRequest.DestinationCity != null)
        {
            distance = DistanceHelper.CalculateDistance(
                tripRequest.OriginCity.Latitude,
                tripRequest.OriginCity.Longitude,
                tripRequest.DestinationCity.Latitude,
                tripRequest.DestinationCity.Longitude);
        }

        return new GetBusinessTripRequestByIdResponse
        {
            Id = tripRequest.Id,
            RequestNumber = tripRequest.RequestNumber,
            User = new GetBusinessTripRequestByIdResponse.UserInfo
            {
                Id = tripRequest.User.Id,
                Name = tripRequest.User.Name
            },
            DepartureDate = tripRequest.DepartureDate,
            ReturnDate = tripRequest.ReturnDate,
            OriginCityId = tripRequest.OriginCityId,
            OriginCityName = tripRequest.OriginCity.Name,
            DestinationCityId = tripRequest.DestinationCityId,
            DestinationCityName = tripRequest.DestinationCity?.Name,
            DestinationCountryId = tripRequest.DestinationCountryId,
            DestinationCountryName = tripRequest.DestinationCountry.Name,
            Status = tripRequest.Status,
            ApproverId = tripRequest.ApproverId,
            ApproverName = tripRequest.Approver?.Name,
            Purpose = tripRequest.Purpose,
            PocketMoney = tripRequest.PocketMoney,
            Distance = distance,
            TotalDays = tripRequest.DurationInDays,
            ApprovedAt = tripRequest.ApprovedAt,
            CreatedAt = tripRequest.CreatedAt,
            UpdatedAt = tripRequest.UpdatedAt
        };
    }
}
