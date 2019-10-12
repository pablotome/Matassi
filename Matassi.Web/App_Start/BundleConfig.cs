using System.Web;
using System.Web.Optimization;

namespace Matassi.Web
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			BundleTable.EnableOptimizations = false;
			
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));
						//"~/Scripts/jquery-1.11.3.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*",
						"~/Scripts/jquery.unobtrusive*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/lightslider").Include(
						"~/Scripts/lightSlider.js"));

			bundles.Add(new ScriptBundle("~/bundles/bxslider").Include(
						"~/Scripts/jquery.bxslider*"));

			//bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/Reset.css", 
						"~/Content/Fuentes.css", 
						"~/Content/Estilos.css", 
						"~/Content/lightslider.css",
						"~/Content/jquery.bxslider.css",
						"~/Content/themes/base/jquery-ui.min.css"));

			bundles.Add(new StyleBundle("~/Content/cssAdmin").Include("~/Content/EstilosAdmin.css"));


			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css",
						"~/Content/themes/base/jquery-ui.min.css"));

			bundles.Add(new ScriptBundle("~/bundles/functionesJS").Include(
				"~/Scripts/funciones1.js"));

			bundles.Add(new StyleBundle("~/bundles/estilosCSS").Include(
				//"https://fonts.googleapis.com/css?family=Raleway", 
				"~/Content/css/normalize.css", 
				"~/Content/css/matassi.css", 
				"~/Content/css/matassi-celular.css", 
				"~/Content/css/slick.css", 
				"~/Content/css/slick-theme.css",
				"~/Content/css/jquery-ui.css"));







    
			bundles.Add(new ScriptBundle("~/bundles/funcionesJS1").Include(
				"~/Content/js/jquery-2.2.4.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/funcionesJS2").Include(
				"~/Content/js/jquery-ui.min.js",
				"~/Content/js/matassi.js",
				"~/Content/js/slick.js"));

			bundles.Add(new StyleBundle("~/bundles/estilosCSSModelo").Include(
				"~/Content/css/estilo-modelo.css", 
				"~/Content/css/estilo-modelo-celular.css", 
				"~/Content/css/matassi-modelo.css", 
				"~/Content/css/matassi-carousel.css"));

			bundles.Add(new ScriptBundle("~/bundles/funcionesModelo").Include(
				"~/Content/js/jquery.mobile-1.4.5.js", 
				"~/Content/js/jquery.touchwipe.1.1.1.js", 
				"~/Content/js/matassi-modelo.js", 
				"~/Content/js/matassi-carousel.js"));


			bundles.Add(new ScriptBundle("~/bundles/funcionesEmpresaContactenos").Include(
				"~/Content/js/empresa_contactenos.js"
			));

			bundles.Add(new ScriptBundle("~/bundles/funcionesEmpresaEncuestaVenta").Include(
				"~/Content/js/empresa_encuesta_venta.js"
			));

			bundles.Add(new ScriptBundle("~/bundles/funcionesEmpresaEncuestaPosventa").Include(
				"~/Content/js/empresa_encuesta_posventa.js"
			));

			bundles.Add(new ScriptBundle("~/bundles/funcionesEmpresaPruebaManejo").Include(
				"~/Content/js/empresa_prueba_manejo.js"
			));

			bundles.Add(new StyleBundle("~/bundles/estilosEmpresa").Include(
				"~/Content/css/empresa.css"
			));

			bundles.Add(new ScriptBundle("~/bundles/calendarioEspaniol").Include(
				"~/Content/js/dpEspaniol.js"
			));


		}
	}
}