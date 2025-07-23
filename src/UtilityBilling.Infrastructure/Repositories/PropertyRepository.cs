using Microsoft.EntityFrameworkCore;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
{
    public PropertyRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Property>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Properties
            .AsNoTracking()
            .Where(p => p.OwnerId == userId)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }
}