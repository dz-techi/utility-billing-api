using System.ComponentModel.DataAnnotations;

namespace UtilityBilling.Contracts.Requests.UtilityBillPeriod;

public class AddUtilityBillPeriodRequest
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
}