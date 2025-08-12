using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Common.UtilityUnitType;
using UtilityBilling.Domain.Exceptions;

namespace UtilityBilling.Domain.Models;

public class UtilityBillPeriod : BaseEntity
{
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid PropertyId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public BillPeriodStatus Status { get; set; }

    public Property Property { get; set; } = null!;

    public List<UtilityBill> UtilityBills { get; set; } = [];

    public UtilityBillPeriod() { }

    public UtilityBillPeriod(Guid userId, Guid propertyId, string name, DateTime startDate, DateTime endDate)
    {
        UserId = userId;
        PropertyId = propertyId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void AddUtilityBill(UtilityBillType utilityBillType, decimal usage, decimal cost, MeasurementUnitType measurementUnitType)
    {
        var utilityBillAlreadyExists = UtilityBills.Any(ub => ub.UtilityBillType == utilityBillType);

        if (utilityBillAlreadyExists)
        {
            throw new EntityAlreadyExistsException($"Utility bill of type {utilityBillType} already exists for this utility bill period.");
        }

        var utilityBill = new UtilityBill(utilityBillType, usage, cost, measurementUnitType);

        UtilityBills.Add(utilityBill);
    }

    public void RemoveUtilityBill(Guid utilityBillId)
    {
        var utilityBill = UtilityBills.SingleOrDefault(ub => ub.Id == utilityBillId);

        if (utilityBill == null)
        {
            throw new EntityNotFoundException($"Utility bill with id {utilityBillId} not found.");
        }

        UtilityBills.Remove(utilityBill);
    }

    public UtilityBill? FindBillById(Guid utilityBillId)
    {
        return UtilityBills.SingleOrDefault(ub => ub.Id == utilityBillId);
    }
}