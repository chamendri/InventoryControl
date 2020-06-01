using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryControl.DAL.Repository;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.DAL.UnitOfWork
{
	/// <summary>
	/// contains the definition of unit of work
	/// </summary>
	public interface IUnitOfWork
	{
		IRepository<InventoryPartDto> InventoryParts { get; }
		void Commit();
		void Dispose();
	}
}
