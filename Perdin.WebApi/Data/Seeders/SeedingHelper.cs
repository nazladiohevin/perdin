using System;
using Microsoft.EntityFrameworkCore;

namespace Perdin.WebApi.Data.Seeders
{
    public static class SeedingHelper
    {
        public static void ExecuteWithIdentityInsert(AppDbContext context, string tableName, Action action)
        {
            var connection = context.Database.GetDbConnection();
            var wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT " + tableName + " ON");
                action();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT " + tableName + " OFF");
            }
            finally
            {
                if (!wasOpen) connection.Close();
            }
        }
    }
}
