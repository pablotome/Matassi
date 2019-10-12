using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Matassi.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			/*routes.MapRoute(
				name: "Modelos",
				url: "Modelos/{nombreModelo}/{version}",
				defaults: new { controller = "Modelo", action = "Info", nombreModelo = "", version = 0 }
			);*/

			/*routes.MapRoute(
				name: "Admin",
				url: "{controller}/{action}/{id}",
				defaults: new { area = "Admin", controller = "Admin", action = "Index", id = UrlParameter.Optional }
			).DataTokens.Add("area", "Admin");*/

			routes.MapRoute(
				"Web_default",
				"{controller}/{action}/{id}",
				defaults: new { area = "Web", controller = "Home", action = "Inicio", id = UrlParameter.Optional },
				namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			).DataTokens.Add("Area", "Web");


			/*routes.MapRoute(
				"Web_default",
				"{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Inicio", id = UrlParameter.Optional },
				namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			);*/

			//////////routes.MapRoute(
			//////////	name: "Default1",
			//////////	url: "{action}",
			//////////	defaults: new { area = "Web", controller = "Home", action = "Inicio", id = UrlParameter.Optional },
			//////////	namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			//////////);//.DataTokens.Add("area", "Web");//.Add("area", "Admin");*/

			//////////routes.MapRoute(
			//////////	name: "Default2",
			//////////	url: "{controller}/{action}/{id}",
			//////////	defaults: new { area = "Web", controller = "Home", action = "Inicio", id = UrlParameter.Optional },
			//////////	namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			//////////);//.DataTokens.Add("area", "Web");//.Add("area", "Admin");*/

			/*routes.MapRoute(
				name: "Modelos",
				url: "{controller}/{id}",
				defaults: new { area = "Web", controller = "Home", action = "Info", id = UrlParameter.Optional },
				namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			).DataTokens.Add("area", "Web");//.Add("area", "Admin");*/
		}
	}
}