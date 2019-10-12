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
	public class AccesosHome
	{
		public virtual int CodAccesosHome { get; set; }
		public virtual string Titulo { get; set; }
		public virtual char CodTipoImagen { get; set; }
		public virtual int CodImagen { get; set; }
		public virtual string Link { get; set; }
		public virtual int Orden { get; set; }
		public virtual string ClaseCSSTitulo { get; set; }
		public virtual bool Vigente { get; set; }

		public class TipoImagen
		{
			public const int Modelo = 1;
			public const int Accesorio = 1;
		}

	}
	public class AccesosHomeMap : ClassMap<AccesosHome>
	{
		public AccesosHomeMap()
		{
			Table("AccesosHome");

			Id(am => am.CodAccesosHome);
			Map(am => am.Titulo);
			Map(am => am.CodTipoImagen);
			Map(am => am.CodImagen);
			Map(am => am.Link);
			Map(am => am.Orden);
			Map(am => am.ClaseCSSTitulo);
			Map(am => am.Vigente);
		}
	}
}
