using System.Web.Mvc;

namespace Matassi.Web.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			////////////////context.MapRoute(
			////////////////	name: "Admin_login",
			////////////////	url: "Admin/Login",
			////////////////	defaults: new { area = "Admin", controller = "Admin", action = "Login", id = UrlParameter.Optional },
			////////////////	namespaces: new[] { "Matassi.Web.Areas.Admin.Controllers" }
			////////////////);

			context.MapRoute(
				name: "Admin_default",
				url: "Admin/{controller}/{action}/{id}",
				defaults: new { area = "Admin", controller = "Admin", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "Matassi.Web.Areas.Admin.Controllers" }
			);

			/*context.MapRoute(
				"Admin_modelos",
				"Admin/{controller}/{action}/{id}",
				new { area = "Admin", controller = "AdminModelo", action = "Index", id = UrlParameter.Optional }
			);*/

			/*context.MapRoute(
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
			);*/
		}
	}
}
