using Domain.Products;
using MediatR;

namespace Application.Products.GetProducts;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
{
	private readonly IProductRepository _productRepository;

	public GetProductQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		var products = await _productRepository.GetAll(cancellationToken);

		var response = products.Select(x => new ProductResponse(x.TenantId.Value, x.Id.Value, x.Name, x.Sku));

		return response;
	}
}
