using Domain.Organizations;
using Domain.Users;
using Infrastructure.Tenat;
using MediatR;

namespace Application.Users.GetUserById;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
{
	private readonly IUserRepository _userRepository;
	private readonly ITenantProvider _tenantProvider;

	private OrganizationId _tenantId;

	public GetUserByIdQueryHandler(IUserRepository userRepository, ITenantProvider tenantProvider)
	{
		_userRepository = userRepository;
		_tenantProvider = tenantProvider;
		_tenantId = new OrganizationId(_tenantProvider.GetTenantId());
	}

	public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var userDb = await _userRepository.GetById(new UserId(request.Id), cancellationToken);

		var response = new UserResponse(userDb.Id.Value, userDb.Email, userDb.TenantId.Value);

		return response;
	}
}