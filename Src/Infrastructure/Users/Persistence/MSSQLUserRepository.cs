using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users.Persistence;

public class MSSQLUserRepository : IUserRepository
{
	private readonly EdynamicsLogContext _context;

	private readonly ITenantProvider _tenantProvider;

	private readonly OrganizationId _tenantId;

	public MSSQLUserRepository(EdynamicsLogContext context, ITenantProvider tenantProvider)
	{
		_context = context;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
	}

	public async Task Add(Domain.Users.User user, CancellationToken cancellationToken)
	{
		var usersDb = await _context.Users
			.Where(x => x.TenantId == _tenantId)
			.ToListAsync(cancellationToken);

		if(usersDb.Any(x => x.Email == user.Email))
		{
			throw new Exception("user already exists");
		}

		await _context.AddAsync(user, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task Delete(Domain.Users.User user, CancellationToken cancellationToken)
	{
		var usersDb = await _context.Users
			.Where(x => x.TenantId == _tenantId)
			.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);

		if(user is null)
		{
			throw new Exception("user not exists");
		}

		_context.Remove(user);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
	{
		var usersDb = await _context.Users
			.Where(x => x.TenantId == _tenantId)
			.ToListAsync(cancellationToken);

		return usersDb;
	}

	public async Task<Domain.Users.User> GetById(UserId id, CancellationToken cancellationToken)
	{
		var user = await _context.Users
			.Where(x => x.TenantId == _tenantId)
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

		return user is null ? throw new Exception("user not exists") : user;
	}

	public async Task Update(Domain.Users.User user, CancellationToken cancellationToken)
	{
		var usersDb = await _context.Users
			.Where(x => x.TenantId == _tenantId)
			.Where(x => x.Id != user.Id)
			.ToListAsync(cancellationToken);

		if (usersDb.Any(x => x.Email == user.Email))
		{
			throw new Exception("user already exists");
		}

		_context.Update(user);
		await _context.SaveChangesAsync(cancellationToken);
	}
}