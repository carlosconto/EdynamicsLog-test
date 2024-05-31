using Domain.Organizations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users
{
	public sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Users.User>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Users.User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(t => new { t.TenantId, t.Id });

			builder.HasIndex(t => new { t.TenantId, t.Id });

			builder.Property(x => x.Id)
		   .HasConversion(v => v.Value, v => new UserId(v))
		   .HasColumnName("Id")
		   .ValueGeneratedOnAdd();

			builder.Property(x => x.TenantId)
				.HasConversion(v => v.Value, v => new OrganizationId(v))
				.HasColumnName("TenantId");

			builder.Property(x => x.Email).HasColumnName("Email");

			builder.Property(x => x.Password).HasColumnName("Password");

			builder.Property(x => x.Salt).HasColumnName("Salt");
		}
	}
}
