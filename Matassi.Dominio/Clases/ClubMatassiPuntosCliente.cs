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
	public class ClubMatassiPuntosCliente
	{
		public virtual int CodClubMatassiPuntosCliente { get; set; }
		public virtual string Patente { get; set; }
		public virtual int CantidadPuntos { get; set; }
	}

	public class ClubMatassiPuntosClienteMap : ClassMap<ClubMatassiPuntosCliente>
	{
		public ClubMatassiPuntosClienteMap()
		{
			Table("ClubMatassiPuntosCliente");

			Id(cmpc => cmpc.CodClubMatassiPuntosCliente).GeneratedBy.Identity();
			Map(cmpc => cmpc.Patente);
			Map(cmpc => cmpc.CantidadPuntos);
		}
	}
}
