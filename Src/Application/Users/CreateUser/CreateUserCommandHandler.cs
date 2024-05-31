using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
	private readonly IUserRepository _userRepository;
	private readonly ITenantProvider _tenantProvider;
	private readonly IHashPassword _hashPassword;

	private OrganizationId _tenantId;

	public CreateUserCommandHandler(IUserRepository userRepository, ITenantProvider tenantProvider, IHashPassword hashPassword)
	{
		_userRepository = userRepository;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
		_hashPassword = hashPassword;
	}

	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		
		var passwordHash = _hashPassword.Hash(request.Password, out var _salt);
		var salt = Convert.ToHexString(_salt);

		var user = User.CreateFromRequest(request.Email, passwordHash, salt, _tenantId);

		await _userRepository.Add(user, cancellationToken);
	}
}
