using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.Property;
using UtilityBilling.Domain.Common;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class AddPropertyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/properties", HandleAddProperty)
            .WithName("AddProperty")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAddProperty(
        [FromBody] AddPropertyRequest request,
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        // Validate the request
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Results.BadRequest("Invalid property data.");
        }

        var userId = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");

        // Check if a property with the same name already exists for the user
        var existingProperty = await propertyRepository.GetByUserIdAndNameAsync(userId, request.Name, cancellationToken);
        if (existingProperty != null)
        {
            return Results.Conflict($"A property with the name '{request.Name}' already exists.");
        }

        // Create a new property
        var newProperty = new Property
        {
            Name = request.Name,
            PropertyType = request.PropertyType,
            Address = request.Address,
            OwnerId = userId,
            PropertyUsers = [
                new PropertyUser
                {
                    AssignedDate = DateTime.UtcNow,
                    UserId = userId,
                    Role = UserRole.Owner
                }
            ]
        };

        await propertyRepository.AddAsync(newProperty, cancellationToken);

        await propertyRepository.SaveChangesAsync(cancellationToken);

        return Results.Created($"/properties/{newProperty.Id}", newProperty.Id);
    }
}