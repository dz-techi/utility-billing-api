using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.UtilityBillPeriod;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public static class UpdateUtilityBillEndpoint
{
    public static void MapUpdateUtilityBillEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/utility-bill-periods/{id}/utility-bills/{utilityBillId}", HandleUpdateUtilityBill)
            .WithName("UpdateUtilityBill")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleUpdateUtilityBill(
        Guid id,
        Guid utilityBillId,
        [FromBody] UpdateUtilityBillRequest request,
        [FromServices] IMapper mapper,
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

        utilityBill.Update(request.Usage, request.Cost);

        utilityBillPeriodRepository.Update(utilityBillPeriod);
        await utilityBillPeriodRepository.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<GetUtilityBillPeriodResult>(utilityBillPeriod);
        return Results.Ok(result);
    }
}