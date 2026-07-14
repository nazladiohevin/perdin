using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.Users.Delete;

public class DeleteUserCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new BadHttpRequestException("Akun tidak ditemukan", 404);
        }

        user.DeletedAt = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
