using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Provinces.GetAll;

public class GetAllProvincesQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllProvincesQuery, List<GetAllProvincesResponse>>
{
    public async Task<List<GetAllProvincesResponse>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Provinces
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new GetAllProvincesResponse
            {
                Id = p.Id,
                CountryId = p.CountryId,
                Name = p.Name,
                Island = p.Island,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
