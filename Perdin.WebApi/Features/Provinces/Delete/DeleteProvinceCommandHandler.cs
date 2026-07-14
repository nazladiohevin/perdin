using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Provinces.Delete;

public class DeleteProvinceCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteProvinceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = await dbContext.Provinces.FindAsync(new object[] { request.Id }, cancellationToken);
        if (province == null)
        {
            throw new BadHttpRequestException("Data provinsi tidak ditemukan.", 404);
        }

        var isUsedInCities = await dbContext.Cities.AnyAsync(c => c.ProvinceId == request.Id, cancellationToken);
        if (isUsedInCities)
        {
            throw new BadHttpRequestException("Gagal menghapus! Provinsi ini sedang digunakan pada data Kota.", 400);
        }

        dbContext.Provinces.Remove(province);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
