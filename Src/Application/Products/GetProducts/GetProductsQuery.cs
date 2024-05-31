
using MediatR;

namespace Application.Products.GetProducts;

public record GetProductsQuery() : IRequest<IEnumerable<ProductResponse>>;
