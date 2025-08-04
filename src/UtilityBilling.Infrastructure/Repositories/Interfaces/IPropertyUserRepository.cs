using UtilityBilling.Domain.Models;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IPropertyUserRepository : IBaseRepository<PropertyUser>
{
    Task<PropertyUser?> GetByPropertyAndUserAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken);
    Task<List<PropertyUser>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken);
} 