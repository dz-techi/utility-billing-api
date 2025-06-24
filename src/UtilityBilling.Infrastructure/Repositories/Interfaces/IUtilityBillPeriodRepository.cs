using UtilityBilling.Domain.UtilityBillPeriod;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IUtilityBillPeriodRepository : IBaseRepository<UtilityBillPeriod>
{
    Task<UtilityBillPeriod?> FindExistingBillPeriodWithinDatesAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    
    Task<IList<UtilityBillPeriod>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}