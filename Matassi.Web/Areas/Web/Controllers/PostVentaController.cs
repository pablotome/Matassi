using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using Matassi.Servicios;

namespace Matassi.Web.Areas.Web.Controllers
{
    public class PostVentaController : Controller
    {
        // GET: Web/PostVenta
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult GarantiaVW()
		{
			List<ServicioMantenimiento> serviciosMantenimiento = ServicioSistema<ServicioMantenimiento>.Get(sm => sm.Vigente == true).OrderBy(sm => sm.Orden).ToList();
			List<GarantiaPeriodoIntervalo> garantiasPeriodoIntervalo = ServicioSistema<GarantiaPeriodoIntervalo>.Get(gpi => gpi.Vigente == true).OrderBy(gpi => gpi.Orden).ToList();

			ViewBag.ServiciosMantenimiento = serviciosMantenimiento;
			ViewBag.GarantiasPeriodoIntervalo = garantiasPeriodoIntervalo;

			return View();
		}

		public ActionResult ServiciosInspeccion()
		{
			List<ServicioMantenimiento> serviciosMantenimiento = ServicioSistema<ServicioMantenimiento>.Get(sm => sm.Vigente == true).OrderBy(sm => sm.Orden).ToList();

			ViewBag.ServiciosMantenimiento = serviciosMantenimiento;

			return View();
		}

		public ActionResult YoCuidoMiVW()
		{
			return View();
		}

		public ActionResult GarantiaVW2()
		{
			return View();
		}

		public ActionResult Encuestas()
		{
			return View();
		}

    }
}