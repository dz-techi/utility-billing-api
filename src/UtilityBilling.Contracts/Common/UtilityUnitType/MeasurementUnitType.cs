using System.Text.Json.Serialization;

namespace UtilityBilling.Contracts.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<MeasurementUnitType>))]
public enum MeasurementUnitType
{
    None,
    KilowattHours,
    CubicMeters,
    Liters
}