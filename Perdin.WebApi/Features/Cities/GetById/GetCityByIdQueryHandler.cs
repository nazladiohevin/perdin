using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Cities.GetById;

public class GetCityByIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetCityByIdQuery, GetCityByIdResponse>
{
    public async Task<GetCityByIdResponse> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await dbContext.Cities
            .Include(c => c.Province)
            .ThenInclude(p => p.Country)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (city == null)
        {
            throw new BadHttpRequestException("Data kota tidak ditemukan.", 404);
        }

        return new GetCityByIdResponse
        {
            Id = city.Id,
            ProvinceId = city.ProvinceId,
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            CreatedAt = city.CreatedAt,
            UpdatedAt = city.UpdatedAt,
            Province = city.Province != null ? new GetCityByIdResponse.ProvinceInfo
            {
                Id = city.Province.Id,
                CountryId = city.Province.CountryId,
                Name = city.Province.Name,
                CreatedAt = city.Province.CreatedAt,
                UpdatedAt = city.Province.UpdatedAt,
                Country = city.Province.Country != null ? new GetCityByIdResponse.CountryInfo
                {
                    Id = city.Province.Country.Id,
                    Name = city.Province.Country.Name,
                    IsForeign = city.Province.Country.IsForeign,
                    CreatedAt = city.Province.Country.CreatedAt,
                    UpdatedAt = city.Province.Country.UpdatedAt
                } : null!
            } : null!
        };
    }
}
