using Domain.Products;
using MediatR;

namespace Application.Products.CreateProduct;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
	private readonly IProductRepository _productRepository;

	public CreateProductCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var product = Product.CreateFromRequest(
			request.Name,
			request.Sku,
			request.Price,
			request.TenantId,
			request.createdBy
			);

		await _productRepository.Add(product, cancellationToken);
	}
}