using MediatR;

namespace Application.Products.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductResponse>
{
}