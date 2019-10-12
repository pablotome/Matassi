using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Matassi.Web.Models
{
	public class Pregunta
	{ 
		public string TextoPregunta { get; set; }
		public string TextoRespuesta { get; set; }
	}

	public class Encuesta
	{
		public List<Pregunta> Preguntas { get; set; }
		public string Nombre { get; set; }
		public string EMail { get; set; }
		public string Telefono { get; set; }
	}

	public class EncuestaPostventa
	{
		public List<Pregunta> Preguntas { get; set; }
		public string Comentarios { get; set; }
		public string Nombre { get; set; }
		public string Telefono { get; set; }
		public string EMail { get; set; }
	}
}