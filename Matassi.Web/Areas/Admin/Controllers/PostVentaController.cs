using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using Matassi.Servicios;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Admin.Controllers
{
	[Authorize]
	[AttributeHelper.NoCache]
    public class PostVentaController : Controller
    {
        public ActionResult ServiciosInspeccion_Lista()
        {
			ViewBag.Title = "Servicios de Inspección";

			List<Matassi.Dominio.ServicioMantenimiento> serviciosMantenimiento = ServicioSistema<ServicioMantenimiento>.GetAll().OrderBy(sm => sm.Orden).ToList();

			return View("ServiciosInspeccion-Lista", serviciosMantenimiento);
        }

		public ActionResult ServiciosInspeccion_Crear()
		{
			ViewBag.Title = "Nuevo Servicio de Inspección";

			ServicioMantenimiento servicioMantenimiento = new ServicioMantenimiento();

			return View("ServiciosInspeccion-Crear", servicioMantenimiento);
		}

		[HttpPost]
		public ActionResult ServiciosInspeccion_Crear(ServicioMantenimiento servicioMantenimientoPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ServicioMantenimiento servicioMantenimiento = new ServicioMantenimiento();

					servicioMantenimiento.DesServicioMantenimiento = servicioMantenimientoPost.DesServicioMantenimiento;
					servicioMantenimiento.Orden = servicioMantenimientoPost.Orden;
					servicioMantenimiento.Vigente = servicioMantenimientoPost.Vigente;

					servicioMantenimiento = ServicioSistema<ServicioMantenimiento>.SaveOrUpdate(servicioMantenimiento);

					return RedirectToAction("ServiciosInspeccion_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View(); 
		}

		public ActionResult ServiciosInspeccion_Editar(int codServicioMantenimiento)
		{
			ViewBag.Title = "Edición de Servicio de Inspección";

			ServicioMantenimiento servicioMantenimiento = ServicioSistema<ServicioMantenimiento>.GetById(sm => sm.CodServicioMantenimiento == codServicioMantenimiento);

			if (servicioMantenimiento != null)
				return View("ServiciosInspeccion-Editar", servicioMantenimiento);

			return View();
		}

		[HttpPost]
		public ActionResult ServiciosInspeccion_Editar(int codServicioMantenimiento, ServicioMantenimiento servicioMantenimientoPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ServicioMantenimiento servicioMantenimiento = ServicioSistema<ServicioMantenimiento>.GetById(sm => sm.CodServicioMantenimiento == codServicioMantenimiento);

					if (servicioMantenimiento != null)
					{ 
						servicioMantenimiento.DesServicioMantenimiento = servicioMantenimientoPost.DesServicioMantenimiento;
						servicioMantenimiento.Orden = servicioMantenimientoPost.Orden;
						servicioMantenimiento.Vigente = servicioMantenimientoPost.Vigente;

						servicioMantenimiento = ServicioSistema<ServicioMantenimiento>.SaveOrUpdate(servicioMantenimiento);
					}

					return RedirectToAction("ServiciosInspeccion_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ServiciosInspeccion_Borrar(int codServicioMantenimiento)
		{
			ServicioMantenimiento servicioMantenimiento = ServicioSistema<ServicioMantenimiento>.GetById(sm => sm.CodServicioMantenimiento == codServicioMantenimiento);

			if (servicioMantenimiento != null)
				ServicioSistema<ServicioMantenimiento>.Delete(servicioMantenimiento);

			return RedirectToAction("ServiciosInspeccion_Lista");
		}
    }
}