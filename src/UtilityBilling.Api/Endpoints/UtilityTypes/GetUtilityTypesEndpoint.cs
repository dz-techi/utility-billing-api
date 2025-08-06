using UtilityBilling.Contracts.Results.UtilityType;
using UtilityBilling.Domain.Common.UtilityUnitType;

namespace UtilityBilling.Api.Endpoints.UtilityTypes;

public class GetUtilityTypesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/utility-types", HandleGetUtilityTypes)
            .WithName("GetUtilityTypes")
            .Produces<IEnumerable<GetUtilityTypeResult>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static Task<IResult> HandleGetUtilityTypes(CancellationToken cancellationToken)
    {
        var utilityTypes = Enum.GetValues<UtilityBillType>()
            .Select(GetUtilityTypeResult.FromEnum)
            .ToList();

        return Task.FromResult(Results.Ok(utilityTypes));
    }
}