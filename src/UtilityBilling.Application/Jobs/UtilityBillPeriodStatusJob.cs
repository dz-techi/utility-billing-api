using Quartz;
using UtilityBilling.Domain.Common;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Jobs;

public class UtilityBillPeriodStatusJob : IJob
{
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public UtilityBillPeriodStatusJob(IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var today = DateTime.UtcNow.Date;
        
        // Expire old active periods
        var activePeriods = await _utilityBillPeriodRepository
            .GetPeriodsByStatusAsync(BillPeriodStatus.Active, context.CancellationToken);

        var overduePeriods = activePeriods
            .Where(p => p.EndDate < today);

        foreach (var period in overduePeriods)
        {
            period.Status = BillPeriodStatus.Overdue;
        }
        
        await _utilityBillPeriodRepository.SaveChangesAsync(context.CancellationToken);


        // Activate upcoming periods
        var upcomingPeriods = await _utilityBillPeriodRepository
            .GetPeriodsByStatusAsync(BillPeriodStatus.Upcoming, context.CancellationToken);

        var periodsToActivate = upcomingPeriods
            .Where(p => p.StartDate <= today);

        foreach (var period in periodsToActivate)
        {
            period.Status = BillPeriodStatus.Active;
        }
        
        await _utilityBillPeriodRepository.SaveChangesAsync(context.CancellationToken);
    }
}