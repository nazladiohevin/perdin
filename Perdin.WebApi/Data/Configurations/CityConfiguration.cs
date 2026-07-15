using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.ProvinceId)
                .HasColumnName("province_id")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Latitude)
                .HasColumnName("latitude")
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(e => e.Longitude)
                .HasColumnName("longitude")
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.HasOne(e => e.Province)
                .WithMany(p => p.Cities)
                .HasForeignKey(e => e.ProvinceId)
                .HasConstraintName("FK_Cities_Provinces")
                .OnDelete(DeleteBehavior.NoAction);

            var now = new DateTime(2026, 1, 1);
            builder.HasData(
                new City { Id = 1, ProvinceId = 1, Name = "Jakarta Pusat", Latitude = -6.1805m, Longitude = 106.8284m, CreatedAt = now },
                new City { Id = 2, ProvinceId = 1, Name = "Jakarta Selatan", Latitude = -6.2615m, Longitude = 106.8106m, CreatedAt = now },
                new City { Id = 3, ProvinceId = 2, Name = "Bandung", Latitude = -6.9175m, Longitude = 107.6191m, CreatedAt = now },
                new City { Id = 4, ProvinceId = 2, Name = "Bogor", Latitude = -6.5971m, Longitude = 106.7932m, CreatedAt = now },
                new City { Id = 5, ProvinceId = 3, Name = "Semarang", Latitude = -6.9667m, Longitude = 110.4167m, CreatedAt = now },
                new City { Id = 6, ProvinceId = 3, Name = "Surakarta", Latitude = -7.5667m, Longitude = 110.8167m, CreatedAt = now },
                new City { Id = 7, ProvinceId = 4, Name = "Yogyakarta", Latitude = -7.7956m, Longitude = 110.3695m, CreatedAt = now },
                new City { Id = 8, ProvinceId = 5, Name = "Surabaya", Latitude = -7.2504m, Longitude = 112.7688m, CreatedAt = now },
                new City { Id = 9, ProvinceId = 5, Name = "Malang", Latitude = -7.9839m, Longitude = 112.6214m, CreatedAt = now },
                new City { Id = 10, ProvinceId = 6, Name = "Tangerang", Latitude = -6.1702m, Longitude = 106.6403m, CreatedAt = now },
                new City { Id = 11, ProvinceId = 7, Name = "Denpasar", Latitude = -8.6705m, Longitude = 115.2126m, CreatedAt = now },
                new City { Id = 12, ProvinceId = 8, Name = "Medan", Latitude = 3.5952m, Longitude = 98.6722m, CreatedAt = now },
                new City { Id = 13, ProvinceId = 9, Name = "Pekanbaru", Latitude = 0.5074m, Longitude = 101.4478m, CreatedAt = now },
                new City { Id = 14, ProvinceId = 10, Name = "Batam", Latitude = 1.1301m, Longitude = 104.0531m, CreatedAt = now },
                new City { Id = 15, ProvinceId = 11, Name = "Padang", Latitude = -0.9471m, Longitude = 100.4172m, CreatedAt = now },
                new City { Id = 16, ProvinceId = 12, Name = "Palembang", Latitude = -2.9909m, Longitude = 104.7567m, CreatedAt = now },
                new City { Id = 17, ProvinceId = 13, Name = "Bandar Lampung", Latitude = -5.3971m, Longitude = 105.2668m, CreatedAt = now },
                new City { Id = 18, ProvinceId = 14, Name = "Pontianak", Latitude = -0.0263m, Longitude = 109.3425m, CreatedAt = now },
                new City { Id = 19, ProvinceId = 15, Name = "Samarinda", Latitude = -0.5022m, Longitude = 117.1536m, CreatedAt = now },
                new City { Id = 20, ProvinceId = 16, Name = "Makassar", Latitude = -5.1476m, Longitude = 119.4327m, CreatedAt = now }
            );
        }
    }
}
