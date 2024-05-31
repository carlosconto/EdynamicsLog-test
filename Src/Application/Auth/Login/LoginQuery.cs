using MediatR;

namespace Application.Auth.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthResponse>;
