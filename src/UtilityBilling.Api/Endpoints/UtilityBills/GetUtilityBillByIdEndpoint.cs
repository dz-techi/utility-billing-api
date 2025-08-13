using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityBill;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBills;

public class GetUtilityBillByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/utility-bills/{utilityBillId:guid}", HandleGetUtilityBillById)
            .WithName("GetUtilityBillById")
            .WithOpenApi()
            .Produces<GetUtilityBillResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleGetUtilityBillById(
        Guid utilityBillId,
        [FromServices] IUtilityBillRepository utilityBillRepository,
        CancellationToken cancellationToken)
    {
        var utilityBill = await utilityBillRepository.GetByIdAsync(utilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            return Results.NotFound($"Utility bill with ID {utilityBillId} not found.");
        }

        var result = GetUtilityBillResult.FromDto(utilityBill);

        return Results.Ok(result);
    }
}
