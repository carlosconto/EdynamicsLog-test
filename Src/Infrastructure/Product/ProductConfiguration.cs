using Domain.Organizations;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Product;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Domain.Products.Product>
{
	public void Configure(EntityTypeBuilder<Domain.Products.Product> builder)
	{
		builder.ToTable("Products");

		builder.HasIndex(x => new { x.Id, x.TenantId, x.CreatedBy });

		builder.HasKey(x => new { x.Id, x.TenantId, x.CreatedBy });

		builder.Property(x => x.Id)
			.HasConversion(v => v.Value, v => new ProductId(v))
			.HasColumnName("id")
			.ValueGeneratedOnAdd();

		builder.Property(x => x.Name).HasColumnName("Name");

		builder.Property(x => x.Sku).HasColumnName("Sku");

		builder.Property(x => x.Price).HasColumnName("Price").HasPrecision(12, 6);

		builder.Property(x => x.Stock).HasColumnName("Stock");

		builder.Property(x => x.TenantId)
			.HasConversion(v => v.Value, v => new OrganizationId(v))
			.HasColumnName("TenantId");

		builder.Property(x => x.CreatedBy)
			.HasConversion(v => v.Value, v => new UserId(v))
			.HasColumnName("CreatedBy");
	}
}
