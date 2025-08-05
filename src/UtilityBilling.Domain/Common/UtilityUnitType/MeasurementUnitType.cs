using System.Text.Json.Serialization;

namespace UtilityBilling.Domain.Common.UtilityUnitType;

[JsonConverter(typeof(JsonStringEnumConverter<MeasurementUnitType>))]
public enum MeasurementUnitType
{
    None,
    KilowattHours,
    CubicMeters,
    Liters
}