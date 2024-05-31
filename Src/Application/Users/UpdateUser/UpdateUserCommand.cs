using MediatR;

namespace Application.Users.UpdateUser;

public record UpdateUserCommand(
	int Id,
	string Email,
	string? Password) : IRequest;