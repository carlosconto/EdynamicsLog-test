using Domain.Authentication;
using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Persistence;

public class MSSQLAuthRepository : IAuthRepository
{

	private readonly EdynamicsLogContext _context;

	private readonly ITenantProvider _tenantProvider;

	private readonly OrganizationId _tenantId;

	private readonly IHashPassword _hashPassword;

	public MSSQLAuthRepository(EdynamicsLogContext context, ITenantProvider tenantProvider, IHashPassword hashPassword)
	{
		_context = context;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
		_hashPassword = hashPassword;
	}

	public async Task<User?> Login(User user, CancellationToken cancellationToken)
	{
		var users = await _context.Users.Where(x => x.TenantId == _tenantId).ToListAsync(cancellationToken);


		var userDb = users.FirstOrDefault(x => x.Email == user.Email);

		var verifyPass = _hashPassword.VerifyPassword(user.Password, userDb!.Password, Convert.FromHexString(userDb.Salt));

		if (!verifyPass)
		{
			throw new Exception("Crendentials are incorrect");
		}

		return userDb;
	}
}
