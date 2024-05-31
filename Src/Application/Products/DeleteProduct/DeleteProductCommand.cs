using MediatR;

namespace Application.Products.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;