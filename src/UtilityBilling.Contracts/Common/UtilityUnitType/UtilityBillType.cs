using System.Text.Json.Serialization;

namespace UtilityBilling.Contracts.Common.UtilityUnitType;

[JsonConverter(typeof(JsonStringEnumConverter<UtilityBillType>))]
public enum UtilityBillType
{
    Electricity,
    Water,
    Gas,
    Sewer,
    TrashAndRecycling,
    Heating,
    Internet,
    CableOrSatelliteTv,
    Telephone,
    SecuritySystem,
    Maintenance,
    RentalFee,
    DrinkingWater,
}