using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Contracts.Requests.Product;
using UtilityBilling.Contracts.Results.Product;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Api.Endpoints.Products;

public static class AddProductEndpoint
{
    public static void MapAddProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", HandleAddProduct)
            .WithName("AddProduct")
            .WithOpenApi();
    }

    private static async Task<IResult> HandleAddProduct(
        [FromBody] AddProductRequest request,
        [FromServices] IMapper mapper,
        [FromServices] IBaseRepository<ProductDto> productRepository,
        CancellationToken cancellationToken)
    {
        var productDto = new ProductDto
        {
            Name = request.Name
        };

        try
        {
            productDto = await productRepository.AddAsync(productDto, cancellationToken);

            if (productDto == null)
            {
                // TODO: Log error
                return Results.BadRequest();
            }

            var result = mapper.Map<GetProductResult>(productDto);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            // TODO: Log message
            return Results.BadRequest();
        }
    }
}