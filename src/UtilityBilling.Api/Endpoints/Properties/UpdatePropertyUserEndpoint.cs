using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.Property;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class UpdatePropertyUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/properties/{propertyId}/users/{userId}", HandleUpdatePropertyUser)
            .WithName("UpdatePropertyUser")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleUpdatePropertyUser(
        Guid propertyId,
        Guid userId,
        [FromBody] UpdatePropertyUserRequest request,
        [FromServices] IPropertyUserRepository propertyUserRepository,
        CancellationToken cancellationToken)
    {
        // Find the property user assignment
        var propertyUser = await propertyUserRepository.GetByPropertyAndUserAsync(propertyId, userId, cancellationToken);
        if (propertyUser == null)
        {
            return Results.NotFound($"User assignment not found for property {propertyId} and user {userId}.");
        }

        // Update the role
        propertyUser.Role = request.Role;
        
        await propertyUserRepository.SaveChangesAsync(cancellationToken);
        
        return Results.Ok(new { 
            PropertyId = propertyId, 
            UserId = userId, 
            Role = request.Role 
        });
    }
} 