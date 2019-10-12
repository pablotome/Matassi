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
	public class AutoAhorroController : Controller
	{
		// GET: Admin/AutoAhorro
		public ActionResult CargaArchivos()
		{
			ObtenerArchivosCargados();
			return View();
		}

		[HttpPost]
		public ActionResult CargaArchivos(Matassi.Web.Areas.Admin.Models.ArchivosAutoahorro archivosAutoahorro)
		{
			//Proceso Ofertas de Autoahorro
			ArchivoAutoahorro archivoAutoahorro;
			string sql;

			if (archivosAutoahorro.ArchivoOfertas != null && archivosAutoahorro.ArchivoOfertas.ContentLength > 0)
			{
				archivoAutoahorro = new ArchivoAutoahorro();

				archivoAutoahorro.NombreArchivo = archivosAutoahorro.ArchivoOfertas.FileName;
				archivoAutoahorro.FechaAlta = DateTime.Now;
				archivoAutoahorro.Activo = true;
				int i = 0;

				//sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
				//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);

				IList<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
				listaAutoahorro = HelperWeb.Parseo.ParseoOfertas(archivosAutoahorro.ArchivoOfertas.InputStream, ref archivoAutoahorro);

				try
				{
					if (listaAutoahorro != null)
					{
						ServicioSistema<AutoahorroDato>.BeginTransaction();
						archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
						foreach (AutoahorroDato dato in listaAutoahorro)
						{
							dato.ArchivoAutoahorro = archivoAutoahorro;
							ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

							if (++i % 20 == 0)
								ServicioSistema<AutoahorroDato>.Flush();
						}

						archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
						ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

						ServicioSistema<AutoahorroDato>.CommitTransaction();
					}
					else
						throw new ApplicationException("Error en el archivo de ofertas.");
				}
				catch (Exception ex)
				{
					ServicioSistema<AutoahorroDato>.RollbackTransaction();
					throw ex;
				}

			}

			if (archivosAutoahorro.ArchivoEmisiones != null && archivosAutoahorro.ArchivoEmisiones.ContentLength > 0)
			{
				archivoAutoahorro = new ArchivoAutoahorro();

				archivoAutoahorro.NombreArchivo = archivosAutoahorro.ArchivoEmisiones.FileName;
				archivoAutoahorro.FechaAlta = DateTime.Now;
				archivoAutoahorro.Activo = true;
				int i = 0;

				//sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
				//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);

				List<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
				listaAutoahorro = HelperWeb.Parseo.ParseoEmisiones(archivosAutoahorro.ArchivoEmisiones.InputStream, ref archivoAutoahorro);

				try
				{
					ServicioSistema<AutoahorroDato>.BeginTransaction();
					foreach (AutoahorroDato dato in listaAutoahorro)
					{
						dato.ArchivoAutoahorro = archivoAutoahorro;
						ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

						if (++i % 20 == 0)
							ServicioSistema<AutoahorroDato>.Flush();
					}

					archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
					ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

					ServicioSistema<AutoahorroDato>.CommitTransaction();

					//litCantidadEmisiones.Text = string.Format("{0} emisiones cargadas", listaAutoahorro.Count);
				}
				catch (Exception ex)
				{
					ServicioSistema<AutoahorroDato>.RollbackTransaction();
					throw ex;
				}

			}

			if (archivosAutoahorro.ArchivoGanadores != null && archivosAutoahorro.ArchivoGanadores.ContentLength > 0)
			{
				archivoAutoahorro = new ArchivoAutoahorro();

				archivoAutoahorro.NombreArchivo = archivosAutoahorro.ArchivoGanadores.FileName;
				archivoAutoahorro.FechaAlta = DateTime.Now;
				archivoAutoahorro.Activo = true;
				int i = 0;

				//sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
				//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);


				IList<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
				listaAutoahorro = HelperWeb.Parseo.ParseoGanadores(archivosAutoahorro.ArchivoGanadores.InputStream, ref archivoAutoahorro);

				try
				{
					ServicioSistema<AutoahorroDato>.BeginTransaction();
					foreach (AutoahorroDato dato in listaAutoahorro)
					{
						dato.ArchivoAutoahorro = archivoAutoahorro;
						ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

						if (++i % 20 == 0)
							ServicioSistema<AutoahorroDato>.Flush();
					}

					archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
					ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

					ServicioSistema<AutoahorroDato>.CommitTransaction();

					//litCantidadGanadores.Text = string.Format("{0} ganadores cargados", listaAutoahorro.Count);
				}
				catch (Exception ex)
				{
					ServicioSistema<AutoahorroDato>.RollbackTransaction();
					throw ex;
				}

			}

			ObtenerArchivosCargados();


			return View();
		}

		protected void ObtenerArchivosCargados()
		{
			ViewBag.ArchivosOfertas = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroOferta as oferta where oferta.ArchivoAutoahorro = archivo) order by archivo.Acto");
			ViewBag.ArchivosEmisiones = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroEmision as emision where emision.ArchivoAutoahorro = archivo) order by archivo.Acto");
			ViewBag.ArchivosGanadores = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroGanador as ganador where ganador.ArchivoAutoahorro = archivo) order by archivo.Acto");
		}

		public ActionResult EliminarArchivo(int CodArchivo)
		{
			try
			{
				ServicioSistema<ArchivoAutoahorro>.BeginTransaction();
				ServicioSistema<ArchivoAutoahorro>.Delete(aa => aa.CodArchivoAutoahorro == CodArchivo);
				ServicioSistema<ArchivoAutoahorro>.CommitTransaction();
			}
			catch (Exception ex)
			{
				ServicioSistema<ArchivoAutoahorro>.RollbackTransaction();
			}
			finally
			{
				ObtenerArchivosCargados();
			}

			return RedirectToAction("CargaArchivos");
		}

		public ActionResult Planes()
		{
			List<PlanAutoahorro> planes = ServicioSistema<PlanAutoahorro>.GetAll().OrderBy(p => p.Orden).ToList();
			ViewBag.Title = "Planes Autoahorro";
			return View(planes);
		}

		public ActionResult NuevoPlan()
		{
			ViewBag.Title = "Nuevo Plan de Autoahorro";
			return View(new PlanAutoahorro());
		}

		[HttpPost]
		//public ActionResult NuevoPlan(FormCollection collection)
		public ActionResult NuevoPlan(PlanAutoahorro planAutoahorroPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					PlanAutoahorro planAutoahorro = new PlanAutoahorro();

					planAutoahorro.Titulo = planAutoahorroPost.Titulo;
					planAutoahorro.Subtitulo = planAutoahorroPost.Subtitulo;
					planAutoahorro.Orden = planAutoahorroPost.Orden;
					planAutoahorro.Vigente = planAutoahorroPost.Vigente;

					if (Request.Files != null)
					{
						if (Request.Files["ImagenPosteada"] != null
							&& Request.Files["ImagenPosteada"].ContentLength > 0)
						{
							using (var binaryReader = new BinaryReader(Request.Files["ImagenPosteada"].InputStream))
							{
								planAutoahorro.Imagen = binaryReader.ReadBytes(Request.Files["ImagenPosteada"].ContentLength);
							}
						}
					}

					planAutoahorro = ServicioSistema<PlanAutoahorro>.SaveOrUpdate(planAutoahorro);

					return RedirectToAction("Planes");
				}
			}
			catch
			{
				return View();
			}
			return View();
		}

		public ActionResult Cuotas(int codPlanAutoahorro)
		{
			PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == codPlanAutoahorro);

			List<ValorCuotaPlanAutoahorro> valoresCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.Get(vcpa => vcpa.PlanAutoahorro.CodPlanAutoahorro == codPlanAutoahorro).OrderBy(vcpa => vcpa.Orden).ToList();

			if (planAutoahorro != null)
				ViewBag.Title = string.Format("Cuotas del Plan \"{0}\"", planAutoahorro.Titulo);
			else
				ViewBag.Title = "Cuotas del Plan";

			return View(valoresCuotaPlanAutoahorro);
		}

		public ActionResult NuevoValorCuota(int codPlanAutoahorro)
		{
			PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == codPlanAutoahorro);

			if (planAutoahorro != null)
				ViewBag.Title = "Nueva Cuota del Plan \"" + planAutoahorro.Titulo + "\"";
			else
				ViewBag.Title = "Nueva Cuota del Plan";

			return View(new ValorCuotaPlanAutoahorro() { PlanAutoahorro = new PlanAutoahorro() { CodPlanAutoahorro = codPlanAutoahorro } });
		}

		[HttpPost]
		public ActionResult NuevoValorCuota(int codPlanAutoahorro, ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorroPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro = new ValorCuotaPlanAutoahorro();

					valorCuotaPlanAutoahorro.PlanAutoahorro = new PlanAutoahorro() { CodPlanAutoahorro = codPlanAutoahorro };
					valorCuotaPlanAutoahorro.RangoCuota = valorCuotaPlanAutoahorroPost.RangoCuota;
					valorCuotaPlanAutoahorro.Valor = valorCuotaPlanAutoahorroPost.Valor;
					valorCuotaPlanAutoahorro.Orden = valorCuotaPlanAutoahorroPost.Orden;

					valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.SaveOrUpdate(valorCuotaPlanAutoahorro);

					return RedirectToAction("Cuotas", new { codPlanAutoahorro = codPlanAutoahorro });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ValorCuota_Editar(int codValorCuotaPlanAutoahorro)
		{
			ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.GetById(vcpa => vcpa.CodValorCuotaPlanAutoahorro == codValorCuotaPlanAutoahorro);

			if (valorCuotaPlanAutoahorro != null)
				ViewBag.Title = "Editar Cuota del Plan \"" + valorCuotaPlanAutoahorro.PlanAutoahorro.Titulo + "\"";
			else
				ViewBag.Title = "Editar Cuota del Plan";

			return View("ValorCuota-Editar", valorCuotaPlanAutoahorro);
		}

		public ActionResult ValorCuota_Borrar(int codValorCuotaPlanAutoahorro)
		{
			ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.GetById(vcpa => vcpa.CodValorCuotaPlanAutoahorro == codValorCuotaPlanAutoahorro);

			if (valorCuotaPlanAutoahorro != null)
			{
				ServicioSistema<ValorCuotaPlanAutoahorro>.Delete(valorCuotaPlanAutoahorro);
			}

			return RedirectToAction("Cuotas", new { codPlanAutoahorro = valorCuotaPlanAutoahorro.PlanAutoahorro.CodPlanAutoahorro });
		}

		[HttpPost]
		public ActionResult ValorCuota_Editar(int codValorCuotaPlanAutoahorro, ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorroPost)
		{
			ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.GetById(vcpa => vcpa.CodValorCuotaPlanAutoahorro == codValorCuotaPlanAutoahorro);

			try
			{
				if (valorCuotaPlanAutoahorro != null)
				{
					valorCuotaPlanAutoahorro.RangoCuota = valorCuotaPlanAutoahorroPost.RangoCuota;
					valorCuotaPlanAutoahorro.Valor = valorCuotaPlanAutoahorroPost.Valor;
					valorCuotaPlanAutoahorro.Orden = valorCuotaPlanAutoahorroPost.Orden;

					valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.SaveOrUpdate(valorCuotaPlanAutoahorro);

					return RedirectToAction("Cuotas", new { codPlanAutoahorro = valorCuotaPlanAutoahorro.PlanAutoahorro.CodPlanAutoahorro });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		/*[HttpPost]
		public ActionResult NuevoValorCuota(int codPlanAutoahorro, ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorroPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro = new ValorCuotaPlanAutoahorro();

					valorCuotaPlanAutoahorro.PlanAutoahorro = new PlanAutoahorro() { CodPlanAutoahorro = codPlanAutoahorro };
					valorCuotaPlanAutoahorro.RangoCuota = valorCuotaPlanAutoahorroPost.RangoCuota;
					valorCuotaPlanAutoahorro.Valor = valorCuotaPlanAutoahorroPost.Valor;
					valorCuotaPlanAutoahorro.Orden = valorCuotaPlanAutoahorroPost.Orden;

					valorCuotaPlanAutoahorro = ServicioSistema<ValorCuotaPlanAutoahorro>.SaveOrUpdate(valorCuotaPlanAutoahorro);

					return RedirectToAction("Cuotas", new { codPlanAutoahorro = codPlanAutoahorro });
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}*/

		public ActionResult EditarPlan(int codPlanAutoahorro)
		{
			PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == codPlanAutoahorro);

			if (planAutoahorro != null)
				ViewBag.Title = "Edición del Plan \"" + planAutoahorro.Titulo + "\"";
			else
				ViewBag.Title = "Edición del Plan";

			return View(planAutoahorro);
		}

		[HttpPost]
		public ActionResult EditarPlan(PlanAutoahorro planAutoahorroPost)
		{
			try
			{
				if (ModelState.IsValid)
				{
					PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == planAutoahorroPost.CodPlanAutoahorro);

					planAutoahorro.Titulo = planAutoahorroPost.Titulo;
					planAutoahorro.Subtitulo = planAutoahorroPost.Subtitulo;
					planAutoahorro.Orden = planAutoahorroPost.Orden;
					planAutoahorro.Vigente = planAutoahorroPost.Vigente;

					if (Request.Files != null)
					{
						if (Request.Files["ImagenPosteada"] != null
							&& Request.Files["ImagenPosteada"].ContentLength > 0)
						{
							using (var binaryReader = new BinaryReader(Request.Files["ImagenPosteada"].InputStream))
							{
								planAutoahorro.Imagen = binaryReader.ReadBytes(Request.Files["ImagenPosteada"].ContentLength);
							}
						}
					}

					planAutoahorro = ServicioSistema<PlanAutoahorro>.SaveOrUpdate(planAutoahorro);

					return RedirectToAction("Planes");
				}
			}
			catch
			{
				return View();
			}
			return View();
		}

		public ActionResult ListarArchivos(int Tipo)
		{
			List<ArchivoAutoahorro> archivos;

			ViewBag.Tipo = Tipo;

			if (Tipo == 1)
			{
				archivos = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroOferta as oferta where oferta.ArchivoAutoahorro = archivo) order by archivo.Acto").ToList();
				ViewBag.Title = "Archivos de Ofertas";
			}
			else if (Tipo == 2)
			{
				archivos = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroEmision as emision where emision.ArchivoAutoahorro = archivo) order by archivo.Acto").ToList();
				ViewBag.Title = "Archivos de Emisiones";
			}
			else
			{
				archivos = ServicioSistema<ArchivoAutoahorro>.ExecuteSelectHQL<ArchivoAutoahorro>("from ArchivoAutoahorro as archivo where exists (from AutoahorroGanador as ganador where ganador.ArchivoAutoahorro = archivo) order by archivo.Acto").ToList();
				ViewBag.Title = "Archivos de Ganadores";
			}

			return View(archivos);
		}

		public ActionResult Archivo_Crear(int Tipo)
		{
			ViewBag.Tipo = Tipo;

			if (Tipo == 1)
			{
				ViewBag.Title = "Nuevo Archivo de Ofertas";
			}
			else if (Tipo == 2)
			{
				ViewBag.Title = "Nuevo Archivo de Emisiones";
			}
			else
			{
				ViewBag.Title = "Nuevo Archivo de Ganadores";
			}

			return View("NuevoArchivo");
		}

		[HttpPost]
		public ActionResult Archivo_Crear(HttpPostedFileBase[] ArchivosPosteados, int Tipo)
		{
			if (Tipo == 1)
			{
				if (ArchivosPosteados.Length > 0
					&& ArchivosPosteados[0].ContentLength > 0)
				{
					ArchivoAutoahorro archivoAutoahorro = new ArchivoAutoahorro();

					archivoAutoahorro.NombreArchivo = ArchivosPosteados[0].FileName;
					archivoAutoahorro.FechaAlta = DateTime.Now;
					archivoAutoahorro.Activo = true;
					int i = 0;

					//string sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
					//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);

					IList<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
					listaAutoahorro = HelperWeb.Parseo.ParseoOfertas(ArchivosPosteados[0].InputStream, ref archivoAutoahorro);

					DateTime inicio, fin;

					inicio = DateTime.Now;

					try
					{
						if (listaAutoahorro != null)
						{
							ServicioSistema<AutoahorroDato>.BeginTransaction();
							archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
							//foreach (AutoahorroDato dato in listaAutoahorro.Where(aad => ((AutoahorroOferta)aad).Concesionario == "01411").ToList())
							foreach (AutoahorroDato dato in listaAutoahorro)
							{
								dato.ArchivoAutoahorro = archivoAutoahorro;
								ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

								if (++i % 500 == 0)
									ServicioSistema<AutoahorroDato>.Flush();
							}

							archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
							ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

							ServicioSistema<AutoahorroDato>.CommitTransaction();
						}
						else
							throw new ApplicationException("Error en el archivo de ofertas.");
					}
					catch (Exception ex)
					{
						ServicioSistema<AutoahorroDato>.RollbackTransaction();
						throw ex;
					}

					fin = DateTime.Now;

				}

			}
			else if (Tipo == 2)
			{
				if (ArchivosPosteados.Length > 0
					&& ArchivosPosteados[0].ContentLength > 0)
				{
					ArchivoAutoahorro archivoAutoahorro = new ArchivoAutoahorro();

					archivoAutoahorro.NombreArchivo = ArchivosPosteados[0].FileName;
					archivoAutoahorro.FechaAlta = DateTime.Now;
					archivoAutoahorro.Activo = true;
					int i = 0;

					//string sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
					//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);

					List<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
					listaAutoahorro = HelperWeb.Parseo.ParseoEmisiones(ArchivosPosteados[0].InputStream, ref archivoAutoahorro);

					try
					{
						if (listaAutoahorro != null)
						{
							ServicioSistema<AutoahorroDato>.BeginTransaction();
							foreach (AutoahorroDato dato in listaAutoahorro)
							{
								dato.ArchivoAutoahorro = archivoAutoahorro;
								ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

								if (++i % 500 == 0)
									ServicioSistema<AutoahorroDato>.Flush();
							}

							archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
							ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

							ServicioSistema<AutoahorroDato>.CommitTransaction();
						}
						else
							throw new ApplicationException("Error en el archivo de emisiones.");
					}
					catch (Exception ex)
					{
						ServicioSistema<AutoahorroDato>.RollbackTransaction();
						throw ex;
					}

				}
			}
			else if (Tipo == 3)
			{
				if (ArchivosPosteados.Length > 0
					&& ArchivosPosteados[0].ContentLength > 0)
				{
					ArchivoAutoahorro archivoAutoahorro = new ArchivoAutoahorro();

					archivoAutoahorro.NombreArchivo = ArchivosPosteados[0].FileName;
					archivoAutoahorro.FechaAlta = DateTime.Now;
					archivoAutoahorro.Activo = true;
					int i = 0;

					//string sql = string.Format("exec BuscarArchivoYBorrarlo @archivo = '{0}'", archivoAutoahorro.NombreArchivo);
					//ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);


					IList<AutoahorroDato> listaAutoahorro = new List<AutoahorroDato>();
					listaAutoahorro = HelperWeb.Parseo.ParseoGanadores(ArchivosPosteados[0].InputStream, ref archivoAutoahorro);

					try
					{
						ServicioSistema<AutoahorroDato>.BeginTransaction();
						foreach (AutoahorroDato dato in listaAutoahorro.Where(aad => ((AutoahorroGanador)aad).Concesionario == "01411").ToList())
						{
							dato.ArchivoAutoahorro = archivoAutoahorro;
							ServicioSistema<AutoahorroDato>.SaveOrUpdateWithoutFlush(dato);

							if (++i % 500 == 0)
								ServicioSistema<AutoahorroDato>.Flush();
						}

						archivoAutoahorro.CantidadRegistros = listaAutoahorro.Count;
						ServicioSistema<ArchivoAutoahorro>.SaveOrUpdate(archivoAutoahorro);

						ServicioSistema<AutoahorroDato>.CommitTransaction();

						//litCantidadGanadores.Text = string.Format("{0} ganadores cargados", listaAutoahorro.Count);
					}
					catch (Exception ex)
					{
						ServicioSistema<AutoahorroDato>.RollbackTransaction();
						throw ex;
					}
				}
			}

			return RedirectToAction("ListarArchivos", new { Tipo = Tipo });
		}

		public ActionResult Archivo_Borrar(int codArchivoAutoahorro, int Tipo)
		{
			try
			{
				ServicioSistema<ArchivoAutoahorro>.BeginTransaction();

				string sql = string.Format("exec EliminarArchivo @CodArchivoAutoahorro = {0}", codArchivoAutoahorro);
				ServicioSistema<ArchivoAutoahorro>.ExecuteSQLQueryUniqueResult(sql);

				ServicioSistema<ArchivoAutoahorro>.CommitTransaction();

				ModelState.Clear();

				return RedirectToAction("ListarArchivos", new { Tipo = Tipo });
			}
			catch (Exception ex)
			{
				ServicioSistema<ArchivoAutoahorro>.RollbackTransaction();
				throw ex;
			}
		}
	}
}