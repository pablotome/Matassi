using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Matassi.Web.Clases
{
	public class AttributeHelper
	{
		public class EnforceTrueAttribute : ValidationAttribute, IClientValidatable
		{
			public override bool IsValid(object value)
			{
				if (value == null) return false;
				if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");
				return (bool)value == true;
			}

			public override string FormatErrorMessage(string name)
			{
				return "The " + name + " field must be checked in order to continue.";
			}

			public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
			{
				yield return new ModelClientValidationRule
				{
					ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
					ValidationType = "enforcetrue"
				};
			}
		}

		public class HttpParamActionAttribute : ActionNameSelectorAttribute
		{
			public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
			{
				if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
					return true;

				var request = controllerContext.RequestContext.HttpContext.Request;
				return request[methodInfo.Name] != null;
			}
		}

		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
		public sealed class NoCacheAttribute : ActionFilterAttribute
		{
			public override void OnResultExecuting(ResultExecutingContext filterContext)
			{
				filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
				filterContext.HttpContext.Response.Cache.SetNoStore();
				filterContext.HttpContext.Response.Cache.SetNoServerCaching();
				/*filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
				filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				filterContext.HttpContext.Response.Cache.SetNoStore();
				filterContext.HttpContext.Response.Cache.SetMaxAge(new TimeSpan(0));
				filterContext.HttpContext.Response.Cache.SetValidUntilExpires(true);*/

				base.OnResultExecuting(filterContext);
			}
		}
	}

	public static class AttributeHelperST
	{
		public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
		{
			return ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, new ViewDataDictionary<TModel>(model)).DisplayName;
		}
	}
}