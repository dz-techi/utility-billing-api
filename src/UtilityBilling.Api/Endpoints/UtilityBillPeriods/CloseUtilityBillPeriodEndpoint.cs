using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Domain.Common;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class CloseUtilityBillPeriodEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("utility-bill-periods/{id:guid}/close", HandleCloseUtilityBillPeriod)
            .WithName("CloseUtilityBillPeriodEndpoint")
            .WithOpenApi()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);;
    }

    private static async Task<IResult> HandleCloseUtilityBillPeriod(
        Guid id,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        CancellationToken cancellationToken)
    {
        var utilityBillPeriod = await utilityBillPeriodRepository.GetByIdAsync(id, cancellationToken);

        if (utilityBillPeriod == null)
        {
            return Results.NotFound($"Unable to find utility bill period with id: {id}");
        }

        utilityBillPeriod.Status = BillPeriodStatus.Closed;
        
        await utilityBillPeriodRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
}