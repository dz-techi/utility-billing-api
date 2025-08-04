using UtilityBilling.Contracts.Common;

namespace UtilityBilling.Application.Results.UtilityBillPeriod;

public class GetUtilityBillPeriodResult
{
    public Guid Id { get; set; }

    public int BillCount { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public BillPeriodStatus Status { get; set; }
}