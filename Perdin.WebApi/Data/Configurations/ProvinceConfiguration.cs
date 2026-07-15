using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("provinces");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CountryId)
                .HasColumnName("country_id")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Island)
                .HasColumnName("island")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.HasOne(e => e.Country)
                .WithMany(c => c.Provinces)
                .HasForeignKey(e => e.CountryId)
                .HasConstraintName("FK_Provinces_Countries")
                .OnDelete(DeleteBehavior.NoAction);

            var now = new DateTime(2026, 1, 1);
            builder.HasData(
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
        }
    }
}
