using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace Matassi.Dominio
{
	public class UserProfile
	{
		public virtual int UserId { get; set; }
		public virtual string UserName { get; set; }
	}

	public class UserProfileMap : ClassMap<UserProfile>
	{
		public UserProfileMap()
		{
			Table("UserProfile");

			Id(m => m.UserId);
			Map(m => m.UserName);
		}
	}
}
