using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InventoryControl.Common.Exceptions;
using InventoryControl.Common.Utilities;
using InventoryControl.Common.ViewModels.InventoryItems;
using InventoryControl.DAL;
using InventoryControl.DAL.UnitOfWork;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.Business.InventoryItem
{
	public class InventoryPartManager
	{
		private readonly UnitOfWork unitOfWork;
		private MapperConfiguration config;
		private IMapper iMapper;

		public InventoryPartManager()
		{
			InventoryContext db = new InventoryContext();
			this.unitOfWork = new UnitOfWork(db);
			this.config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<InventoryPartView, InventoryPartDto>();
			});
			this.iMapper = config.CreateMapper();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryPartManager"/> class. To be used in unit tests
		/// </summary>
		/// <param name="db">The database.</param>
		public InventoryPartManager(InventoryContext db)
		{
			this.unitOfWork = new UnitOfWork(db);
			this.config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<InventoryPartView, InventoryPartDto>();
			});
			this.iMapper = config.CreateMapper();
		}

		public virtual IEnumerable<InventoryPartView> GetInventoryParts(string sortOrder, string searchString)
		{
			try
			{
				IEnumerable<InventoryPartView> InventoryParts = new List<InventoryPartView>();

				switch(sortOrder)
				{
					case "name_desc":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderByDescending(d => d.Name));
					break;
					case "unit_price":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderBy(d => d.UnitPrice));
					break;
					case "unit_price_desc":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderByDescending(d => d.UnitPrice));
					break;
					case "reorder_level":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderBy(d => d.ReorderLevel));
					break;
					case "reorder_level_desc":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderByDescending(d => d.ReorderLevel));
					break;
					case "availabel_no_of_units":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderBy(d => d.AvailabeNoOfUnits));
					break;
					case "availabel_no_of_units_desc":
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderByDescending(d => d.AvailabeNoOfUnits));
					break;
					default:
					InventoryParts = this.GetInventoryPartist(searchString, q => q.OrderBy(d => d.Name));
					break;
				}
				return InventoryParts;
			}
			catch(Exception e)
			{
				Log.Error(e.Message);
				throw new DatabaseAccessException(e.Message);
			}
		}

		public virtual InventoryPartView GetInventoryPartFromId(int? id)
		{
			try
			{
				InventoryPartDto inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
				InventoryPartView inventoryPartView = iMapper.Map<InventoryPartDto, InventoryPartView>(inventoryPart);
				return inventoryPartView;
			}
			catch(Exception e)
			{
				Log.Error(e.Message);
				throw new DatabaseAccessException(e.Message);
			}
		}

		public virtual void InsertInventoryPart(InventoryPartView inventoryPart)
		{
			try
			{ 
				InventoryPartDto inventoryPartDto = iMapper.Map<InventoryPartView, InventoryPartDto>(inventoryPart);
				this.unitOfWork.InventoryParts.Insert(inventoryPartDto);
				this.unitOfWork.Commit();
			}
			catch(Exception e)
			{
				Log.Error(e.Message);
				throw new DatabaseAccessException(e.Message);
			}
		}

		public virtual void UpdateInventoryPart(InventoryPartView inventoryPart)
		{
			try
			{ 
				InventoryPartDto inventoryPartDto = iMapper.Map<InventoryPartView, InventoryPartDto>(inventoryPart);
				this.unitOfWork.InventoryParts.Update(inventoryPartDto);
				this.unitOfWork.Commit();
			}
			catch(Exception e)
			{
				Log.Error(e.Message);
				throw new DatabaseAccessException(e.Message);
			}
		}

		public virtual void DeleteInventoryPart(InventoryPartView inventoryPart)
		{
			try
			{ 
				this.unitOfWork.InventoryParts.Delete(inventoryPart.ID);
				this.unitOfWork.Commit();
			}
			catch(Exception e)
			{
				Log.Error(e.Message);
				throw new DatabaseAccessException(e.Message);
			}
		}

		private IEnumerable<InventoryPartView> GetInventoryPartist(String searchString,
			Func<IQueryable<InventoryPartDto>, IOrderedQueryable<InventoryPartDto>> orderBy = null)
		{
			IEnumerable<InventoryPartDto> InventoryParts = null;
			if(!String.IsNullOrEmpty(searchString))
			{
				InventoryParts = this.unitOfWork.InventoryParts.Get(s => s.Name.Contains(searchString), orderBy: orderBy);
			}
			else
			{
				InventoryParts = this.unitOfWork.InventoryParts.Get(orderBy: orderBy);
			}
			IEnumerable<InventoryPartView> InventoryPartViewist = iMapper.Map<IEnumerable<InventoryPartDto>, IEnumerable<InventoryPartView>>(InventoryParts);
			return InventoryPartViewist;
		}
	}
}
