using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNecessaryDatesForUtilityBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UtilityBills",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UtilityBills",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "UtilityBills",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UtilityBills");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UtilityBills");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "UtilityBills");
        }
    }
}
