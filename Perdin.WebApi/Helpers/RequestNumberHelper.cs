using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Helpers
{
    public static class RequestNumberHelper
    {
        public static async Task<string> GenerateRequestNumberAsync(AppDbContext context)
        {
            var today = DateTime.UtcNow;
            var dateStr = today.ToString("yyyyMMdd");
            var prefix = $"PERDIN/{dateStr}/";

            // Get the last request number for today
            var lastRequest = await context.BusinessTripRequests
                .IgnoreQueryFilters()
                .Where(r => r.RequestNumber.StartsWith(prefix))
                .OrderByDescending(r => r.RequestNumber)
                .FirstOrDefaultAsync();

            int nextSequence = 1;

            if (lastRequest != null)
            {
                var lastSequenceStr = lastRequest.RequestNumber.Substring(prefix.Length);
                if (int.TryParse(lastSequenceStr, out int lastSequence))
                {
                    nextSequence = lastSequence + 1;
                }
            }

            return $"{prefix}{nextSequence:D4}";
        }
    }
}
