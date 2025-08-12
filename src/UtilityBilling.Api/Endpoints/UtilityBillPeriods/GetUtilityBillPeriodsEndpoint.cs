using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class GetUtilityBillPeriodsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/properties/{propertyId:guid}/billing-periods", HandleGetUtilityBillPeriods)
            .WithName("GetUtilityBillPeriods")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleGetUtilityBillPeriods(
        Guid propertyId,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        [FromServices] ILogger<object> logger,
        CancellationToken cancellationToken)
    {
        // Hardcoded user id.
        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        logger.LogInformation("User ID: {UserId}", userId);
        logger.LogInformation("Property ID: {PropertyId}", propertyId);

        var utilityBillPeriods = await utilityBillPeriodRepository.GetAllByPropertyIdAsync(propertyId, cancellationToken);
        logger.LogInformation("Retrieved {Count} utility bill periods", utilityBillPeriods.Count);

        if (utilityBillPeriods.Count == 0)
        {
            logger.LogInformation("No utility bill periods found, returning empty array");
            return Results.Ok(new List<GetUtilityBillPeriodResult>());
        }

        var result = utilityBillPeriods
            .Select(GetUtilityBillPeriodResult.FromDto)
            .ToList();

        logger.LogInformation("Returning {Count} results", result.Count);
        return Results.Ok(result);
    }
}