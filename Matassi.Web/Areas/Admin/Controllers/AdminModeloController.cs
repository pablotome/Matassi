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
	public class AdminModeloController : Controller
	{
		public ActionResult Index()
		{
			List<Modelo> modelos = ServicioSistema<Modelo>.GetAll().ToList();
			return View("Modelo-Lista", modelos);
		}

		public ActionResult Modelo_Lista()
		{
			return RedirectToAction("Index");
		}

		public ActionResult Modelo_Crear()
		{
			return View("Modelo-Crear");
		}

		[HttpPost]
		public ActionResult Modelo_Crear(Modelo modeloForm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Modelo modelo = new Modelo();

					modelo.Nombre = modeloForm.Nombre;
					modelo.NombreClave = modelo.GenerateSlug();
					modelo.Bajada = modeloForm.Bajada;
					modelo.Vigente = modeloForm.Vigente;

					if (Request.Files != null)
					{
						if (Request.Files["ImagenAccesoriosPosteada"] != null
							&& Request.Files["ImagenAccesoriosPosteada"].ContentLength > 0)
						{
							using (var binaryReader = new BinaryReader(Request.Files["ImagenAccesoriosPosteada"].InputStream))
							{
								modelo.ImagenAccesorios = binaryReader.ReadBytes(Request.Files["ImagenAccesoriosPosteada"].ContentLength);
							}
						}

						if (Request.Files["ImagenContactoPosteada"] != null
							&& Request.Files["ImagenContactoPosteada"].ContentLength > 0)
						{
							using (var binaryReader = new BinaryReader(Request.Files["ImagenContactoPosteada"].InputStream))
							{
								modelo.ImagenContacto = binaryReader.ReadBytes(Request.Files["ImagenContactoPosteada"].ContentLength);
							}
						}
					}

					modelo = ServicioSistema<Modelo>.SaveOrUpdate(modelo);

				}

				return RedirectToAction("Index");
			}
			catch
			{
				return View("Modelo-Crear");
			}
		}

		public ActionResult Modelo_Editar(int id)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == id);

			return View("Modelo-Editar", modelo);
		}

		[HttpPost]
		public ActionResult Modelo_Editar(int id, Modelo modeloForm)
		{
			try
			{
				Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == id) as Modelo;

				modelo.Nombre = modeloForm.Nombre;
				modelo.NombreClave = modelo.GenerateSlug();
				modelo.Bajada = modeloForm.Bajada;
				modelo.Vigente = modeloForm.Vigente;

				if (Request.Files != null)
				{
					if (Request.Files["ImagenAccesoriosPosteada"] != null
						&& Request.Files["ImagenAccesoriosPosteada"].ContentLength > 0)
					{
						using (var binaryReader = new BinaryReader(Request.Files["ImagenAccesoriosPosteada"].InputStream))
						{
							modelo.ImagenAccesorios = binaryReader.ReadBytes(Request.Files["ImagenAccesoriosPosteada"].ContentLength);
						}
					}

					if (Request.Files["ImagenContactoPosteada"] != null
						&& Request.Files["ImagenContactoPosteada"].ContentLength > 0)
					{
						using (var binaryReader = new BinaryReader(Request.Files["ImagenContactoPosteada"].InputStream))
						{
							modelo.ImagenContacto = binaryReader.ReadBytes(Request.Files["ImagenContactoPosteada"].ContentLength);
						}
					}
				}

				modelo = ServicioSistema<Modelo>.SaveOrUpdate(modelo);

				ModelState.Clear();

				return RedirectToAction("Index");
			}
			catch
			{
				return View("Modelo-Editar");
			}
		}




		public ActionResult Version_Lista(int codModelo)
		{
			List<VersionModelo> versiones = ServicioSistema<VersionModelo>.Get(vm => vm.Modelo.CodModelo == codModelo).ToList();

			if (versiones.Count > 0)
				ViewBag.Title = "Versiones de " + versiones[0].Modelo.Nombre;
			else
				ViewBag.Title = "Versiones";

			return View("Version-Lista", versiones);
		}

		public ActionResult Version_Crear(int codModelo)
		{
			Modelo m = ServicioSistema<Modelo>.GetById(mod => mod.CodModelo == codModelo);
			VersionModelo vm = new VersionModelo();
			vm.Modelo = new Modelo() { CodModelo = codModelo };

			if (m != null)
				ViewBag.Title = "Nueva versión de " + m.Nombre;
			else
				ViewBag.Title = "Nueva versión";

			return View("Version-Crear", vm);
		}

		[HttpPost]
		public ActionResult Version_Crear(VersionModelo vm)
		{
			try
			{
				//if (ModelState.IsValid)
				{
					VersionModelo vmActualizar = new VersionModelo();

					if (vm.ImagenPosteada != null)
					{
						using (var binaryReader = new BinaryReader(vm.ImagenPosteada.InputStream))
						{
							vmActualizar.Imagen = binaryReader.ReadBytes(vm.ImagenPosteada.ContentLength);
						}
					}

					vmActualizar.Nombre = vm.Nombre;
					vmActualizar.Bajada = vm.Bajada;
					//vmActualizar.Modelo = new Modelo() { CodModelo = int.Parse(Request.Form["Modelo.CodModelo"]) };
					vmActualizar.Modelo = new Modelo() { CodModelo = vm.Modelo.CodModelo };

					vmActualizar = ServicioSistema<VersionModelo>.SaveOrUpdate(vmActualizar);

					return RedirectToAction("Version_Lista", new { codModelo = vmActualizar.Modelo.CodModelo });
				}

				return RedirectToAction("Modelo_Lista");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Version_Editar(int idVersion)
		{
			/*List<CaracteristicaVersion> cv = ServicioSistema<CaracteristicaVersion>.GetAll().ToList();
			CaracteristicaModelo cm2 = ServicioSistema<CaracteristicaModelo>.GetById(vm2a => vm2a.CodCaracteristicaModelo == 1);*/
			VersionModelo versionModelo = ServicioSistema<VersionModelo>.GetById(version => version.CodVersionModelo == idVersion);

			if (versionModelo != null)
				ViewBag.Title = "Versión de Modelo " + versionModelo.Modelo.Nombre;
			else
				ViewBag.Title = "Versión de Modelo";

			return View("Version-Editar", versionModelo);
		}

		[HttpPost]
		public ActionResult Version_Editar(VersionModelo vm)
		{
			VersionModelo vmActualizar = ServicioSistema<VersionModelo>.GetById(vma => vma.CodVersionModelo == vm.CodVersionModelo);

			if (vm.ImagenPosteada != null)
			{
				using (var binaryReader = new BinaryReader(vm.ImagenPosteada.InputStream))
				{
					vmActualizar.Imagen = binaryReader.ReadBytes(vm.ImagenPosteada.ContentLength);
				}
			}

			vmActualizar.Nombre = vm.Nombre;
			vmActualizar.Bajada = vm.Bajada;

			vmActualizar = ServicioSistema<VersionModelo>.SaveOrUpdate(vmActualizar);

			return RedirectToAction("Version_Lista", new { codModelo = vmActualizar.Modelo.CodModelo });
		}





		public ActionResult Imagen_Lista(int codModelo)
		{
			List<ImagenModelo> imagenesModelo = ServicioSistema<ImagenModelo>.Get(im => im.Modelo.CodModelo == codModelo).ToList();

			if (imagenesModelo.Count > 0)
				ViewBag.Title = "Imágenes del modelo " + imagenesModelo[0].Modelo.Nombre;
			else
				ViewBag.Title = "Imágenes del Modelo";

			return View("Imagen-Lista", imagenesModelo);
		}

		public ActionResult Imagen_Crear(int codModelo)
		{
			Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == codModelo);
			ImagenModelo imagenModelo = new ImagenModelo() { Modelo = new Modelo() { CodModelo = codModelo } };

			if (modelo != null)
				ViewBag.Title = "Nueva imagen del " + modelo.Nombre;
			else
				ViewBag.Title = "Imágenes del Modelo";

			return View("Imagen-Crear", imagenModelo);
		}

		[HttpPost]
		public ActionResult Imagen_Crear(ImagenModelo im)
		{
			//if (ModelState.IsValid)
			{
				for (int i = 0; i < Request.Files.Count; i++)
				{
					ImagenModelo imActualizar = new ImagenModelo();

					if (im.ImagenPosteada != null)
					{
						using (var binaryReader = new BinaryReader(Request.Files[i].InputStream))
						{
							imActualizar.Imagen = binaryReader.ReadBytes(Request.Files[i].ContentLength);
						}
					}

					imActualizar.Nombre = im.Nombre;
					imActualizar.ClaseCSSTitulo = im.ClaseCSSTitulo;
					imActualizar.Bajada = im.Bajada;
					imActualizar.MostrarEnHome = im.MostrarEnHome;
					imActualizar.MostrarEnAccesoHome = im.MostrarEnAccesoHome;
					imActualizar.Vigente = im.Vigente;
					imActualizar.Modelo = new Modelo() { CodModelo = int.Parse(Request.Form["Modelo.CodModelo"]) };

					imActualizar = ServicioSistema<ImagenModelo>.SaveOrUpdate(imActualizar);
				}

				return RedirectToAction("Imagen_Lista", new { codModelo = im.Modelo.CodModelo });
			}
			return View();
		}

		public ActionResult Imagen_Editar(int CodImagenModelo, int CodModelo)
		{
			ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == CodImagenModelo);
			return View("Imagen-Editar", imagenModelo);
		}

		[HttpPost]
		public ActionResult Imagen_Editar(int codImagenModelo, int codModelo, ImagenModelo imagenModeloPost)
		{
			try
			{
				ServicioSistema<ImagenModelo>.BeginTransaction();

				ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == codImagenModelo);

				if (imagenModelo != null)
				{
					imagenModelo.Nombre = imagenModeloPost.Nombre;
					imagenModelo.ClaseCSSTitulo = imagenModeloPost.ClaseCSSTitulo;
					imagenModelo.Bajada = imagenModeloPost.Bajada;
					imagenModelo.MostrarEnHome = imagenModeloPost.MostrarEnHome;
					imagenModelo.MostrarEnAccesoHome = imagenModeloPost.MostrarEnAccesoHome;
					imagenModelo.Vigente = imagenModeloPost.Vigente;
					imagenModelo = ServicioSistema<ImagenModelo>.SaveOrUpdate(imagenModelo);

				}

				ServicioSistema<ImagenModelo>.CommitTransaction();
				return RedirectToAction("Imagen_Lista", new { codModelo = codModelo });
			}
			catch (Exception ex)
			{
				ServicioSistema<ImagenModelo>.RollbackTransaction();
				throw ex;
			}
		}

		public ActionResult Imagen_Borrar(int CodImagenModelo, int CodModelo)
		{
			ServicioSistema<ImagenModelo>.Delete(im => im.CodImagenModelo == CodImagenModelo);
			return RedirectToAction("Imagen_Lista", new { codModelo = CodModelo });
		}

		public ActionResult VerImagenModelo(int CodImagenModelo)
		{
			ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == CodImagenModelo);
			//return File(imagenModelo.Imagen, "image/jpg");

			if (imagenModelo == null
				|| imagenModelo.Imagen == null)
				return null;

			return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(imagenModelo.Imagen, 150, 0)), "image/jpg");

			//return File(imagenModelo, "image/jpg");
		}






		public ActionResult Caracteristica_Lista(int codVersion)
		{
			VersionModelo vm = ServicioSistema<VersionModelo>.GetById(v => v.CodVersionModelo == codVersion);
			dynamic caracteristicasModelo = new ExpandoObject();

			if (vm != null)
				ViewBag.Title = "Características de la versión " + vm.Nombre + " del modelo " + vm.Modelo.Nombre;
			else
				ViewBag.Title = "Características de la versión";

			caracteristicasModelo.Caracteristicas = vm.Caracteristicas.OrderBy(c => c.DesCaracteristicaModelo);
			caracteristicasModelo.VersionModelo = vm;

			ModelState.Clear();

			return View("Caracteristica-Lista", caracteristicasModelo);
		}

		public ActionResult Caracteristica_Crear(int codVersionModelo)
		{
			VersionModelo vm = ServicioSistema<VersionModelo>.GetById(v => v.CodVersionModelo == codVersionModelo);

			if (vm != null)
				ViewBag.Title = "Nueva características de la versión " + vm.Nombre + " del modelo " + vm.Modelo.Nombre;
			else
				ViewBag.Title = "Nueva características de la versión";

			List<VersionModelo> versiones = new List<VersionModelo>();
			versiones.Add(vm);

			CaracteristicaModelo cm = new CaracteristicaModelo() { Versiones = versiones };

			return View("Caracteristica-Crear", cm);
		}

		[HttpPost]
		public ActionResult Caracteristica_Crear(CaracteristicaModelo caracteristicaModeloPost)
		{
			try
			{
				ServicioSistema<CaracteristicaModelo>.BeginTransaction();
				CaracteristicaModelo caracteristicaModelo = new CaracteristicaModelo();
				dynamic caracteristicasModelo = new ExpandoObject();

				//Esta es la versión del modelo a la que se agregará la caractrística
				VersionModelo versionModelo = ServicioSistema<VersionModelo>.GetById(v => v.CodVersionModelo == caracteristicaModeloPost.Versiones[0].CodVersionModelo);

				//1º busco si existe la carcterística
				CaracteristicaModelo caracteristicaExistente = ServicioSistema<CaracteristicaModelo>.Get(cm => cm.DesCaracteristicaModelo.ToLower() == caracteristicaModeloPost.DesCaracteristicaModelo.ToLower()).FirstOrDefault();

				//Si existe
				if (caracteristicaExistente != null)
				{
					//Si el modelo actual no contiene esa característica, la asigno
					if (!versionModelo.Caracteristicas.Contains<CaracteristicaModelo>(caracteristicaExistente))
					{
						versionModelo.Caracteristicas.Add(caracteristicaExistente);
						versionModelo = ServicioSistema<VersionModelo>.SaveOrUpdate(versionModelo);
					}
				}
				//Si no existe la característica, la asigno
				else
				{
					CaracteristicaModelo caracteristicaNueva = new CaracteristicaModelo();
					caracteristicaNueva.DesCaracteristicaModelo = caracteristicaModeloPost.DesCaracteristicaModelo;
					caracteristicaNueva = ServicioSistema<CaracteristicaModelo>.SaveOrUpdate(caracteristicaNueva);

					versionModelo.Caracteristicas.Add(caracteristicaNueva);
					versionModelo = ServicioSistema<VersionModelo>.SaveOrUpdate(versionModelo);
				}

				caracteristicasModelo.Caracteristicas = versionModelo.Caracteristicas;
				caracteristicasModelo.VersionModelo = versionModelo;

				ModelState.Clear();

				ServicioSistema<CaracteristicaModelo>.CommitTransaction();
				//return View("Caracteristica-Lista", caracteristicasModelo);
				return RedirectToAction("Caracteristica_Lista", new { codVersion = versionModelo.CodVersionModelo });
			}
			catch (Exception ex)
			{
				ServicioSistema<CaracteristicaModelo>.RollbackTransaction();
				throw ex;
			}
		}

		public ActionResult Caracteristica_Editar(int codCaracteristicaModelo, int codVersionModelo)
		{
			CaracteristicaModelo caracteristicaModelo = ServicioSistema<CaracteristicaModelo>.GetById(cm => cm.CodCaracteristicaModelo == codCaracteristicaModelo);
			List<CaracteristicaVersion> versionesQueUsanEstaCaracteristica = ServicioSistema<CaracteristicaVersion>.Get(cv => cv.CaracteristicaModelo.CodCaracteristicaModelo == codCaracteristicaModelo).ToList();
			VersionModelo versionModelo = ServicioSistema<VersionModelo>.GetById(vm => vm.CodVersionModelo == codVersionModelo);

			CaracteristicaVersion caracteristicarepetida = null;
			foreach (CaracteristicaVersion caracteristicaVersion in versionesQueUsanEstaCaracteristica)
			{
				if (caracteristicaVersion.VersionModelo.CodVersionModelo == codVersionModelo)
					caracteristicarepetida = caracteristicaVersion;
			}

			if (caracteristicarepetida != null)
				versionesQueUsanEstaCaracteristica.Remove(caracteristicarepetida);


			if (caracteristicaModelo != null)
				ViewBag.Title = "Caracteristica \"" + caracteristicaModelo.DesCaracteristicaModelo + "\"";
			else
				ViewBag.Title = "Caracteristica de Modelo";

			ViewBag.VersionesQueUsanEstaCaracteristica = versionesQueUsanEstaCaracteristica;
			ViewBag.VersionModeloActual = versionModelo;

			return View("Caracteristica-Editar", caracteristicaModelo);
		}

		[HttpPost]
		public ActionResult Caracteristica_Editar(int codCaracteristicaModelo, int codVersionModelo, CaracteristicaModelo caracteristicaModeloPost)
		{
			CaracteristicaModelo caracteristicaModelo = ServicioSistema<CaracteristicaModelo>.GetById(cm => cm.CodCaracteristicaModelo == codCaracteristicaModelo);

			caracteristicaModelo.DesCaracteristicaModelo = caracteristicaModeloPost.DesCaracteristicaModelo;

			caracteristicaModelo = ServicioSistema<CaracteristicaModelo>.SaveOrUpdate(caracteristicaModelo);

			ModelState.Clear();

			return RedirectToAction("Caracteristica_Lista", new { codVersion = codVersionModelo });
		}

		public ActionResult Caracteristica_Eliminar(int codCaracteristicaModelo, int codVersionModelo)
		{
			try
			{
				ServicioSistema<CaracteristicaVersion>.BeginTransaction();

				ServicioSistema<CaracteristicaVersion>.Delete(cv => cv.CaracteristicaModelo.CodCaracteristicaModelo == codCaracteristicaModelo && cv.VersionModelo.CodVersionModelo == codVersionModelo);

				ServicioSistema<CaracteristicaVersion>.CommitTransaction();

				ModelState.Clear();

				return RedirectToAction("Caracteristica_Lista", new { codVersion = codVersionModelo });
			}
			catch (Exception ex)
			{
				ServicioSistema<CaracteristicaVersion>.RollbackTransaction();
				throw ex;
			}
		}







		public ActionResult Accesorio_Lista(int codModelo)
		{
			List<AccesorioModelo> accesoriosModelo = ServicioSistema<AccesorioModelo>.Get(am => am.Modelo.CodModelo == codModelo).ToList();

			if (accesoriosModelo.Count > 0)
				ViewBag.Title = "Accesorios del modelo " + accesoriosModelo[0].Modelo.Nombre;
			else
				ViewBag.Title = "Accesorios del Modelo";

			return View("Accesorio-Lista", accesoriosModelo);
		}

		public ActionResult Accesorio_Crear(int codModelo)
		{
			AccesorioModelo am = new AccesorioModelo() { Modelo = new Modelo() { CodModelo = codModelo } };
			return View("Accesorio-Crear", am);
		}

		[HttpPost]
		public ActionResult Accesorio_Crear(AccesorioModelo am)
		{
			try
			{
				ServicioSistema<AccesorioModelo>.BeginTransaction();
				AccesorioModelo accesorio = new AccesorioModelo();

				if (am.ImagenPosteada != null)
				{
					using (var binaryReader = new BinaryReader(am.ImagenPosteada.InputStream))
					{
						accesorio.Imagen = binaryReader.ReadBytes(am.ImagenPosteada.ContentLength);
					}
				}

				accesorio.Titulo = am.Titulo;
				accesorio.Descripcion = am.Descripcion;
				accesorio.MostrarEnAccesoHome = am.MostrarEnAccesoHome;
				accesorio.Vigente = am.Vigente;
				accesorio.Modelo = new Modelo() { CodModelo = am.Modelo.CodModelo };

				accesorio = ServicioSistema<AccesorioModelo>.SaveOrUpdate(accesorio);

				ModelState.Clear();

				ServicioSistema<AccesorioModelo>.CommitTransaction();

				return RedirectToAction("Accesorio_Lista", new { codModelo = accesorio.Modelo.CodModelo });
			}
			catch (Exception ex)
			{
				ServicioSistema<AccesorioModelo>.RollbackTransaction();
				throw ex;
			}
		}

		public ActionResult Accesorio_Editar(int id)
		{
			AccesorioModelo accesorioModelo = ServicioSistema<AccesorioModelo>.GetById(accesorio => accesorio.CodAccesorioModelo == id);

			if (accesorioModelo != null)
				ViewBag.Title = "Accesorios del modelo " + accesorioModelo.Modelo.Nombre;
			else
				ViewBag.Title = "Accesorios del Modelo";

			return View("Accesorio-Editar", accesorioModelo);
		}

		[HttpPost]
		public ActionResult Accesorio_Editar(int id, AccesorioModelo am)
		{
			AccesorioModelo amActualizar = ServicioSistema<AccesorioModelo>.GetById(ama => ama.CodAccesorioModelo == id);

			if (am.ImagenPosteada != null)
			{
				using (var binaryReader = new BinaryReader(am.ImagenPosteada.InputStream))
				{
					amActualizar.Imagen = binaryReader.ReadBytes(am.ImagenPosteada.ContentLength);
				}
			}

			amActualizar.Titulo = am.Titulo;
			amActualizar.Descripcion = am.Descripcion;
			amActualizar.MostrarEnAccesoHome = am.MostrarEnAccesoHome;
			amActualizar.Vigente = am.Vigente;
			amActualizar.Modelo = new Modelo() { CodModelo = am.Modelo.CodModelo };

			amActualizar = ServicioSistema<AccesorioModelo>.SaveOrUpdate(amActualizar);

			ModelState.Clear();

			return RedirectToAction("Accesorio_Lista", new { codModelo = amActualizar.Modelo.CodModelo });
		}

		public ActionResult Accesorio_Borrar(int codAccesorioModelo, int codModelo)
		{
			try
			{
				ServicioSistema<Modelo>.BeginTransaction();

				Modelo modelo = ServicioSistema<Modelo>.GetById(m => m.CodModelo == codModelo);

				AccesorioModelo accesorioModelo = modelo.Accesorios.FirstOrDefault<AccesorioModelo>(am => am.CodAccesorioModelo == codAccesorioModelo);

				if (accesorioModelo != null)
				{
					modelo.Accesorios.Remove(accesorioModelo);

					ServicioSistema<AccesorioModelo>.Delete(am => am.CodAccesorioModelo == codAccesorioModelo);

					ServicioSistema<Modelo>.SaveOrUpdate(modelo);
				}

				ServicioSistema<Modelo>.CommitTransaction();

				ModelState.Clear();

				return RedirectToAction("Accesorio_Lista", new { codModelo = codModelo });
				//return Url.Action("Accesorios", new { codModelo = codModelo });
				//return Accesorios(codModelo);
			}
			catch (Exception ex)
			{
				ServicioSistema<Modelo>.RollbackTransaction();
				//ServicioSistema<AccesorioModelo>.RollbackTransaction();
				throw ex;
			}
		}



		public ActionResult Caracteristica_Crear_Muchas(int codVersionModelo)
		{
			VersionModelo vm = ServicioSistema<VersionModelo>.GetById(v => v.CodVersionModelo == codVersionModelo);

			if (vm != null)
				ViewBag.Title = "Muchas Nuevas características de la versión " + vm.Nombre + " del modelo " + vm.Modelo.Nombre;
			else
				ViewBag.Title = "Muchas Nuevas características de la versión";

			List<VersionModelo> versiones = new List<VersionModelo>();
			versiones.Add(vm);

			CaracteristicaModelo cm = new CaracteristicaModelo() { Versiones = versiones };

			return View("Caracteristica-Crear-Muchas", cm);
		}

		[HttpPost]
		public ActionResult Caracteristica_Crear_Muchas(CaracteristicaModelo caracteristicaModeloPost)
		{
			try
			{
				ServicioSistema<VersionModelo>.BeginTransaction();

				//string caracteristicas = Request.Form["txtCaractristicas"];
				//int codVersionModelo = int.Parse(Request.Form["CodVersionModelo"]);

				VersionModelo vm = ServicioSistema<VersionModelo>.GetById(v => v.CodVersionModelo == caracteristicaModeloPost.Versiones[0].CodVersionModelo);

				CaracteristicaModelo cm;

				caracteristicaModeloPost.MuchasCarcteristicas.Split('\n').ToList().ForEach(caracteristica =>
				{
					caracteristica = caracteristica.Trim();

					if (caracteristica != string.Empty)
					{
						cm = ServicioSistema<CaracteristicaModelo>.Get(c => c.DesCaracteristicaModelo.ToLower() == caracteristica.ToLower()).FirstOrDefault();

						if (cm == null)
						{
							cm = new CaracteristicaModelo() { DesCaracteristicaModelo = caracteristica };
							cm = ServicioSistema<CaracteristicaModelo>.SaveOrUpdate(cm);
						}

						if (!vm.Caracteristicas.Contains<CaracteristicaModelo>(cm))
						{
							vm.Caracteristicas.Add(cm);
							vm = ServicioSistema<VersionModelo>.SaveOrUpdate(vm);
						}
					}
				});

				ServicioSistema<VersionModelo>.CommitTransaction();

				return RedirectToAction("Caracteristica_Lista", new { codVersion = vm.CodVersionModelo });
			}
			catch (Exception ex)
			{
				ServicioSistema<VersionModelo>.RollbackTransaction();
			}

			return null;
		}
	}
}
