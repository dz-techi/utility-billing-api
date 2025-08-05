using System.ComponentModel.DataAnnotations;
using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Domain.Models;

public class UtilityType : BaseEntity
{
    public Guid PropertyId { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = null!;
    
    public bool Default { get; set; }
    
    public bool HasUnitMeasurement { get; set; }
    
    public MeasurementUnitType? UnitMeasurementType { get; set; }
    
    public UtilityBillType UtilityBillType { get; set; }
    
    public Property Property { get; set; } = null!;
    
    public UtilityType()
    {
    }
}