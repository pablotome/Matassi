using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;

using Matassi.Web.Clases;
using Matassi.Web.Areas.Admin;
using Matassi.Web.Areas.Web;
using log4net;


namespace Matassi.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly ILog logError = LogManager.GetLogger("ErroresAppLog");

		protected void Application_Start()
		{
			//GlobalConfiguration.Configure(WebApiConfig.Register);

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			//RouteTable.Routes.IgnoreRoute("");


			//AreaRegistration.RegisterAllAreas();

			HelperWeb.RegisterArea<AdminAreaRegistration>(RouteTable.Routes, null);
			HelperWeb.RegisterArea<WebAreaRegistration>(RouteTable.Routes, null);

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();

			string error = exception.Message;

			logError.Error("Error en sitio Matassi", exception);

			Response.Clear();

			HttpException httpException = exception as HttpException;

			if (httpException != null)
			{
				string action;

				switch (httpException.GetHttpCode())
				{
					case 404:
						// page not found
						action = "HttpError404";
						break;
					case 500:
						// server error
						action = "HttpError500";
						break;
					default:
						action = "General";
						break;
				}

				// clear error on server
				Server.ClearError();

				Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, error));
			}
		}
	}
}