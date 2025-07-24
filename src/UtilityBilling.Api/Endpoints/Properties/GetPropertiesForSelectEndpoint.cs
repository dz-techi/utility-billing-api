using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Api.Mapping;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class GetPropertiesForSelectEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/properties/select", HandleGetPropertiesForSelect)
            .WithName("GetPropertiesForSelect")
            .Produces<IEnumerable<object>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleGetPropertiesForSelect(
        [FromServices] IPropertyRepository propertyRepository,
        CancellationToken cancellationToken)
    {
        var user1Id = new Guid("99d5d2cf-93e1-4300-ac09-39849738d744");
        
        var properties = await propertyRepository.GetAllByUserIdAsync(user1Id, cancellationToken);
        
        if (properties.Count == 0)
        {
            return Results.NotFound("No properties found for the user.");
        }

        var result = properties.Select(p => p.ToSelectItem())
            .ToList();
        
        return Results.Ok(result);
    }
}