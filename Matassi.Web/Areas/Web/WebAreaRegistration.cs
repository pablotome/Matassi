using System.Web.Mvc;

namespace Matassi.Web.Areas.Web
{
    public class WebAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Web";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
			context.MapRoute(
				name: "Web_Modelos",
				url: "Modelo/{id}",
				defaults: new { area = "Web", controller = "Modelo", action = "Inicio", id = UrlParameter.Optional },
				namespaces: new[] { "Matassi.Web.Areas.Web.Controllers" }
			);
		}
    }
}