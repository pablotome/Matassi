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
	public class AccesorioModelo
	{
		public virtual int CodAccesorioModelo { get; set; }
		public virtual Modelo Modelo { get; set; }
		public virtual string Titulo { get; set; }
		[DataType(DataType.MultilineText)]
		public virtual string Descripcion { get; set; }
		public virtual bool MostrarEnAccesoHome { get; set; }
		public virtual bool Vigente { get; set; }
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenPosteada { get; set; }
		public virtual byte[] Imagen { get; set; }
	}
	public class AccesorioModeloMap : ClassMap<AccesorioModelo>
	{
		public AccesorioModeloMap()
		{
			Table("AccesorioModelo");

			Id(am => am.CodAccesorioModelo);
			References(am => am.Modelo).Column("CodModelo");
			Map(am => am.Titulo);
			Map(am => am.Descripcion);
			Map(am => am.MostrarEnAccesoHome);
			Map(am => am.Vigente);
			Map(am => am.Imagen).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
		}
	}
}
