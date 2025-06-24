using MapsterMapper;
using MediatR;
using UtilityBilling.Application.Commands.UtilityBill;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Handlers.UtilityBill;

public class AddUtilityBillHandler : IRequestHandler<AddUtilityBillCommand, GetUtilityBillPeriodResult?>
{
    private readonly IMapper _mapper;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public AddUtilityBillHandler(
        IMapper mapper,
        IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _mapper = mapper;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }
    
    public async Task<GetUtilityBillPeriodResult?> Handle(AddUtilityBillCommand request, CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await _utilityBillPeriodRepository.GetByIdAsync(request.UtilityBillPeriodId, cancellationToken);
        
        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {request.UtilityBillPeriodId} not found");
        }

        var utilityBill = request.UtilityBill;
        
        utilityBillPeriod.AddUtilityBill(utilityBill.UtilityBillType, utilityBill.Usage, utilityBill.Cost, utilityBill.MeasurementUnitType);

        await _utilityBillPeriodRepository.AddAsync(utilityBillPeriod, cancellationToken);
        
        return _mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriod);
    }
}