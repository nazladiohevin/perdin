using MediatR;
using Microsoft.AspNetCore.Http;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Countries.GetById;

public class GetCountryByIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetCountryByIdQuery, GetCountryByIdResponse>
{
    public async Task<GetCountryByIdResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await dbContext.Countries.FindAsync(new object[] { request.Id }, cancellationToken);

        if (country == null)
        {
            throw new BadHttpRequestException("Data negara tidak ditemukan.", 404);
        }

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
