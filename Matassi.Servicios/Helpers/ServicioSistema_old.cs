using System;
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
	public partial class ServicioSistema<T>
	{
		//private static readonly IRepository<BaseClass> storeRepository = new Repository<BaseClass>();
		static object syncObject = new object();
        static ITransaction transaction = null;

		public static ISession Session
		{
			get {

				if (!CurrentSessionContext.HasBind(ServiciosApp.SessionFactory))
					CurrentSessionContext.Bind(ServiciosApp.SessionFactory.OpenSession());
				return ServiciosApp.SessionFactory.GetCurrentSession(); }
		}

		/*public static void DisposeCurrentSession()
		{
			ISession currentSession = CurrentSessionContext.Unbind(ServiciosApp.SessionFactory);

			if (currentSession != null)
			{
				if (currentSession.IsOpen)
					currentSession.Close();
				currentSession.Dispose();
			}
		}*/

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
				return listaResultado.FirstOrDefault<T>();
			return default(T);
		}

		public static void Delete(Expression<Func<T, bool>> predicate)
		{
			T obj = GetAll().Where(predicate).First<T>();

			ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();

				Session.Delete(obj);
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

		}

        public static void Delete(T entity)
        {
			ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();

				Session.Delete(entity);
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
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

		/*public static T SaveOrUpdateCopy(T entity)
		{
			ITransaction trans = null;

			try
			{
				trans = Session.BeginTransaction();
				Session.Merge<T>(entity);
				//Log(entity);
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

		/*private static void Log(T entity)
		{
			if (entity.GetType().FullName.Equals("Beneficios.Modelo.Dominio.Beneficio"))
			{
				Logger<Beneficio>.LogObject(entity);
			}
		}*/


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
            Session.BeginTransaction();
        }

        public static void CommitTransaction()
        {

            if (Session.Transaction != null)
                Session.Transaction.Commit();
        }

        public static void RollbackTransaction()
        {
            if (Session.Transaction != null
                && Session.Transaction.IsActive)
                Session.Transaction.Rollback();
        }

        public static DateTime GetDate()
        {
            var query = Session.CreateSQLQuery("SELECT Getdate();");
            DateTime results = (DateTime)query.UniqueResult();

            return results;
        }

		//public static IList<T> GetAll2()
		public static IQueryable<T> GetAll2(Expression<Func<T, T>> selector)
		{
			//return Session.CreateCriteria<T>().SetProjection(Projections.ProjectionList().Add(Projections.Property("Dato"))).List<T>();
			return Session.Query<T>().Select<T, T>(selector);
		}

		
	}
}
