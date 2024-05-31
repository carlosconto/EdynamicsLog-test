using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Users;
using Infrastructure.Tenat;

namespace Infrastructure.Users.Services;

public class JwtService : IJwtService
{
	private readonly IConfiguration _configuration;
	private readonly ITenantProvider _tenantProvider;

	public JwtService(IConfiguration configuration, ITenantProvider tenantProvider)
	{
		_configuration = configuration;
		_tenantProvider = tenantProvider;
	}

	public string CreateToken(User user)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim("UserId", user.Id.Value.ToString()),
			new Claim("TenantId", _tenantProvider.GetTenantId().ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(CultureInfo.InvariantCulture)),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var tokenDescriptor = new SecurityTokenDescriptor()
		{
			Audience = _configuration["Jwt:Audience"],
			Issuer = _configuration["Jwt:Issuer"],
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.Now.AddHours(8),
			//Expires = DateTime.Now.AddMinutes(4),
			NotBefore = DateTime.UtcNow.AddMinutes(1),
			SigningCredentials = credentials

		};

		var tokenHandler = new JwtSecurityTokenHandler();

		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}
}