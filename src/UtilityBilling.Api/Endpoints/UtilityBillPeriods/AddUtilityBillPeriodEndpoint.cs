using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.UtilityBillPeriod;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Domain.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public static class AddUtilityBillPeriodEndpoint
{
    public static void MapAddUtilityBillPeriodEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/utility-bill-periods", HandleAddUtilityBillPeriod)
            .WithName("AddUtilityBillPeriod")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleAddUtilityBillPeriod(
        [FromBody] AddUtilityBillPeriodRequest request,
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        // Hardcoded user id.
        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");

        var existingBillPeriod = await utilityBillPeriodRepository
            .FindExistingBillPeriodWithinDatesAsync(userId, request.StartDate, request.EndDate, cancellationToken);

        if (existingBillPeriod != null)
        {
            throw new EntityAlreadyExistsException($"Billing period between dates: {request.StartDate} - {request.EndDate} already exists");
        }

        var utilityBillPeriodDto = new Domain.UtilityBillPeriod.UtilityBillPeriod(userId, request.Name, request.StartDate, request.EndDate);

        await utilityBillPeriodRepository.AddAsync(utilityBillPeriodDto, cancellationToken);

        await utilityBillPeriodRepository.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriodDto);
        return Results.Ok(result);
    }
}