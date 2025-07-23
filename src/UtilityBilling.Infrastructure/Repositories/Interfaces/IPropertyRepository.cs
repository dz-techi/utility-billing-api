using UtilityBilling.Domain.Models;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task<List<Property>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}