using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using Matassi.Servicios;
using Matassi.Web.Areas.Web.Models;
using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Controllers
{
    public class EmpresaController : Controller
    {

		public ActionResult Ventas_Corporativas()
		{
			return View();
		}

		public ActionResult Contactenos()
		{
			return View();
		}

		public ActionResult Inicio()
		{
			return View();
		}

		public ActionResult Internos()
		{
			return View();
		}




		// GET: Web/Empresa
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Horarios()
		{
			return View();
		}

		public ActionResult Encuesta_Venta()
		{
			return View();
		}

		public ActionResult Encuesta_Posventa()
		{
			return View();
		}

		public ActionResult Prueba_Manejo()
		{
			return View();
		}

		//public ActionResult Internos()
		//{
		//	List<ContactoSector> contactos = ServicioSistema<ContactoSector>.GetAll().OrderBy(cs => cs.SectorInterno.Orden).ThenBy(cs => cs.Orden).ToList();

		//	return View(contactos);
		//}

		public ActionResult EncuestaSatisfaccionVenta()
		{ 
			EncuestaVentas ev = new EncuestaVentas();
			return View(ev);
		}


		[HttpPost]
		public ActionResult EncuestaSatisfaccionVenta(EncuestaVentas encuestaVentas)
		{
			StringBuilder sb = new StringBuilder();
			string preguntaRespuesta;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", encuestaVentas.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", encuestaVentas.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", encuestaVentas.Telefono));
			sb.Append(string.Format("<b>Fecha encuesta</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));

			//¿Cuál es su nivel de satisfacción con Matassi e Imperiale S.A?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccion), encuestaVentas.NivelSatisfaccion);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto a la actitud del vendedor que lo/la atendió?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionVendedor), encuestaVentas.NivelSatisfaccionVendedor);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto al conocimiento que ha demostrado el vendedor sobre el producto que ha comprado y la operación en general?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionConocimientoVendedor), encuestaVentas.NivelSatisfaccionConocimientoVendedor);
			sb.Append(preguntaRespuesta);

			//¿En nuestro concesionario se le ha ofrecido realizar una prueba de manejo de un vehículo VW?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.PruebaManejo), encuestaVentas.PruebaManejo);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto a la gestión administrativa en cuanto a cordialidad?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionAdministrativa), encuestaVentas.NivelSatisfaccionAdministrativa);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción respecto a la facilidad de comunicarse y realizar consultas administrativas?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionComunicacion), encuestaVentas.NivelSatisfaccionComunicacion);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto a la explicación de los trámites administrativos y sus tiempos?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionExplicacionTramites), encuestaVentas.NivelSatisfaccionExplicacionTramites);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto a la entrega de su 0km en cuanto a condiciones técnicas y la limpieza?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionEntrega), encuestaVentas.NivelSatisfaccionEntrega);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto a la explicación del funcionamiento del vehiculo, mantenimiento, garantía, etc?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionExplicacionFuncionamiento), encuestaVentas.NivelSatisfaccionExplicacionFuncionamiento);
			sb.Append(preguntaRespuesta);

			//¿Cuál es su nivel de satisfacción con respecto al cumplimiento de la fecha y hora acordada?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.NivelSatisfaccionCumplimientoFecha), encuestaVentas.NivelSatisfaccionCumplimientoFecha);
			sb.Append(preguntaRespuesta);

			//¿Le informaron quien será su contacto post venta?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.InformaronContacto), (encuestaVentas.InformaronContacto) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//¿El vendedor se ha contactado con usted luego de la entrega?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.ContactoVendedor), (encuestaVentas.ContactoVendedor) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//¿Volvería a comprar en nuestro concesionario?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.VolveriaAComprar), (encuestaVentas.VolveriaAComprar) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//¿Está interesado en colocar accesorios?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaVentas, ev => ev.InteresaAccesorio), (encuestaVentas.InteresaAccesorio) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			sb.Append("</div>");

			Parametro mailToNameVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNameVenta");
			Parametro mailToVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToVenta");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", encuestaVentas.Nombre),
							encuestaVentas.EMail, string.Format("{0}", encuestaVentas.Nombre),
							mailToVenta.Valor, mailToNameVenta.Valor,
							"Encuesta de Satisfacción de Venta", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return View("GraciasResponderEncuesta");
		}

		public ActionResult EncuestaSatisfaccionPostVenta()
		{
			EncuestaPostVentas epv = new EncuestaPostVentas();
			return View(epv);
		}

		[HttpPost]
		public ActionResult EncuestaSatisfaccionPostVenta(EncuestaPostVentas encuestaPostVentas)
		{
			StringBuilder sb = new StringBuilder();
			string preguntaRespuesta;

			sb.Append("<div style=\"font-family:verdana;font-size:12px;\">");

			sb.Append(string.Format("<b>Nombre</b>: {0}<br/>", encuestaPostVentas.Nombre));
			sb.Append(string.Format("<b>E-Mail</b>: {0}<br/>", encuestaPostVentas.EMail));
			sb.Append(string.Format("<b>Teléfono</b>: {0}<br/>", encuestaPostVentas.Telefono));
			sb.Append(string.Format("<b>Fecha encuesta</b>: {0}<br/><br/>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));





			//Pensando en su experiencia durante la última visita al taller, ¿ cuál es su grado de satisfacción general con el servicio prestado en Matassi e Imperiale ?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.SatisfaccionGeneral), encuestaPostVentas.SatisfaccionGeneral);
			sb.Append(preguntaRespuesta);

			//¿Recomendaría nuestro taller a parientes o amigos?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.Recomendaria), encuestaPostVentas.Recomendaria);
			sb.Append(preguntaRespuesta);

			//¿Llevaría nuevamente su auto a este mismo Taller para realizar una reparación o un servicio de mantenimiento?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.Llevaria), encuestaPostVentas.Llevaria);
			sb.Append(preguntaRespuesta);

			//Pensando en su última visita al taller: ¿cuál es su grado de satisfacción con las explicaciones de los trabajos antes de ser realizados?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.GradoSatisfaccion), encuestaPostVentas.GradoSatisfaccion);
			sb.Append(preguntaRespuesta);

			//¿Ha recibido algún consejo sobre los próximos servicios de mantenimiento y reparaciones de su vehículo?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.ConsejoProximosServicios), (encuestaPostVentas.ConsejoProximosServicios) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//¿Cuál es su grado de satisfacción en relación con la explicación de los trabajos realizados o de la factura?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.SatisfaccionExplicacion), encuestaPostVentas.SatisfaccionExplicacion);
			sb.Append(preguntaRespuesta);

			//¿Se cumplió con el plazo de entrega acordado?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.CumplioPlazo), (encuestaPostVentas.CumplioPlazo) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//La razón de su última visita al taller, ¿fue debido a que el taller hizo un trabajo incompleto o incorrecto en su visita anterior?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.TrabajoIncompleto), (encuestaPostVentas.TrabajoIncompleto) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//¿Cuál es el grado de satisfacción con su taller en relación con los siguientes aspectos?
			//Amabilidad del personal
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosAmabilidad), encuestaPostVentas.AspectosAmabilidad);
			sb.Append(preguntaRespuesta);

			//Asesoramiento técnico
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosAsesoramiento), encuestaPostVentas.AspectosAsesoramiento);
			sb.Append(preguntaRespuesta);

			//Disposición del asesor para atender sus necesidades y deseos
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosDisposicion), encuestaPostVentas.AspectosDisposicion);
			sb.Append(preguntaRespuesta);

			//Confianza que le transmitió el personal
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosConfianza), encuestaPostVentas.AspectosConfianza);
			sb.Append(preguntaRespuesta);

			//Realización correcta de los trabajos de taller
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosRealizacionCorrecta), encuestaPostVentas.AspectosRealizacionCorrecta);
			sb.Append(preguntaRespuesta);

			//Tiempo de espera cuando Ud. entrega el vehículo
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosTiempoEsperaEntrega), encuestaPostVentas.AspectosTiempoEsperaEntrega);
			sb.Append(preguntaRespuesta);

			//Tiempo de espera cuando Ud. retira el vehículo
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosTiempoEsperaRetira), encuestaPostVentas.AspectosTiempoEsperaRetira);
			sb.Append(preguntaRespuesta);

			//Limpieza con la que le fue entregado su vehículo
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosLimpieza), encuestaPostVentas.AspectosLimpieza);
			sb.Append(preguntaRespuesta);

			//Apariencia del área de Servicio
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosApariencia), encuestaPostVentas.AspectosApariencia);
			sb.Append(preguntaRespuesta);

			//Relación precio-prestaciones del trabajo realizado en el taller
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.AspectosRelacionPrecioPrestaciones), encuestaPostVentas.AspectosRelacionPrecioPrestaciones);
			sb.Append(preguntaRespuesta);

			//¿El Taller del Concesionario lo contactó por algún medio para saber si estaba satisfecho con los trabajos realizados?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.ContactoSatisfaccion), (encuestaPostVentas.ContactoSatisfaccion) ? "SI" : "NO");
			sb.Append(preguntaRespuesta);

			//Suponiendo que nuevamente fuera a comprar un nuevo Volkswagen, ¿compraría su próximo auto en Matassi e Imperiale?
			preguntaRespuesta = string.Format("<b>Pregunta</b>: {0}<br/><b>Respuesta</b>: {1}<br/><br/>", AttributeHelperST.GetDisplayName(encuestaPostVentas, ev => ev.ComprariaEnMEI), encuestaPostVentas.ComprariaEnMEI);
			sb.Append(preguntaRespuesta);

			sb.Append(string.Format("<b>Comentarios</b>: {0}<br/>", encuestaPostVentas.Comentarios));

			sb.Append("</div>");

			Parametro mailToNamePostVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToNamePostVenta");
			Parametro mailToPostVenta = ServicioSistema<Parametro>.GetById(p => p.CodParametro == "mailToPostVenta");

			HelperWeb.Mail.SendMail(
							"webmaster@matassi.com.ar", string.Format("{0}", encuestaPostVentas.Nombre),
							encuestaPostVentas.EMail, string.Format("{0}", encuestaPostVentas.Nombre), 
							mailToPostVenta.Valor, mailToNamePostVenta.Valor,
							"Encuesta de Satisfacción de Postventa", HelperWeb.DisplayWithBreaks(sb.ToString())
						);

			return View("GraciasResponderEncuesta");
		}
    }
}