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
	public class ClubMatassiCatalogo
	{
		public virtual int CodClubMatassiCatalogo { get; set; }
		public virtual string TituloPremio { get; set; }
		public virtual string DescripcionPremio { get; set; }
		public virtual int CantidadPuntos { get; set; }
	}

	public class ClubMatassiCatalogoMap : ClassMap<ClubMatassiCatalogo>
	{
		public ClubMatassiCatalogoMap()
		{
			Table("ClubMatassiCatalogo");

			Id(cmc => cmc.CodClubMatassiCatalogo).GeneratedBy.Identity();
			Map(cmc => cmc.TituloPremio);
			Map(cmc => cmc.DescripcionPremio);
			Map(cmc => cmc.CantidadPuntos);
		}
	}
}
