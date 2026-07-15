using System;
using System.Linq;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Seeders
{
    public static class RoleSeeding
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                SeedingHelper.ExecuteWithIdentityInsert(context, "roles", () =>
                {
                    context.Roles.AddRange(
                        new Role { Id = 1, Name = "ADMIN" },
                        new Role { Id = 2, Name = "PEGAWAI" },
                        new Role { Id = 3, Name = "SDM" }
                    );
                    context.SaveChanges();
                });
            }
        }
    }
}
