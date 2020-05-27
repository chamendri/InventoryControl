using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryControl.DAL.Repository;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.DAL.UnitOfWork
{
	public interface IUnitOfWork
	{
		IRepository<InventoryPart> InventoryParts { get; }
	//	void Commit();
	}
}
