using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Provinces.GetById;

namespace Perdin.WebApi.Features.Provinces.Update;

public class UpdateProvinceCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateProvinceCommand, GetProvinceByIdResponse>
{
    public async Task<GetProvinceByIdResponse> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = await dbContext.Provinces
            .Include(p => p.Country)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (province == null)
        {
            throw new BadHttpRequestException("Data provinsi tidak ditemukan.", 404);
        }

        province.Name = request.Request.Name;
        province.Island = request.Request.Island;
        province.CountryId = request.Request.CountryId;
        province.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        if (province.Country == null || province.Country.Id != province.CountryId)
        {
            await dbContext.Entry(province).Reference(p => p.Country).LoadAsync(cancellationToken);
        }

        return new GetProvinceByIdResponse
        {
            Id = province.Id,
            CountryId = province.CountryId,
            Name = province.Name,
            Island = province.Island,
            CreatedAt = province.CreatedAt,
            UpdatedAt = province.UpdatedAt,
            Country = province.Country != null ? new GetProvinceByIdResponse.CountryInfo
            {
                Id = province.Country.Id,
                Name = province.Country.Name,
                IsForeign = province.Country.IsForeign,
                CreatedAt = province.Country.CreatedAt,
                UpdatedAt = province.Country.UpdatedAt
            } : null!
        };
    }
}
