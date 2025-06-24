using MapsterMapper;
using MediatR;
using UtilityBilling.Application.Commands.UtilityBill;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Handlers.UtilityBill;

public class RemoveUtilityBillHandler : IRequestHandler<RemoveUtilityBillCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public RemoveUtilityBillHandler(
        IMapper mapper,
        IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _mapper = mapper;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }
    
    public async Task<bool> Handle(RemoveUtilityBillCommand request, CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await _utilityBillPeriodRepository.GetByIdAsync(request.UtilityBillPeriodId, cancellationToken);
        
        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {request.UtilityBillPeriodId} not found");
        }
        
        var utilityBill = utilityBillPeriod.FindBillById(request.UtilityBillId);

        if (utilityBill == null)
        {
            throw new EntityNotFoundException($"Utility bill with id: {request.UtilityBillId} within utility bill period with id: {request.UtilityBillPeriodId} not found");
        }

        utilityBillPeriod.RemoveUtilityBill(utilityBill.Id);
        
        /*
        await _utilityBillPeriodRepository.UpsertAsync(utilityBillPeriod, cancellationToken);
        */

        return true;
    }
}