using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryControl.Utilities
{
	public static class LanguageHelper
	{
		/// <summary>
		/// creates and returns the url to change the language
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="Name">The name.</param>
		/// <param name="routeData">The route data.</param>
		/// <param name="lang">The language.</param>
		/// <returns></returns>
		public static MvcHtmlString LangSwitcher(this UrlHelper url, string Name, RouteData routeData, string lang)
		{
			var liTagBuilder = new TagBuilder("li");
			var aTagBuilder = new TagBuilder("a");
			var routeValueDictionary = new RouteValueDictionary(routeData.Values);
			if(routeValueDictionary.ContainsKey("lang"))
			{
				if(routeData.Values["lang"] as string == lang)
				{
					liTagBuilder.AddCssClass("active");
				}
				else
				{
					routeValueDictionary["lang"] = lang;
				}
			}
			aTagBuilder.MergeAttribute("href", url.RouteUrl(routeValueDictionary));
			aTagBuilder.SetInnerText(Name);
			liTagBuilder.InnerHtml = aTagBuilder.ToString();
			return new MvcHtmlString(liTagBuilder.ToString());
		}
	}
}