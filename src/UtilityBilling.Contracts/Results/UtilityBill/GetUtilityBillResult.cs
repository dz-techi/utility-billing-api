using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Results.UtilityBill;

public class GetUtilityBillResult
{
    public Guid Id { get; set; }

    public UtilityBillType UtilityBillType { get; set; }

    public MeasurementUnitType MeasurementUnitType { get; set; }

    public decimal Usage { get; set; }

    public decimal Cost { get; set; }

    public bool Paid { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public static GetUtilityBillResult FromDto(Domain.Models.UtilityBill utilityBill)
    {
        return new GetUtilityBillResult
        {
            Id = utilityBill.Id,
            UtilityBillType = utilityBill.UtilityBillType,
            MeasurementUnitType = utilityBill.MeasurementUnitType,
            Usage = utilityBill.Usage,
            Cost = utilityBill.Cost,
            Paid = utilityBill.Paid,
        };
    }
}