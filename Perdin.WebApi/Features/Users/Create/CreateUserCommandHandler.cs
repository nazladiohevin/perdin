using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Features.Users.Create;

public class CreateUserCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateUserCommand, Unit>
{
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Users.AnyAsync(u => u.Username == request.Request.Username, cancellationToken))
        {
            throw new BadHttpRequestException("Username sudah dipakai", 400);
        }

        if (await dbContext.Users.AnyAsync(u => u.Email == request.Request.Email, cancellationToken))
        {
            throw new BadHttpRequestException("Email sudah dipakai", 400);
        }

        using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = new User
            {
                Name = request.Request.Name,
                Username = request.Request.Username,
                Email = request.Request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Request.Password),
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (request.Request.RoleIds != null && request.Request.RoleIds.Any())
            {
                var roles = await dbContext.Roles
                    .Where(r => request.Request.RoleIds.Contains(r.Id))
                    .ToListAsync(cancellationToken);
                
                if (roles.Count != request.Request.RoleIds.Count)
                {
                    throw new BadHttpRequestException("Satu atau lebih role id tidak ada", 400);
                }

                user.Roles = roles;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);

            return Unit.Value;
        }
        catch (BadHttpRequestException)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new BadHttpRequestException("Terjadi error ketika membuat akun baru", 500);
        }
    }
}
