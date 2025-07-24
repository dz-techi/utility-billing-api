using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class RemoveUtilityBillEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("utility-bill-periods/{id}/utility-bills/{utilityBillId}", HandleRemoveUtilityBill)
            .WithName("RemoveUtilityBill")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleRemoveUtilityBill(
        Guid id,
        Guid utilityBillId,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await utilityBillPeriodRepository.GetByIdAsync(id, cancellationToken);

        if (utilityBillPeriod == null)
        {
            throw new EntityNotFoundException($"Utility bill period with id: {id} not found");
        }

        var utilityBill = utilityBillPeriod.FindBillById(utilityBillId);

        if (utilityBill == null)
        {
            throw new EntityNotFoundException($"Utility bill with id: {utilityBillId} within utility bill period with id: {id} not found");
        }

        utilityBillPeriod.RemoveUtilityBill(utilityBill.Id);

        /*
        await utilityBillPeriodRepository.UpsertAsync(utilityBillPeriod, cancellationToken);
        */

        return Results.NoContent();
    }
}