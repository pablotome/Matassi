using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Models
{
	public class NivelSatisfaccion
	{
		public string Nivel { get; set; }

		public static List<NivelSatisfaccion> NivelesSatisfaccion()
		{
			List<NivelSatisfaccion> nivelesSatisfaccion = new List<NivelSatisfaccion>();

			nivelesSatisfaccion.Add(new NivelSatisfaccion(){ Nivel = "Sumamente satisfecho"});
			nivelesSatisfaccion.Add(new NivelSatisfaccion(){ Nivel = "Muy satisfecho"});
			nivelesSatisfaccion.Add(new NivelSatisfaccion(){ Nivel = "Satisfecho"});
			nivelesSatisfaccion.Add(new NivelSatisfaccion(){ Nivel = "Poco Satisfecho"});
			nivelesSatisfaccion.Add(new NivelSatisfaccion() { Nivel = "Nada Satisfecho" });

			return nivelesSatisfaccion;
		}
	}

	public class EncuestaVentas
	{
		[Display(Name = "¿Cuál es su nivel de satisfacción con Matassi e Imperiale S.A?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccion { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto a la actitud del vendedor que lo/la atendió?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionVendedor { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto al conocimiento que ha demostrado el vendedor sobre el producto que ha comprado y la operación en general?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionConocimientoVendedor { get; set; }

		[Display(Name = "¿En nuestro concesionario se le ha ofrecido realizar una prueba de manejo de un vehículo VW?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string PruebaManejo { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto a la gestión administrativa en cuanto a cordialidad?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionAdministrativa { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción respecto a la facilidad de comunicarse y realizar consultas administrativas?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionComunicacion { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto a la explicación de los trámites administrativos y sus tiempos?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionExplicacionTramites { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto a la entrega de su 0km en cuanto a condiciones técnicas y la limpieza?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionEntrega { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto a la explicación del funcionamiento del vehiculo, mantenimiento, garantía, etc?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionExplicacionFuncionamiento { get; set; }

		[Display(Name = "¿Cuál es su nivel de satisfacción con respecto al cumplimiento de la fecha y hora acordada?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public string NivelSatisfaccionCumplimientoFecha { get; set; }

		[Display(Name = "¿Le informaron quien será su contacto post venta?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool InformaronContacto { get; set; }

		[Display(Name = "¿El vendedor se ha contactado con usted luego de la entrega?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool ContactoVendedor { get; set; }

		[Display(Name = "¿Volvería a comprar en nuestro concesionario?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool VolveriaAComprar { get; set; }

		[Display(Name = "¿Está interesado en colocar accesorios?")]
		[Required(ErrorMessage = "Por favor, seleccione su opción")]
		public bool InteresaAccesorio { get; set; }

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