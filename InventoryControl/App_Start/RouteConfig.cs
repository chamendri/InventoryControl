﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryControl
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
			name: "Language",
			url: "{lang}/{controller}/{action}/{id}",
			defaults: 
			new {
				controller = "Home",
				action = "Index",
				id = UrlParameter.Optional,
				lang = "en"
			},
			constraints: new { lang = @"no|en" }
		);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
