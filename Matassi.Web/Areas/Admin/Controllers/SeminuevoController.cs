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
	public class SeminuevoController : Controller
	{
		public ActionResult Seminuevo_Lista()
		{
			List<Seminuevo> seminuevos = ServicioSistema<Seminuevo>.GetAll().OrderBy(s => s.Orden).ToList();
			return View("Seminuevo-Lista", seminuevos);
		}

		public ActionResult Seminuevo_Crear()
		{
			return View("Seminuevo-Crear");
		}

		[HttpPost]
		public ActionResult Seminuevo_Crear(Seminuevo seminuevoForm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Seminuevo seminuevo = new Seminuevo();

					seminuevo.Modelo = seminuevoForm.Modelo;
					seminuevo.Anio = seminuevoForm.Anio;
					seminuevo.Kilometraje = seminuevoForm.Kilometraje;
					seminuevo.Precio = seminuevoForm.Precio;
					seminuevo.Combustible = seminuevoForm.Combustible;
					seminuevo.Color = seminuevoForm.Color;
					seminuevo.Vendedor = seminuevoForm.Vendedor;
					seminuevo.Comentarios = seminuevoForm.Comentarios;
					seminuevo.Transmision = seminuevoForm.Transmision;
					seminuevo.CantidadPuertas = seminuevoForm.CantidadPuertas;

					if (Request.Files != null)
					{
						if (Request.Files["ImagenPosteada"] != null
							&& Request.Files["ImagenPosteada"].ContentLength > 0)
						{
							using (var binaryReader = new BinaryReader(Request.Files["ImagenPosteada"].InputStream))
							{
								seminuevo.Imagen = binaryReader.ReadBytes(Request.Files["ImagenPosteada"].ContentLength);
							}
						}
					}

					seminuevo.Orden = seminuevoForm.Orden;
					seminuevo.Publicado = seminuevoForm.Publicado;

					seminuevo = ServicioSistema<Seminuevo>.SaveOrUpdate(seminuevo);

					return RedirectToAction("Seminuevo_Lista");
				}
				/*else
				{
					foreach (ModelState modelState in ViewData.ModelState.Values)
					{
						foreach (ModelError error in modelState.Errors)
						{
							//ModelState.AddModelError(string.Empty, error.ErrorMessage);
						}
					}
				}*/

				return View("Seminuevo-Crear");
			}
			catch
			{
				return View("Seminuevo-Crear");
			}
		}

		public ActionResult Seminuevo_Editar(int codSeminuevo)
		{
			Seminuevo seminuevo = ServicioSistema<Seminuevo>.GetById(m => m.CodSeminuevo == codSeminuevo);

			return View("Seminuevo-Editar", seminuevo);
		}

		[HttpPost]
		public ActionResult Seminuevo_Editar(int codSeminuevo, Seminuevo seminuevoForm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Seminuevo seminuevo = ServicioSistema<Seminuevo>.GetById(s => s.CodSeminuevo == codSeminuevo);

					if (seminuevo != null)
					{ 
						seminuevo.Modelo = seminuevoForm.Modelo;
						seminuevo.Anio = seminuevoForm.Anio;
						seminuevo.Kilometraje = seminuevoForm.Kilometraje;
						seminuevo.Precio = seminuevoForm.Precio;
						seminuevo.Combustible = seminuevoForm.Combustible;
						seminuevo.Color = seminuevoForm.Color;
						seminuevo.Vendedor = seminuevoForm.Vendedor;
						seminuevo.Comentarios = seminuevoForm.Comentarios;
						seminuevo.Transmision = seminuevoForm.Transmision;
						seminuevo.CantidadPuertas = seminuevoForm.CantidadPuertas;

						if (Request.Files != null)
						{
							if (Request.Files["ImagenPosteada"] != null
								&& Request.Files["ImagenPosteada"].ContentLength > 0)
							{
								using (var binaryReader = new BinaryReader(Request.Files["ImagenPosteada"].InputStream))
								{
									seminuevo.Imagen = binaryReader.ReadBytes(Request.Files["ImagenPosteada"].ContentLength);
								}
							}
						}

						seminuevo.Orden = seminuevoForm.Orden;
						seminuevo.Publicado = seminuevoForm.Publicado;

						seminuevo = ServicioSistema<Seminuevo>.SaveOrUpdate(seminuevo);
					}

				}

				return RedirectToAction("Seminuevo_Lista");
			}
			catch
			{
				return View("Seminuevo-Lista");
			}
		}

		public ActionResult Seminuevo_Borrar(int codSeminuevo)
		{
			ServicioSistema<Seminuevo>.Delete(s => s.CodSeminuevo == codSeminuevo);
			return RedirectToAction("Seminuevo_Lista");
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
