using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Results.UtilityType;

public class GetUtilityTypeResult
{
    public UtilityBillType Value { get; set; }
    public string Icon { get; set; } = null!;
    public MeasurementUnitType UnitMeasurementType { get; set; }

    public static GetUtilityTypeResult FromEnum(UtilityBillType utilityBillType)
    {
        return new GetUtilityTypeResult
        {
            Value = utilityBillType,
            Icon = GetIcon(utilityBillType),
            UnitMeasurementType = GetMeasurementUnit(utilityBillType)
        };
    }

    private static string GetIcon(UtilityBillType utilityBillType)
    {
        return utilityBillType switch
        {
            UtilityBillType.Electricity => "⚡",
            UtilityBillType.Water => "💧",
            UtilityBillType.Gas => "🔥",
            UtilityBillType.Sewer => "🚰",
            UtilityBillType.TrashAndRecycling => "🗑️",
            UtilityBillType.Heating => "🔥",
            UtilityBillType.Internet => "🌐",
            UtilityBillType.CableOrSatelliteTv => "📺",
            UtilityBillType.Telephone => "📞",
            UtilityBillType.SecuritySystem => "🔒",
            UtilityBillType.Maintenance => "🔧",
            UtilityBillType.RentalFee => "💰",
            UtilityBillType.DrinkingWater => "🚰",
            _ => "📋"
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