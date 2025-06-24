using MediatR;
using UtilityBilling.Application.Commands.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Handlers.UtilityBillPeriod;

public class RemoveUtilityBillPeriodHandler : IRequestHandler<RemoveUtilityBillPeriodCommand, bool>
{
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public RemoveUtilityBillPeriodHandler(IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }

    public async Task<bool> Handle(RemoveUtilityBillPeriodCommand request, CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await _utilityBillPeriodRepository.GetByIdAsync(request.UtilityBillPeriodId, cancellationToken);
        
        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {request.UtilityBillPeriodId} not found");
        }
        
        return await _utilityBillPeriodRepository.RemoveAsync(utilityBillPeriod, cancellationToken);
    }
}