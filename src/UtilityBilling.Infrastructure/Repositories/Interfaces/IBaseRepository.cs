using UtilityBilling.Domain.Common;

namespace UtilityBilling.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    void Update(T entityDto);
    
    Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}