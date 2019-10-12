using System;
using System.Text;
using System.Reflection;
using System.Web;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

using log4net;

namespace Matassi.Servicios
{
	public class ServiciosApp
	{
		//public static readonly Configuration Configuration = null;
		public static ISessionFactory _sessionFactory = null;
		static readonly object factorylock = new object();

		private static readonly ILog logError = LogManager.GetLogger("AlianzasError");

		static ServiciosApp()
		{
		}

		private static void BuildSchema(Configuration cfg)
		{
			new SchemaUpdate(cfg);
		}

		public static ISessionFactory SessionFactory
		{
			get {
				if (_sessionFactory == null)
					CrearSessionFactory();

				return _sessionFactory; 
			}
		}

		private static void CrearSessionFactory()
		{
			try
			{
				lock (factorylock)
				{
					log4net.Config.XmlConfigurator.Configure();
					StringBuilder basePath = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory);
					if (basePath.ToString().EndsWith("\\") == false)
						basePath.Append("\\");
					basePath.Append(@"bin\Matassi.Dominio.dll");

					Assembly asm = Assembly.LoadFile(basePath.ToString());

					_sessionFactory = Fluently
						.Configure()
						//.Database(MsSqlConfiguration.MsSql2008.MsSql2008.Driver<IntranetSqlClientDriver>().Provider<IntranetDriverConnectionProvider>())
						.Database(MsSqlConfiguration.MsSql2008.ConnectionString( c => c
							.FromConnectionStringWithKey( "cnMatassi" ) ) )
						.Mappings(m => m.FluentMappings.AddFromAssembly(asm))
						.ExposeConfiguration(c =>
						{
							BuildSchema(c);
							c.Properties[NHibernate.Cfg.Environment.CurrentSessionContextClass] = "web";
							//call
							//"managed_web";No current session context configured.
							c.Properties[NHibernate.Cfg.Environment.Hbm2ddlKeyWords] = "none";
							//c.Properties[NHibernate.Cfg.Environment.CurrentSessionContextClass] = "wcf_operation";
							//c.Properties[NHibernate.Cfg.Environment.CurrentSessionContextClass] = typeof (LazySessionContext).AssemblyQualifiedName;
						})
						.BuildSessionFactory();

					if (!_sessionFactory.IsClosed)
						_sessionFactory.Close();
				}
			}
			catch (Exception ex)
			{
				logError.Error("Error al crear SessionFactory - Verificar permisos del usuario", ex);
			}
		}
	}
}
