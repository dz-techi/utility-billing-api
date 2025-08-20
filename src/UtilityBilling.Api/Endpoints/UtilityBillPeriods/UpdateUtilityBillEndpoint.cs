using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class UpdateUtilityBillEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("utility-bill-periods/{id}/utility-bills/{utilityBillId}", HandleUpdateUtilityBill)
            .WithName("UpdateUtilityBill")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleUpdateUtilityBill(
        Guid id,
        Guid utilityBillId,
        [FromBody] UpdateUtilityBillRequest request,
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillRepository utilityBillRepository,
        CancellationToken cancellationToken)
    {
        var utilityBill = await utilityBillRepository.GetByIdAsync(utilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            throw new EntityNotFoundException($"Utility bill with id: {utilityBillId} within utility bill period with id: {id} not found");
        }

        utilityBill.Update(request.Usage, request.Cost);

        await utilityBillRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
}