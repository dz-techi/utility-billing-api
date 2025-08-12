using UtilityBilling.Contracts.Results.UtilityBill;
using UtilityBilling.Domain.Common;

namespace UtilityBilling.Contracts.Results.UtilityBillPeriod;

public class GetUtilityBillPeriodResult
{
    public Guid Id { get; set; }

    public int BillCount { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public BillPeriodStatus Status { get; set; }
    
    public List<GetUtilityBillResult> UtilityBills { get; set; } = [];

    public static GetUtilityBillPeriodResult FromDto(Domain.Models.UtilityBillPeriod utilityBillPeriod)
    {
        return new GetUtilityBillPeriodResult
        {
            Id = utilityBillPeriod.Id,
            BillCount = utilityBillPeriod.UtilityBills.Count,
            Name = utilityBillPeriod.Name,
            StartDate = utilityBillPeriod.StartDate,
            EndDate = utilityBillPeriod.EndDate,
            Status = utilityBillPeriod.Status,
            UtilityBills = utilityBillPeriod.UtilityBills.Select(GetUtilityBillResult.FromDto).ToList()
        };
    }
}