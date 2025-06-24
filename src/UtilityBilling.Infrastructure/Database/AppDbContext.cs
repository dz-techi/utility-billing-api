using Microsoft.EntityFrameworkCore;
using UtilityBilling.Contracts.Common;
using UtilityBilling.Contracts.Common.Enums;
using UtilityBilling.Contracts.Common.UtilityUnitType;
using UtilityBilling.Domain.UtilityBillPeriod;

namespace UtilityBilling.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    // Define DbSets for the entities
    public DbSet<UtilityBillPeriod> UtilityBillPeriods { get; set; }
    public DbSet<UtilityBill> UtilityBills { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UtilityBillPeriod>()
            .HasMany(u => u.UtilityBills)
            .WithOne(u => u.UtilityBillPeriod)
            .HasForeignKey(u => u.UtilityBillPeriodId)
            .OnDelete(DeleteBehavior.Cascade);

        var billPeriod1Id = new Guid("813ae334-2637-4212-b0de-100cf7faa6ab");
        var billPeriod2Id = new Guid("60a42c2d-c5e9-4692-8746-dee5831a16e6");
        var billPeriod3Id = new Guid("e3a31ffb-ff65-4fcd-a652-e241e372e757");
        var billPeriod4Id = new Guid("a2b48d07-98bc-48fa-893b-290d3f068a39");
        var billPeriod5Id = new Guid("29a69221-03a7-41fe-9c10-4645a7db10e7");
        
        var user1Id = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        
        modelBuilder.Entity<UtilityBillPeriod>().HasData(
            new UtilityBillPeriod
            {
                Id = billPeriod1Id,
                Name = "December 2024",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 12, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 12, 31), DateTimeKind.Utc)
            },
            new UtilityBillPeriod
            {
                Id = billPeriod2Id,
                Name = "January 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 1, 31), DateTimeKind.Utc)
            },
            new UtilityBillPeriod
            {
                Id = billPeriod3Id,
                Name = "February 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 2, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 2, 28), DateTimeKind.Utc)
            },
            new UtilityBillPeriod
            {
                Id = billPeriod4Id,
                Name = "March 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Active,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 3, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 3, 31), DateTimeKind.Utc)
            },
            new UtilityBillPeriod
            {
                Id = billPeriod5Id,
                Name = "April 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Upcoming,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 4, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 4, 30), DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<UtilityBill>().HasData(
            new UtilityBill
            {
                Id = new Guid("997f4444-f8f3-4ca0-9b2e-ea7464938f65"),
                UtilityBillType = UtilityBillType.Electricity,
                MeasurementUnitType = MeasurementUnitType.KilowattHours,
                Usage = 25.52m,
                Cost = 32.44m,
                UtilityBillPeriodId = billPeriod1Id
            },
            new UtilityBill
            {
                Id = new Guid("e67328ae-bcdb-4f40-b5f2-bfaf2a584736"),
                UtilityBillType = UtilityBillType.Water,
                MeasurementUnitType = MeasurementUnitType.CubicMeters,
                Usage = 7.50m,
                Cost = 17.29m,
                UtilityBillPeriodId = billPeriod1Id
            },
            new UtilityBill
            {
                Id = new Guid("a1c25f4f-1a01-44f2-bb10-8519607ee466"),
                UtilityBillType = UtilityBillType.Gas,
                MeasurementUnitType = MeasurementUnitType.KilowattHours,
                Usage = 3.22m,
                Cost = 12.25m,
                UtilityBillPeriodId = billPeriod1Id
            }
        );
    }
}