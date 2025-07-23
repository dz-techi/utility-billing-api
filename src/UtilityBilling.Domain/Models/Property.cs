using System.ComponentModel.DataAnnotations;
using UtilityBilling.Contracts.Common;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Domain.Models;

public class Property : BaseEntity
{
    public Guid OwnerId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public Address Address { get; set; } = null!;

    public PropertyType PropertyType { get; set; }
    
    public List<UtilityBillPeriod> UtilityBillPeriods { get; set; } = null!;
    
    public List<UtilityType> UtilityTypes { get; set; } = null!;
}