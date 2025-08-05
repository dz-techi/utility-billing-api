using System.ComponentModel.DataAnnotations;
using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Models;

namespace UtilityBilling.Contracts.Requests.Property;

public class AddPropertyRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public PropertyType PropertyType { get; set; }

    [Required]
    public Address Address { get; set; } = null!;
}