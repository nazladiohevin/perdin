using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Users.GetAll;

public class GetAllUsersQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllUsersQuery, List<GetAllUsersResponse>>
{
    public async Task<List<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(u => u.Roles)
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new GetAllUsersResponse
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Roles = u.Roles.Select(r => new GetAllUsersResponse.RoleItem { Id = r.Id, Name = r.Name }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
