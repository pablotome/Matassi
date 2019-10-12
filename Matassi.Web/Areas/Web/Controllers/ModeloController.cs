using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using Matassi.Servicios;
using Matassi.Dominio;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Controllers
{
	//[OutputCache(CacheProfile = "CacheWeb")]
	public class ModeloController : Controller
    {
		//
        // GET: /Modelo/
		public ActionResult Inicio(string id)
		{
			if (id == string.Empty)
				return View();
			else
				return View(id);
			//Modelo m = ServicioSistema<Modelo>.GetById(o => o.NombreClave == id);
			//return View(m);
		}

		public ActionResult Versiones(string id)
		{
			List<VersionModelo> versionesModelo = ServicioSistema<VersionModelo>.Get(vm => vm.Modelo.NombreClave == id).ToList();
			return View(versionesModelo);
		}
		
		public ActionResult Asesoramiento(string id)
		{
			Modelo m = ServicioSistema<Modelo>.GetById(o => o.NombreClave == id);
			return RedirectToAction("Contacto", "Home", new { @modelo = m.Nombre, @codModelo = m.CodModelo });
		}

		public ActionResult Accesorios(string id)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.NombreClave == id);
			return View(modelo);
		}

		public ActionResult VerImagenModelo(int id)
		{
			ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == id);

			if (imagenModelo.Imagen == null)
				return null;

			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(imagenModelo.Imagen, 960, 0)), "image/jpg");
		}

		public ActionResult VerImagenVersionModelo(int id)
		{
			//<img src="" alt="" width="321" height="196">
			VersionModelo versionModelo = ServicioSistema<VersionModelo>.GetById(am => am.CodVersionModelo == id);
			if (versionModelo.Imagen != null)
				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(versionModelo.Imagen, 228, 0)), "image/jpg");
			return null;
		}

		public ActionResult VerImagenAccesorioModelo(int id)
		{
			//<img src="" alt="" width="321" height="196">
			AccesorioModelo accesorioModelo = ServicioSistema<AccesorioModelo>.GetById(am => am.CodAccesorioModelo == id);
			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(accesorioModelo.Imagen, 310, 0)), "image/jpg");
		}

		public ActionResult VerImagenHomeAccesorioModelo(int id)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == id);
			if (modelo.ImagenAccesorios.Length > 0)
				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(modelo.ImagenAccesorios, 153, 120)), "image/jpg");
			return null;
		}

		public ActionResult VerImagenContactoModelo(int id)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == id);
			if (modelo != null
				&& modelo.ImagenContacto != null
				&& modelo.ImagenContacto.Length > 0)
				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(modelo.ImagenContacto, 900, 200)), "image/jpg");
			return File(Server.MapPath("/Content/images/Matassi.jpg"), "image/jpeg");
		}
	}
}
