using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Matassi.Web.BootstrapBundleConfig), "RegisterBundles")]

namespace Matassi.Web
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Styles.Render("~/Content/bootstrap") in the <head/> of your _Layout.cshtml view
			// For Bootstrap theme add @Styles.Render("~/Content/bootstrap-theme") in the <head/> of your _Layout.cshtml view
			// Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap/bootstrap.js", 
				"~/Scripts/bootstrap/bootstrap-alert.js",
				"~/Scripts/bootstrap/bootstrap-button.js", 
				"~/Scripts/bootstrap/bootstrap-carousel.js", 
				"~/Scripts/bootstrap/bootstrap-collapse.js", 
				"~/Scripts/bootstrap/bootstrap-dropdown.js", 
				//"~/Scripts/bootstrap/bootstrap-modal.js", 
				"~/Scripts/bootstrap/bootstrap-popover.js", 
				"~/Scripts/bootstrap/bootstrap-scrollspy.js", 
				"~/Scripts/bootstrap/bootstrap-tab.js", 
				"~/Scripts/bootstrap/bootstrap-tooltip.js", 
				"~/Scripts/bootstrap/bootstrap-transition.js", 
				"~/Scripts/bootstrap/bootstrap-typeahead.js"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-responsive").Include("~/Content/bootstrap-responsive.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));
			//BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-fixed-top").Include("~/Content/navbar-fixed-top.css"));
		}
	}
}
