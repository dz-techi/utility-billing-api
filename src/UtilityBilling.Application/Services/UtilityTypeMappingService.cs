using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Application.Services;

public interface IUtilityTypeMappingService
{
    MeasurementUnitType GetMeasurementUnitType(UtilityBillType utilityBillType);
}

public class UtilityTypeMappingService : IUtilityTypeMappingService
{
    public MeasurementUnitType GetMeasurementUnitType(UtilityBillType utilityBillType)
    {
        return utilityBillType switch
        {
            UtilityBillType.Electricity => MeasurementUnitType.KilowattHours,
            UtilityBillType.Water => MeasurementUnitType.CubicMeters,
            UtilityBillType.Gas => MeasurementUnitType.CubicMeters,
            UtilityBillType.Sewer => MeasurementUnitType.CubicMeters,
            UtilityBillType.TrashAndRecycling => MeasurementUnitType.None,
            UtilityBillType.Heating => MeasurementUnitType.KilowattHours,
            UtilityBillType.Internet => MeasurementUnitType.None,
            UtilityBillType.CableOrSatelliteTv => MeasurementUnitType.None,
            UtilityBillType.Telephone => MeasurementUnitType.None,
            UtilityBillType.SecuritySystem => MeasurementUnitType.None,
            UtilityBillType.Maintenance => MeasurementUnitType.None,
            UtilityBillType.RentalFee => MeasurementUnitType.None,
            UtilityBillType.DrinkingWater => MeasurementUnitType.Liters,
            _ => throw new ArgumentOutOfRangeException(nameof(utilityBillType), "Unsupported utility bill type")
        };
    }
}
