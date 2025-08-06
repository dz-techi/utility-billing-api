using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public class UtilityTypeRepository : BaseRepository<PropertyUtilityType>, IUtilityTypeRepository
{
    public UtilityTypeRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}