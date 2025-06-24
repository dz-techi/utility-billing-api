using MapsterMapper;
using MediatR;
using UtilityBilling.Application.Commands.UtilityBill;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Handlers.UtilityBill;

public class UpdateUtilityBillHandler : IRequestHandler<UpdateUtilityBillCommand, GetUtilityBillPeriodResult?>
{
    private readonly IMapper _mapper;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public UpdateUtilityBillHandler(
        IMapper mapper,
        IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _mapper = mapper;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }
    
    public async Task<GetUtilityBillPeriodResult?> Handle(UpdateUtilityBillCommand request, CancellationToken cancellationToken)
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

        utilityBill.Update(request.Usage, request.Cost);
        
        _utilityBillPeriodRepository.Update(utilityBillPeriod);
        await _utilityBillPeriodRepository.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriod);
    }
}