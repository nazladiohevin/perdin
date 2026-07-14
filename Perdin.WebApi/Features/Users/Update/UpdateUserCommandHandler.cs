using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Users.Update;

public class UpdateUserCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new BadHttpRequestException("User tidak ditemukan", 404);
        }

        if (await dbContext.Users.AnyAsync(u => u.Username == request.Request.Username && u.Id != request.Id, cancellationToken))
        {
            throw new BadHttpRequestException("Username sudah dipakai", 400);
        }

        if (await dbContext.Users.AnyAsync(u => u.Email == request.Request.Email && u.Id != request.Id, cancellationToken))
        {
            throw new BadHttpRequestException("Email sudah dipakai", 400);
        }

        using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            user.Name = request.Request.Name;
            user.Username = request.Request.Username;
            user.Email = request.Request.Email;
            
            if (!string.IsNullOrEmpty(request.Request.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Request.Password);
            }
            user.UpdatedAt = DateTime.UtcNow;

            user.Roles.Clear();
            
            if (request.Request.RoleIds != null && request.Request.RoleIds.Any())
            {
                var roles = await dbContext.Roles
                    .Where(r => request.Request.RoleIds.Contains(r.Id))
                    .ToListAsync(cancellationToken);
                
                if (roles.Count != request.Request.RoleIds.Count)
                {
                    throw new BadHttpRequestException("Satu atau lebih role id tidak ada", 400);
                }

                user.Roles.AddRange(roles);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
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
            throw new BadHttpRequestException("Terjadi error ketika mengubah akun", 500);
        }
    }
}
