using Domain.Products;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
	private readonly IProductRepository _productRepository;
	private readonly ITenantProvider _tenantProvider;

	public UpdateProductCommandHandler(IProductRepository productRepository, ITenantProvider tenantProvider)
	{
		_productRepository = productRepository;
		_tenantProvider = tenantProvider;
	}

	public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		var productDb = await _productRepository.GetById(new ProductId(request.Id), cancellationToken);

		productDb.Update(request.Name, request.Sku, request.Price, 0);

		await _productRepository.Update(productDb, cancellationToken);
	}
}