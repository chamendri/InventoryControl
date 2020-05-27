using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.DAL
{
	public class InventoryContext : DbContext
	{
		public InventoryContext() : base("InventoryContext")
		{
		}

		public DbSet<InventoryPart> InventoryParts { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}