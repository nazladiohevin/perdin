namespace Perdin.WebApi.Services
{
    public interface ILoginAttemptService
    {
        bool IsBlocked(string ipAddress);
        void RecordFailedAttempt(string ipAddress);
        void ResetAttempts(string ipAddress);
    }
}
