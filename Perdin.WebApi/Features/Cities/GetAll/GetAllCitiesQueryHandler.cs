using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Cities.GetAll;

public class GetAllCitiesQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllCitiesQuery, List<GetAllCitiesResponse>>
{
    public async Task<List<GetAllCitiesResponse>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        var includeProvince = !string.IsNullOrEmpty(request.Include) && 
            (request.Include.Equals("province", StringComparison.OrdinalIgnoreCase) || 
             request.Include.Equals("provinces", StringComparison.OrdinalIgnoreCase));

        var query = dbContext.Cities.OrderByDescending(c => c.CreatedAt).AsQueryable();

        if (includeProvince)
        {
            return await query
                .Select(c => new GetAllCitiesResponse
                {
                    Id = c.Id,
                    ProvinceId = c.ProvinceId,
                    Name = c.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Province = new GetAllCitiesResponse.ProvinceInfo
                    {
                        Id = c.Province.Id,
                        CountryId = c.Province.CountryId,
                        Name = c.Province.Name,
                        CreatedAt = c.Province.CreatedAt,
                        UpdatedAt = c.Province.UpdatedAt
                    }
                })
                .ToListAsync(cancellationToken);
        }
        else
        {
            return await query
                .Select(c => new GetAllCitiesResponse
                {
                    Id = c.Id,
                    ProvinceId = c.ProvinceId,
                    Name = c.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Province = null
                })
                .ToListAsync(cancellationToken);
        }
    }
}
