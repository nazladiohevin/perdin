using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "name", "password", "updated_at", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rudi@gmail.com", "Rudi Hartono", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "rudi" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "siti@gmail.com", "Siti Nurhaliza", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "siti" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "budi@gmail.com", "Budi Santoso", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "budi" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "andi@gmail.com", "Andi Prasetyo", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "andi" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dewi@gmail.com", "Dewi Lestari", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "dewi" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eko@gmail.com", "Eko Wibowo", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "eko" },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fitri@gmail.com", "Fitri Handayani", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "fitri" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gunawan@gmail.com", "Gunawan Putra", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "gunawan" },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hana@gmail.com", "Hana Permata", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "hana" },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "irfan@gmail.com", "Irfan Maulana", "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC", null, "irfan" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 7 },
                    { 1, 10 },
                    { 2, 2 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 8 },
                    { 2, 10 },
                    { 3, 3 },
                    { 3, 5 },
                    { 3, 8 },
                    { 3, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 10);
        }
    }
}
