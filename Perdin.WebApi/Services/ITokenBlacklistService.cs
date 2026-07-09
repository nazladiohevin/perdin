namespace Perdin.WebApi.Services
{
    public interface ITokenBlacklistService
    {
        void BlacklistToken(string token, DateTime expiry);
        bool IsBlacklisted(string token);
    }
}
