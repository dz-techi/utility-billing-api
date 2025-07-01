using UtilityBilling.Contracts.Common;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Domain.Models;

public class Property : BaseEntity
{
    public Guid OwnerId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Address { get; set; } = null!;

    public PropertyType PropertyType { get; set; }
    
    public List<UtilityBillPeriod> UtilityBillPeriods { get; set; } = null!;
    
    public List<UtilityType> UtilityTypes { get; set; } = null!;
}