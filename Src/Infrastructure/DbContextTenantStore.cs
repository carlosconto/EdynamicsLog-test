using Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure;

public class DbContextTenantStore: ITenantStore<Tenant>
{
    private readonly EdynamicsLogContext _edynamicsLogContext;

    private readonly IMemoryCache _cache;

    public DbContextTenantStore(EdynamicsLogContext edynamicsLogContext, IMemoryCache cache)
    {
        _edynamicsLogContext = edynamicsLogContext;
        _cache = cache;
    }

    public async Task<Tenant?> GetTenantAsync(string identifier)
    {
        var cacheKey = $"Cache_{identifier}";
        var tenant = _cache.Get<Tenant>(cacheKey);

        if (tenant is null)
        {
            var entity = await _edynamicsLogContext.Organization
                .FirstOrDefaultAsync(q => q.SlugTenant == identifier);

            if(entity is null)
            {
                return null;
            }

            tenant = new Tenant(entity.Id.Value, entity.SlugTenant);

            tenant.Items["Name"] = entity.Name;
            tenant.Items["id"] = entity.Id;

            _cache.Set(cacheKey, tenant);
        }

        return tenant;
    }
}