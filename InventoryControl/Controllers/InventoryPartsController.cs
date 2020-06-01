using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryControl.Business.InventoryItem;
using InventoryControl.Common.Exceptions;
using InventoryControl.Common.Utilities;
using InventoryControl.Common.ViewModels.InventoryItems;
using InventoryControl.Utilities;
using PagedList;

namespace InventoryControl.Controllers
{
	[Authorize]
	public class InventoryPartsController : BaseController
	{
		private readonly InventoryPartManager InventoryPartManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryPartsController"/> class.
		/// </summary>
		public InventoryPartsController()
		{
			this.InventoryPartManager = new InventoryPartManager();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InventoryPartsController"/> class.
		/// </summary>
		public InventoryPartsController(InventoryPartManager manager)
		{
			this.InventoryPartManager = manager;
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

			IEnumerable<InventoryPartView> InventoryParts = this.InventoryPartManager.GetInventoryParts(sortOrder, searchString);

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
			InventoryPartView inventoryPart = null;
			try
			{
				inventoryPart = this.InventoryPartManager.GetInventoryPartFromId(id);
			}
			catch(DatabaseAccessException e)
			{
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
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
		public ActionResult Create([Bind(Include = "ID,Name,AvailabeNoOfUnits,ReorderLevel,UnitPrice")] InventoryPartView inventoryPart)
		{
			if(ModelState.IsValid)
			{
				try
				{ 
					this.InventoryPartManager.InsertInventoryPart(inventoryPart);
					return RedirectToAction("Index");
				}
				catch(DatabaseAccessException e)
				{
					return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
				}
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
			InventoryPartView inventoryPart = null;
			try
			{ 
				inventoryPart = this.InventoryPartManager.GetInventoryPartFromId(id);
				if(inventoryPart == null)
				{
					Log.Error($"Record for id :{id} not found.");
					return HttpNotFound();
				}
				return View(inventoryPart);
			}
			catch(DatabaseAccessException e)
			{
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
		}

		// POST: InventoryParts/Edit/5		
		/// <summary>
		/// Edits the specified inventory part.
		/// </summary>
		/// <param name="inventoryPart">The inventory part.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,AvailabeNoOfUnits,ReorderLevel,UnitPrice")] InventoryPartView inventoryPart)
		{
			if(ModelState.IsValid)
			{
				try
				{
					this.InventoryPartManager.UpdateInventoryPart(inventoryPart);
					return RedirectToAction("Index");
				}
				catch(DatabaseAccessException e)
				{
					return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
				}
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
			InventoryPartView inventoryPart = null;
			try
			{ 
				inventoryPart = this.InventoryPartManager.GetInventoryPartFromId(id);
				if(inventoryPart == null)
				{
					Log.Error($"Record for id :{id} not found.");
					return HttpNotFound();
				}
			}
			catch(DatabaseAccessException e)
			{
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
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
			InventoryPartView inventoryPart = this.InventoryPartManager.GetInventoryPartFromId(id);
			if(inventoryPart == null)
			{
				Log.Error($"Record for id :{id} not found.");
				return HttpNotFound();
			}
			else
			{
				try
				{
					this.InventoryPartManager.DeleteInventoryPart(inventoryPart);
					return RedirectToAction("Index");
				}
				catch(DatabaseAccessException e)
				{
					return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
				}
			}
		}

		
	}
}
