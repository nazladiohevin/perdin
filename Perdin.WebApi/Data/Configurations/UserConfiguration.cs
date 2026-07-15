using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Username)
                .HasColumnName("username")
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.HasIndex(e => e.Username).IsUnique();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at")
                .HasColumnType("datetime");

            builder.HasQueryFilter(e => e.DeletedAt == null);

            // Many-to-many relationship with Role using user_roles table
            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j =>
                {
                    j.ToTable("user_roles");
                    j.HasData(
                        new { UsersId = 1, RolesId = 1 },
                        new { UsersId = 2, RolesId = 2 },
                        new { UsersId = 3, RolesId = 3 },
                        new { UsersId = 4, RolesId = 2 },
                        new { UsersId = 4, RolesId = 1 },
                        new { UsersId = 5, RolesId = 2 },
                        new { UsersId = 5, RolesId = 3 },
                        new { UsersId = 6, RolesId = 2 },
                        new { UsersId = 7, RolesId = 1 },
                        new { UsersId = 8, RolesId = 2 },
                        new { UsersId = 8, RolesId = 3 },
                        new { UsersId = 9, RolesId = 3 },
                        new { UsersId = 10, RolesId = 2 },
                        new { UsersId = 10, RolesId = 1 }
                    );
                });

            // Seeding User data
            var hashedPassword = "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC";
            builder.HasData(
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
        }
    }
}
