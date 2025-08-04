using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.Property;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class AddPropertyUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/properties/{propertyId}/users", HandleAddPropertyUser)
            .WithName("AddPropertyUser")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAddPropertyUser(
        Guid propertyId,
        [FromBody] AddPropertyUserRequest request,
        [FromServices] IPropertyUserRepository propertyUserRepository,
        [FromServices] IUserRepository userRepository,
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        // Validate that property exists
        var property = await propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        if (property == null)
        {
            return Results.NotFound($"Property with ID {propertyId} not found.");
        }

        // Find user by email, or create if doesn't exist
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null)
        {
            // Create new user with the provided email
            user = new User
            {
                FirstName = request.FirstName ?? "User",
                LastName = request.LastName ?? "",
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedDate = DateTime.UtcNow
            };

            await userRepository.AddAsync(user, cancellationToken);
        }

        await userRepository.SaveChangesAsync(cancellationToken);

        // Check if user is already assigned to this property
        var existingAssignment = await propertyUserRepository.ExistsAsync(propertyId, user.Id, cancellationToken);
        if (existingAssignment)
        {
            return Results.Conflict($"User {request.Email} is already assigned to this property.");
        }

        // Create new property user assignment
        var propertyUser = new PropertyUser
        {
            PropertyId = propertyId,
            UserId = user.Id,
            Role = request.Role,
            AssignedDate = DateTime.UtcNow
        };

        await propertyUserRepository.AddAsync(propertyUser, cancellationToken);

        await propertyUserRepository.SaveChangesAsync(cancellationToken);
        
        return Results.Created($"/properties/{propertyId}/users/{user}", new
        {
            PropertyId = propertyId,
            UserId = user.Id,
            Role = request.Role
        });
    }
}