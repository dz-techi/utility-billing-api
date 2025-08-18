using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Models;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IUtilityBillPeriodRepository : IBaseRepository<UtilityBillPeriod>
{
    Task<UtilityBillPeriod?> FindExistingBillPeriodWithinDatesAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    
    Task<IList<UtilityBillPeriod>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<IList<UtilityBillPeriod>> GetAllByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
    
    Task<IList<UtilityBillPeriod>> GetPeriodsByStatusAsync(BillPeriodStatus status, CancellationToken cancellationToken);
}