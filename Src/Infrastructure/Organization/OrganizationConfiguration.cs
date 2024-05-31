using Domain.Organizations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Infrastructure.Organization;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Domain.Organizations.Organization>
{
	public void Configure(EntityTypeBuilder<Domain.Organizations.Organization> builder)
	{
		builder.ToTable("Organizations");

		builder.HasKey(t => new { t.Id });

		builder.HasIndex(t => new { t.Id });

		builder.Property(x => x.Id)
	   .HasConversion(v => v.Value, v => new OrganizationId(v))
	   .HasColumnName("Id")
	   .ValueGeneratedOnAdd();

		builder.Property(x => x.Name).HasColumnName("Name");

		builder.Property(x => x.SlugTenant).HasColumnName("SlugTenant");
	}
}
