using Domain.Tenant;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Tenat;

public class TenantMiddleware<T> where T : Tenant
{
	private readonly RequestDelegate next;


	public TenantMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		var tenantStore = context.RequestServices.GetService(typeof(ITenantStore<T>)) as ITenantStore<T>;
		var resolutionStrategy = context.RequestServices.GetService(typeof(ITenantResolutionStrategy)) as ITenantResolutionStrategy;
		var identifier = await resolutionStrategy!.GetTenantIdentifierAsync();
		
		var tenant = await tenantStore!.GetTenantAsync(identifier);
		
		if (tenant is not null)
		{
			context.Items["master"] = identifier;
			context.Items["tenantId"] = tenant!.Id;
			await next(context);
		}
		else
		{
			context.Response.StatusCode = 404;
			await context.Response.WriteAsync("Not Found");
		}

		//Continue processing
		//if (next != null)
			
	}
}