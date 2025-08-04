using Microsoft.EntityFrameworkCore;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public class PropertyUserRepository : BaseRepository<PropertyUser>, IPropertyUserRepository
{
    public PropertyUserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PropertyUser?> GetByPropertyAndUserAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.PropertyUsers
            .Include(pu => pu.User)
            .Include(pu => pu.Property)
            .FirstOrDefaultAsync(pu => pu.PropertyId == propertyId && pu.UserId == userId, cancellationToken);
    }

    public async Task<List<PropertyUser>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken)
    {
        return await _context.PropertyUsers
            .Include(pu => pu.User)
            .Include(pu => pu.Property)
            .Where(pu => pu.PropertyId == propertyId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.PropertyUsers
            .AnyAsync(pu => pu.PropertyId == propertyId && pu.UserId == userId, cancellationToken);
    }
} 