using Domain.Organizations;

namespace Domain.Users;

public class User
{
	public UserId Id { get; private set; }

	public string Email { get; private set; }

	public string Password { get; private set; }

	public string Salt { get; private set; }

	public OrganizationId TenantId { get; set; }

	public User()
	{
	}

	public User(UserId id, string email, string password, int organizationId, string salt)
	{
		Id = id;
		Email = email;
		Password = password;
		TenantId = new OrganizationId(organizationId);
		Salt = salt;
	}

	public static User Create(int id, string email, string password, int organizationId, string salt)
	{
		return new(new UserId(id), email, password, organizationId, salt);
	}

	public static User CreateFromRequest(string email, string password, string salt, OrganizationId? tenantId = null)
	{
		return new()
		{
			Email = email,
			Password = password,
			Salt = salt,
			TenantId = tenantId
		};
	}

	public void Update(string email, string? password = null, string? salt = null)
	{
		Email = email;

		if(password is not null)
		{
			Password = password;
		}

		if(salt is not null)
		{
			Salt = salt;
		}

	}
}
