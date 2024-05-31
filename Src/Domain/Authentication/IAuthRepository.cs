using Domain.Users;

namespace Domain.Authentication;

public interface IAuthRepository
{
	Task<User?> Login(User user, CancellationToken cancellationToken);
}