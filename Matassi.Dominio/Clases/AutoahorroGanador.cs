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
	public class AutoahorroGanador : AutoahorroDato
	{
		public virtual int CodAutoahorroGanador { get; set; }
		//public virtual ArchivoAutoahorro ArchivoAutoahorro { get; set; }
		public virtual int Grupo{ get; set; }
		public virtual int Orden{ get; set; }
		public virtual string Nombre{ get; set; }
		public virtual string Tipo{ get; set; }
		public virtual float Monto{ get; set; }
		public virtual int Grilla{ get; set; }
		public virtual string Concesionario{ get; set; }
	}

	public class AutoahorroGanadorMap : ClassMap<AutoahorroGanador>
	{
		public AutoahorroGanadorMap()
		{
			Table("AutoahorroGanador");

			Id(aag => aag.CodAutoahorroGanador);
			References(aae => aae.ArchivoAutoahorro).Column("CodArchivoAutoahorro").Cascade.All(); ;
			Map(aag => aag.Grupo);
			Map(aag => aag.Orden);
			Map(aag => aag.Nombre);
			Map(aag => aag.Tipo);
			Map(aag => aag.Monto);
			Map(aag => aag.Grilla);
			Map(aag => aag.Concesionario);
		}
	}
}
