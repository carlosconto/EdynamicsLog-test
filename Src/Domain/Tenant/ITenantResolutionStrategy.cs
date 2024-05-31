namespace Domain.Tenant;

public interface ITenantResolutionStrategy
{
	Task<string> GetTenantIdentifierAsync();
}
