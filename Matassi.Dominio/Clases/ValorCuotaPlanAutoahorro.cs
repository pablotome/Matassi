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
	public class ValorCuotaPlanAutoahorro
	{
		public virtual int CodValorCuotaPlanAutoahorro { get; set; }
		public virtual PlanAutoahorro PlanAutoahorro { get; set; }
		public virtual string RangoCuota { get; set; }
		public virtual string Valor { get; set; }
		public virtual int Orden { get; set; }

	}
	public class ValorCuotaPlanAutoahorroMap : ClassMap<ValorCuotaPlanAutoahorro>
	{
		public ValorCuotaPlanAutoahorroMap()
		{
			Table("ValorCuotaPlanAutoahorro");

			Id(vcpa => vcpa.CodValorCuotaPlanAutoahorro);
			References(vcpa => vcpa.PlanAutoahorro).Column("CodPlanAutoahorro");
			Map(vcpa => vcpa.RangoCuota);
			Map(vcpa => vcpa.Valor);
			Map(vcpa => vcpa.Orden);
		}
	}
}
