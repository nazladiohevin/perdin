using System;
using System.Linq;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Seeders
{
    public static class ProvinceSeeding
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Provinces.Any())
            {
                var now = new DateTime(2026, 1, 1);
                SeedingHelper.ExecuteWithIdentityInsert(context, "provinces", () =>
                {
                    context.Provinces.AddRange(
                        new Province { Id = 1, CountryId = 1, Name = "DKI Jakarta", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 2, CountryId = 1, Name = "Jawa Barat", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 3, CountryId = 1, Name = "Jawa Tengah", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 4, CountryId = 1, Name = "DI Yogyakarta", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 5, CountryId = 1, Name = "Jawa Timur", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 6, CountryId = 1, Name = "Banten", Island = "Jawa", CreatedAt = now },
                        new Province { Id = 7, CountryId = 1, Name = "Bali", Island = "Bali", CreatedAt = now },
                        new Province { Id = 8, CountryId = 1, Name = "Sumatera Utara", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 9, CountryId = 1, Name = "Riau", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 10, CountryId = 1, Name = "Kepulauan Riau", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 11, CountryId = 1, Name = "Sumatera Barat", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 12, CountryId = 1, Name = "Sumatera Selatan", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 13, CountryId = 1, Name = "Lampung", Island = "Sumatera", CreatedAt = now },
                        new Province { Id = 14, CountryId = 1, Name = "Kalimantan Barat", Island = "Kalimantan", CreatedAt = now },
                        new Province { Id = 15, CountryId = 1, Name = "Kalimantan Timur", Island = "Kalimantan", CreatedAt = now },
                        new Province { Id = 16, CountryId = 1, Name = "Sulawesi Selatan", Island = "Sulawesi", CreatedAt = now },
                        new Province { Id = 17, CountryId = 1, Name = "Sulawesi Utara", Island = "Sulawesi", CreatedAt = now },
                        new Province { Id = 18, CountryId = 1, Name = "Nusa Tenggara Barat", Island = "Lombok", CreatedAt = now },
                        new Province { Id = 19, CountryId = 1, Name = "Nusa Tenggara Timur", Island = "Timor", CreatedAt = now },
                        new Province { Id = 20, CountryId = 1, Name = "Papua", Island = "Papua", CreatedAt = now }
                    );
                    context.SaveChanges();
                });
            }
        }
    }
}
