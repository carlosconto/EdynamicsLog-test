using MediatR;

namespace Application.Users.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest
{ 
}
