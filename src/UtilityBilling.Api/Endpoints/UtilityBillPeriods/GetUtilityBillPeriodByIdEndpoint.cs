using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public static class GetUtilityBillPeriodByIdEndpoint
{
    public static void MapGetUtilityBillPeriodByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/utility-bill-periods/{id}", HandleGetUtilityBillPeriodById)
            .WithName("GetUtilityBillPeriodById")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleGetUtilityBillPeriodById(
        Guid id,
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await utilityBillPeriodRepository.GetByIdAsync(id, cancellationToken);

        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Entity with id: {id} not found");
        }

        var result = mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriod);
        return Results.Ok(result);
    }
}