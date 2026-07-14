using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Cities.GetById;

namespace Perdin.WebApi.Features.Cities.Create;

public class CreateCityCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateCityCommand, GetCityByIdResponse>
{
    public async Task<GetCityByIdResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = new Models.City
        {
            Name = request.Request.Name,
            ProvinceId = request.Request.ProvinceId!.Value,
            Latitude = request.Request.Latitude!.Value,
            Longitude = request.Request.Longitude!.Value,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Cities.Add(city);
        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Entry(city)
            .Reference(c => c.Province)
            .Query()
            .Include(p => p.Country)
            .LoadAsync(cancellationToken);

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
