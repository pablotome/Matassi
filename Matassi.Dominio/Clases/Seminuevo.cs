using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;

using NHibernate;
using Matassi.Dominio.Helpers;


namespace Matassi.Dominio
{
	public class Seminuevo
	{
		public virtual int CodSeminuevo { get; set; }

		[Display(Name = "Año")]
		[MaxLength(4)]
		public virtual string Anio { get; set; }
		public virtual string Modelo { get; set; }
		public virtual string Kilometraje { get; set; }
		public virtual string Precio { get; set; }
		public virtual string Combustible { get; set; }
		public virtual string Color { get; set; }
		public virtual string Vendedor { get; set; }
		[MaxLength(1000)]
		public virtual string Comentarios { get; set; }
		public virtual string Transmision { get; set; }
		public virtual string CantidadPuertas { get; set; }
		
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenPosteada { get; set; }
		public virtual byte[] Imagen { get; set; }

		public virtual int Orden { get; set; }
		public virtual bool Publicado { get; set; }
	}

	public class SeminuevoMap : ClassMap<Seminuevo>
	{
		public SeminuevoMap()
		{
			Table("Seminuevo");

			Id(m => m.CodSeminuevo);
			Map(m => m.Anio);
			Map(m => m.Modelo);
			Map(m => m.Kilometraje);
			Map(m => m.Precio);
			Map(m => m.Combustible);
			Map(m => m.Color);
			Map(m => m.Vendedor);
			Map(m => m.Comentarios);
			Map(m => m.Transmision);
			Map(m => m.CantidadPuertas);
			
			Map(m => m.Imagen).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");

			Map(m => m.Orden);
			Map(m => m.Publicado);
		}
	}
}
