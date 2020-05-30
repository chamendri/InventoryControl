using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryControl.DAL;
using InventoryControl.DAL.UnitOfWork;
using InventoryControl.Models.InventoryItems;
using InventoryControl.Utilities;
using PagedList;

namespace InventoryControl.Controllers
{
	[Authorize]
	public class InventoryPartsController : BaseController
	{
		private readonly UnitOfWork unitOfWork;

		public InventoryPartsController()
		{
			InventoryContext db = new InventoryContext();
			this.unitOfWork = new UnitOfWork(db);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryPartsController"/> class.
		/// Introduced for the purpose of unit testing.
		/// </summary>
		/// <param name="unitOfWork">The unit of work.</param>
		public InventoryPartsController(InventoryContext context)
		{
			this.unitOfWork = new UnitOfWork(context);
		}

		// GET: InventoryParts		
		/// <summary>
		/// Returns the entire list of inventory items.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.UnitPriceSortParam = sortOrder == "unit_price" ? "unit_price_desc" : 
				"unit_price";
			ViewBag.AvailabelNoOfUnitsSortParam = sortOrder == "availabel_no_of_units" ?
				"availabel_no_of_units_desc" : "availabel_no_of_units";
			ViewBag.ReorderLevelSortParam = sortOrder == "reorder_level" ? "reorder_level_desc" :
				"reorder_level";

			if(searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			int pageSize = 5;
			int pageNumber = (page ?? 1);

			ViewBag.CurrentFilter = searchString;

			IEnumerable<InventoryPart> InventoryParts = null;

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
			return View(InventoryParts.ToPagedList(pageNumber, pageSize));
		}

		// GET: InventoryParts/Details/5		
		/// <summary>
		/// Returns the inventory item specified by the id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public ActionResult Details(int? id)
		{
			if(id == null)
			{
				Log.Error("Input is empty");
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			InventoryPart inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
			if(inventoryPart == null)
			{
				Log.Error($"Record for id :{id} not found.");
				return HttpNotFound();
			}
			return View(inventoryPart);
		}

		// GET: InventoryParts/Create		
		/// <summary>
		/// Returns the create inventory item view.
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
		{
			return View();
		}

		// POST: InventoryParts/Create		
		/// <summary>
		/// Creates the specified inventory part.
		/// </summary>
		/// <param name="inventoryPart">The inventory part.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,Name,AvailabeNoOfUnits,ReorderLevel,UnitPrice")] InventoryPart inventoryPart)
		{
			if(ModelState.IsValid)
			{
				this.unitOfWork.InventoryParts.Insert(inventoryPart);
				this.unitOfWork.Commit();
				return RedirectToAction("Index");
			}

			return View(inventoryPart);
		}

		// GET: InventoryParts/Edit/5		
		/// <summary>
		/// Edits the inventory item specified by the id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public ActionResult Edit(int? id)
		{
			if(id == null)
			{
				Log.Error("Input value is empty.");
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			InventoryPart inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
			if(inventoryPart == null)
			{
				Log.Error($"Record for id :{id} not found.");
				return HttpNotFound();
			}
			return View(inventoryPart);
		}

		// POST: InventoryParts/Edit/5		
		/// <summary>
		/// Edits the specified inventory part.
		/// </summary>
		/// <param name="inventoryPart">The inventory part.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,AvailabeNoOfUnits,ReorderLevel,UnitPrice")] InventoryPart inventoryPart)
		{
			if(ModelState.IsValid)
			{
				this.unitOfWork.InventoryParts.Update(inventoryPart);
				this.unitOfWork.Commit();
				return RedirectToAction("Index");
			}
			return View(inventoryPart);
		}

		// GET: InventoryParts/Delete/5		
		/// <summary>
		/// Returns the specified item to be deleted.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public ActionResult Delete(int? id)
		{
			if(id == null)
			{
				Log.Error("Input value is empty.");
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			InventoryPart inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
			if(inventoryPart == null)
			{
				Log.Error($"Record for id :{id} not found.");
				return HttpNotFound();
			}
			return View(inventoryPart);
		}

		// POST: InventoryParts/Delete/5		
		/// <summary>
		/// Deletes the inventory item once confirmed.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			InventoryPart inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
			if(inventoryPart == null)
			{
				Log.Error($"Record for id :{id} not found.");
				return HttpNotFound();
			}
			else
			{
				this.unitOfWork.InventoryParts.Delete(inventoryPart);
				this.unitOfWork.Commit();
				return RedirectToAction("Index");
			}
		}

		/// <summary>
		/// Releases unmanaged resources and optionally releases managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				this.unitOfWork.Dispose();
			}
			base.Dispose(disposing);
		}

		private IEnumerable<InventoryPart> GetInventoryPartist(String searchString,
			Func<IQueryable<InventoryPart>, IOrderedQueryable<InventoryPart>> orderBy = null)
		{
			IEnumerable<InventoryPart> InventoryParts = null;
			if(!String.IsNullOrEmpty(searchString))
			{
				InventoryParts = this.unitOfWork.InventoryParts.Get(s => s.Name.Contains(searchString), orderBy: orderBy);
			}
			else
			{
				InventoryParts = this.unitOfWork.InventoryParts.Get(orderBy: orderBy);
			}
			return InventoryParts;
		}
	}
}
