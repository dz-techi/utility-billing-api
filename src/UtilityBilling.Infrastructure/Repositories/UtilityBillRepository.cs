using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure.Repositories;

public class UtilityBillRepository : BaseRepository<UtilityBill>, IUtilityBillRepository
{
    public UtilityBillRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}