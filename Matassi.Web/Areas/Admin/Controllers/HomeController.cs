using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using Matassi.Dominio;
using Matassi.Servicios;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Admin.Controllers
{
	[Authorize]
	[AttributeHelper.NoCache]
	public class HomeController : Controller
    {
        // GET: Admin/Home
		public ActionResult AccesosHome_Listar()
        {
			List<AccesosHome> accesosHome = ServicioSistema<AccesosHome>.GetAll().ToList();

			return View("AccesosHome-Listar", accesosHome);
        }

		public ActionResult AccesosHome_Crear()
		{
			ViewBag.Title = "Nuevo Acceso desde la Home";

			List<ImagenModelo> imagenesModelo = ServicioSistema<ImagenModelo>.Get(am => am.Vigente && am.MostrarEnAccesoHome).ToList();
			while(imagenesModelo.Count % 4 != 0)
			{
				imagenesModelo.Add(new ImagenModelo());
			}
			ViewBag.ImagenesModelo = imagenesModelo;

			List<AccesorioModelo> accesoriosModelo = ServicioSistema<AccesorioModelo>.Get(am => am.Vigente && am.MostrarEnAccesoHome).ToList();
			while (accesoriosModelo.Count % 4 != 0)
			{
				accesoriosModelo.Add(new AccesorioModelo());
			}
			ViewBag.AccesoriosModelo = accesoriosModelo;

			return View("AccesosHome-Crear");
		}

		[HttpPost]
		public ActionResult AccesosHome_Crear(AccesosHome accesosHomePost)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AccesosHome accesosHome  = new AccesosHome();

					accesosHome.Titulo = accesosHomePost.Titulo;
					accesosHome.ClaseCSSTitulo = accesosHomePost.ClaseCSSTitulo;
					accesosHome.CodTipoImagen = accesosHomePost.CodTipoImagen;
					accesosHome.CodImagen = accesosHomePost.CodImagen;
					accesosHome.Link = accesosHomePost.Link;
					accesosHome.Orden = accesosHomePost.Orden;
					accesosHome.Vigente = accesosHomePost.Vigente;

					accesosHome = ServicioSistema<AccesosHome>.SaveOrUpdate(accesosHome);

				}

				return RedirectToAction("AccesosHome_Listar");
			}
			catch
			{
				return View("AccesosHome_Crear");
			}
		}

		public ActionResult AccesosHome_Editar(int codAccesosHome)
		{
			ViewBag.Title = "Editar Acceso desde la Home";

			List<ImagenModelo> imagenesModelo = ServicioSistema<ImagenModelo>.Get(am => am.Vigente && am.MostrarEnAccesoHome).ToList();
			while (imagenesModelo.Count % 4 != 0)
			{
				imagenesModelo.Add(new ImagenModelo());
			}
			ViewBag.ImagenesModelo = imagenesModelo;

			List<AccesorioModelo> accesoriosModelo = ServicioSistema<AccesorioModelo>.Get(am => am.Vigente && am.MostrarEnAccesoHome).ToList();
			while (accesoriosModelo.Count % 4 != 0)
			{
				accesoriosModelo.Add(new AccesorioModelo());
			}
			ViewBag.AccesoriosModelo = accesoriosModelo;

			AccesosHome accesosHome = ServicioSistema<AccesosHome>.GetById(ah => ah.CodAccesosHome == codAccesosHome);

			return View("AccesosHome-Editar", accesosHome);
		}

		[HttpPost]
		public ActionResult AccesosHome_Editar(AccesosHome accesosHomePost)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AccesosHome accesosHome = ServicioSistema<AccesosHome>.GetById(ah => ah.CodAccesosHome == accesosHomePost.CodAccesosHome);

					accesosHome.Titulo = accesosHomePost.Titulo;
					accesosHome.ClaseCSSTitulo = accesosHomePost.ClaseCSSTitulo;
					accesosHome.CodTipoImagen = accesosHomePost.CodTipoImagen;
					accesosHome.CodImagen = accesosHomePost.CodImagen;
					accesosHome.Link = accesosHomePost.Link;
					accesosHome.Orden = accesosHomePost.Orden;
					accesosHome.Vigente = accesosHomePost.Vigente;

					accesosHome = ServicioSistema<AccesosHome>.SaveOrUpdate(accesosHome);

				}

				return RedirectToAction("AccesosHome_Listar");
			}
			catch
			{
				return View("AccesosHome_Crear");
			}
		}

		public ActionResult AccesosHome_Borrar(int codAccesosHome)
		{
			AccesosHome accesosHome = ServicioSistema<AccesosHome>.GetById(ah => ah.CodAccesosHome == codAccesosHome);

			if (accesosHome != null)
				ServicioSistema<AccesosHome>.Delete(accesosHome);

			return RedirectToAction("AccesosHome_Listar");
		}

		public ActionResult VerImagenModelo(int codImagenModelo)
		{
			ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == codImagenModelo);

			if (imagenModelo == null
				|| imagenModelo.Imagen == null)
				return null;

			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(imagenModelo.Imagen, 200, 0)), "image/jpg");
		}

		public ActionResult VerImagenAccesorioModelo(int codImagenAccesorioModelo)
		{
			AccesorioModelo accesorioModelo = ServicioSistema<AccesorioModelo>.GetById(am => am.CodAccesorioModelo == codImagenAccesorioModelo);

			if (accesorioModelo == null
				|| accesorioModelo.Imagen == null)
				return null;

			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(accesorioModelo.Imagen, 200, 0)), "image/jpg");
		}

    }
}