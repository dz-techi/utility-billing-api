using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBillPeriods;

public static class GetUtilityBillPeriodsEndpoint
{
    public static void MapGetUtilityBillPeriodsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/utility-bill-periods", HandleGetUtilityBillPeriods)
            .WithName("GetUtilityBillPeriods")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleGetUtilityBillPeriods(
        [FromServices] IMapper mapper,
        [FromServices] IUtilityBillPeriodRepository utilityBillPeriodRepository,
        [FromServices] ILogger<object> logger,
        CancellationToken cancellationToken)
    {
        // TEST BREAKPOINT - PUT BREAKPOINT ON NEXT LINE
        var test = "breakpoint test";

        // DEBUG TEST - This should show in console
        Console.WriteLine("=== DEBUG TEST - ENDPOINT CALLED ===");

        logger.LogInformation("=== BREAKPOINT SHOULD HIT HERE ===");
        logger.LogInformation("Getting utility bill periods.");

        // Hardcoded user id.
        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        logger.LogInformation("User ID: {UserId}", userId);

        var utilityBillPeriods = await utilityBillPeriodRepository.GetAllByUserIdAsync(userId, cancellationToken);
        logger.LogInformation("Retrieved {Count} utility bill periods", utilityBillPeriods.Count);

        var result = mapper.Map<List<GetUtilityBillPeriodResult>>(utilityBillPeriods);

        if (result.Count == 0)
        {
            logger.LogInformation("No content returned");
            return Results.NoContent();
        }

        logger.LogInformation("Returning {Count} results", result.Count);
        return Results.Ok(result);
    }
}