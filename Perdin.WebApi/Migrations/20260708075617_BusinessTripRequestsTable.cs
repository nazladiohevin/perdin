using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class BusinessTripRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "business_trip_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_number = table.Column<string>(type: "varchar(30)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    departure_date = table.Column<DateOnly>(type: "date", nullable: false),
                    return_date = table.Column<DateOnly>(type: "date", nullable: false),
                    origin_city_id = table.Column<int>(type: "int", nullable: false),
                    destination_city_id = table.Column<int>(type: "int", nullable: true),
                    destination_country_id = table.Column<int>(type: "int", nullable: false),
                    duration_in_days = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "reviewed"),
                    approver_id = table.Column<int>(type: "int", nullable: false),
                    purpose = table.Column<string>(type: "text", nullable: false),
                    pocket_money = table.Column<int>(type: "int", nullable: false),
                    approved_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_trip_requests", x => x.id);
                    table.CheckConstraint("CK_BusinessTrip_Status", "status IN ('reviewed','rejected','approved')");
                    table.ForeignKey(
                        name: "FK_BusinessTrip_Approver",
                        column: x => x.approver_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BusinessTrip_DestinationCity",
                        column: x => x.destination_city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BusinessTrip_DestinationCountry",
                        column: x => x.destination_country_id,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BusinessTrip_OriginCity",
                        column: x => x.origin_city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BusinessTrip_User",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_approver_id",
                table: "business_trip_requests",
                column: "approver_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_destination_city_id",
                table: "business_trip_requests",
                column: "destination_city_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_destination_country_id",
                table: "business_trip_requests",
                column: "destination_country_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_origin_city_id",
                table: "business_trip_requests",
                column: "origin_city_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_request_number",
                table: "business_trip_requests",
                column: "request_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_business_trip_requests_user_id",
                table: "business_trip_requests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_trip_requests");
        }
    }
}
