using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Matassi.Web.Clases;
using Matassi.Dominio;

namespace Matassi.Web.Areas.Web.Models
{
	public class PruebaDeManejo
	{
		public string CodPruebaDeManejo { get; set; }

		public string Modelo { get; set; }

		[StringLength(100, ErrorMessage = "La longitud máxima debe ser de 10 caracteres.")]
		[Display(Description = "Nombre y Apellido")]
		[Required(ErrorMessage = "El nombre y apellido es un campo requerido")]
		public string NombreYApellido { get; set; }

		[Display(Description = "Fecha para la prueba")]
		[Required(ErrorMessage = "La fecha para la prueba es un campo requerido")]
		public string Fecha { get; set; }

		[Display(Description = "Característica")]
		[Required(ErrorMessage = "La característica es un campo requerido")]
		public string Caracteristica { get; set; }

		[Display(Description = "Teléfono")]
		[Required(ErrorMessage = "El teléfono es un campo requerido")]
		public string Telefono { get; set; }

		[Display(Description = "Hora")]
		[Required(ErrorMessage = "La hora de la prueba es un campo requerido")]
		public string Hora { get; set; }

		[Display(Description = "E-Mail")]
		[Required(ErrorMessage = "El e-mail es un campo requerido")]
		public string EMail { get; set; }
		
		[Display(Description = "Comentarios")]
		[Required(ErrorMessage = "Debe indicar un comentario")]
		public string Comentarios { get; set; }
		
		//[AttributeHelper.EnforceTrue(ErrorMessage = @"Debe aceptar las políticas de privacidad")]
		public bool AceptoTerminos { get; set; }

		public PruebaDeManejo()
		{

		}
	}
}