using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNamePropertyForProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Properties",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Properties");
        }
    }
}
