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
    public class SeminuevoController : Controller
    {
        // GET: Web/Seminuevo
        public ActionResult Inicio()
        {

			List<Seminuevo> seminuevos = ServicioSistema<Seminuevo>.Get(s => s.Publicado == true).ToList();



            return View(seminuevos);
        }


		public ActionResult VerImagenSeminuevo(int codSeminuevo)
		{
			Seminuevo seminuevo = ServicioSistema<Seminuevo>.GetById(s => s.CodSeminuevo == codSeminuevo);

			if (seminuevo == null
				|| seminuevo.Imagen == null)
				return null;

			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(seminuevo.Imagen, 150, 0)), "image/jpg");
		}
    }
}