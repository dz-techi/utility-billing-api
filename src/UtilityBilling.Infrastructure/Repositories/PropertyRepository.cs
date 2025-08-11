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

    public new async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Properties
            .Where(p => p.Id == id)
            .Include(p => p.UtilityTypes)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Property>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Properties
            .AsNoTracking()
            .Where(p => p.OwnerId == userId)
            .Include(p => p.Address)
            .Include(p => p.UtilityTypes)
            .Include(p => p.PropertyUsers)
            .ThenInclude(pu => pu.User)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Property?> GetByUserIdAndNameAsync(Guid userId, string name, CancellationToken cancellationToken)
    {
        return await _context.Properties
            .AsNoTracking()
            .Where(p => p.OwnerId == userId && p.Name == name)
            .SingleOrDefaultAsync(cancellationToken);
    }
}