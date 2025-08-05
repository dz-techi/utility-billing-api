using System.ComponentModel.DataAnnotations;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Contracts.Requests.Property;

public class AddPropertyUserRequest
{
    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Required]
    public UserRole Role { get; set; }
}