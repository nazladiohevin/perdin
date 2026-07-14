using MediatR;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Provinces.GetById;

namespace Perdin.WebApi.Features.Provinces.Create;

public class CreateProvinceCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateProvinceCommand, GetProvinceByIdResponse>
{
    public async Task<GetProvinceByIdResponse> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = new Models.Province
        {
            Name = request.Request.Name,
            Island = request.Request.Island,
            CountryId = request.Request.CountryId,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Provinces.Add(province);
        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Entry(province).Reference(p => p.Country).LoadAsync(cancellationToken);

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
