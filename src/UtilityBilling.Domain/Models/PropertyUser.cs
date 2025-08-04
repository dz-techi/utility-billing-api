using System.ComponentModel.DataAnnotations;
using UtilityBilling.Contracts.Common;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Domain.Models;

public class PropertyUser : BaseEntity
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    public Property Property { get; set; } = null!;
    public User User { get; set; } = null!;
}