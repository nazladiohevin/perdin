using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Features.Users.GetAll;

namespace Perdin.WebApi.Features.Users.GetById;

public class GetUserByIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetUserByIdQuery, GetAllUsersResponse>
{
    public async Task<GetAllUsersResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new BadHttpRequestException("Akun tidak ditemukan", 404);
        }

        return user;
    }
}
