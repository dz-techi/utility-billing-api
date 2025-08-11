using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedPropertyDescriptionFromUtilityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UtilityTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UtilityTypes",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
