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
	public class AutoahorroOferta : AutoahorroDato
	{
		public virtual int CodAutoahorroOferta { get; set; }
		//public virtual ArchivoAutoahorro ArchivoAutoahorro { get; set; }
		public virtual string Grupo { get; set; }
		public virtual string Orden { get; set; }
		public virtual string Modelo { get; set; }
		public virtual float TAjustado { get; set; }
		public virtual float TLicitado { get; set; }
		public virtual string Observacion { get; set; }
		public virtual string Concesionario { get; set; }
		public virtual string SecNro { get; set; }

	}
	public class AutoahorroOfertaaoap : ClassMap<AutoahorroOferta>
	{
		public AutoahorroOfertaaoap()
		{
			Table("AutoahorroOferta");

			Id(aao => aao.CodAutoahorroOferta);
			References(aao => aao.ArchivoAutoahorro).Column("CodArchivoAutoahorro").Cascade.All();
			Map(aao => aao.Grupo);
			Map(aao => aao.Orden);
			Map(aao => aao.Modelo);
			Map(aao => aao.TAjustado);
			Map(aao => aao.TLicitado);
			Map(aao => aao.Observacion);
			Map(aao => aao.Concesionario);
			Map(aao => aao.SecNro);
		}
	}
}
