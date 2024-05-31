namespace Domain.Organizations;

public class Organization
{
	public OrganizationId Id { get; private set; }

	public string Name { get; private set; }

	public string SlugTenant { get; private set; }

	public Organization() { }

	public Organization(OrganizationId id, string name, string slugTenant)
	{
		Id = id;
		Name = name;
		SlugTenant = slugTenant;
	}

	public static Organization Create(int id, string name, string slugTenant)
	{
		return new(new OrganizationId(id), name, slugTenant);
	}

	public void Update(string name, string slugTenant)
	{
		Name = name;
		SlugTenant = slugTenant;
	}
}
