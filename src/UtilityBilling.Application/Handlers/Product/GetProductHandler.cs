/*using UtilityBilling.Application.Queries.Product;
using UtilityBilling.Contracts.Results.Product;
using UtilityBilling.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;
using UtilityBilling.Domain.Exceptions;

namespace UtilityBilling.Application.Handlers.Product;

public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResult>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productDto = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (productDto == null)
        {
            throw new EntityNotFoundException($"Entity with id: {request.ProductId} not found");
        }

        return _mapper.Map<GetProductResult>(productDto);
    }
}*/