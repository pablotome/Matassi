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
	public class GarantiaPeriodoIntervalo
	{
		public virtual int CodGarantiaPeriodoIntervalo { get; set; }
		public virtual string ModeloGarantia { get; set; }
		public virtual string GarantiaVW { get; set; }
		public virtual string ServiciosCada { get; set; }
		public virtual string ServiciosManoObraSinCargo { get; set; }
		public virtual int Orden { get; set; }
		public virtual bool Vigente { get; set; }
	}

	public class GarantiaPeriodoIntervaloMap : ClassMap<GarantiaPeriodoIntervalo>
	{
		public GarantiaPeriodoIntervaloMap()
		{
			Table("GarantiaPeriodoIntervalo");

			Id(sm => sm.CodGarantiaPeriodoIntervalo).GeneratedBy.Identity();
			Map(sm => sm.ModeloGarantia);
			Map(sm => sm.GarantiaVW);
			Map(sm => sm.ServiciosCada);
			Map(sm => sm.ServiciosManoObraSinCargo);
			Map(sm => sm.Orden);
			Map(sm => sm.Vigente);
		}
	}
}
