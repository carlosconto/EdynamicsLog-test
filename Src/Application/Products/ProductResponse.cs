namespace Application.Products;

public record ProductResponse(int TenantId, int ProductId, string Name, string Sku)
{
}
