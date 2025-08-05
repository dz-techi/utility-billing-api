using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.UtilityBillPeriod;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class AddUtilityBillEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("utility-bill-periods/{id}/utility-bills", HandleAddUtilityBill)
            .WithName("AddUtilityBill")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleAddUtilityBill(
        Guid id,
        [FromBody] AddUtilityBillRequest request,
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await utilityBillPeriodRepository.GetByIdAsync(id, cancellationToken);

        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {id} not found");
        }

        var utilityBill = request;

        utilityBillPeriod.AddUtilityBill(utilityBill.UtilityBillType, utilityBill.Usage, utilityBill.Cost, utilityBill.MeasurementUnitType);

        await utilityBillPeriodRepository.AddAsync(utilityBillPeriod, cancellationToken);

        var result = GetUtilityBillPeriodResult.FromDto(utilityBillPeriod);
        
        return Results.Ok(result);
    }
}