namespace Domain.Users;

public interface IUserRepository
{
	Task Add(User user, CancellationToken cancellationToken);

	Task Update(User user, CancellationToken cancellationToken);

	Task Delete(User user, CancellationToken cancellationToken);

	Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);

	Task<User> GetById(UserId id, CancellationToken cancellationToken);
}
