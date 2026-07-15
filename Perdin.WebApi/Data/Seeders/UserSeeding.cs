using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Seeders
{
    public static class UserSeeding
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var hashedPassword = "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC";
                SeedingHelper.ExecuteWithIdentityInsert(context, "users", () =>
                {
                    context.Users.AddRange(
                        new User { Id = 1, Name = "Rudi Hartono", Username = "rudi", Email = "rudi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 2, Name = "Siti Nurhaliza", Username = "siti", Email = "siti@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 3, Name = "Budi Santoso", Username = "budi", Email = "budi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 4, Name = "Andi Prasetyo", Username = "andi", Email = "andi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 5, Name = "Dewi Lestari", Username = "dewi", Email = "dewi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 6, Name = "Eko Wibowo", Username = "eko", Email = "eko@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 7, Name = "Fitri Handayani", Username = "fitri", Email = "fitri@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 8, Name = "Gunawan Putra", Username = "gunawan", Email = "gunawan@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 9, Name = "Hana Permata", Username = "hana", Email = "hana@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                        new User { Id = 10, Name = "Irfan Maulana", Username = "irfan", Email = "irfan@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) }
                    );
                    context.SaveChanges();
                });

                // Seed User Roles
                var rolesMap = context.Roles.ToDictionary(r => r.Id);
                var userRolesSeed = new List<(int UserId, int RoleId)>
                {
                    (1, 1), (2, 2), (3, 3), (4, 2), (4, 1), (5, 2), (5, 3),
                    (6, 2), (7, 1), (8, 2), (8, 3), (9, 3), (10, 2), (10, 1)
                };

                foreach (var ur in userRolesSeed)
                {
                    var user = context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == ur.UserId);
                    if (user != null && rolesMap.TryGetValue(ur.RoleId, out var role))
                    {
                        user.Roles.Add(role);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
