using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeApproverIdNullableAndAddProvinceIsland : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "island",
                table: "provinces",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "approver_id",
                table: "business_trip_requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 1,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 2,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 3,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 4,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 5,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 6,
                column: "island",
                value: "Jawa");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 7,
                column: "island",
                value: "Bali");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 8,
                column: "island",
                value: "Sumatera");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 9,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 10,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 11,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 12,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 13,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 14,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 15,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 16,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 17,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 18,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 19,
                column: "island",
                value: "Luar Negeri");

            migrationBuilder.UpdateData(
                table: "provinces",
                keyColumn: "id",
                keyValue: 20,
                column: "island",
                value: "Luar Negeri");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "island",
                table: "provinces");

            migrationBuilder.AlterColumn<int>(
                name: "approver_id",
                table: "business_trip_requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
