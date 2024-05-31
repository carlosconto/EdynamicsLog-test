using Domain.Tenant;

public class HostResolutionStrategy : ITenantResolutionStrategy
{
	private readonly HttpContext? _httpContext;

	public HostResolutionStrategy(IHttpContextAccessor httpContext)
	{
		_httpContext = httpContext.HttpContext;
	}

	public async Task<string> GetTenantIdentifierAsync()
	{
		if (_httpContext is null)
		{
			return string.Empty;
		}

		var identifier = string.Empty;

		if (_httpContext.Request.Path.Value is not null && _httpContext.Request.Path.Value.Split('/').Length > 0)
		{
			var test = _httpContext.Request.Path.Value.Split('/');
			identifier = _httpContext.Request.Path.Value.Split('/')[1];
		}

		return await Task.FromResult(identifier);
	}
}