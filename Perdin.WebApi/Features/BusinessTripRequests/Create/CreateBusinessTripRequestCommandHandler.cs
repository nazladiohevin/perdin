using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Helpers;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Features.BusinessTripRequests.Create;

public class CreateBusinessTripRequestCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateBusinessTripRequestCommand, Unit>
{
    public async Task<Unit> Handle(CreateBusinessTripRequestCommand request, CancellationToken cancellationToken)
    {
        var body = request.Request;

        if (!body.DepartureDate.HasValue || !body.ReturnDate.HasValue)
        {
            throw new BusinessTripRequestException(400, "Tanggal keberangkatan dan kepulangan wajib diisi");
        }

        if (body.ReturnDate.Value < body.DepartureDate.Value)
        {
            throw new BusinessTripRequestException(400, "Tanggal kepulangan tidak boleh lebih awal dari keberangkatan");
        }

        var originCity = await dbContext.Cities
            .Include(c => c.Province)
            .FirstOrDefaultAsync(c => c.Id == body.OriginCityId, cancellationToken);

        if (originCity == null)
        {
            throw new BusinessTripRequestException(400, "Kota asal tidak valid");
        }

        Country? destinationCountry;
        if (body.DestinationCountryId.HasValue)
        {
            destinationCountry = await dbContext.Countries
                .FirstOrDefaultAsync(c => c.Id == body.DestinationCountryId.Value, cancellationToken);

            if (destinationCountry == null)
            {
                throw new BusinessTripRequestException(400, "Negara tujuan tidak valid");
            }
        }
        else
        {
            destinationCountry = await dbContext.Countries
                .FirstOrDefaultAsync(c => c.Id == 1, cancellationToken);

            if (destinationCountry == null)
            {
                throw new BusinessTripRequestException(400, "Negara tujuan tidak valid");
            }
        }

        City? destinationCity = null;
        if (!destinationCountry.IsForeign && body.DestinationCityId.HasValue)
        {
            destinationCity = await dbContext.Cities
                .Include(c => c.Province)
                .FirstOrDefaultAsync(c => c.Id == body.DestinationCityId.Value, cancellationToken);

            if (destinationCity == null)
            {
                throw new BusinessTripRequestException(400, "Kota tujuan tidak valid");
            }
        }

        int userId;
        if (request.IsAdminOrSdm)
        {
            if (!body.UserId.HasValue)
            {
                throw new BusinessTripRequestException(400, "UserId wajib diisi oleh Admin/SDM");
            }

            userId = body.UserId.Value;
            var userExists = await dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);
            if (!userExists)
            {
                throw new BusinessTripRequestException(400, "User tidak ditemukan");
            }
        }
        else
        {
            userId = request.CurrentUserId;
        }

        var durationInDays = body.ReturnDate.Value.DayNumber - body.DepartureDate.Value.DayNumber;

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

        var pocketMoney = PocketMoneyHelper.CalculatePocketMoney(
            distance,
            isForeign,
            isSameProvince,
            isSameIsland,
            durationInDays);

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var requestNumber = await RequestNumberHelper.GenerateRequestNumberAsync(dbContext);
        var tripRequest = new BusinessTripRequest
        {
            RequestNumber = requestNumber,
            UserId = userId,
            DepartureDate = body.DepartureDate.Value,
            ReturnDate = body.ReturnDate.Value,
            OriginCityId = body.OriginCityId,
            DestinationCityId = body.DestinationCityId,
            DestinationCountryId = destinationCountry.Id,
            DurationInDays = durationInDays,
            Purpose = body.Purpose,
            PocketMoney = pocketMoney,
            Status = "reviewed"
        };

        dbContext.BusinessTripRequests.Add(tripRequest);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
