using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
	private readonly IUserRepository _userRepository;
	private readonly ITenantProvider _tenantProvider;

	private OrganizationId _tenantId;

	public DeleteUserCommandHandler(IUserRepository userRepository, ITenantProvider tenantProvider)
	{
		_userRepository = userRepository;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
	}

	public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var userDb = await _userRepository.GetById(new UserId(request.Id), cancellationToken);

		await _userRepository.Delete(userDb, cancellationToken);
	}
}
