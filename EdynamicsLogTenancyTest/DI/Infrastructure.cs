using Domain.Tenant;
using Domain.Users;
using Infrastructure;
using Infrastructure.Tenat;
using Infrastructure.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIEdynamicsLogTenancyTest.DI;

public static class Infrastructure
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<EdynamicsLogContext, EdynamicsLogContext>();
		services.AddScoped<EdynamicsLogProductContext, EdynamicsLogProductContext>();		
		services.AddDbContext<EdynamicsLogContext>(
			options => options.UseSqlServer(configuration.GetConnectionString("master"), b => b.MigrationsAssembly("Infrastructure"))
			);

		services.AddDbContext<EdynamicsLogProductContext>(
			options => options.UseSqlServer(configuration.GetConnectionString("product"), b => b.MigrationsAssembly("Infrastructure"))
			);

		services.AddRouting(opt => opt.LowercaseUrls = true);

		services.AddTransient<ITenantProvider, TenantProvider>();

		services.AddMultiTenancy()
				.WithResolutionStrategy<HostResolutionStrategy>()
				.WithStore<DbContextTenantStore>();

		services.AddScoped<IHashPassword, HashPassword>();
		services.AddScoped<IJwtService, JwtService>();


		var jwtIssuer = configuration["Jwt:Issuer"]!;
		var jwtAudience = configuration["Jwt:Audience"]!;
		var jwtKey = configuration["Jwt:Key"]!;

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
			opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
			opt.DefaultSignInScheme = IdentityConstants.ExternalScheme;
		})
			.AddIdentityCookies();

		services.AddAuthorization();

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtIssuer,
					ValidAudience = jwtAudience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
				};
			});

		return services;
	}

	public static TenantBuilder<T> AddMultiTenancy<T>(this IServiceCollection services) where T : Tenant
	   => new(services);

	public static TenantBuilder<Tenant> AddMultiTenancy(this IServiceCollection services)
		=> new(services);
}