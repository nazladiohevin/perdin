using MediatR;

namespace Perdin.WebApi.Features.Users.Update;

public class UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public UpdateUserRequest Request { get; set; } = null!;
}
