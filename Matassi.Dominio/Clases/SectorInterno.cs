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
	public class SectorInterno
	{
		public virtual int CodSectorInterno { get; set; }
		public virtual string DesSectorInterno { get; set; }
		public virtual int Orden { get; set; }
	}
	public class SectorInternoMap : ClassMap<SectorInterno>
	{
		public SectorInternoMap()
		{
			Table("SectorInterno");

			Id(si => si.CodSectorInterno).GeneratedBy.Assigned();
			Map(si => si.DesSectorInterno);
			Map(si => si.Orden);
		}
	}
}
