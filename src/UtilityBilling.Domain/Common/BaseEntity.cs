using System.ComponentModel.DataAnnotations;

namespace UtilityBilling.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    /*public DateTime CreatedDate { get; set; }
    
    public DateTime UpdatedDate { get; set; }*/

    /*protected BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }*/
}