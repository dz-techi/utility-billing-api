using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUser_Properties_PropertyId",
                table: "PropertyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUser_User_UserId",
                table: "PropertyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyUser",
                table: "PropertyUser");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "PropertyUser",
                newName: "PropertyUsers");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyUser_UserId",
                table: "PropertyUsers",
                newName: "IX_PropertyUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyUser_PropertyId",
                table: "PropertyUsers",
                newName: "IX_PropertyUsers_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyUsers",
                table: "PropertyUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUsers_Properties_PropertyId",
                table: "PropertyUsers",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUsers_Users_UserId",
                table: "PropertyUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUsers_Properties_PropertyId",
                table: "PropertyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUsers_Users_UserId",
                table: "PropertyUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyUsers",
                table: "PropertyUsers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "PropertyUsers",
                newName: "PropertyUser");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyUsers_UserId",
                table: "PropertyUser",
                newName: "IX_PropertyUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyUsers_PropertyId",
                table: "PropertyUser",
                newName: "IX_PropertyUser_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyUser",
                table: "PropertyUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUser_Properties_PropertyId",
                table: "PropertyUser",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUser_User_UserId",
                table: "PropertyUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
