using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace Matassi.Dominio
{
	public class CaracteristicaVersion
	{
		public virtual int CodCaracteristicaVersion { get; set; }
		public virtual VersionModelo VersionModelo { get; set; }
		public virtual CaracteristicaModelo CaracteristicaModelo { get; set; }
	}

	public class CaracteristicaVersionMap : ClassMap<CaracteristicaVersion>
	{
		public CaracteristicaVersionMap()
		{
			Table("CaracteristicaVersion");
			Id(cm => cm.CodCaracteristicaVersion).Column("CodCaracteristicaVersion");
			References(cm => cm.VersionModelo).Column("CodVersionModelo");
			References(cm => cm.CaracteristicaModelo).Column("CodCaracteristicaModelo");
		}
		
	}
}
