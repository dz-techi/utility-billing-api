using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Results.Product;
using UtilityBilling.Domain.Exceptions;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Products;

public static class GetProductEndpoint
{
    public static void MapGetProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{id:guid}", HandleGetProduct)
            .WithName("GetProduct")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleGetProduct(
        Guid id,
        [FromServices] IMapper mapper,
        [FromServices] IBaseRepository<ProductDto> productRepository,
        CancellationToken cancellationToken)
    {
        var productDto = await productRepository.GetByIdAsync(id, cancellationToken);

        if (productDto == null)
        {
            throw new EntityNotFoundException($"Entity with id: {id} not found");
        }

        var result = mapper.Map<GetProductResult>(productDto);
        return Results.Ok(result);
    }
}