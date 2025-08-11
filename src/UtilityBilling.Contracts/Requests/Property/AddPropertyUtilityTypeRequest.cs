using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Contracts.Requests.Property;

public class AddPropertyUtilityTypeRequest
{
    public UtilityBillType UtilityBillType { get; set; }
}