using Matassi.Dominio;
using Matassi.Servicios;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Matassi.Web.Models;
using System.Text;
using Matassi.Web.Clases;
using System.Net.Http;

namespace Matassi.Web.API
{
	public class MatassiController : ApiController
	{
		[HttpGet]
		public object MesAnioGanadores()
		{
			List<AutoahorroGanador> ganadores;
			string mes, anio;
			mes = anio = string.Empty;

			//Veo si hay ganadores en el mes actual.
			ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1) && aa.Concesionario == "01411").ToList();

			//Mientras no haya ganadores, me voy un mes para atrás, hasta 6 meses
			int mesesAtras = 1;
			while (mesesAtras <= 6 && (ganadores == null || ganadores.Count == 0))
			{
				ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.AddMonths(-1 * mesesAtras).Year, DateTime.Now.AddMonths(-1 * mesesAtras).Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Year, DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Month, 1) && aa.Concesionario == "01411").OrderBy(aa => aa.Grupo).ThenBy(aa => aa.Orden).ToList();
				mesesAtras++;
			}

			if (ganadores != null && ganadores.Count > 0)
			{
				mes = ganadores[0].ArchivoAutoahorro.Fecha.ToString("MMMM");
				anio = ganadores[0].ArchivoAutoahorro.Fecha.Year.ToString();
			}

			var result = new { Mes = mes, Anio = anio };

			//return Newtonsoft.Json.JsonConvert.SerializeObject(result);
			return result;
		}

		[HttpGet]
		public List<object> Ganadores()
		{
			List<object> listaGanadores = new List<object>();

			//Veo si hay ganadores en el mes actual.
			List<AutoahorroGanador> ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1) && aa.Concesionario == "01411").ToList();

			//Mientras no haya ganadores, me voy un mes para atrás, hasta 6 meses
			int mesesAtras = 1;
			while (mesesAtras <= 6 && (ganadores == null || ganadores.Count == 0))
			{
				ganadores = ServicioSistema<AutoahorroGanador>.Get(aa => aa.ArchivoAutoahorro != null && new DateTime(DateTime.Now.AddMonths(-1 * mesesAtras).Year, DateTime.Now.AddMonths(-1 * mesesAtras).Month, 1) <= aa.ArchivoAutoahorro.Fecha && aa.ArchivoAutoahorro.Fecha < new DateTime(DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Year, DateTime.Now.AddMonths(-1 * (mesesAtras - 1)).Month, 1) && aa.Concesionario == "01411").OrderBy(aa => aa.Grupo).ThenBy(aa => aa.Orden).ToList();
				mesesAtras++;
			}

			foreach (AutoahorroGanador autoahorroGanador in ganadores)
			{
				listaGanadores.Add(new { Grupo = autoahorroGanador.Grupo, Orden = autoahorroGanador.Orden, Nombre = autoahorroGanador.Nombre ?? string.Empty, Tipo = autoahorroGanador.Tipo, Monto = autoahorroGanador.Monto });
			}

			/*if (ganadores != null && ganadores.Count > 0)
			{
				return ganadores;
			}*/

			return listaGanadores;
		}

		[HttpGet]
		public List<object> Ofertas(int id)
		{
			List<AutoahorroOferta> ofertas = ServicioSistema<AutoahorroOferta>.Get(o => int.Parse(o.Grupo) == id && o.ArchivoAutoahorro.Fecha > DateTime.Now.AddMonths(-6)).OrderByDescending(o => o.ArchivoAutoahorro.Fecha).ToList();
			List<object> listaFechas = new List<object>();
			List<object> listaOfertas;

			foreach (DateTime fecha in ofertas.Select(ao => ao.ArchivoAutoahorro.Fecha).Distinct().OrderByDescending(ao => ao.Date))
			{
				listaOfertas = new List<object>();
				var fechas = new { Fecha = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(fecha.ToString("MMMM 'de' yyyy")), Ofertas = listaOfertas };
				
				foreach (AutoahorroOferta oferta in ofertas.Where(ao => ao.ArchivoAutoahorro.Fecha == fecha))
				{
					var ofertaTmp = new { Fecha = oferta.ArchivoAutoahorro.Fecha.ToString("dd/MM/yyyy"), Grupo = oferta.Grupo.ToString().PadLeft(4, '0'), Modelo = oferta.Modelo, TotalAjustado = string.Format("{0:0.00}", oferta.TAjustado), TotalLicitado = string.Format("{0:0.00}", oferta.TLicitado) };
					fechas.Ofertas.Add(ofertaTmp);
				}

				listaFechas.Add(fechas);
			}

			return listaFechas;
		}

		[HttpGet]
		public List<object> Emisiones(int id, int ord)
		{
			List<AutoahorroEmision> cuotas = ServicioSistema<AutoahorroEmision>.Get(o => o.Gpo == id && o.Ord == ord && o.ArchivoAutoahorro.Fecha > DateTime.Now.AddMonths(-10)).OrderByDescending(o => o.ArchivoAutoahorro.Fecha).ToList();
			List<object> listaEmisiones = new List<object>();

			foreach (AutoahorroEmision cuota in cuotas)
			{
				listaEmisiones.Add(new { Nombre = cuota.Nombre,
					Vencimiento = cuota.Vence.ToString("dd/MM/yyyy"),
					Grupo = cuota.Gpo.ToString().PadLeft(4, '0'),
					Alicuota = string.Format("{0:0.00}", cuota.Alicuota),
					Orden = cuota.Ord.ToString().PadLeft(3, '0'),
					CargosAdm = string.Format("{0:0.00}", cuota.Cargos),
					Desvio = cuota.Desv,
					ActAlicuota = string.Format("{0:0.00}", cuota.Actalicuota),
					NroCuota = cuota.Cuot.ToString().PadLeft(2, '0'),
					CargosActAlic = string.Format("{0:0.00}", cuota.Caactalic),
					SegBien = string.Format("{0:0.00}", cuota.SegBien), 
					SegVida = string.Format("{0:0.00}", cuota.SegVida),
					CuotasPlan = cuota.Plan,
					Mora = string.Format("{0:0.00}", cuota.Mora), 
					Modelo = cuota.Mod,
					DebCred = string.Format("{0:0.00}", cuota.DebCred),
					Banco = cuota.Banco, 
					IntLiq = string.Format("{0:0.00}", cuota.Intliquid),
					Sucursal = cuota.Sucursal,
					Otros = string.Format("{0:0.00}", cuota.Otros),
					Cuenta = cuota.Cuenta, 
					Total = string.Format("{0:0.00}", cuota.Total)
				});
			}

			return listaEmisiones;
		}


		[HttpGet]
		public List<object> Planes()
		//public HttpResponseMessage Planes()
		{
			List<object> listaPlanes = new List<object>();
			List<object> listaCuotas;

			//Obtengo todos los planes
			List<PlanAutoahorro> planesAutoahorro = ServicioSistema<PlanAutoahorro>.Get(pa => pa.Vigente == true).OrderBy(pa => pa.Orden).ToList();

			foreach (PlanAutoahorro planAutoahorro in planesAutoahorro) {

				listaCuotas = new List<object>();

				foreach (ValorCuotaPlanAutoahorro valorCuotaPlanAutoahorro in planAutoahorro.CuotasPlanAutoahorro) {
				
					listaCuotas.Add(new {
						RangoCuota = valorCuotaPlanAutoahorro.RangoCuota,
						Valor = valorCuotaPlanAutoahorro.Valor
					});

				}

				listaPlanes.Add(new { 
					CodPlanAutoahorro = planAutoahorro.CodPlanAutoahorro, 
					Titulo = planAutoahorro.Titulo,
					Subtitulo = planAutoahorro.Subtitulo,
					Cuotas = listaCuotas
				});
			}

			return listaPlanes;
		}

		//[Route("API/Matassi/ResponderEncuestaVenta/{respuestas}")]
		[HttpPost]
		public string ResponderEncuestaVenta([FromBody] Encuesta encuesta)
		{
			StringBuilder sb = new StringBuilder();
			String preguntaRespuesta = string.Empty;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", encuesta.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", encuesta.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", encuesta.Telefono));
			sb.Append(string.Format("<b>Fecha encuesta</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));


			foreach (Pregunta pregunta in encuesta.Preguntas)
			{
				preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", pregunta.TextoPregunta, pregunta.TextoRespuesta);
				sb.Append(preguntaRespuesta);
			}

			sb.Append("</div>");

			Parametro mailToNameVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNameVenta");
			Parametro mailToVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToVenta");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", encuesta.Nombre),
							encuesta.EMail, string.Format("{0}", encuesta.Nombre),
							mailToVenta.Valor, mailToNameVenta.Valor,
							"Encuesta de Satisfacción de Venta", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return string.Empty;
		}

		[HttpPost]
		public string ResponderEncuestaPostVenta([FromBody] EncuestaPostventa encuesta)
		{
			StringBuilder sb = new StringBuilder();
			String preguntaRespuesta = string.Empty;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Comentarios</b>: {0}<br/>", encuesta.Comentarios));
			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", encuesta.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", encuesta.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", encuesta.Telefono));
			sb.Append(string.Format("<b>Fecha encuesta</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));


			foreach (Pregunta pregunta in encuesta.Preguntas)
			{
				preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", pregunta.TextoPregunta, pregunta.TextoRespuesta);
				sb.Append(preguntaRespuesta);
			}

			sb.Append("</div>");

			Parametro mailToNamePostVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNamePostVenta");
			Parametro mailToPostVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToPostVenta");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", encuesta.Nombre),
							encuesta.EMail, string.Format("{0}", encuesta.Nombre),
							mailToPostVenta.Valor, mailToNamePostVenta.Valor,
							"Encuesta de Satisfacción de Post-Venta", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return string.Empty;
		}

		[HttpPost]
		public string EnviarContacto([FromBody] Contacto contacto)
		{
			StringBuilder sb = new StringBuilder();
			String preguntaRespuesta = string.Empty;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", contacto.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", contacto.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", contacto.Telefono));
			sb.Append(string.Format("<b>Comentarios</b>: {0}<br/>", contacto.Comentarios));
			sb.Append(string.Format("<b>Fecha contacto</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));

			sb.Append("</div>");

			Parametro mailToNameContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNameGeneral");
			Parametro mailToContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToGeneral");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", contacto.Nombre),
							contacto.EMail, string.Format("{0}", contacto.Nombre),
							mailToContacto.Valor, mailToNameContacto.Valor,
							"Consulta desde el sitio Web", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return string.Empty;
		}

		[HttpPost]
		public string EnviarPruebaManejo([FromBody] PruebaManejo pruebaManejo)
		{
			StringBuilder sb = new StringBuilder();
			String preguntaRespuesta = string.Empty;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", pruebaManejo.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", pruebaManejo.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", pruebaManejo.Telefono));
			sb.Append(string.Format("<b>Modelo</b>: {0}<br/>", pruebaManejo.Modelo));
			sb.Append(string.Format("<b>Fecha</b>: {0}<br/>", pruebaManejo.Fecha));
			sb.Append(string.Format("<b>Hora</b>: {0}<br/>", pruebaManejo.Hora));
			sb.Append(string.Format("<b>Comentarios</b>: {0}<br/>", pruebaManejo.Comentarios));
			sb.Append(string.Format("<b>Fecha contacto</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));

			sb.Append("</div>");

			Parametro mailToNameContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNameGeneral");
			Parametro mailToContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToGeneral");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", pruebaManejo.Nombre),
							pruebaManejo.EMail, string.Format("{0}", pruebaManejo.Nombre),
							mailToContacto.Valor, mailToNameContacto.Valor,
							"Solicitud de prueba de manejo", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return string.Empty;
		}

		[HttpPost]
		public string Turno([FromBody] Contacto contacto)
		{
			StringBuilder sb = new StringBuilder();
			String preguntaRespuesta = string.Empty;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", contacto.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", contacto.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", contacto.Telefono));
			sb.Append(string.Format("<b>Comentarios</b>: {0}<br/>", contacto.Comentarios));
			sb.Append(string.Format("<b>Fecha contacto</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));

			sb.Append("</div>");

			Parametro mailToNameContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNameGeneral");
			Parametro mailToContacto = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToGeneral");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", contacto.Nombre),
							contacto.EMail, string.Format("{0}", contacto.Nombre),
							mailToContacto.Valor, mailToNameContacto.Valor,
							"Solicitud de turno desde el sitio Web", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return string.Empty;
		}

	}
}