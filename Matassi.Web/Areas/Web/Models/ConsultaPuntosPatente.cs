using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Matassi.Web.Clases;

namespace Matassi.Web.Areas.Web.Models
{
	public class ConsultaPuntosPatente
	{
		//[RegularExpression("\\[a-zA-Z]{2}", ErrorMessage = "El campo tiene que ser numérico de hasta 4 dígitos")]
		public string Patente { get; set; }
	}
}