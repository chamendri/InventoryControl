using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryControl.Controllers
{
	/// <summary>
	/// this abstract class implements the laguage initialization for a controller.
	/// every new controller must be extended from this to be translatable.
	/// </summary>
	/// <seealso cref="System.Web.Mvc.Controller" />
	public abstract class BaseController : Controller
	{
		private string CurrentLanguageCode { get; set; }

		/// <summary>
		/// Initializes data that might not be available when the constructor is called.
		/// Initializes the language.
		/// </summary>
		/// <param name="requestContext">The HTTP context and route data.</param>
		/// <exception cref="NotSupportedException">Invalid language code '{CurrentLanguageCode}</exception>
		protected override void Initialize(RequestContext requestContext)
		{
			if(requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
			{
				CurrentLanguageCode = (string) requestContext.RouteData.Values["lang"];
				if(CurrentLanguageCode != null)
				{
					try
					{
						Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguageCode);
					}
					catch(Exception)
					{
						throw new NotSupportedException($"Invalid language code '{CurrentLanguageCode}'.");
					}
				}
			}
			base.Initialize(requestContext);
		}
	}
}