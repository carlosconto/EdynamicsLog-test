using Domain.Organizations;
using Domain.Users;

namespace Domain.Products;

public class Product
{
	public ProductId Id { get; private set; }

	public string Name { get; private set; }

	public string Sku { get; private set; }

	public decimal Price { get; private set; }

	public int Stock { get; private set; }

	public OrganizationId TenantId { get; private set; }

	public UserId CreatedBy { get; private set; }

	public Product() { }

	public Product(ProductId id, string name, string sku, decimal price, int stock, int tenantId, int createdBy)
	{
		Id = id;
		Name = name;
		Sku = sku;
		Price = price;
		Stock = stock;
		TenantId = new OrganizationId(tenantId);
		CreatedBy = new UserId(createdBy);
	}

	public static Product Create(int id, string name, string sku, decimal price, int tenantId, int createdBy)
	{
		return new(
			new ProductId(id),
			name,
			sku,
			price,
			1,
			tenantId,
			createdBy
			);
	}

	public static Product CreateFromRequest(string name, string sku, decimal price, int tenantId, int createdBy)
	{
		return new()
		{
			Name = name,
			Sku = sku,
			Price = price,
			TenantId = new OrganizationId(tenantId),
			CreatedBy = new UserId(createdBy)
		};
	}

	public void Update(string name, string sku, decimal price, int stock)
	{
		Name = name;
		Sku = sku;
		Price = price;
		Stock = stock;
	}
}
