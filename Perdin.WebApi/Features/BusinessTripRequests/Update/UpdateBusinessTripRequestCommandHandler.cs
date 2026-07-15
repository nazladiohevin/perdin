using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Helpers;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Features.BusinessTripRequests.Update;

public class UpdateBusinessTripRequestCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateBusinessTripRequestCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBusinessTripRequestCommand request, CancellationToken cancellationToken)
    {
        var tripRequest = await dbContext.BusinessTripRequests
            .Include(r => r.OriginCity)
                .ThenInclude(c => c.Province)
            .Include(r => r.DestinationCity)
                .ThenInclude(c => c!.Province)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken) ?? throw new BusinessTripRequestException(404, "Pengajuan tidak ditemukan");

        if (!request.IsAdminOrSdm && tripRequest.UserId != request.CurrentUserId)
        {
            throw new BusinessTripRequestException(403, "Anda tidak memiliki akses");
        }

        var body = request.Request;
        var needsRecalculateDuration = false;
        var needsRecalculatePocketMoney = false;

        if (body.DepartureDate.HasValue)
        {
            tripRequest.DepartureDate = body.DepartureDate.Value;
            needsRecalculateDuration = true;
        }

        if (body.ReturnDate.HasValue)
        {
            tripRequest.ReturnDate = body.ReturnDate.Value;
            needsRecalculateDuration = true;
        }

        if (needsRecalculateDuration)
        {
            if (tripRequest.ReturnDate < tripRequest.DepartureDate)
            {
                throw new BusinessTripRequestException(400, "Tanggal kepulangan tidak boleh lebih awal dari keberangkatan");
            }

            tripRequest.DurationInDays = tripRequest.ReturnDate.DayNumber - tripRequest.DepartureDate.DayNumber;
            needsRecalculatePocketMoney = true;
        }

        if (body.OriginCityId.HasValue)
        {
            tripRequest.OriginCityId = body.OriginCityId.Value;
            needsRecalculatePocketMoney = true;
        }

        if (body.DestinationCityId.HasValue)
        {
            tripRequest.DestinationCityId = body.DestinationCityId.Value;
            tripRequest.DestinationCountryId = 1;
            needsRecalculatePocketMoney = true;
        }

        if (body.DestinationCountryId.HasValue)
        {
            tripRequest.DestinationCountryId = body.DestinationCountryId.Value;
            if (tripRequest.DestinationCountryId != 1)
            {
                tripRequest.DestinationCityId = null;
            }
            needsRecalculatePocketMoney = true;
        }

        if (body.Purpose != null)
        {
            tripRequest.Purpose = body.Purpose;
        }

        if (body.UserId.HasValue && request.IsAdminOrSdm)
        {
            var userExists = await dbContext.Users.AnyAsync(u => u.Id == body.UserId.Value, cancellationToken);
            if (!userExists)
            {
                throw new BusinessTripRequestException(400, "User tidak ditemukan");
            }

            tripRequest.UserId = body.UserId.Value;
        }

        if (needsRecalculatePocketMoney)
        {
            var originCity = await dbContext.Cities
                .Include(c => c.Province)
                .FirstOrDefaultAsync(c => c.Id == tripRequest.OriginCityId, cancellationToken);
            if (originCity == null)
            {
                throw new BusinessTripRequestException(400, "Kota asal tidak valid");
            }

            var destinationCountry = await dbContext.Countries
                .FirstOrDefaultAsync(c => c.Id == tripRequest.DestinationCountryId, cancellationToken);
            if (destinationCountry == null)
            {
                throw new BusinessTripRequestException(400, "Negara tujuan tidak valid");
            }

            City? destinationCity = null;
            if (tripRequest.DestinationCityId.HasValue)
            {
                destinationCity = await dbContext.Cities
                    .Include(c => c.Province)
                    .FirstOrDefaultAsync(c => c.Id == tripRequest.DestinationCityId.Value, cancellationToken);
            }

            var distance = 0d;
            var isForeign = destinationCountry.IsForeign;
            var isSameProvince = false;
            var isSameIsland = false;

            if (!isForeign && destinationCity != null)
            {
                distance = DistanceHelper.CalculateDistance(
                    originCity.Latitude,
                    originCity.Longitude,
                    destinationCity.Latitude,
                    destinationCity.Longitude);

                isSameProvince = originCity.ProvinceId == destinationCity.ProvinceId;
                isSameIsland = originCity.Province.Island == destinationCity.Province.Island;
            }

            tripRequest.PocketMoney = PocketMoneyHelper.CalculatePocketMoney(
                distance,
                isForeign,
                isSameProvince,
                isSameIsland,
                tripRequest.DurationInDays);
        }

        tripRequest.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
