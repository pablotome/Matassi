using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Models
{
	public class Consulta
	{
		public string CodConsulta { get; set; }

		public int CodModelo { get; set; }

		[StringLength(100, ErrorMessage="La longitud máxima debe ser de 10 caracteres.")]
		[Display(Description = "Motivo de Consulta")]
		[Required(ErrorMessage = "El Motivo de Consulta es un campo requerido")]
		//[RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers of First Name")]
		public string MotivoConsulta { get; set; }

		[Required(ErrorMessage = "El Nombre es un dato requerido")]
		[StringLength(50, ErrorMessage = "El nombre debe ser de 50 caracteres.")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El Apellido es un dato requerido")]
		[StringLength(50, ErrorMessage = "El apellido debe ser de 50 caracteres.")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "El Teléfono es un dato requerido")]
		[StringLength(20, ErrorMessage = "El teléfono debe ser de 20 caracteres.")]
		public string Telefono { get; set; }

		[Required(ErrorMessage = "El E-Mail es un dato requerido")]
		[StringLength(70, ErrorMessage = "El e-mail debe ser de 70 caracteres.")]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Ingrese un e-mail válido")]
		public string EMail { get; set; }

		[AttributeHelper.EnforceTrue(ErrorMessage = @"Debe aceptar las políticas de privacidad")]
		public bool AceptoTerminos { get; set; }

		public bool EsConsultaModelo { get; set; }

		public bool EsConsultaPlan { get; set; }
		
		public string Comentarios { get; set; }

		public Consulta()
		{
			EsConsultaModelo = false;
		}
	}
}