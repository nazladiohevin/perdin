using System.Collections.Concurrent;

namespace Perdin.WebApi.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();

        public void BlacklistToken(string token, DateTime expiry)
        {
            _blacklistedTokens.TryAdd(token, expiry);

            CleanupExpiredTokens();
        }

        public bool IsBlacklisted(string token)
        {
            return _blacklistedTokens.ContainsKey(token);
        }

        private void CleanupExpiredTokens()
        {
            var now = DateTime.UtcNow;
            foreach (var entry in _blacklistedTokens)
            {
                if (entry.Value <= now)
                {
                    _blacklistedTokens.TryRemove(entry.Key, out _);
                }
            }
        }
    }
}
