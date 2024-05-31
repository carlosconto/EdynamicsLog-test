namespace Domain.Users;

public interface IHashPassword
{
	string Hash(string password, out byte[] salt);

	bool VerifyPassword(string password, string hash, byte[] salt);
}