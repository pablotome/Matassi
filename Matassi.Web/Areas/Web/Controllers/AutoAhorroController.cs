using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using Matassi.Servicios;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Controllers
{
	[AttributeHelper.NoCache]
	public class AutoAhorroController : Controller
    {
        // GET: Web/AutoAhorro
		public ActionResult QueEs()
        {
            return View();
        }

		public ActionResult MediosPago()
		{
			return View();
		}

		public ActionResult Ganadores()
		{
			List<AutoahorroGanador> ganadores;

			//Veo si hay ganadores en el mes actual.
			ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1) && aa.Concesionario == "01411").ToList();
			
			//Mientras no haya ganadores, me voy un mes para atrás, hasta 6 meses
			int mesesAtras = 1;
			while (mesesAtras <= 6 && (ganadores == null || ganadores.Count == 0))
			{
				ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.AddMonths(-1 * mesesAtras).Year, DateTime.Now.AddMonths(-1 * mesesAtras).Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Year, DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Month, 1) && aa.Concesionario == "01411").OrderBy(aa => aa.Grupo).ThenBy(aa => aa.Orden).ToList();
				mesesAtras++;
			}

			if (ganadores != null && ganadores.Count > 0)
			{
				ViewBag.MesGanadores = ganadores[0].ArchivoAutoahorro.Fecha.ToString("MMMM");
				ViewBag.AnioGanadores = ganadores[0].ArchivoAutoahorro.Fecha.Year.ToString();
				
				return View(ganadores);
			}
			
			return View();
		}

		public ActionResult Planes()
		{
			List<PlanAutoahorro> planesAutoahorro = ServicioSistema<PlanAutoahorro>.Get(pa => pa.Vigente == true).OrderBy(pa => pa.Orden).ToList();
			return View(planesAutoahorro);
		}

		public ActionResult VerImagenPlanAutoahorro(int codPlanAutoahorro)
		{
			//<img src="" alt="" width="321" height="196">
			PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == codPlanAutoahorro);
			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(planAutoahorro.Imagen, 180, 0)), "image/jpg");
		}

		public ActionResult OfertasEmisiones()
		{
			return View();
		}

		[HttpPost]
		[AttributeHelper.HttpParamAction]
		public ActionResult Ofertas(Matassi.Web.Areas.Web.Models.ConsultaOfertaEmision consultaOfertaEmision)
		{
			List<AutoahorroOferta> ofertas = ServicioSistema<AutoahorroOferta>.Get(o => int.Parse(o.Grupo) == consultaOfertaEmision.Grupo && o.ArchivoAutoahorro.Fecha > DateTime.Now.AddMonths(-6)).OrderByDescending(o => o.ArchivoAutoahorro.Fecha).ToList();

			ViewBag.Ofertas = ofertas;
			ViewBag.Cuotas = null;

			return View();
		}

		[HttpPost]
		[AttributeHelper.HttpParamAction]
		public ActionResult Cuota(Matassi.Web.Areas.Web.Models.ConsultaOfertaEmision consultaOfertaEmision)
		{
			List<AutoahorroEmision> cuotas = ServicioSistema<AutoahorroEmision>.Get(o => o.Gpo == consultaOfertaEmision.Grupo && o.Ord == consultaOfertaEmision.Orden.Value && o.ArchivoAutoahorro.Fecha > DateTime.Now.AddMonths(-3)).OrderByDescending(o => o.ArchivoAutoahorro.Fecha).ToList();

			ViewBag.Cuotas = cuotas;
			ViewBag.Ofertas = null;

			return View();
		}

		public ActionResult PagoPuntual()
		{
			return View();
		}

		public ActionResult ComoLicitar()
		{
			return View();
		}

		public ActionResult Formularios()
		{
			return View();
		}
		
    }
}