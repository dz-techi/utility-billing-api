using UtilityBilling.Contracts.Common.Enums;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Domain.Models;

public class UtilityType : BaseEntity
{
    public Guid PropertyId { get; set; }
    
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public bool Default { get; set; }
    
    public bool HasUnitMeasurement { get; set; }
    
    public MeasurementUnitType? UnitMeasurementType { get; set; }
    
    public Property Property { get; set; } = null!;
    
    public UtilityType()
    {
    }
}