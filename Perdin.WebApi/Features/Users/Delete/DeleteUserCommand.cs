using MediatR;

namespace Perdin.WebApi.Features.Users.Delete;

public class DeleteUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
