using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using FluentNHibernate.Mapping;

namespace Matassi.Dominio
{
	public class CaracteristicaModelo
	{
		public virtual int CodCaracteristicaModelo { get; set; }
		public virtual string DesCaracteristicaModelo { get; set; }
		public virtual IList<VersionModelo> Versiones { get; set; }

		[DataType(DataType.MultilineText)]
		public virtual string MuchasCarcteristicas { get; set; }
	}

	public class CaracteristicaModeloMap : ClassMap<CaracteristicaModelo>
	{
		public CaracteristicaModeloMap()
		{
			Table("CaracteristicaModelo");

			Id(cm => cm.CodCaracteristicaModelo);
			Map(cm => cm.DesCaracteristicaModelo);

			HasManyToMany(x => x.Versiones)
				.Cascade.All()
				.ParentKeyColumn("CodCaracteristicaModelo")
				.ChildKeyColumn("CodVersionModelo")
				.Table("CaracteristicaVersion");
		}
	}

}
