using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.Property;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class UpdatePropertyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/properties/{propertyId}", HandleUpdateProperty)
            .WithName("UpdateProperty")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleUpdateProperty(
        Guid propertyId,
        [FromBody] UpdatePropertyRequest request,
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        var property = await propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        if (property == null)
        {
            return Results.NotFound($"Property with ID {propertyId} not found.");
        }

        // Update the property
        property.Name = request.Name;
        property.PropertyType = request.PropertyType;
        property.Address = request.Address;

        await propertyRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new
        {
            PropertyId = propertyId,
            request.Name,
            request.PropertyType
        });
    }
}