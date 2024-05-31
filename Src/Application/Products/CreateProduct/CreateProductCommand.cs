using MediatR;

namespace Application.Products.CreateProduct;

public record CreateProductCommand(
	int TenantId,
	string Name,
	string Sku,
	decimal Price,
	int Stock,
	int createdBy
	) : IRequest
{
}