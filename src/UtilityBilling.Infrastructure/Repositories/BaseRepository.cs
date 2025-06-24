using Microsoft.EntityFrameworkCore;
using UtilityBilling.Domain.Common;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<T> AddAsync(T entityDto, CancellationToken cancellationToken)
    {
        if (entityDto.Id == Guid.Empty)
        {
            entityDto.Id = Guid.NewGuid();
        }
        
        /*
        entityDto.CreatedDate = DateTime.UtcNow;
        */
        
        await _dbSet.AddAsync(entityDto, cancellationToken);

        return entityDto;
    }

    public void Update(T entityDto)
    {
        /*entityDto.UpdatedDate = DateTime.UtcNow;*/
        
        _dbSet.Update(entityDto);
    }
    
    public async Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}