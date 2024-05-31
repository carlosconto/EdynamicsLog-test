using Domain.Products;
using MediatR;

namespace Application.Products.GetProductById;

internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
	private readonly IProductRepository _productRepository;

	public GetProductByIdQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
	{
		var productDb = await _productRepository.GetById(new ProductId(request.Id), cancellationToken);

		return new ProductResponse(productDb.TenantId.Value, productDb.Id.Value, productDb.Name, productDb.Sku);
	}
}