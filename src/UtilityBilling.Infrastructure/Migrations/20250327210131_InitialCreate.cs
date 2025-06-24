using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UtilityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtilityBillPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityBillPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilityBills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UtilityBillType = table.Column<int>(type: "integer", nullable: false),
                    Usage = table.Column<decimal>(type: "numeric", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    MeasurementUnitType = table.Column<int>(type: "integer", nullable: false),
                    UtilityBillPeriodId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilityBills_UtilityBillPeriods_UtilityBillPeriodId",
                        column: x => x.UtilityBillPeriodId,
                        principalTable: "UtilityBillPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UtilityBillPeriods",
                columns: new[] { "Id", "EndDate", "Name", "StartDate", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("29a69221-03a7-41fe-9c10-4645a7db10e7"), new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Utc), "April 2025", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("99d5d2cf-93e1-4300-ac09-39849738d744") },
                    { new Guid("60a42c2d-c5e9-4692-8746-dee5831a16e6"), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "January 2025", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("99d5d2cf-93e1-4300-ac09-39849738d744") },
                    { new Guid("813ae334-2637-4212-b0de-100cf7faa6ab"), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "December 2024", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("99d5d2cf-93e1-4300-ac09-39849738d744") },
                    { new Guid("a2b48d07-98bc-48fa-893b-290d3f068a39"), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), "March 2025", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("99d5d2cf-93e1-4300-ac09-39849738d744") },
                    { new Guid("e3a31ffb-ff65-4fcd-a652-e241e372e757"), new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "February 2025", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("99d5d2cf-93e1-4300-ac09-39849738d744") }
                });

            migrationBuilder.InsertData(
                table: "UtilityBills",
                columns: new[] { "Id", "Cost", "MeasurementUnitType", "Usage", "UtilityBillPeriodId", "UtilityBillType" },
                values: new object[,]
                {
                    { new Guid("997f4444-f8f3-4ca0-9b2e-ea7464938f65"), 32.44m, 0, 25.52m, new Guid("813ae334-2637-4212-b0de-100cf7faa6ab"), 0 },
                    { new Guid("a1c25f4f-1a01-44f2-bb10-8519607ee466"), 12.25m, 0, 3.22m, new Guid("813ae334-2637-4212-b0de-100cf7faa6ab"), 2 },
                    { new Guid("e67328ae-bcdb-4f40-b5f2-bfaf2a584736"), 17.29m, 1, 7.50m, new Guid("813ae334-2637-4212-b0de-100cf7faa6ab"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilityBills_UtilityBillPeriodId",
                table: "UtilityBills",
                column: "UtilityBillPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilityBills");

            migrationBuilder.DropTable(
                name: "UtilityBillPeriods");
        }
    }
}
