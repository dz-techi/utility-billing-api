using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Results.UtilityType;

public class GetUtilityTypeResult
{
    public UtilityBillType Value { get; set; }
    public MeasurementUnitType UnitMeasurementType { get; set; }

    public static GetUtilityTypeResult FromEnum(UtilityBillType utilityBillType)
    {
        return new GetUtilityTypeResult
        {
            Value = utilityBillType,
            UnitMeasurementType = GetMeasurementUnit(utilityBillType)
        };
    }

    private static MeasurementUnitType GetMeasurementUnit(UtilityBillType utilityBillType)
    {
        return utilityBillType switch
        {
            UtilityBillType.Electricity => MeasurementUnitType.KilowattHours,
            UtilityBillType.Water => MeasurementUnitType.CubicMeters,
            UtilityBillType.Gas => MeasurementUnitType.CubicMeters,
            UtilityBillType.Sewer => MeasurementUnitType.CubicMeters,
            UtilityBillType.DrinkingWater => MeasurementUnitType.Liters,
            _ => MeasurementUnitType.None
        };
    }
}