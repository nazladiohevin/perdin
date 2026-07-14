using MediatR;
using Microsoft.AspNetCore.Http;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Countries.GetById;

namespace Perdin.WebApi.Features.Countries.Update;

public class UpdateCountryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateCountryCommand, GetCountryByIdResponse>
{
    public async Task<GetCountryByIdResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await dbContext.Countries.FindAsync(new object[] { request.Id }, cancellationToken);
        if (country == null)
        {
            throw new BadHttpRequestException("Data negara tidak ditemukan.", 404);
        }

        country.Name = request.Request.Name;
        country.IsForeign = request.Request.IsForeign ?? false;
        country.UpdatedAt = DateTime.UtcNow;

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
