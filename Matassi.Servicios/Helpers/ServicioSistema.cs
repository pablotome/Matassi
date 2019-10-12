using System;
using System.Data;
using System.Web;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Context;
using NHibernate.Linq;

using Matassi.Dominio;

namespace Matassi.Servicios
{
	public partial class ServicioSistema<T> where T : class
	{
		//private static readonly IRepository<BaseClass> storeRepository = new Repository<BaseClass>();
		static object syncObject = new object();
		static ITransaction transaction = null;

		public static ISession Session
		{
			get
			{
				if (!WebSessionContext.HasBind(ServiciosApp.SessionFactory))
					WebSessionContext.Bind(ServiciosApp.SessionFactory.OpenSession());
				return ServiciosApp.SessionFactory.GetCurrentSession();
			}
		}

		public static IQueryable<T> Get(Expression<Func<T, bool>> predicate)
		{
			return GetAll().Where(predicate);
		}

		/// <summary>
		/// Devuelve el primer elemento encontrado
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T GetById(Expression<Func<T, bool>> predicate)
		{
			List<T> listaResultado = GetAll().Where(predicate).ToList<T>();
			if (listaResultado.Count > 0)
				return listaResultado.First<T>();
			return default(T);
		}

		public static void Delete(Expression<Func<T, bool>> predicate)
		{
			T obj = GetAll().Where(predicate).FirstOrDefault<T>();
			
			if (obj != null)
			{ 
				Session.Delete(obj);
				Session.Flush();
			}
		}

		public static void Delete(T entity)
		{
			Session.Delete(entity);
			Session.Flush();
		}

		public static IEnumerable<T> SaveOrUpdateAll(params T[] entities)
		{
			foreach (var entity in entities)
			{
				Session.SaveOrUpdate(entity);
			}

			return entities;
		}

		public static T SaveOrUpdate(T entity)
		{
			Session.SaveOrUpdate(entity);
			Session.Flush();
			return entity;
		}

		public static T SaveOrUpdateWithoutFlush(T entity)
		{
			Session.SaveOrUpdate(entity);
			return entity;
		}


		public static IQueryable<T> GetAll()
		{
			return Session.Query<T>();
		}

		public static void BeginTransaction()
		{
			if (transaction == null || !transaction.IsActive)
				transaction = Session.BeginTransaction();
		}

		public static void CommitTransaction()
		{

			if (transaction != null && transaction.IsActive)
				transaction.Commit();
		}

		public static void RollbackTransaction()
		{
			if (transaction != null && transaction.IsActive)
				transaction.Rollback();
		}

		public static bool Contains(T entity)
		{
			return Session.Contains(entity);
		}

		public static void Flush()
		{
			Session.Flush();
		}

		/*public static T Merge(T entity)
		{
			ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();
				Session.Merge(entity);
				Session.Flush();
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
			return entity;
		}*/

		public static void ExecuteUpdateHQL(string hql)
		{
			Session.CreateQuery(hql).ExecuteUpdate();

			/*ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();
				Session.CreateQuery(hql).ExecuteUpdate();
				Session.Flush();
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}*/
		}

		public static IList<T> ExecuteSelectHQL<T>(string hql)
		{
			return (IList<T>)Session.CreateQuery(hql).List<T>();

			/*ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();
				Session.CreateQuery(hql).ExecuteUpdate();
				Session.Flush();
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}*/
		}

		public static DateTime GetDate()
		{
			var query = Session.CreateSQLQuery("SELECT Getdate();");
			DateTime results = (DateTime)query.UniqueResult();

			return results;
		}


		public static IList<T> RetrieveAll()
		{
			return RetrieveAll(null);
		}

		public static IList<T> RetrieveAll(Expression<Func<T, bool>> predicate)
		{
			// Retrieve all objects of the type passed in
			//ICriteria targetObjects = Session.CreateCriteria(typeof(T)).;//.SetFetchMode("Promocion.ParticipantePromocion", FetchMode.Select);
			IQueryOver<T> targetObjects;
			if (predicate != null)
				targetObjects = Session.QueryOver<T>().Where(predicate);
			else
				targetObjects = Session.QueryOver<T>();
			//targetObjects.SetFetchSize(0);
			IList<T> itemList = targetObjects.List<T>();

			// Set return value
			return itemList;
		}

		public static object ExecuteSQLQueryUniqueResult(string sql)
		{
			ISQLQuery sqlQuery = Session.CreateSQLQuery(sql);
			return sqlQuery.UniqueResult();
		}

		/*public static void ExecuteSQLSPNoResult(string sql)
		{
			ISQLQuery sqlQuery = Session.CreateSQLQuery(sql);
			sqlQuery.UniqueResult();
		}*/

		/*public static DataSet ExecuteSQLQueryDataSet(string sql)
		{
			using (SysNet sysnet = new SysNet(ProviderType.SqlServer, Session.Connection))
			{
				DataSet ds = sysnet.DB.ExecuteReturnDS(sql);
				return ds;
			}
		}*/

	}
}
