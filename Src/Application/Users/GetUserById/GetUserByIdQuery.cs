using MediatR;

namespace Application.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<UserResponse>;