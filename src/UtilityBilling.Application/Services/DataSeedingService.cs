using Microsoft.Extensions.Logging;
using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Services;

public class DataSeedingService : IDataSeedingService
{
    private readonly ILogger<DataSeedingService> _logger;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;
    private readonly IUtilityBillRepository _utilityBillRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public DataSeedingService(
        ILogger<DataSeedingService> logger,
        IUtilityBillPeriodRepository utilityBillPeriodRepository,
        IUtilityBillRepository utilityBillRepository,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository)
    {
        _logger = logger;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
        _utilityBillRepository = utilityBillRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task SeedTestingData()
    {
        _logger.LogInformation("Starting to seed data for Development");

        var billPeriod1Id = new Guid("813ae334-2637-4212-b0de-100cf7faa6ab");
        var billPeriod2Id = new Guid("60a42c2d-c5e9-4692-8746-dee5831a16e6");
        var billPeriod3Id = new Guid("e3a31ffb-ff65-4fcd-a652-e241e372e757");
        var billPeriod4Id = new Guid("a2b48d07-98bc-48fa-893b-290d3f068a39");
        var billPeriod5Id = new Guid("29a69221-03a7-41fe-9c10-4645a7db10e7");

        var user1Id = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        var user2Id = new Guid("c70fb249-ad8c-45da-a675-93735e0ede70");
        var user3Id = new Guid("9fdda49e-7557-4ac9-90b4-27e2534d9a9e");

        var property1Id = new Guid("f3b2c4d5-6e7f-8a9b-0c1d-2e3f4a5b6c7d");
        var property2Id = new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d");

        var users = new List<User>
        {
            new()
            {
                Id = user1Id,
                CreatedDate = DateTime.UtcNow,
                Email = "dainis.zogots@gmail.com",
                FirstName = "Dainis",
                IsActive = true,
                LastLoginDate = DateTime.UtcNow,
                LastName = "Žogots"
            },
            new()
            {
                Id = user2Id,
                CreatedDate = DateTime.UtcNow,
                Email = "jelena.zogota@gmail.com",
                FirstName = "Jeļena",
                IsActive = true,
                LastLoginDate = DateTime.UtcNow,
                LastName = "Žogota"
            },
            new()
            {
                Id = user3Id,
                CreatedDate = DateTime.UtcNow,
                Email = "daniels.vilanu@gmail.com",
                FirstName = "Daniels",
                IsActive = true,
                LastLoginDate = DateTime.UtcNow,
                LastName = "Bikovskis"
            }
        };

        foreach (var user in users)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id, CancellationToken.None);

            if (existingUser != null)
            {
                _logger.LogInformation("User with ID {Id} already exists, skipping", user.Id);

                continue;
            }

            await _userRepository.AddAsync(user, CancellationToken.None);

            _logger.LogInformation("Added user with ID {Id}", user.Id);
        }

        await _userRepository.SaveChangesAsync(CancellationToken.None);

        var properties = new List<Property>
        {
            new Property
            {
                Id = property1Id,
                Name = "My Apartment in Rēzekne",
                Address = new Address("Atbrīvošanas aleja 119B - 48", "Rēzekne", "LV-4601", "Latvia"),
                OwnerId = user1Id,
                PropertyType = PropertyType.Apartment,
                PropertyUsers = [
                    new PropertyUser
                    {
                        AssignedDate = DateTime.UtcNow.AddDays(-20),
                        UserId = user1Id,
                        Role = UserRole.Owner,
                        PropertyId = property1Id
                    },
                    new PropertyUser
                    {
                        AssignedDate = DateTime.UtcNow.AddDays(-10),
                        UserId = user2Id,
                        Role = UserRole.Admin,
                        PropertyId = property1Id
                    }
                ],
                UtilityTypes = [
                    new PropertyUtilityType
                    {
                        Id = new Guid("d7c47590-ef00-4b6b-96a8-d82cf277e5ad"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.KilowattHours,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Electricity
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("3f9c2714-05ee-41d1-b25c-04f3312ead47"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.CubicMeters,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Water
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("042e4a44-f0ea-4c1d-8caf-12d67ffc73b7"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.CubicMeters,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Gas
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("c09b02c5-d706-40a0-b734-ab8313715d32"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.KilowattHours,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Heating
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("f5ff2d6d-10e2-4792-8bec-799125ff80e7"),
                        HasUnitMeasurement = false,
                        UnitMeasurementType = MeasurementUnitType.None,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Maintenance
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("6b279229-6f9f-4ad4-b860-b6d7ae4b177f"),
                        HasUnitMeasurement = false,
                        UnitMeasurementType = MeasurementUnitType.None,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.Internet
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("2aa082e4-ff34-4dd9-8eaf-c4b414890084"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.Liters,
                        PropertyId = property1Id,
                        UtilityBillType = UtilityBillType.DrinkingWater
                    }
                ]
            },
            new Property
            {
                Id = property2Id,
                Name = "Tēva dzīvoklis Rēzeknē",
                Address = new Address("Vaļņu iela 2A - 51", "Rēzekne", "LV-4601", "Latvia"),
                OwnerId = user1Id,
                PropertyType = PropertyType.Apartment,
                PropertyUsers = [
                    new PropertyUser
                    {
                        AssignedDate = DateTime.UtcNow.AddDays(-20),
                        UserId = user1Id,
                        Role = UserRole.Owner,
                        PropertyId = property2Id
                    },
                    new PropertyUser
                    {
                        AssignedDate = DateTime.UtcNow.AddDays(-10),
                        UserId = user3Id,
                        Role = UserRole.Viewer,
                        PropertyId = property2Id
                    }
                ],
                UtilityTypes = [
                    new PropertyUtilityType
                    {
                        Id = new Guid("c1d2e3f4-5a6b-7c8d-9e0f-1a2b3c4d5e6f"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.KilowattHours,
                        PropertyId = property2Id,
                        UtilityBillType = UtilityBillType.Electricity
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("dcd9e715-c1cd-4ee4-b391-6e6260f51dfb"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.CubicMeters,
                        PropertyId = property2Id,
                        UtilityBillType = UtilityBillType.Water
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("c3cae69a-8787-4656-b033-7122f4d4e7ff"),
                        HasUnitMeasurement = true,
                        UnitMeasurementType = MeasurementUnitType.KilowattHours,
                        PropertyId = property2Id,
                        UtilityBillType = UtilityBillType.Heating
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("10d2ffd3-7f6a-4736-ad40-3a00dd15d592"),
                        HasUnitMeasurement = false,
                        UnitMeasurementType = MeasurementUnitType.None,
                        PropertyId = property2Id,
                        UtilityBillType = UtilityBillType.Maintenance
                    },
                    new PropertyUtilityType
                    {
                        Id = new Guid("165b85a0-8afc-4c47-82a4-7870f2b4914c"),
                        HasUnitMeasurement = false,
                        UnitMeasurementType = MeasurementUnitType.None,
                        PropertyId = property2Id,
                        UtilityBillType = UtilityBillType.RentalFee
                    },
                ]
            }
        };

        foreach (var property in properties)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(property.Id, CancellationToken.None);

            if (existingProperty != null)
            {
                _logger.LogInformation("Property with ID {Id} already exists, skipping.", property.Id);
                continue;
            }

            await _propertyRepository.AddAsync(property, CancellationToken.None);

            _logger.LogInformation("Added property with ID {Id}", property.Id);
        }

        await _propertyRepository.SaveChangesAsync(CancellationToken.None);

        var utilityBillPeriods = new List<UtilityBillPeriod>
        {
            new UtilityBillPeriod
            {
                Id = billPeriod1Id,
                Name = "December 2024",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2024, 12, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2024, 12, 31), DateTimeKind.Utc),
                PropertyId = property1Id,
            },
            new UtilityBillPeriod
            {
                Id = billPeriod2Id,
                Name = "January 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 1, 31), DateTimeKind.Utc),
                PropertyId = property1Id,
            },
            new UtilityBillPeriod
            {
                Id = billPeriod3Id,
                Name = "February 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Closed,
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 2, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 2, 28), DateTimeKind.Utc),
                PropertyId = property1Id,
            },
            new UtilityBillPeriod
            {
                Id = billPeriod4Id,
                Name = "March 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Active,
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 3, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 3, 31), DateTimeKind.Utc),
                PropertyId = property1Id,
            },
            new UtilityBillPeriod
            {
                Id = billPeriod5Id,
                Name = "April 2025",
                UserId = user1Id,
                Status = BillPeriodStatus.Upcoming,
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 4, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 4, 30), DateTimeKind.Utc),
                PropertyId = property1Id,
            }
        };

        foreach (var billPeriod in utilityBillPeriods)
        {
            var existingBillPeriod = await _utilityBillPeriodRepository.GetByIdAsync(billPeriod.Id, CancellationToken.None);

            if (existingBillPeriod != null)
            {
                _logger.LogInformation("Utility bill period with ID {Id} already exists, skipping.", billPeriod.Id);
                continue;
            }

            await _utilityBillPeriodRepository.AddAsync(billPeriod, CancellationToken.None);

            _logger.LogInformation("Added utility bill period with ID {Id}", billPeriod.Id);
        }

        await _utilityBillPeriodRepository.SaveChangesAsync(CancellationToken.None);

        var utilityBills = new List<UtilityBill>
        {
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
        };

        foreach (var utilityBill in utilityBills)
        {
            var existingUtilityBill = await _utilityBillRepository.GetByIdAsync(utilityBill.Id, CancellationToken.None);

            if (existingUtilityBill != null)
            {
                _logger.LogInformation("Utility bill with ID {Id} already exists, skipping.", utilityBill.Id);
                continue;
            }

            await _utilityBillRepository.AddAsync(utilityBill, CancellationToken.None);

            _logger.LogInformation("Added utility bill with ID {Id}", utilityBill.Id);
        }

        await _utilityBillRepository.SaveChangesAsync(CancellationToken.None);

        _logger.LogInformation("Data seeding for Development completed");
    }
}