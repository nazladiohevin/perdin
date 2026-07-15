using System;
using System.Linq;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Seeders
{
    public static class CountrySeeding
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Countries.Any())
            {
                var now = new DateTime(2026, 1, 1);
                SeedingHelper.ExecuteWithIdentityInsert(context, "countries", () =>
                {
                    context.Countries.AddRange(
                        new Country { Id = 1, Name = "Indonesia", IsForeign = false, CreatedAt = now },
                        new Country { Id = 2, Name = "Malaysia", IsForeign = true, CreatedAt = now },
                        new Country { Id = 3, Name = "Singapura", IsForeign = true, CreatedAt = now },
                        new Country { Id = 4, Name = "Thailand", IsForeign = true, CreatedAt = now },
                        new Country { Id = 5, Name = "Vietnam", IsForeign = true, CreatedAt = now },
                        new Country { Id = 6, Name = "Filipina", IsForeign = true, CreatedAt = now },
                        new Country { Id = 7, Name = "Brunei", IsForeign = true, CreatedAt = now },
                        new Country { Id = 8, Name = "Jepang", IsForeign = true, CreatedAt = now },
                        new Country { Id = 9, Name = "Korea Selatan", IsForeign = true, CreatedAt = now },
                        new Country { Id = 10, Name = "China", IsForeign = true, CreatedAt = now },
                        new Country { Id = 11, Name = "Amerika Serikat", IsForeign = true, CreatedAt = now },
                        new Country { Id = 12, Name = "Inggris", IsForeign = true, CreatedAt = now },
                        new Country { Id = 13, Name = "Jerman", IsForeign = true, CreatedAt = now },
                        new Country { Id = 14, Name = "Perancis", IsForeign = true, CreatedAt = now },
                        new Country { Id = 15, Name = "Belanda", IsForeign = true, CreatedAt = now },
                        new Country { Id = 16, Name = "Australia", IsForeign = true, CreatedAt = now },
                        new Country { Id = 17, Name = "Selandia Baru", IsForeign = true, CreatedAt = now },
                        new Country { Id = 18, Name = "Arab Saudi", IsForeign = true, CreatedAt = now },
                        new Country { Id = 19, Name = "Uni Emirat Arab", IsForeign = true, CreatedAt = now },
                        new Country { Id = 20, Name = "India", IsForeign = true, CreatedAt = now }
                    );
                    context.SaveChanges();
                });
            }
        }
    }
}
