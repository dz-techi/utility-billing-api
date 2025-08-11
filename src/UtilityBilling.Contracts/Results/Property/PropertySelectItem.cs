using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;
using UtilityBilling.Domain.Models;

namespace UtilityBilling.Contracts.Results.Property;

public class PropertySelectItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public PropertyType PropertyType { get; set; }

    public List<PropertyUtilityTypeResult> UtilityTypes { get; set; } = new();

    public static PropertySelectItem FromPropertyDto(Domain.Models.Property property)
    {
        return new PropertySelectItem
        {
            Id = property.Id,
            Name = property.Name,
            PropertyType = property.PropertyType,
            UtilityTypes = property.UtilityTypes
                .Select(PropertyUtilityTypeResult.FromDto)
                .ToList()
        };
    }
}

public class PropertyUtilityTypeResult
{
    public bool HasUnitMeasurement { get; set; }
    
    public UtilityBillType UtilityBillType { get; set; }
    
    public MeasurementUnitType? UnitMeasurementType { get; set; }
    
    public static PropertyUtilityTypeResult FromDto(PropertyUtilityType utilityType)
    {
        return new PropertyUtilityTypeResult
        {
            HasUnitMeasurement = utilityType.HasUnitMeasurement,
            UtilityBillType = utilityType.UtilityBillType,
            UnitMeasurementType = utilityType.UnitMeasurementType
        };
    }
}