
using Application;
using Domain.Authentication;
using Domain.Products;
using Domain.Users;
using Infrastructure.Authentication.Persistence;
using Infrastructure.Product.Persistence;
using Infrastructure.Tenat;
using Infrastructure.Users.Persistence;
using Shared.Helpers;

namespace APIEdynamicsLogTenancyTest.DI;

public static class Application
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IProductRepository, MSSQLProductRepository>();
		services.AddScoped<IUserRepository, MSSQLUserRepository>();
		services.AddScoped<IAuthRepository, MSSQLAuthRepository>();
		services.AddScoped<ITenantProvider, TenantProvider>();
		
		return services;
	}
}