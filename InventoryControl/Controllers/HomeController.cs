using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryControl.Utilities;

namespace InventoryControl.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			Log.Info("Navigate to home page");
			return View();
		}
	}
}