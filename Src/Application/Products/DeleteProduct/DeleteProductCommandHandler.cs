using Domain.Products;
using MediatR;

namespace Application.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
	private readonly IProductRepository _productRepository;

	public DeleteProductCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var productDb = await _productRepository.GetById(new ProductId(request.Id), cancellationToken);

		await _productRepository.Delete(productDb, cancellationToken);
	}
}
