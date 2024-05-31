using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
	private readonly IUserRepository _userRepository;
	private readonly ITenantProvider _tenantProvider;
	private readonly IHashPassword _hashPassword;

	private OrganizationId _tenantId;

	public UpdateUserCommandHandler(IUserRepository userRepository, ITenantProvider tenantProvider, IHashPassword hashPassword)
	{
		_userRepository = userRepository;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
		_hashPassword = hashPassword;
	}

	public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var userDb = await _userRepository.GetById(new UserId(request.Id), cancellationToken);
		
		if (request.Password is not null)
		{
			var passwordHash = _hashPassword.Hash(request.Password, out var _salt);
			var salt = Convert.ToHexString(_salt);

			userDb.Update(request.Email, passwordHash, salt);
		}
		else
		{
			userDb.Update(request.Email);
		}

		await _userRepository.Update(userDb, cancellationToken);
	}
}
