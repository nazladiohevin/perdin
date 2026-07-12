using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProvinceAndCitySeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 0.5074m, 101.4478m, "Pekanbaru" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 1.1301m, 104.0531m, "Batam" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { -0.9471m, 100.4172m, "Padang" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { -2.9909m, 104.7567m, "Palembang" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { -5.3971m, 105.2668m, "Bandar Lampung", 13 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { -0.0263m, 109.3425m, "Pontianak", 14 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { -0.5022m, 117.1536m, "Samarinda", 15 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { -5.1476m, 119.4327m, "Makassar", 16 });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sumatera", "Riau" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sumatera", "Kepulauan Riau" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sumatera", "Sumatera Barat" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sumatera", "Sumatera Selatan" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sumatera", "Lampung" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Kalimantan", "Kalimantan Barat" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Kalimantan", "Kalimantan Timur" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sulawesi", "Sulawesi Selatan" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Sulawesi", "Sulawesi Utara" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Lombok", "Nusa Tenggara Barat" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Timor", "Nusa Tenggara Timur" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 1, "Papua", "Papua" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 3.1390m, 101.6869m, "Kuala Lumpur City" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 3.1073m, 101.6067m, "Petaling Jaya" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 1.3521m, 103.8198m, "Singapore City" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "latitude", "longitude", "name" },
                values: new object[] { 13.7563m, 100.5018m, "Bangkok City" });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { 35.6938m, 139.7034m, "Shinjuku", 15 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { 37.4979m, 127.0276m, "Gangnam", 16 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { 34.0522m, -118.2437m, "Los Angeles", 18 });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "latitude", "longitude", "name", "province_id" },
                values: new object[] { 51.5074m, -0.1278m, "London City", 20 });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 2, "Luar Negeri", "Kuala Lumpur" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 2, "Luar Negeri", "Selangor" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 3, "Luar Negeri", "Central Singapore" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 4, "Luar Negeri", "Bangkok" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 5, "Luar Negeri", "Hanoi" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 6, "Luar Negeri", "Metro Manila" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 8, "Luar Negeri", "Tokyo" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 9, "Luar Negeri", "Seoul" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 10, "Luar Negeri", "Beijing" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 11, "Luar Negeri", "California" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 11, "Luar Negeri", "New York" });

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "country_id", "island", "name" },
                values: new object[] { 12, "Luar Negeri", "Greater London" });
        }
    }
}
