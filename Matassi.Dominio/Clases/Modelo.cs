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
	public class Modelo
	{
		public virtual int CodModelo { get; set; }

		[Display(Name = "Modelo")]
		public virtual string Nombre { get; set; }
		public virtual string NombreClave { get; set; }

		[Display(Name = "Bajada de título")]
		public virtual string Bajada { get; set; }

		[Display(Name = "Imagen para publicar el modelo en página de accesorios (en 150 x 120 px)")]
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenAccesoriosPosteada { get; set; }
		public virtual byte[] ImagenAccesorios { get; set; }

		[Display(Name = "Imagen para publicar el modelo en página de contacto comercial (en 900 x 200 px)")]
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenContactoPosteada { get; set; }
		public virtual byte[] ImagenContacto { get; set; }

		[Display(Description = "¿Publicar este modelo?")]
		public virtual bool Vigente { get; set; }
		public virtual IList<VersionModelo> Versiones { get; set; }
		public virtual IList<AccesorioModelo> Accesorios { get; set; }
		public virtual IList<ImagenModelo> Imagenes { get; set; }

		public virtual int Orden { get; set; }

		// Slug generation taken from http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
		public virtual string GenerateSlug()
		{
			//string phrase = string.Format("{0}-{1}", Id, Name);
			string phrase = string.Format("{0}", Nombre);

			string str = RemoveAccent(phrase).ToLower();
			// invalid chars           
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
			// convert multiple spaces into one space   
			str = Regex.Replace(str, @"\s+", " ").Trim();
			// cut and trim 
			str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
			str = Regex.Replace(str, @"\s", "-"); // hyphens   
			return str;
		}

		private string RemoveAccent(string text)
		{
			byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
			return System.Text.Encoding.ASCII.GetString(bytes);
		}
	}

	public class ModeloMap : ClassMap<Modelo>
	{
		public ModeloMap()
		{
			Table("Modelo");

			Id(m => m.CodModelo);
			Map(m => m.Nombre);
			Map(m => m.NombreClave);
			Map(m => m.Bajada);
			Map(m => m.ImagenAccesorios).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
			Map(m => m.ImagenContacto).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
			Map(m => m.Vigente);
			HasMany<VersionModelo>(m => m.Versiones)
				.AsBag()
				.LazyLoad()
				.KeyColumn("CodModelo")
				.Not.KeyUpdate()
				.Not.KeyNullable()
				.Cascade.AllDeleteOrphan()
				.Inverse();
			
			HasMany<AccesorioModelo>(m => m.Accesorios)
				.AsBag()
				.LazyLoad()
				.KeyColumn("CodModelo")
				.Not.KeyUpdate()
				.Not.KeyNullable()
				.Cascade.AllDeleteOrphan()
				.Inverse();

			HasMany<ImagenModelo>(m => m.Imagenes)
				.AsBag()
				.LazyLoad()
				.KeyColumn("CodModelo")
				.Not.KeyUpdate()
				.Not.KeyNullable()
				.Cascade.AllDeleteOrphan()
				.Inverse();

			Map(m => m.Orden);
		}
	}
}
