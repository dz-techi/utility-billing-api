using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UtilityBilling.Contracts.Requests.UtilityBillPeriod;

public class AddUtilityBillPeriodRequest
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [Required]
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [Required]
    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [Required]
    [JsonPropertyName("propertyId")]
    public Guid PropertyId { get; set; }
}