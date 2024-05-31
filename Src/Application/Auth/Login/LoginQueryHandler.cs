using Domain.Authentication;
using Domain.Users;
using MediatR;

namespace Application.Auth.Login;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
	private readonly IAuthRepository _authRepository;
	private readonly IHashPassword _hashPassword;
	private readonly IJwtService _jwtService;

	public LoginQueryHandler(IAuthRepository authRepository, IHashPassword hashPassword, IJwtService jwtService)
	{
		_authRepository = authRepository;
		_hashPassword = hashPassword;
		_jwtService = jwtService;
	}

	public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var user = User.CreateFromRequest(request.Email, request.Password, "");

		var userDb = await _authRepository.Login(user, cancellationToken);
		
		var token = _jwtService.CreateToken(userDb!);

		return new AuthResponse(token);
	}
}
