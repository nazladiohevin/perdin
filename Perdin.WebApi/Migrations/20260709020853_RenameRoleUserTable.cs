using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perdin.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoleUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_user_roles_RolesId",
                table: "role_user");

            migrationBuilder.DropForeignKey(
                name: "FK_role_user_users_UsersId",
                table: "role_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_user",
                table: "role_user");

            migrationBuilder.RenameTable(
                name: "role_user",
                newName: "user_roles");

            migrationBuilder.RenameIndex(
                name: "IX_role_user_UsersId",
                table: "user_roles",
                newName: "IX_user_roles_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_RolesId",
                table: "user_roles",
                column: "RolesId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_UsersId",
                table: "user_roles",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_RolesId",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_UsersId",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "role_user");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_UsersId",
                table: "role_user",
                newName: "IX_role_user_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_user",
                table: "role_user",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_role_user_roles_RolesId",
                table: "role_user",
                column: "RolesId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_user_users_UsersId",
                table: "role_user",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
