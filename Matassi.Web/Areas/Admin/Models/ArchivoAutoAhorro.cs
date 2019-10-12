using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Matassi.Dominio.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Matassi.Web.Areas.Admin.Models
{
	public class ArchivosAutoahorro
	{
		[HttpPostedFileExtensions(Extensions = "txt", ErrorMessage = "Solo archivos TXT")]
		public HttpPostedFileBase ArchivoOfertas { get; set; }

		[HttpPostedFileExtensions(Extensions = "txt", ErrorMessage = "Solo archivos TXT")]
		public HttpPostedFileBase ArchivoEmisiones { get; set; }

		[HttpPostedFileExtensions(Extensions = "txt", ErrorMessage = "Solo archivos TXT")]
		public HttpPostedFileBase ArchivoGanadores { get; set; }
	}
}