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
	public class PlanAutoahorro
	{
		public virtual int CodPlanAutoahorro { get; set; }
		public virtual string Titulo { get; set; }
		public virtual string Subtitulo { get; set; }
		[HttpPostedFileExtensions(Extensions = "jpg,png", ErrorMessage = "Solo imágenes JPG ó PNG")]
		public virtual HttpPostedFileBase ImagenPosteada { get; set; }
		public virtual byte[] Imagen { get; set; }
		public virtual int Orden { get; set; }
		public virtual bool Vigente { get; set; }

		public virtual IList<ValorCuotaPlanAutoahorro> CuotasPlanAutoahorro { get; set; }



	}
	public class PlanAutoahorroMap : ClassMap<PlanAutoahorro>
	{
		public PlanAutoahorroMap()
		{
			Table("PlanAutoahorro");

			Id(pa => pa.CodPlanAutoahorro);
			Map(pa => pa.Titulo);
			Map(pa => pa.Subtitulo);
			Map(pa => pa.Imagen).CustomType(NHibernateUtil.BinaryBlob.GetType()).CustomSqlType("image");
			Map(pa => pa.Orden);
			Map(pa => pa.Vigente);
			HasMany<ValorCuotaPlanAutoahorro>(pa => pa.CuotasPlanAutoahorro)
				.AsBag()
				.LazyLoad()
				.KeyColumn("CodPlanAutoahorro")
				.Not.KeyUpdate()
				.Not.KeyNullable()
				.Cascade.AllDeleteOrphan()
				.OrderBy("Orden")
				.Inverse();
		}
	}
}
