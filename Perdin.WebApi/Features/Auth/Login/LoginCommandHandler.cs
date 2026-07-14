using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;
using Perdin.WebApi.Services;

namespace Perdin.WebApi.Features.Auth.Login;

public class LoginCommandHandler(
    AppDbContext dbContext, IJwtService jwtService, ILoginAttemptService loginAttemptService, ILogger<LoginCommandHandler> logger
) : IRequestHandler<LoginCommand, LoginResponse>
{
  public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    if (loginAttemptService.IsBlocked(request.IpAddress))
    {
      logger.LogWarning("Login attempt blocked for IP: {IpAddress}", request.IpAddress);
      throw new Exception("429:Terlalu banyak percobaan login. Silakan coba lagi nanti.");
    }

    var user = await dbContext.Users
        .Include(u => u.Roles)
        .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

    if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
    {
      loginAttemptService.RecordFailedAttempt(request.IpAddress);
      logger.LogWarning("Failed login attempt for username: {Username} from IP: {IpAddress}",
          request.Username, request.IpAddress);

      throw new Exception("401:Username atau password salah!");
    }

    loginAttemptService.ResetAttempts(request.IpAddress);

    var roles = user.Roles.Select(r => r.Name).ToList();
    var accessToken = jwtService.GenerateAccessToken(user, roles);

    logger.LogInformation("User {Username} logged in successfully from IP: {IpAddress}",
        user.Username, request.IpAddress);

    return new LoginResponse
    {
      AccessToken = accessToken,
      ExpiresIn = 86400,
      TokenType = "Bearer",
      User = new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Roles = roles
      }
    };
  }

}