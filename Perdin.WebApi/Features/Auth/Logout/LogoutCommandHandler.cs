using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Perdin.WebApi.Services;

namespace Perdin.WebApi.Features.Auth.Logout;

public class LogoutCommandHandler(
    ITokenBlacklistService tokenBlacklistService, ILogger<LogoutCommandHandler> logger
) : IRequestHandler<LogoutCommand, Unit>
{
    private readonly ITokenBlacklistService _tokenBlacklistService = tokenBlacklistService;
    private readonly ILogger<LogoutCommandHandler> _logger = logger;

    public Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(request.Token);
        var expiry = jwtToken.ValidTo;

        _tokenBlacklistService.BlacklistToken(request.Token, expiry);

        _logger.LogInformation("User logged out successfully");

        return Task.FromResult(Unit.Value);
    }
}