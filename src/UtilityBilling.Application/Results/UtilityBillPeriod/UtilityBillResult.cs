using UtilityBilling.Contracts.Common.Enums;
using UtilityBilling.Contracts.Common.UtilityUnitType;

namespace UtilityBilling.Application.Results.UtilityBillPeriod;

public class UtilityBillResult
{
    public Guid Id { get; set; }
    
    public UtilityBillType UtilityBillType { get; set; }

    public decimal Usage { get; set; }
    
    public decimal Cost { get; set; }

    public MeasurementUnitType MeasurementUnitType { get; set; }
}