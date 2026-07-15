using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class BusinessTripRequestConfiguration : IEntityTypeConfiguration<BusinessTripRequest>
    {
        public void Configure(EntityTypeBuilder<BusinessTripRequest> builder)
        {
            builder.ToTable("business_trip_requests", t =>
            {
                t.HasCheckConstraint("CK_BusinessTrip_Status",
                    "status IN ('reviewed','rejected','approved')");
            });

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.RequestNumber)
                .HasColumnName("request_number")
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.HasIndex(e => e.RequestNumber).IsUnique();

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.DepartureDate)
                .HasColumnName("departure_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.ReturnDate)
                .HasColumnName("return_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.OriginCityId)
                .HasColumnName("origin_city_id")
                .IsRequired();

            builder.Property(e => e.DestinationCityId)
                .HasColumnName("destination_city_id");

            builder.Property(e => e.DestinationCountryId)
                .HasColumnName("destination_country_id")
                .IsRequired();

            builder.Property(e => e.DurationInDays)
                .HasColumnName("duration_in_days")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(20)")
                .IsRequired()
                .HasDefaultValue("reviewed");

            builder.Property(e => e.ApproverId)
                .HasColumnName("approver_id");

            builder.Property(e => e.Purpose)
                .HasColumnName("purpose")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(e => e.PocketMoney)
                .HasColumnName("pocket_money")
                .IsRequired();

            builder.Property(e => e.ApprovedAt)
                .HasColumnName("approved_at")
                .HasColumnType("datetime");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            // Foreign keys
            builder.HasOne(e => e.User)
                .WithMany(u => u.BusinessTripRequestsAsUser)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_BusinessTrip_User")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Approver)
                .WithMany(u => u.BusinessTripRequestsAsApprover)
                .HasForeignKey(e => e.ApproverId)
                .HasConstraintName("FK_BusinessTrip_Approver")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.OriginCity)
                .WithMany(c => c.BusinessTripRequestsAsOrigin)
                .HasForeignKey(e => e.OriginCityId)
                .HasConstraintName("FK_BusinessTrip_OriginCity")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.DestinationCity)
                .WithMany(c => c.BusinessTripRequestsAsDestination)
                .HasForeignKey(e => e.DestinationCityId)
                .HasConstraintName("FK_BusinessTrip_DestinationCity")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.DestinationCountry)
                .WithMany(c => c.BusinessTripRequests)
                .HasForeignKey(e => e.DestinationCountryId)
                .HasConstraintName("FK_BusinessTrip_DestinationCountry")
                .OnDelete(DeleteBehavior.NoAction);

            var now = new DateTime(2026, 1, 1);
            builder.HasData(
                new BusinessTripRequest
                {
                    Id = 1,
                    RequestNumber = "PERDIN/20260701/0001",
                    UserId = 2,
                    DepartureDate = new DateOnly(2026, 7, 1),
                    ReturnDate = new DateOnly(2026, 7, 4),
                    OriginCityId = 1,
                    DestinationCityId = 2,
                    DestinationCountryId = 1,
                    DurationInDays = 3,
                    Status = "reviewed",
                    Purpose = "Meeting Internal",
                    PocketMoney = 0,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 2,
                    RequestNumber = "PERDIN/20260701/0002",
                    UserId = 4,
                    DepartureDate = new DateOnly(2026, 7, 1),
                    ReturnDate = new DateOnly(2026, 7, 3),
                    OriginCityId = 1,
                    DestinationCityId = 3,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "approved",
                    Purpose = "Kunjungan Cabang Bandung",
                    PocketMoney = 500000,
                    ApproverId = 1,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 3,
                    RequestNumber = "PERDIN/20260702/0001",
                    UserId = 5,
                    DepartureDate = new DateOnly(2026, 7, 5),
                    ReturnDate = new DateOnly(2026, 7, 7),
                    OriginCityId = 3,
                    DestinationCityId = 4,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "reviewed",
                    Purpose = "Audit Kebun Bogor",
                    PocketMoney = 400000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 4,
                    RequestNumber = "PERDIN/20260702/0002",
                    UserId = 6,
                    DepartureDate = new DateOnly(2026, 7, 6),
                    ReturnDate = new DateOnly(2026, 7, 10),
                    OriginCityId = 1,
                    DestinationCityId = 11,
                    DestinationCountryId = 1,
                    DurationInDays = 4,
                    Status = "reviewed",
                    Purpose = "Rapat Koordinasi Nasional Bali",
                    PocketMoney = 1200000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 5,
                    RequestNumber = "PERDIN/20260703/0001",
                    UserId = 8,
                    DepartureDate = new DateOnly(2026, 7, 10),
                    ReturnDate = new DateOnly(2026, 7, 15),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 3,
                    DurationInDays = 5,
                    Status = "approved",
                    Purpose = "IT Conference Singapore",
                    PocketMoney = 4000000,
                    ApproverId = 3,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 6,
                    RequestNumber = "PERDIN/20260703/0002",
                    UserId = 10,
                    DepartureDate = new DateOnly(2026, 7, 12),
                    ReturnDate = new DateOnly(2026, 7, 18),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 8,
                    DurationInDays = 6,
                    Status = "rejected",
                    Purpose = "Studi Banding Sistem Kereta",
                    PocketMoney = 4800000,
                    ApproverId = 1,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 7,
                    RequestNumber = "PERDIN/20260704/0001",
                    UserId = 2,
                    DepartureDate = new DateOnly(2026, 7, 15),
                    ReturnDate = new DateOnly(2026, 7, 17),
                    OriginCityId = 8,
                    DestinationCityId = 9,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "reviewed",
                    Purpose = "Sosialisasi SOP Baru",
                    PocketMoney = 400000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 8,
                    RequestNumber = "PERDIN/20260704/0002",
                    UserId = 4,
                    DepartureDate = new DateOnly(2026, 7, 20),
                    ReturnDate = new DateOnly(2026, 7, 25),
                    OriginCityId = 12,
                    DestinationCityId = 1,
                    DestinationCountryId = 1,
                    DurationInDays = 5,
                    Status = "reviewed",
                    Purpose = "General Meeting Direksi",
                    PocketMoney = 1500000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 9,
                    RequestNumber = "PERDIN/20260705/0001",
                    UserId = 5,
                    DepartureDate = new DateOnly(2026, 7, 22),
                    ReturnDate = new DateOnly(2026, 7, 26),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 2,
                    DurationInDays = 4,
                    Status = "approved",
                    Purpose = "Negosiasi Kontrak Vendor Malaysia",
                    PocketMoney = 3200000,
                    ApproverId = 3,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 10,
                    RequestNumber = "PERDIN/20260705/0002",
                    UserId = 6,
                    DepartureDate = new DateOnly(2026, 7, 28),
                    ReturnDate = new DateOnly(2026, 8, 4),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 12,
                    DurationInDays = 7,
                    Status = "reviewed",
                    Purpose = "Pelatihan Manajemen Risiko London",
                    PocketMoney = 5600000,
                    CreatedAt = now
                }
            );
        }
    }
}
