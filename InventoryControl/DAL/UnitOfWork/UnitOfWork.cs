using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryControl.DAL.Repository;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.DAL.UnitOfWork
{
	/// <summary>
	/// Unit of work pattern is used to prevent partial transactions.
	/// </summary>
	/// <seealso cref="InventoryControl.DAL.UnitOfWork.IUnitOfWork" />
	public class UnitOfWork : IUnitOfWork
	{ 
		private InventoryContext _dbContext;
		private BaseRepository<InventoryPart> _inventoryParts;

		public UnitOfWork(InventoryContext dbContext)
		{
			_dbContext = dbContext;
		}

		/// <summary>
		/// Gets the inventory parts repository.
		/// </summary>
		/// <value>
		/// The inventory parts.
		/// </value>
		public IRepository<InventoryPart> InventoryParts
		{
			get
			{
				return _inventoryParts ??
					(_inventoryParts = new BaseRepository<InventoryPart>(_dbContext));
			}
		}

		public void Commit()
		{
			_dbContext.SaveChanges();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}