using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.UtilityType;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityTypes;

public class GetPropertyUtilityTypesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/properties/{propertyId}/utility-types", HandleGetPropertyUtilityTypes)
            .WithName("GetPropertyUtilityTypes")
            .Produces<IEnumerable<GetUtilityTypeResult>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleGetPropertyUtilityTypes(
        Guid propertyId,
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        // Validate that property exists
        var property = await propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        if (property == null)
        {
            return Results.NotFound($"Property with ID {propertyId} not found.");
        }

        // Map the utility types to the result format
        var utilityTypes = property.UtilityTypes
            .Select(ut => GetUtilityTypeResult.FromEnum(ut.UtilityBillType))
            .ToList();

        return Results.Ok(utilityTypes);
    }
}