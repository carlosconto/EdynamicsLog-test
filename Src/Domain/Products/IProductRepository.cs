namespace Domain.Products;

public interface IProductRepository
{
	Task Add(Product product, CancellationToken cancellationToken);

	Task<Product> GetById(ProductId id, CancellationToken cancellationToken);

	Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken);

	Task Update(Product product, CancellationToken cancellationToken);

	Task Delete(Product product, CancellationToken cancellationToken);
}
