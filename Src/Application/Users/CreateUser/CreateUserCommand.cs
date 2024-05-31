using MediatR;

namespace Application.Users.CreateUser;

public record CreateUserCommand(
	string Email,
	string Password
	) : IRequest;