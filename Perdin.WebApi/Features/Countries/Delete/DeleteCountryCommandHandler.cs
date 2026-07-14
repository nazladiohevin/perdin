using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Countries.Delete;

public class DeleteCountryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteCountryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await dbContext.Countries.FindAsync(new object[] { request.Id }, cancellationToken);
        if (country == null)
        {
            throw new BadHttpRequestException("Data negara tidak ditemukan.", 404);
        }

        var isUsedInProvinces = await dbContext.Provinces.AnyAsync(p => p.CountryId == request.Id, cancellationToken);
        if (isUsedInProvinces)
        {
            throw new BadHttpRequestException("Gagal menghapus! Negara ini sedang digunakan pada data Provinsi.", 400);
        }

        var isUsedInTrips = await dbContext.BusinessTripRequests.AnyAsync(t => t.DestinationCountryId == request.Id, cancellationToken);
        if (isUsedInTrips)
        {
            throw new BadHttpRequestException("Gagal menghapus! Negara ini sedang digunakan pada data Perjalanan Dinas.", 400);
        }

        dbContext.Countries.Remove(country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
