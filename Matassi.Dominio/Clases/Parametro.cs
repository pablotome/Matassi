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
	public class Parametro
	{
		public virtual string CodParametro { get; set; }
		public virtual string DesParametro { get; set; }
		public virtual string Valor { get; set; }
	}

	public class ParametroMap : ClassMap<Parametro>
	{
		public ParametroMap()
		{
			Table("Parametro");

			Id(p => p.CodParametro).GeneratedBy.Assigned();
			Map(p => p.DesParametro);
			Map(p => p.Valor);
		}
	}
}
