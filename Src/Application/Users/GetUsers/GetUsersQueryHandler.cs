using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
	private readonly IUserRepository _userRepository;
	private readonly ITenantProvider _tenantProvider;

	private OrganizationId _tenantId;

	public GetUsersQueryHandler(IUserRepository userRepository, ITenantProvider tenantProvider)
	{
		_userRepository = userRepository;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
	}

	public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		var usersDb = await _userRepository.GetAll(cancellationToken);

		var response = usersDb.Select(x => new UserResponse(x.Id.Value, x.Email, x.TenantId.Value));

		return response;
	}
}