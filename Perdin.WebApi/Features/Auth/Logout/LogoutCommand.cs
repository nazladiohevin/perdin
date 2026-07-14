using MediatR;

namespace Perdin.WebApi.Features.Auth.Logout;

public class LogoutCommand : IRequest<Unit>
{
    public string Token { get; set; } = string.Empty;
}
