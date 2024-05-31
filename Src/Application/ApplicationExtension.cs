using Domain.Products;
using Infrastructure.Product.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helpers;

namespace Application;

public static class ApplicationExtension
{
	public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
	{
		
		//---- comands
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyHelper.GetAssembly(Assemblies.Application)));
		//------------

		return services;
	}
}
