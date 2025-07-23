using UtilityBilling.Contracts.Common;

namespace UtilityBilling.Contracts.Results.Property;

public class PropertySelectItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public PropertyType PropertyType { get; set; }
}