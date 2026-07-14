using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Countries.GetAll;

public class GetAllCountriesQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllCountriesQuery, List<GetAllCountriesResponse>>
{
    public async Task<List<GetAllCountriesResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Countries
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new GetAllCountriesResponse
            {
                Id = c.Id,
                Name = c.Name,
                IsForeign = c.IsForeign,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
