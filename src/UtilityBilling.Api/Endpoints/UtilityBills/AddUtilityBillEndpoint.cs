using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Application.Services;
using UtilityBilling.Contracts.Requests.UtilityBill;
using UtilityBilling.Contracts.Results.UtilityBill;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.UtilityBills;

public class AddUtilityBillEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/utility-bill-periods/{utilityBillPeriodId:guid}/bills", HandleAddUtilityBill)
            .WithName("AddUtilityBill")
            .WithOpenApi()
            .Produces<AddUtilityBillResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
    
    private static async Task<IResult> HandleAddUtilityBill(
        Guid utilityBillPeriodId,
        [FromBody] AddUtilityBillRequest request,
        [FromServices] IUtilityBillRepository utilityBillRepository,
        [FromServices] IUtilityTypeMappingService utilityTypeMappingService,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return Results.BadRequest("Request body cannot be null.");
        }

        var utilityBill = new UtilityBill
        {
            UtilityBillPeriodId = utilityBillPeriodId,
            UtilityBillType = request.UtilityBillType,
            Usage = request.Usage,
            Cost = request.Cost,
            MeasurementUnitType = utilityTypeMappingService.GetMeasurementUnitType(request.UtilityBillType),
        };

        await utilityBillRepository.AddAsync(utilityBill, cancellationToken);
        
        await utilityBillRepository.SaveChangesAsync(cancellationToken);

        var result = new AddUtilityBillResult
        {
            Id = utilityBill.Id
        };

        return Results.Ok(result);
    }
}