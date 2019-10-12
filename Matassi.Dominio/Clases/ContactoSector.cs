using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace Matassi.Dominio
{
	public class ContactoSector
	{
		public virtual int CodContactoSector { get; set; }
		public virtual SectorInterno SectorInterno { get; set; }
		public virtual string TipoContacto { get; set; }
		public virtual string Contacto { get; set; }
		public virtual bool EsEmail { get; set; }
		public virtual int Orden { get; set; }
	}

	public class ContactoSectorMap : ClassMap<ContactoSector>
	{
		public ContactoSectorMap()
		{
			Table("ContactoSector");

			Id(cs => cs.CodContactoSector);
			References(cs => cs.SectorInterno).Column("CodSectorInterno");
			Map(cs => cs.TipoContacto);
			Map(cs => cs.Contacto);
			Map(cs => cs.EsEmail);
			Map(cs => cs.Orden);
		}
	}
}
