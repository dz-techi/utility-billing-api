using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Results.UtilityBill;

public class GetUtilityBillResult
{
    public Guid Id { get; set; }

    public UtilityBillType Type { get; set; }

    public MeasurementUnitType Unit { get; set; }
    
    public decimal Amount { get; set; }
    
    public bool Status { get; set; }
    
    public decimal? Consumption { get; set; }
    
    public static GetUtilityBillResult FromDto(Domain.Models.UtilityBill utilityBill)
    {
        return new GetUtilityBillResult
        {
            Id = utilityBill.Id,
            Type = utilityBill.UtilityBillType,
            Unit = utilityBill.MeasurementUnitType,
            Amount = utilityBill.Cost,
            Status = utilityBill.Paid,
            Consumption = utilityBill.Usage
        };
    }
}