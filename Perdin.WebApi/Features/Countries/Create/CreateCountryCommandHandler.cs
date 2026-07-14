using MediatR;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Countries.GetById;

namespace Perdin.WebApi.Features.Countries.Create;

public class CreateCountryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateCountryCommand, GetCountryByIdResponse>
{
    public async Task<GetCountryByIdResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new Models.Country
        {
            Name = request.Request.Name,
            IsForeign = request.Request.IsForeign ?? false,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Countries.Add(country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new GetCountryByIdResponse
        {
            Id = country.Id,
            Name = country.Name,
            IsForeign = country.IsForeign,
            CreatedAt = country.CreatedAt,
            UpdatedAt = country.UpdatedAt
        };
    }
}
