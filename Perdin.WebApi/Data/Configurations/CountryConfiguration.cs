using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.IsForeign)
                .HasColumnName("is_foreign")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            var now = new DateTime(2026, 1, 1);
            builder.HasData(
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
        }
    }
}
