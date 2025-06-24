using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Contracts.Common.Enums;
using UtilityBilling.Contracts.Common.UtilityUnitType;
using UtilityBilling.Domain.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Services;

public class DataSeedingService : IDataSeedingService
{
    private readonly ILogger<DataSeedingService> _logger;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public DataSeedingService(ILogger<DataSeedingService> logger, IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _logger = logger;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }

    public async Task SeedTestingData()
    {
        _logger.LogInformation("Starting to seed data for Development");
        
        var user1Id = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        var user2Id = new Guid("f830e81d-3da7-4846-87a1-54792b6c2229");

        var users = new List<Guid> { user1Id, user2Id };

        var billPeriod1Id = new Guid("813ae334-2637-4212-b0de-100cf7faa6ab");
        var billPeriod2Id = new Guid("377f8986-3f3b-4724-ae42-6fc26f4d5c89");
        var billPeriod3Id = new Guid("1b9e94cb-0745-4be6-bbc5-a051f877a740");
        
        var utilityBillPeriods = new List<UtilityBillPeriod>
        {
            new()
            {
                Id = billPeriod1Id,
                UserId = user1Id,
                UtilityBills =
                [
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Electricity,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 25.52m,
                        Cost = 32.44m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Water,
                        MeasurementUnitType = MeasurementUnitType.CubicMeters,
                        Usage = 7.50m,
                        Cost = 17.29m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Gas,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 3.22m,
                        Cost = 12.25m
                    }
                ]
            },
            new()
            {
                Id = billPeriod2Id,
                UserId = user1Id,
                UtilityBills =
                [
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Electricity,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 20.52m,
                        Cost = 33.44m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Water,
                        MeasurementUnitType = MeasurementUnitType.CubicMeters,
                        Usage = 8.50m,
                        Cost = 21.29m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Gas,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 4.22m,
                        Cost = 19.25m
                    }
                ]
            },
            new()
            {
                Id = billPeriod3Id,
                UserId = user2Id,
                UtilityBills =
                [
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Electricity,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 10.52m,
                        Cost = 19.44m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Water,
                        MeasurementUnitType = MeasurementUnitType.CubicMeters,
                        Usage = 2.50m,
                        Cost = 4.49m
                    },
                    new UtilityBill
                    {
                        UtilityBillType = UtilityBillType.Gas,
                        MeasurementUnitType = MeasurementUnitType.KilowattHours,
                        Usage = 1.22m,
                        Cost = 12.25m
                    }
                ]
            },
        };

        foreach (var billPeriod in utilityBillPeriods)
        {
            var billPeriodDto = await _utilityBillPeriodRepository.GetByIdAsync(billPeriod.Id, CancellationToken.None);

            if (billPeriodDto != null)
            {
                continue;
            }

            await _utilityBillPeriodRepository.AddAsync(billPeriod, CancellationToken.None);
            await _utilityBillPeriodRepository.SaveChangesAsync(CancellationToken.None);
        }
        
        _logger.LogInformation("Data seeding for Development completed");
    }
}