using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Requests.UtilityBill;

public class AddUtilityBillRequest
{
    public UtilityBillType UtilityBillType { get; set; }

    public decimal Usage { get; set; }
    
    public decimal Cost { get; set; }
}