using UtilityBilling.Domain.Models;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task<List<Property>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<Property?> GetByUserIdAndNameAsync(Guid userId, string name, CancellationToken cancellationToken);
}