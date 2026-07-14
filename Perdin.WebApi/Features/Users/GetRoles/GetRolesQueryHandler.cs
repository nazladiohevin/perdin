using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Users.GetRoles;

public class GetRolesQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetRolesQuery, List<GetRolesResponse>>
{
    public async Task<List<GetRolesResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Roles
            .Select(r => new GetRolesResponse { Id = r.Id, Name = r.Name })
            .ToListAsync(cancellationToken);
    }
}
