using System;
using System.Linq;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Seeders
{
    public static class BusinessTripRequestSeeding
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.BusinessTripRequests.Any())
            {
                var now = new DateTime(2026, 1, 1);
                SeedingHelper.ExecuteWithIdentityInsert(context, "business_trip_requests", () =>
                {
                    context.BusinessTripRequests.AddRange(
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
                    context.SaveChanges();
                });
            }
        }
    }
}
