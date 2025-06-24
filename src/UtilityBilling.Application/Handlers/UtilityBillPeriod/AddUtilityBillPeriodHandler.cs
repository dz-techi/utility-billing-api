using MapsterMapper;
using MediatR;
using UtilityBilling.Application.Commands.UtilityBillPeriod;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Domain.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Application.Handlers.UtilityBillPeriod;

public class AddUtilityBillPeriodHandler : IRequestHandler<AddUtilityBillPeriodCommand, GetUtilityBillPeriodResult?>
{
    private readonly IMapper _mapper;
    private readonly IUtilityBillPeriodRepository _utilityBillPeriodRepository;

    public AddUtilityBillPeriodHandler(
        IMapper mapper,
        IUtilityBillPeriodRepository utilityBillPeriodRepository)
    {
        _mapper = mapper;
        _utilityBillPeriodRepository = utilityBillPeriodRepository;
    }

    public async Task<GetUtilityBillPeriodResult?> Handle(AddUtilityBillPeriodCommand request, CancellationToken cancellationToken)
    {
        // Hardcoded user id.
        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        
        var existingBillPeriod = await _utilityBillPeriodRepository
            .FindExistingBillPeriodWithinDatesAsync(userId, request.StartDate, request.EndDate, cancellationToken);

        if (existingBillPeriod != null)
        {
            throw new EntityAlreadyExistsException($"Billing period between dates: {request.StartDate} - {request.EndDate} already exists");
        }

        var utilityBillPeriodDto = new Domain.UtilityBillPeriod.UtilityBillPeriod(userId, request.Name, request.StartDate, request.EndDate);

        await _utilityBillPeriodRepository.AddAsync(utilityBillPeriodDto, cancellationToken);
        
        await _utilityBillPeriodRepository.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriodDto);
    }
}