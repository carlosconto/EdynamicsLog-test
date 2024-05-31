namespace Infrastructure.Tenat;

public static class TenantDb
{
	public const string MasterDb = "master";
	public const string Prodcut = "product";

	public static readonly string[] All = { MasterDb, Prodcut };
}