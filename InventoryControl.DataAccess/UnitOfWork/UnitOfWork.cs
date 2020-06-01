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
		private BaseRepository<InventoryPartDto> _inventoryParts;

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
		public IRepository<InventoryPartDto> InventoryParts
		{
			get
			{
				return _inventoryParts ??
					(_inventoryParts = new BaseRepository<InventoryPartDto>(_dbContext));
			}
		}

		/// <summary>
		/// Commits this instance.
		/// </summary>
		public void Commit()
		{
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}