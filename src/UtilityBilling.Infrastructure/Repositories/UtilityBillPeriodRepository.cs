using Microsoft.EntityFrameworkCore;
using UtilityBilling.Domain.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public class UtilityBillPeriodRepository : BaseRepository<UtilityBillPeriod>, IUtilityBillPeriodRepository
{

    public UtilityBillPeriodRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<UtilityBillPeriod?> FindExistingBillPeriodWithinDatesAsync(
        Guid userId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken)
    {
        return await _context.UtilityBillPeriods
            .Where(u => u.UserId == userId)
            .Where(u => (u.StartDate <= startDate && u.EndDate >= startDate) || (u.StartDate <= endDate && u.EndDate >= endDate))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IList<UtilityBillPeriod>> GetAllByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.UtilityBillPeriods.AsNoTracking()
            .Where(u => u.UserId == userId)
            .OrderByDescending(u => u.StartDate)
            .ToListAsync(cancellationToken);
    }
}