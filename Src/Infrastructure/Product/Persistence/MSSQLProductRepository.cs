using Domain.Products;
using Infrastructure.Tenat;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Product.Persistence;

public sealed class MSSQLProductRepository : IProductRepository
{
	private readonly EdynamicsLogProductContext _context;

	private readonly ITenantProvider _tenantProvider;

	public MSSQLProductRepository(EdynamicsLogProductContext context, ITenantProvider tenantProvider)
	{
		_context = context;
		_tenantProvider = tenantProvider;
	}

	public async Task Add(Domain.Products.Product product, CancellationToken cancellationToken)
	{
		var tenantId = new Domain.Organizations.OrganizationId(_tenantProvider.GetTenantId());

		var productDb = await _context.Products
			.Where(x => x.Sku == product.Sku || x.Name == product.Name)
			.Where(x => x.TenantId == tenantId)
			.FirstOrDefaultAsync(cancellationToken);

		if (productDb is not null)
		{
			throw new Exception("product exists");
		}

		_context.Add(product);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task Delete(Domain.Products.Product product, CancellationToken cancellationToken)
	{
		_context.Remove(product);

		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<Domain.Products.Product>> GetAll(CancellationToken cancellationToken)
	{
		var producstDb = await _context!.Products
			.Where(x => x.TenantId == new Domain.Organizations.OrganizationId(_tenantProvider.GetTenantId()))
			.ToListAsync(cancellationToken);

		return producstDb;
	}

	public async Task<Domain.Products.Product> GetById(ProductId id, CancellationToken cancellationToken)
	{
		var productDb = await _context.Products
			.Where(x => x.TenantId == new Domain.Organizations.OrganizationId(_tenantProvider.GetTenantId()))
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

		return productDb is null ? throw new Exception("product not exists") : productDb;
	}

	public async Task Update(Domain.Products.Product product, CancellationToken cancellationToken)
	{
		var producstDb = await _context!.Products
			.Where(x => x.TenantId == new Domain.Organizations.OrganizationId(_tenantProvider.GetTenantId()))
			.Where(x => x.Id != product.Id)
			.ToListAsync(cancellationToken);

		if(producstDb.Any(x => x.Name == product.Name || x.Sku == product.Sku))
		{
			throw new Exception("product exists");
		}

		_context.Update(product);

		await _context.SaveChangesAsync(cancellationToken);

	}
}
