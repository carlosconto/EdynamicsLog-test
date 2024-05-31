namespace Domain.Tenant;

public class Tenant(int id, string slugTenant)
{
	public int Id { get; set; } = id;

	public string SlugTenant { get; set; } = slugTenant;

	public Dictionary<string, object> Items { get; set; } = [];
}
