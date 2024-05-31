﻿using Infrastructure.Organization;
using Infrastructure.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class EdynamicsLogProductContext : DbContext
{
	public DbSet<Domain.Products.Product> Products { get; set; }

	public EdynamicsLogProductContext(DbContextOptions options) : base(options)
	{
	}

	/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Server=GENTLEMAN\\GENTLEMAN;Database=EdynamicsLog;Trusted_Connection=True; Trust Server Certificate=true;");
*/

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ProductConfiguration());
	}
}