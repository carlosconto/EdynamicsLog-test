namespace Domain.Tenant;

public interface ITenantStore<T> where T : Tenant
{
	Task<T?> GetTenantAsync(string identifier);
}
