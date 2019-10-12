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
    public class AccesoriosController : Controller
    {
		// GET: Web/Accesorios
		public ActionResult Inicio()
		{
			List<Modelo> modelos = ServicioSistema<Modelo>.Get(m => m.ImagenAccesorios != null && m.Vigente == true).ToList();

			return View(modelos);
		}
		public ActionResult Modelo(string id)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.NombreClave == id);
			return View(modelo);
		}

		public ActionResult VerImagenVersionModelo(int codAccesorioModelo)
		{
			//<img src="" alt="" width="321" height="196">
			AccesorioModelo accesorioModelo = ServicioSistema<AccesorioModelo>.GetById(am => am.CodAccesorioModelo == codAccesorioModelo);
			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(accesorioModelo.Imagen, 321, 0)), "image/jpg");
		}
	}
}