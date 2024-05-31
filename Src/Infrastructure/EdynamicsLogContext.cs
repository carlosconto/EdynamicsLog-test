using Infrastructure.Organization;
using Infrastructure.Product;
using Infrastructure.Tenat;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure;

public partial class EdynamicsLogContext : DbContext
{
	public DbSet<Domain.Organizations.Organization> Organization { get; set; }

    public DbSet<Domain.Users.User> Users { get; set; }

	public EdynamicsLogContext()
    {
    }

    public EdynamicsLogContext(DbContextOptions<EdynamicsLogContext> options)
        : base(options)
    {
    }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GENTLEMAN\\GENTLEMAN;Database=EdynamicsLog;Trusted_Connection=True; Trust Server Certificate=true;");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Domain.Organizations.Organization>().HasData(new Domain.Organizations.Organization[]
		{
			new(new Domain.Organizations.OrganizationId(1), "Company_1", "company_1"),
			new(new Domain.Organizations.OrganizationId(2), "Company_2", "company_2"),
			new(new Domain.Organizations.OrganizationId(3), "Company_3", "company_3"),
			new(new Domain.Organizations.OrganizationId(4), "Company_4", "company_4")
		});

		modelBuilder.ApplyConfiguration(new OrganizationConfiguration());

		modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

 }
