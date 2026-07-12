using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBusinessTripRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "business_trip_requests",
                columns: new[] { "id", "approved_at", "approver_id", "created_at", "departure_date", "destination_city_id", "destination_country_id", "duration_in_days", "origin_city_id", "pocket_money", "purpose", "request_number", "return_date", "status", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 1), 2, 1, 3, 1, 0, "Meeting Internal", "PERDIN/20260701/0001", new DateOnly(2026, 7, 4), "reviewed", null, 2 },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 1), 3, 1, 2, 1, 500000, "Kunjungan Cabang Bandung", "PERDIN/20260701/0002", new DateOnly(2026, 7, 3), "approved", null, 4 },
                    { 3, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 5), 4, 1, 2, 3, 400000, "Audit Kebun Bogor", "PERDIN/20260702/0001", new DateOnly(2026, 7, 7), "reviewed", null, 5 },
                    { 4, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 6), 11, 1, 4, 1, 1200000, "Rapat Koordinasi Nasional Bali", "PERDIN/20260702/0002", new DateOnly(2026, 7, 10), "reviewed", null, 6 },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 10), null, 3, 5, 1, 4000000, "IT Conference Singapore", "PERDIN/20260703/0001", new DateOnly(2026, 7, 15), "approved", null, 8 },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 12), null, 8, 6, 1, 4800000, "Studi Banding Sistem Kereta", "PERDIN/20260703/0002", new DateOnly(2026, 7, 18), "rejected", null, 10 },
                    { 7, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 15), 9, 1, 2, 8, 400000, "Sosialisasi SOP Baru", "PERDIN/20260704/0001", new DateOnly(2026, 7, 17), "reviewed", null, 2 },
                    { 8, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 20), 1, 1, 5, 12, 1500000, "General Meeting Direksi", "PERDIN/20260704/0002", new DateOnly(2026, 7, 25), "reviewed", null, 4 },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 22), null, 2, 4, 1, 3200000, "Negosiasi Kontrak Vendor Malaysia", "PERDIN/20260705/0001", new DateOnly(2026, 7, 26), "approved", null, 5 },
                    { 10, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 7, 28), null, 12, 7, 1, 5600000, "Pelatihan Manajemen Risiko London", "PERDIN/20260705/0002", new DateOnly(2026, 8, 4), "reviewed", null, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "business_trip_requests",
                keyColumn: "id",
                keyValue: 10);
        }
    }
}
