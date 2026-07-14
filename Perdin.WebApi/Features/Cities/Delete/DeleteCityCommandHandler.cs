using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Cities.Delete;

public class DeleteCityCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteCityCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await dbContext.Cities.FindAsync(new object[] { request.Id }, cancellationToken);
        if (city == null)
        {
            throw new BadHttpRequestException("Data kota tidak ditemukan.", 404);
        }

        var isUsedInBusinessTrips = await dbContext.BusinessTripRequests
            .AnyAsync(b => b.OriginCityId == request.Id || b.DestinationCityId == request.Id, cancellationToken);
        
        if (isUsedInBusinessTrips)
        {
            throw new BadHttpRequestException("Gagal menghapus! Kota ini sedang digunakan pada data Perjalanan Dinas.", 400);
        }

        dbContext.Cities.Remove(city);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
