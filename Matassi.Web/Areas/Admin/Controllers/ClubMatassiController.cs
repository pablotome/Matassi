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
	[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
	public class ClubMatassiController : Controller
	{
		public ActionResult ClubMatassiCatalogo_Lista()
		{
			ViewBag.Title = "Catálogo de Puntos";

			List<Matassi.Dominio.ClubMatassiCatalogo> catalogoPremios = ServicioSistema<ClubMatassiCatalogo>.GetAll().OrderBy(cmc => cmc.CantidadPuntos).ToList();

			return View("ClubMatassiCatalogo-Lista", catalogoPremios);
		}

		public ActionResult ClubMatassiCatalogo_Crear()
		{
			ViewBag.Title = "Catálogo de Puntos - Nuevo Premio";

			ClubMatassiCatalogo clubMatassiCatalogo = new ClubMatassiCatalogo();

			return View("ClubMatassiCatalogo-Crear", clubMatassiCatalogo);
		}

		[HttpPost]
		public ActionResult ClubMatassiCatalogo_Crear(ClubMatassiCatalogo clubMatassiCatalogoPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ClubMatassiCatalogo clubMatassiCatalogo = new ClubMatassiCatalogo();

					clubMatassiCatalogo.TituloPremio = clubMatassiCatalogoPost.TituloPremio;
					clubMatassiCatalogo.DescripcionPremio = clubMatassiCatalogoPost.DescripcionPremio;
					clubMatassiCatalogo.CantidadPuntos = clubMatassiCatalogoPost.CantidadPuntos;

					clubMatassiCatalogo = ServicioSistema<ClubMatassiCatalogo>.SaveOrUpdate(clubMatassiCatalogo);

					return RedirectToAction("ClubMatassiCatalogo_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ClubMatassiCatalogo_Editar(int codClubMatassiCatalogo)
		{
			ViewBag.Title = "Catálogo de Puntos - Editar Premio";

			ClubMatassiCatalogo clubMatassiCatalogo = ServicioSistema<ClubMatassiCatalogo>.GetById(cmc => cmc.CodClubMatassiCatalogo == codClubMatassiCatalogo);

			if (clubMatassiCatalogo != null)
				return View("ClubMatassiCatalogo-Editar", clubMatassiCatalogo);

			return View();
		}

		[HttpPost]
		public ActionResult ClubMatassiCatalogo_Editar(int codClubMatassiCatalogo, ClubMatassiCatalogo clubMatassiCatalogoPost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ClubMatassiCatalogo clubMatassiCatalogo = ServicioSistema<ClubMatassiCatalogo>.GetById(cmc => cmc.CodClubMatassiCatalogo == codClubMatassiCatalogo);

					if (clubMatassiCatalogo != null)
					{
						clubMatassiCatalogo.TituloPremio = clubMatassiCatalogoPost.TituloPremio;
						clubMatassiCatalogo.DescripcionPremio = clubMatassiCatalogoPost.DescripcionPremio;
						clubMatassiCatalogo.CantidadPuntos = clubMatassiCatalogoPost.CantidadPuntos;

						clubMatassiCatalogo = ServicioSistema<ClubMatassiCatalogo>.SaveOrUpdate(clubMatassiCatalogo);
					}

					return RedirectToAction("ClubMatassiCatalogo_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ClubMatassiCatalogo_Borrar(int codClubMatassiCatalogo)
		{
			ClubMatassiCatalogo premio = ServicioSistema<ClubMatassiCatalogo>.GetById(cmc => cmc.CodClubMatassiCatalogo == codClubMatassiCatalogo);

			if (premio != null)
				ServicioSistema<ClubMatassiCatalogo>.Delete(premio);

			return RedirectToAction("ClubMatassiCatalogo_Lista");
		}


		/************** PUNTOS DEL CLIENTE ***************/
		public ActionResult ClubMatassiPuntosCliente_Lista()
		{
			ViewBag.Title = "Puntos por Cliente";

			List<Matassi.Dominio.ClubMatassiPuntosCliente> puntosCliente = ServicioSistema<ClubMatassiPuntosCliente>.GetAll().OrderBy(cmpc => cmpc.CantidadPuntos).ToList();

			return View("ClubMatassiPuntosCliente-Lista", puntosCliente);
		}

		public ActionResult ClubMatassiPuntosCliente_Crear()
		{
			ViewBag.Title = "Puntos por Cliente - Nuevo Cliente";

			ClubMatassiPuntosCliente clubMatassiPuntosCliente = new ClubMatassiPuntosCliente();

			return View("ClubMatassiPuntosCliente-Crear", clubMatassiPuntosCliente);
		}

		[HttpPost]
		public ActionResult ClubMatassiPuntosCliente_Crear(ClubMatassiPuntosCliente clubMatassiPuntosClientePost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ClubMatassiPuntosCliente clubMatassiPuntosCliente = new ClubMatassiPuntosCliente();

					clubMatassiPuntosCliente.Patente = clubMatassiPuntosClientePost.Patente;
					clubMatassiPuntosCliente.CantidadPuntos = clubMatassiPuntosClientePost.CantidadPuntos;

					clubMatassiPuntosCliente = ServicioSistema<ClubMatassiPuntosCliente>.SaveOrUpdate(clubMatassiPuntosCliente);

					return RedirectToAction("ClubMatassiPuntosCliente_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ClubMatassiPuntosCliente_Editar(int codClubMatassiPuntosCliente)
		{
			ViewBag.Title = "Catálogo de Puntos - Editar Premio";

			ClubMatassiPuntosCliente clubMatassiPuntosCliente = ServicioSistema<ClubMatassiPuntosCliente>.GetById(cmc => cmc.CodClubMatassiPuntosCliente == codClubMatassiPuntosCliente);

			if (clubMatassiPuntosCliente != null)
				return View("ClubMatassiPuntosCliente-Editar", clubMatassiPuntosCliente);

			return View();
		}

		[HttpPost]
		public ActionResult ClubMatassiPuntosCliente_Editar(int codClubMatassiPuntosCliente, ClubMatassiPuntosCliente clubMatassiPuntosClientePost)
		{
			try
			{
				if (ModelState.IsValid)
				{

					ClubMatassiPuntosCliente clubMatassiPuntosCliente = ServicioSistema<ClubMatassiPuntosCliente>.GetById(cmc => cmc.CodClubMatassiPuntosCliente == codClubMatassiPuntosCliente);

					if (clubMatassiPuntosCliente != null)
					{
						clubMatassiPuntosCliente.Patente = clubMatassiPuntosClientePost.Patente;
						clubMatassiPuntosCliente.CantidadPuntos = clubMatassiPuntosClientePost.CantidadPuntos;

						clubMatassiPuntosCliente = ServicioSistema<ClubMatassiPuntosCliente>.SaveOrUpdate(clubMatassiPuntosCliente);
					}

					return RedirectToAction("ClubMatassiPuntosCliente_Lista");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return View();
		}

		public ActionResult ClubMatassiPuntosCliente_Borrar(int codClubMatassiPuntosCliente)
		{
			ClubMatassiPuntosCliente premio = ServicioSistema<ClubMatassiPuntosCliente>.GetById(cmc => cmc.CodClubMatassiPuntosCliente == codClubMatassiPuntosCliente);

			if (premio != null)
				ServicioSistema<ClubMatassiPuntosCliente>.Delete(premio);

			return RedirectToAction("ClubMatassiPuntosCliente_Lista");
		}
	}
}