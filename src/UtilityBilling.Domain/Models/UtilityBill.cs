using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Domain.Models;

public class UtilityBill : BaseEntity
{
    public UtilityBillType UtilityBillType { get; set; }
    
    public bool Paid { get; set; }

    public decimal Usage { get; set; }

    public decimal Cost { get; set; }

    public MeasurementUnitType MeasurementUnitType { get; set; }
    
    public Guid UtilityBillPeriodId { get; set; }
    
    public UtilityBillPeriod UtilityBillPeriod { get; set; }
    
    public UtilityBill()
    {
    }
    
    public UtilityBill(UtilityBillType utilityBillType, decimal usage, decimal cost, MeasurementUnitType measurementUnitType)
    {
        UtilityBillType = utilityBillType;
        Usage = usage;
        Cost = cost;
        MeasurementUnitType = measurementUnitType;
    }
    
    public void Update(decimal usage, decimal cost)
    {
        Usage = usage;
        Cost = cost;
    }
}