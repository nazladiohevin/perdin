using Perdin.WebApi.Models;

namespace Perdin.WebApi.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, List<string> roles);
    }
}
