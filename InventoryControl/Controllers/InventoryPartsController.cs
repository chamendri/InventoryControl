using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryControl.DAL;
using InventoryControl.DAL.UnitOfWork;
using InventoryControl.Models.InventoryItems;
using InventoryControl.Utilities;

namespace InventoryControl.Controllers
{
	public class InventoryPartsController : Controller
	{
		private readonly UnitOfWork unitOfWork;

		public InventoryPartsController()
		{
			InventoryContext db = new InventoryContext();
			this.unitOfWork = new UnitOfWork(db);
		}

		// GET: InventoryParts
		public ActionResult Index()
		{
			Log.Info("view list");
			var InventoryParts = this.unitOfWork.InventoryParts.Get();
			return View(InventoryParts.ToList());
		}

		// GET: InventoryParts/Details/5
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
		public ActionResult Create()
		{
			return View();
		}

		// POST: InventoryParts/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			InventoryPart inventoryPart = this.unitOfWork.InventoryParts.GetByID(id);
			this.unitOfWork.InventoryParts.Delete(inventoryPart);
			this.unitOfWork.Commit();
			return RedirectToAction("Index");
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
	}
}
