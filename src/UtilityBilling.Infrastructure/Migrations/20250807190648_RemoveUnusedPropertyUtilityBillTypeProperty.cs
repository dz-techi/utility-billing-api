using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedPropertyUtilityBillTypeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "UtilityTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "UtilityTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
