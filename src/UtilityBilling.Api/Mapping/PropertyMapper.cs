using UtilityBilling.Contracts.Results.Property;

namespace UtilityBilling.Api.Mapping;

public static class PropertyMapper
{
    public static PropertySelectItem ToSelectItem(this Domain.Models.Property property)
    {
        return new PropertySelectItem
        {
            Id = property.Id,
            Name = property.Name,
            PropertyType = property.PropertyType
        };
    }
}