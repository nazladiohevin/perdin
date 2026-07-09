using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indonesia", null });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "created_at", "is_foreign", "name", "updated_at" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Malaysia", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Singapura", null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Thailand", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vietnam", null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Filipina", null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Brunei", null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Jepang", null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Korea Selatan", null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "China", null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Amerika Serikat", null },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Inggris", null },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Jerman", null },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Perancis", null },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Belanda", null },
                    { 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Australia", null },
                    { 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Selandia Baru", null },
                    { 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Arab Saudi", null },
                    { 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Uni Emirat Arab", null },
                    { 20, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "India", null }
                });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "id", "country_id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DKI Jakarta", null },
                    { 2, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jawa Barat", null },
                    { 3, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jawa Tengah", null },
                    { 4, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DI Yogyakarta", null },
                    { 5, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jawa Timur", null },
                    { 6, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banten", null },
                    { 7, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bali", null },
                    { 8, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sumatera Utara", null },
                    { 9, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuala Lumpur", null },
                    { 10, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Selangor", null },
                    { 11, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Central Singapore", null },
                    { 12, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangkok", null },
                    { 13, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hanoi", null },
                    { 14, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metro Manila", null },
                    { 15, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tokyo", null },
                    { 16, 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seoul", null },
                    { 17, 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beijing", null },
                    { 18, 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "California", null },
                    { 19, 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New York", null },
                    { 20, 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greater London", null }
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "created_at", "latitude", "longitude", "name", "province_id", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.1805m, 106.8284m, "Jakarta Pusat", 1, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.2615m, 106.8106m, "Jakarta Selatan", 1, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.9175m, 107.6191m, "Bandung", 2, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.5971m, 106.7932m, "Bogor", 2, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.9667m, 110.4167m, "Semarang", 3, null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -7.5667m, 110.8167m, "Surakarta", 3, null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -7.7956m, 110.3695m, "Yogyakarta", 4, null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -7.2504m, 112.7688m, "Surabaya", 5, null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -7.9839m, 112.6214m, "Malang", 5, null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -6.1702m, 106.6403m, "Tangerang", 6, null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -8.6705m, 115.2126m, "Denpasar", 7, null },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.5952m, 98.6722m, "Medan", 8, null },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.1390m, 101.6869m, "Kuala Lumpur City", 9, null },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.1073m, 101.6067m, "Petaling Jaya", 10, null },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.3521m, 103.8198m, "Singapore City", 11, null },
                    { 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13.7563m, 100.5018m, "Bangkok City", 12, null },
                    { 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 35.6938m, 139.7034m, "Shinjuku", 15, null },
                    { 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37.4979m, 127.0276m, "Gangnam", 16, null },
                    { 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34.0522m, -118.2437m, "Los Angeles", 18, null },
                    { 20, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.5074m, -0.1278m, "London City", 20, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 12);
        }
    }
}
