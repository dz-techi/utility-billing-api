using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public class GetUtilityBillPeriodsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("utility-bill-periods", HandleGetUtilityBillPeriods)
            .WithName("GetUtilityBillPeriods")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleGetUtilityBillPeriods(
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        [FromServices] ILogger<object> logger,
        CancellationToken cancellationToken)
    {
        // Hardcoded user id.
        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        logger.LogInformation("User ID: {UserId}", userId);

        var utilityBillPeriods = await utilityBillPeriodRepository.GetAllByUserIdAsync(userId, cancellationToken);
        logger.LogInformation("Retrieved {Count} utility bill periods", utilityBillPeriods.Count);

        if (utilityBillPeriods.Count == 0)
        {
            logger.LogInformation("No content returned");
            return Results.NoContent();
        }
        
        var result = utilityBillPeriods
            .Select(GetUtilityBillPeriodResult.FromDto)
            .ToList();
        
        logger.LogInformation("Returning {Count} results", result.Count);
        return Results.Ok(result);
    }
}