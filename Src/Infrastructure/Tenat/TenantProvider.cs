using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Tenat;

public sealed class TenantProvider : ITenantProvider
{
	private const string TenantIdHeaderName = "X-TENANT_ID";

	private const string TenantOrganizationHeaderName = "X-TENANT_ORGANIZATION";

	private readonly TenantConnectionStrings _connectionStrings;

	private readonly IHttpContextAccessor _httpContextAccessor;

	public TenantProvider( 
		IHttpContextAccessor httpContextAccessor,
		IOptions<TenantConnectionStrings> connectionStrings)
	{
		_httpContextAccessor = httpContextAccessor;
		_connectionStrings = connectionStrings.Value;
	}


	public int GetTenantId()
	{
		var tenantIdentifier = _httpContextAccessor.HttpContext?.Items.Where(x => x.Key.ToString() == "tenantId").FirstOrDefault();

		int tenantId;

		try
		{
			tenantId = int.Parse(tenantIdentifier!.Value.Value.ToString());
		}
		catch(Exception ex)
		{
			throw new Exception(ex.Message.ToString() + " ;");
		}
		

		return tenantId;
	}

	public string GetTenantDb()
	{
		var tenantIdHeader = _httpContextAccessor.HttpContext?.Request.Headers[TenantIdHeaderName];
		var tenantOrganizationHeaderName = _httpContextAccessor.HttpContext?.Request.Headers[TenantOrganizationHeaderName];

		if (tenantIdHeader.HasValue
			|| (
				!int.TryParse(tenantIdHeader!.Value, out int tenantId) &&
				TenantDb.All.Contains(tenantOrganizationHeaderName!.Value.ToString())
				)

			)
		{
			throw new ApplicationException("Tenant ID not found");
		}

		return tenantOrganizationHeaderName!.Value.ToString();
	}

	public string GetConnectionString()
	{
		return _connectionStrings.Values[GetTenantDb()];
	}
}