using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;

using NHibernate;
using Matassi.Dominio.Helpers;

namespace Matassi.Dominio
{
	public class ImagenModelo
	{
		public virtual int CodImagenModelo { get; set; }
		public virtual Modelo Modelo { get; set; }
		public virtual string Nombre { get; set; }
		[DataType(DataType.MultilineText)]
		public virtual string Bajada { get; set; }
		public virtual bool MostrarEnHome { get; set; }
		public virtual bool MostrarEnAccesoHome { get; set; }
		public virtual bool Vigente { get; set; }
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenPosteada { get; set; }
		public virtual byte[] Imagen { get; set; }
		public virtual string ClaseCSSTitulo { get; set; }

		public virtual string EstiloTitulo
		{
			get {
				if (ClaseCSSTitulo == "tituloBlancoSombraNegra")
					return "Título Blanco, Sombra Negra";
				else if (ClaseCSSTitulo == "tituloNegroSombraBlanca")
					return "Título Negro, Sombra Blanca";
				return string.Empty;
			}
		}
	}

	//[Required(ErrorMessage="El campo \"Nombre de imagen\" es requerido")]
	//[StringLength(50, ErrorMessage = "La longitud es de hasta 50 caracteres")]
	//[Required]
	//[StringLength(250, ErrorMessage="La longitud es de hasta 250 caracteres")]
	//[Required]//, FileExtensions(Extensions = ".jpg", ErrorMessage = "Solo imágenes JPG ó PNG")]


	public class ImagenModeloMap : ClassMap<ImagenModelo>
	{
		public ImagenModeloMap()
		{
			Table("ImagenModelo");

			Id(m => m.CodImagenModelo);
			References(m => m.Modelo).Column("CodModelo");
			Map(m => m.Nombre);
			Map(m => m.Bajada);
			Map(m => m.MostrarEnHome);
			Map(m => m.MostrarEnAccesoHome);
			Map(m => m.Vigente);
			Map(m => m.Imagen).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
			Map(m => m.ClaseCSSTitulo);
		}
	}
}
