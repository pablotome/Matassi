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
	public class VersionModelo
	{
		public virtual int CodVersionModelo { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Bajada { get; set; }
		public virtual byte[] Imagen { get; set; }
		
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		[Display(Name="Imagen", Description="Imagen2")]
		public virtual HttpPostedFileBase ImagenPosteada { get; set; }
		public virtual Modelo Modelo { get; set; }
		public virtual IList<CaracteristicaModelo> Caracteristicas { get; protected set; }

	}

	public class VersionModeloMap : ClassMap<VersionModelo>
	{
		public VersionModeloMap()
		{
			Table("VersionModelo");

			Id(m => m.CodVersionModelo);
			Map(m => m.Nombre);
			Map(m => m.Bajada);
			Map(m => m.Imagen).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
			References(m => m.Modelo).Column("CodModelo");

			HasManyToMany<CaracteristicaModelo>(x => x.Caracteristicas)
				.Cascade.SaveUpdate()
				//.Inverse()
				.Table("CaracteristicaVersion")
				.ParentKeyColumn("CodVersionModelo")
				.ChildKeyColumn("CodCaracteristicaModelo");
				
		}
	}
}
