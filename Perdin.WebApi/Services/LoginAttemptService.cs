using System.Collections.Concurrent;

namespace Perdin.WebApi.Services
{
    public class LoginAttemptService : ILoginAttemptService
    {
        private readonly ConcurrentDictionary<string, LoginAttemptInfo> _attempts = new();
        private const int MaxAttempts = 3;
        private static readonly TimeSpan WindowDuration = TimeSpan.FromHours(1);

        public bool IsBlocked(string ipAddress)
        {
            if (!_attempts.TryGetValue(ipAddress, out var info))
                return false;

            if (DateTime.UtcNow - info.FirstAttemptAt >= WindowDuration)
            {
                _attempts.TryRemove(ipAddress, out _);
                return false;
            }

            return info.FailedCount >= MaxAttempts;
        }

        public void RecordFailedAttempt(string ipAddress)
        {
            _attempts.AddOrUpdate(
                ipAddress,
                _ => new LoginAttemptInfo
                {
                    FailedCount = 1,
                    FirstAttemptAt = DateTime.UtcNow
                },
                (_, existing) =>
                {
                    if (DateTime.UtcNow - existing.FirstAttemptAt >= WindowDuration)
                    {
                        return new LoginAttemptInfo
                        {
                            FailedCount = 1,
                            FirstAttemptAt = DateTime.UtcNow
                        };
                    }

                    existing.FailedCount++;
                    return existing;
                }
            );
        }

        public void ResetAttempts(string ipAddress)
        {
            _attempts.TryRemove(ipAddress, out _);
        }

        private class LoginAttemptInfo
        {
            public int FailedCount { get; set; }
            public DateTime FirstAttemptAt { get; set; }
        }
    }
}
