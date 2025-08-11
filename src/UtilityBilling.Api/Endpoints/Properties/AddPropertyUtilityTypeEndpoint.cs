using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Application.Services;
using UtilityBilling.Contracts.Requests.Property;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Properties;

public class AddPropertyUtilityTypeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.Map("/properties/{propertyId}/utility-types", HandleAddPropertyUtilityType)
            .WithName("AddPropertyUtilityType")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAddPropertyUtilityType(
        Guid propertyId,
        [FromBody] AddPropertyUtilityTypeRequest request,
        [FromServices] IPropertyRepository propertyRepository,
        [FromServices] IUtilityTypeMappingService utilityTypeMappingService,
        CancellationToken cancellationToken)
    {
        // Validate that property exists
        var property = await propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        if (property == null)
        {
            return Results.NotFound($"Property with ID {propertyId} not found.");
        }

        // Check if the utility type is already assigned to the property
        if (property.UtilityTypes.Any(ut => ut.UtilityBillType == request.UtilityBillType))
        {
            return Results.Conflict($"Utility type {request.UtilityBillType} is already assigned to this property.");
        }

        var unitMeasureType = utilityTypeMappingService.GetMeasurementUnitType(request.UtilityBillType);

        // Add the utility type to the property
        property.UtilityTypes.Add(new PropertyUtilityType
        {
            UtilityBillType = request.UtilityBillType,
            UnitMeasurementType = unitMeasureType
        });

        await propertyRepository.SaveChangesAsync(cancellationToken);

        return Results.Ok(new
        {
            PropertyId = propertyId
        });
    }
}