using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class RemovePropertyUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/properties/{propertyId}/users/{userId}", HandleRemovePropertyUser)
            .WithName("RemovePropertyUser")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleRemovePropertyUser(
        Guid propertyId,
        Guid userId,
        [FromServices] IPropertyUserRepository propertyUserRepository,
        CancellationToken cancellationToken)
    {
        // Find the property user assignment
        var propertyUser = await propertyUserRepository.GetByPropertyAndUserAsync(propertyId, userId, cancellationToken);
        if (propertyUser == null)
        {
            return Results.NotFound($"User assignment not found for property {propertyId} and user {userId}.");
        }

        // Remove the property user assignment
        await propertyUserRepository.RemoveAsync(propertyUser, cancellationToken);
        
        return Results.NoContent();
    }
} 