
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Models
{
	public class NivelSatisfaccionPostVenta1
	{
		public string Nivel { get; set; }

		public static List<NivelSatisfaccionPostVenta1> NivelesSatisfaccion(int codPregunta)
		{
			List<NivelSatisfaccionPostVenta1> nivelSatisfaccionPostVenta1 = new List<NivelSatisfaccionPostVenta1>();

			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "Sumamente satisfecho" });
			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "Muy satisfecho" });
			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "Satisfecho" });
			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "Poco satisfecho" });
			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "Nada satisfecho" });
			nivelSatisfaccionPostVenta1.Add(new NivelSatisfaccionPostVenta1() { Nivel = "No responde" });

			return nivelSatisfaccionPostVenta1;
		}
	}

	public class NivelSatisfaccionPostVenta2
	{
		public string Nivel { get; set; }

		public static List<NivelSatisfaccionPostVenta2> NivelesSatisfaccion(int codPregunta)
		{
			List<NivelSatisfaccionPostVenta2> nivelSatisfaccionPostVenta2 = new List<NivelSatisfaccionPostVenta2>();

			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "Seguramente" });
			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "Probablemente sí" });
			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "Eventualmente" });
			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "Probablemente no" });
			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "Seguramente no" });
			nivelSatisfaccionPostVenta2.Add(new NivelSatisfaccionPostVenta2() { Nivel = "No responde" });

			return nivelSatisfaccionPostVenta2;
		}
	}

	public class EncuestaPostVentas
	{
		[Display(Name = "Pensando en su experiencia durante la última visita al taller, ¿ cuál es su grado de satisfacción general con el servicio prestado en Matassi e Imperiale ?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string SatisfaccionGeneral { get; set; }

		[Display(Name = "¿Recomendaría nuestro taller a parientes o amigos?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string Recomendaria { get; set; }

		[Display(Name = "¿Llevaría nuevamente su auto a este mismo Taller para realizar una reparación o un servicio de mantenimiento?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string Llevaria { get; set; }

		[Display(Name = "Pensando en su última visita al taller: ¿cuál es su grado de satisfacción con las explicaciones de los trabajos antes de ser realizados?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string GradoSatisfaccion { get; set; }

		[Display(Name = "¿Ha recibido algún consejo sobre los próximos servicios de mantenimiento y reparaciones de su vehículo?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool ConsejoProximosServicios { get; set; }

		[Display(Name = "¿Cuál es su grado de satisfacción en relación con la explicación de los trabajos realizados o de la factura?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string SatisfaccionExplicacion { get; set; }

		[Display(Name = "¿Se cumplió con el plazo de entrega acordado?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool CumplioPlazo { get; set; }

		[Display(Name = "La razón de su última visita al taller, ¿fue debido a que el taller hizo un trabajo incompleto o incorrecto en su visita anterior?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool TrabajoIncompleto { get; set; }


		[Display(Name = "Amabilidad del personal")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosAmabilidad { get; set; }
				
		[Display(Name = "Asesoramiento técnico")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosAsesoramiento { get; set; }
				
		[Display(Name = "Disposición del asesor para atender sus necesidades y deseos")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosDisposicion { get; set; }
				
		[Display(Name = "Confianza que le transmitió el personal")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosConfianza { get; set; }
				
		[Display(Name = "Realización correcta de los trabajos de taller")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosRealizacionCorrecta { get; set; }
				
		[Display(Name = "Tiempo de espera cuando Ud. entrega el vehículo")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosTiempoEsperaEntrega { get; set; }
				
		[Display(Name = "Tiempo de espera cuando Ud. retira el vehículo")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosTiempoEsperaRetira { get; set; }
				
		[Display(Name = "Limpieza con la que le fue entregado su vehículo")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosLimpieza { get; set; }
				
		[Display(Name = "Apariencia del área de Servicio")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosApariencia { get; set; }
				
		[Display(Name = "Relación precio-prestaciones del trabajo realizado en el taller")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string AspectosRelacionPrecioPrestaciones { get; set; }

		[Display(Name = "¿El Taller del Concesionario lo contactó por algún medio para saber si estaba satisfecho con los trabajos realizados?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool ContactoSatisfaccion { get; set; }

		[Display(Name = "Suponiendo que nuevamente fuera a comprar un nuevo Volkswagen, ¿compraría su próximo auto en Matassi e Imperiale?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string ComprariaEnMEI { get; set; }

		[Display(Name = "Ingrese sus comentarios adicionales aquí")]
		public string Comentarios { get; set; }

		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "El nombre es un dato requerido")]
		public string Nombre { get; set; }
		
		[StringLength(70, ErrorMessage = "El e-mail debe ser de hasta 70 caracteres.")]
		[Display(Name = "E-Mail")]
		[Required(ErrorMessage = "El e-mail es un dato requerido")]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Ingrese un e-mail válido")]
		public string EMail { get; set; }

		[StringLength(15, ErrorMessage = "El teléfono debe ser de hasta 15 caracteres.")]
		[Display(Name = "Teléfono")]
		[Required(ErrorMessage = "El teléfono es un dato requerido")]
		public string Telefono { get; set; }

		//[AttributeHelper.EnforceTrue(ErrorMessage = @"Debe aceptar las políticas de privacidad")]
		//public bool AceptoTerminos { get; set; }

	}
}