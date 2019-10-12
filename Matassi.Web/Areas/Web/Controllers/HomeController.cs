using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Matassi.Dominio;
using Matassi.Servicios;
using Matassi.Web.Areas.Web.Models;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

			return View();
		}

		public ActionResult Inicio()
		{
			/*ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

			List<AccesosHome> accesosHome = ServicioSistema<AccesosHome>.Get(ah => ah.Vigente).OrderBy(ah => ah.Orden).Take(5).ToList();
			ViewBag.AccesosHome = accesosHome;

			int[] posicionesAccesos = new int[] { 23, 209, 395, 581, 767 };
			ViewBag.PosicionesAccesos = posicionesAccesos;

			object codImagenModelo = ServicioSistema<ImagenModelo>.ExecuteSQLQueryUniqueResult("select top 1 CodImagenModelo from ImagenModelo where MostrarEnHome = 1 order by newid()");


			if (codImagenModelo != null)
			{
				ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == (int)codImagenModelo);

				if (imagenModelo != null)
				{
					ViewBag.TituloImagen = imagenModelo.Modelo.Nombre;
					ViewBag.ImagenModelo = imagenModelo;
					ViewBag.BajadaTitulo = imagenModelo.Modelo.Bajada;
					ViewBag.CodImagenModelo = (int)codImagenModelo;
					ViewBag.EstiloTitulo = imagenModelo.ClaseCSSTitulo;
				}
				else
				{
					ViewBag.ImagenModelo = null;
				}
			}
			else
				ViewBag.CodImagenModelo = null;*/

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult ContactoPlan(PlanAutoahorro planAutoahorroPost)
		{
			PlanAutoahorro planAutoahorro = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == planAutoahorroPost.CodPlanAutoahorro);

			if (planAutoahorroPost != null)
			{
				return View("Contacto", new Consulta() { EsConsultaPlan = true, CodConsulta = "15", CodModelo = planAutoahorro.CodPlanAutoahorro, MotivoConsulta = string.Format("Autoahorro - {0}", planAutoahorro.Titulo) });
			}

			return View();
		}

		public ActionResult Contacto(string id, string modelo, int? codModelo)
		{
			if (id == null)
				id = "General";

			if (modelo == null)
			{
				if (HelperWeb.IsInteger(id))
				{
					ContactoSector contactoSector = ServicioSistema<ContactoSector>.GetById(cs => cs.CodContactoSector == int.Parse(id));
					return View(new Consulta() { CodConsulta = id, MotivoConsulta = contactoSector.SectorInterno.DesSectorInterno });
				}
				else
				{
					Parametro mailToName = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToName" + id);
					return View(new Consulta() { CodConsulta = id, MotivoConsulta = mailToName.Valor });
				}
			}
			else
			{
				return View(new Consulta() { CodConsulta = "ConsultaModelo", MotivoConsulta = "Consulta Modelo " + modelo, EsConsultaModelo = true, CodModelo = (codModelo.HasValue) ? codModelo.Value : 0 });
			}
		}

		[HttpPost]
		public ActionResult Contacto(Consulta consulta)
		{
			if (ModelState.IsValid)
			{
				ContactoSector contactoSector;

				if (HelperWeb.IsInteger(consulta.CodConsulta))
					contactoSector = ServicioSistema<ContactoSector>.GetById(cs => cs.CodContactoSector == int.Parse(consulta.CodConsulta));
				else
				{
					Parametro mailTo, mailToName;
					mailTo = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailTo" + consulta.CodConsulta);
					mailToName = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToName" + consulta.CodConsulta);

					contactoSector = new ContactoSector() { Contacto = mailTo.Valor, EsEmail = true, SectorInterno = new SectorInterno() { DesSectorInterno = "Consulta " + mailToName.Valor } };
				}

				if (contactoSector.EsEmail)
				{
					if (!consulta.EsConsultaModelo)
						consulta.MotivoConsulta = contactoSector.SectorInterno.DesSectorInterno;
					
					else if (consulta.EsConsultaPlan)
					{
						consulta.MotivoConsulta = ServicioSistema<PlanAutoahorro>.GetById(pa => pa.CodPlanAutoahorro == consulta.CodModelo).Titulo;
					}

					string textoMail = string.Format("Nombre: {0}\r\nApellido: {1}\r\nE-Mail: {2}\r\nTeléfono: {3}\r\nComentarios: {4}",
						consulta.Nombre, consulta.Apellido, consulta.EMail, consulta.Telefono, consulta.Comentarios);

					HelperWeb.Mail.SendMail(
							//consulta.EMail, string.Format("{0} {1}", consulta.Nombre, consulta.Apellido),
							"webmaster@matassi.com.ar", string.Format("{0} {1}", consulta.Nombre, consulta.Apellido),
							consulta.EMail, string.Format("{0} {1}", consulta.Nombre, consulta.Apellido),
							contactoSector.Contacto, contactoSector.SectorInterno.DesSectorInterno,
							"Consulta desde el sitio Web", HelperWeb.DisplayWithBreaks(textoMail)
						);

					ViewBag.TextoGracias = "Gracias por su consulta. La misma será respondida a la brevedad.";

					consulta.Nombre = consulta.Apellido = consulta.Telefono = consulta.EMail = consulta.Comentarios = string.Empty;
					consulta.AceptoTerminos = false;

				}
			}


			/*MailMessage mail = new MailMessage();
			mail.To.Add(contactoSector.Contacto);
			mail.Bcc.Add("info@wdm.com");
			mail.From = new MailAddress(consulta.EMail);//"info@wdm.com"
			mail.Subject = string.Format("Consulta desde el sitio Web - {0}", contactoSector.SectorInterno.DesSectorInterno);
			mail.Body = consulta.Comentarios;
			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "mail.wdm.com";
			smtp.Port = 25;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new System.Net.NetworkCredential("info@wdm.com", "aaaaaaa");// Enter seders User name and password
			//smtp.EnableSsl = true;
			smtp.Send(mail);*/

			return View(consulta);
		}

		public ActionResult PoliticasPrivacidad()
		{
			return View();
		}

		public ActionResult VentasCorporativas()
		{
			return View();
		}

		[HttpGet]
		[OutputCache(Duration = 18000, VaryByParam = "codTipoImagen;codImagen", Location = OutputCacheLocation.ServerAndClient)]
		public ActionResult VerImagenModelo(int codTipoImagen, int codImagen)
		{
			if (codTipoImagen == AccesosHome.TipoImagen.Modelo)
			{
				ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == codImagen);

				if (imagenModelo == null
					|| imagenModelo.Imagen == null)
					return null;

				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(imagenModelo.Imagen, 206, 0)), "image/jpg");
			}
			else
			{
				AccesorioModelo accesorioModelo = ServicioSistema<AccesorioModelo>.GetById(am => am.CodAccesorioModelo == codImagen);

				if (accesorioModelo == null
					|| accesorioModelo.Imagen == null)
					return null;

				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(accesorioModelo.Imagen, 206, 0)), "image/jpg");
			}



			//return File(imagenModelo, "image/jpg");
		}


		[HttpGet]
		[OutputCache(Duration = 2592000, VaryByParam = "codImagenModelo", Location = OutputCacheLocation.ServerAndClient)]
		public ActionResult VerImagenModeloHome(int codImagenModelo)
		{
				ImagenModelo imagenModelo = ServicioSistema<ImagenModelo>.GetById(im => im.CodImagenModelo == codImagenModelo);

				if (imagenModelo == null
					|| imagenModelo.Imagen == null)
					return null;

				return File(HelperWeb.ImageToByte2(HelperWeb.ScaleImage(imagenModelo.Imagen, 960, 0)), "image/jpg");
		}

		public ActionResult PruebaDeManejo()
		{
			ViewBag.Title = "Prueba de Manejo";

			List<SelectListItem> modelos = new List<SelectListItem>();
			List<SelectListItem> horas = new List<SelectListItem>();

			modelos.Add(new SelectListItem() { Value = "0", Text = "- Seleccione un modelo" });

			foreach (Modelo m in ServicioSistema<Modelo>.Get(m => m.Vigente == true).OrderBy(m => m.Orden).ToList())
			{
				modelos.Add(new SelectListItem(){ Value = m.Nombre, Text = m.Nombre });
			}

			ViewBag.Modelos = modelos;

			horas.Add(new SelectListItem(){ Value = "0", Text = "- Seleccione una hora" });
			horas.Add(new SelectListItem() { Value = "08:00 hs a 10:00 hs", Text = "08:00 hs a 10:00 hs" });
			horas.Add(new SelectListItem() { Value = "10:00 hs a 12:00 hs", Text = "10:00 hs a 12:00 hs" });
			horas.Add(new SelectListItem() { Value = "15:30 hs a 17:30 hs", Text = "15:30 hs a 17:30 hs" });
			horas.Add(new SelectListItem() { Value = "17:30 hs a 19:30 hs", Text = "17:30 hs a 19:30 hs" });

			ViewBag.Horas = horas;
			ViewBag.TextoGracias = (TempData["TextoGracias"] != null) ? TempData["TextoGracias"].ToString() : string.Empty;

			return View();
		}

		[HttpPost]
		public ActionResult PruebaDeManejo(PruebaDeManejo pruebaManejo)
		{
			if (ModelState.IsValid)
			{
				Parametro mailTo, mailToName;
				
				mailTo = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToPruebaManejo");
				mailToName = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNamePruebaManejo");

				string textoMail = string.Format(@"	Nombre: {0}{8}
													E-Mail: {1}{8}
													Teléfono: {2}-{3}{8}
													Modelo: {4}{8}
													Fecha: {5}{8}
													Hora: {6}{8}
													Comentarios: {7}",
													pruebaManejo.NombreYApellido, 
													pruebaManejo.EMail, 
													pruebaManejo.Caracteristica, 
													pruebaManejo.Telefono, 
													pruebaManejo.Modelo, 
													pruebaManejo.Fecha, 
													pruebaManejo.Hora, 
													pruebaManejo.Comentarios, 
													Environment.NewLine);

				HelperWeb.Mail.SendMail(
						"webmaster@matassi.com.ar", pruebaManejo.NombreYApellido,
						pruebaManejo.EMail, pruebaManejo.NombreYApellido, 
						mailTo.Valor, mailToName.Valor,
						"Solicitud de prueba de manejo desde la web", HelperWeb.DisplayWithBreaks(textoMail)
					);

				ModelState.Clear();

				TempData["TextoGracias"] = "Gracias por su solicitud de prueba de manejo. La misma será respondida a la brevedad.";

			}

			return RedirectToAction("PruebaDeManejo");
		}

	}
}
