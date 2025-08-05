using System.ComponentModel.DataAnnotations;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Contracts.Requests.Property;

public class UpdatePropertyUserRequest
{
    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public UserRole Role { get; set; }
} 