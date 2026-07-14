using MediatR;

namespace Perdin.WebApi.Features.Users.Create;

public class CreateUserCommand : IRequest<Unit>
{
    public CreateUserRequest Request { get; set; } = null!;
}
