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
	public class ServicioMantenimiento
	{
		public virtual int CodServicioMantenimiento { get; set; }
		public virtual string DesServicioMantenimiento { get; set; }
		public virtual int Orden { get; set; }
		public virtual bool Vigente { get; set; }
	}

	public class ServicioMantenimientoMap : ClassMap<ServicioMantenimiento>
	{
		public ServicioMantenimientoMap()
		{
			Table("ServicioMantenimiento");

			Id(sm => sm.CodServicioMantenimiento).GeneratedBy.Identity();
			Map(sm => sm.DesServicioMantenimiento);
			Map(sm => sm.Orden);
			Map(sm => sm.Vigente);
		}
	}
}
