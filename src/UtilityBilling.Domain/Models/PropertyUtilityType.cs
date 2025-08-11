using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Domain.Models;

public class PropertyUtilityType : BaseEntity
{
    public Guid PropertyId { get; set; }

    public bool HasUnitMeasurement { get; set; }

    public MeasurementUnitType? UnitMeasurementType { get; set; }

    public UtilityBillType UtilityBillType { get; set; }

    public Property Property { get; set; } = null!;

    public PropertyUtilityType()
    {
    }
}