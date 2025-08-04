using UtilityBilling.Contracts.Common;

namespace UtilityBilling.Application.Results.Property;

public class PropertySelectItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public PropertyType PropertyType { get; set; }

    public static PropertySelectItem FromPropertyDto(Domain.Models.Property property)
    {
        return new PropertySelectItem
        {
            Id = property.Id,
            Name = property.Name,
            PropertyType = property.PropertyType
        };
    }
}