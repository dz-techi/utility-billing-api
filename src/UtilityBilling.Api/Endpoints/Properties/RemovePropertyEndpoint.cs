using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class RemovePropertyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/properties/{propertyId}", HandleRemoveProperty)
            .WithName("RemoveProperty")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleRemoveProperty(
        Guid propertyId,
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        var property = await propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        
        if (property == null)
        {
            return Results.NotFound($"Property with ID {propertyId} not found.");
        }

        await propertyRepository.RemoveAsync(property, cancellationToken);
        
        return Results.NoContent();
    }
}