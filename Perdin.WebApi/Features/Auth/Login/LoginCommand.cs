using MediatR;

namespace Perdin.WebApi.Features.Auth.Login;

public class LoginCommand : IRequest<LoginResponse>
{
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string IpAddress { get; set; } = string.Empty;
}