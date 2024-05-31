using MediatR;

namespace Application.Products.UpdateProduct;

public record UpdateProductCommand(
	int Id,
	string Name,
	string Sku,
	decimal Price
	): IRequest;