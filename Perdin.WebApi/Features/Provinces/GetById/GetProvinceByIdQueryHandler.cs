using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Provinces.GetById;

public class GetProvinceByIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetProvinceByIdQuery, GetProvinceByIdResponse>
{
    public async Task<GetProvinceByIdResponse> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var province = await dbContext.Provinces
            .Include(p => p.Country)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new BadHttpRequestException("Data provinsi tidak ditemukan.", 404);

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
