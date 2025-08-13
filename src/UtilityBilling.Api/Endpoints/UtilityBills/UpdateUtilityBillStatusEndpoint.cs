using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBills;

public class UpdateUtilityBillStatusEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/utility-bills/{utilityBillId:guid}/status", HandleUpdateUtilityBillStatus)
            .WithName("UpdateUtilityBillStatus")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleUpdateUtilityBillStatus(
        Guid utilityBillId,
        [FromBody] UpdateUtilityBillStatusRequest request,
        [FromServices] IUtilityBillRepository utilityBillRepository,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return Results.BadRequest("Request body cannot be null.");
        }

        var utilityBill = await utilityBillRepository.GetByIdAsync(utilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            return Results.NotFound($"Utility bill with ID {utilityBillId} not found.");
        }

        utilityBill.Paid = request.Paid;

        await utilityBillRepository.UpdateAsync(utilityBill, cancellationToken);
        await utilityBillRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
}

public class UpdateUtilityBillStatusRequest
{
    public bool Paid { get; set; }
}
