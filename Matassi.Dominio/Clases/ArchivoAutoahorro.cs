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
	public class ArchivoAutoahorro
	{
		public virtual int CodArchivoAutoahorro { get; set; }
		public virtual string NombreArchivo { get; set; }
		public virtual DateTime FechaAlta { get; set; }
		public virtual int Acto { get; set; }
		public virtual DateTime Fecha { get; set; }
		public virtual DateTime OfertasRecibidas { get; set; }
		public virtual string Concesionario { get; set; }
		public virtual bool Activo { get; set; }
		public virtual int CantidadRegistros { get; set; }

	}
	public class ArchivoAutoahorroMap : ClassMap<ArchivoAutoahorro>
	{
		public ArchivoAutoahorroMap()
		{
			Table("ArchivoAutoahorro");

			Id(aa => aa.CodArchivoAutoahorro);
			Map(aa => aa.NombreArchivo);
			Map(aa => aa.FechaAlta);
			Map(aa => aa.Acto);
			Map(aa => aa.Fecha);
			Map(aa => aa.OfertasRecibidas);
			Map(aa => aa.Concesionario);
			Map(aa => aa.Activo);
			Map(aa => aa.CantidadRegistros);
		}
	}
}
