using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class RemoveUtilityBillPeriodEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("utility-bill-periods/{id}", HandleRemoveUtilityBillPeriod)
            .WithName("RemoveUtilityBillPeriod")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleRemoveUtilityBillPeriod(
        Guid id,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await utilityBillPeriodRepository.GetByIdAsync(id, cancellationToken);

        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {id} not found");
        }

        var result = await utilityBillPeriodRepository.RemoveAsync(utilityBillPeriod, cancellationToken);

        if (!result)
        {
            return Results.BadRequest();
        }

        return Results.NoContent();
    }
}