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
	public class AutoahorroEmision : AutoahorroDato
	{
		public virtual int CodAutoahorroEmision { get; set; }
		//public virtual ArchivoAutoahorro ArchivoAutoahorro { get; set; }
		public virtual int Gpo { get; set; }
		public virtual int Ord { get; set; }
		public virtual int Dc { get; set; }
		public virtual int Desv { get; set; }
		public virtual int Cuot { get; set; }
		public virtual int Dc2 { get; set; }
		public virtual DateTime Vence { get; set; }
		public virtual string Plan { get; set; }
		public virtual string Mod { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Banco { get; set; }
		public virtual string Sucursal { get; set; }
		public virtual string Cuenta { get; set; }
		public virtual float Alicuota { get; set; }
		public virtual float Cargos { get; set; }
		public virtual float Actalicuota { get; set; }
		public virtual float Caactalic { get; set; }
		public virtual float SegVida { get; set; }
		public virtual float SegBien { get; set; }
		public virtual float Mora { get; set; }
		public virtual float DebCred { get; set; }
		public virtual float Intliquid { get; set; }
		public virtual float Otros { get; set; }
		public virtual float Total { get; set; }
	}
 
	public class AutoahorroEmisionaoap : ClassMap<AutoahorroEmision>
	{
		public AutoahorroEmisionaoap()
		{
			Table("AutoahorroEmision");

			Id(aae => aae.CodAutoahorroEmision);
			References(aae => aae.ArchivoAutoahorro).Column("CodArchivoAutoahorro").Cascade.All();
			Map(aae => aae.Gpo);
			Map(aae => aae.Ord);
			Map(aae => aae.Dc);
			Map(aae => aae.Desv);
			Map(aae => aae.Cuot);
			Map(aae => aae.Dc2);
			Map(aae => aae.Vence);
			Map(aae => aae.Plan).Column("_Plan");
			Map(aae => aae.Mod);
			Map(aae => aae.Nombre);
			Map(aae => aae.Banco);
			Map(aae => aae.Sucursal);
			Map(aae => aae.Cuenta);
			Map(aae => aae.Alicuota);
			Map(aae => aae.Cargos);
			Map(aae => aae.Actalicuota);
			Map(aae => aae.Caactalic);
			Map(aae => aae.SegVida);
			Map(aae => aae.SegBien);
			Map(aae => aae.Mora);
			Map(aae => aae.DebCred);
			Map(aae => aae.Intliquid);
			Map(aae => aae.Otros);
			Map(aae => aae.Total);
		}
	}
}
