/*using UtilityBilling.Application.Commands.Product;
using UtilityBilling.Contracts.Results.Product;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace UtilityBilling.Application.Handlers.Product;

public class AddProductHandler : IRequestHandler<AddProductCommand, GetProductResult?>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public AddProductHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<GetProductResult?> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productDto = new ProductDto
        {
            Name = request.ProductRequest.Name
        };

        try
        {
            productDto = await _productRepository.AddAsync(productDto, cancellationToken);

            if (productDto == null)
            {
                // TODO: Log error

                return null;
            }
            
            return _mapper.Map<GetProductResult>(productDto);
        }
        catch (Exception e)
        {
            // TODO: Log message

            return null;
        }
    }
}*/