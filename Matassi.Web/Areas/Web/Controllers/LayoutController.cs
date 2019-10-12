using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using Matassi.Servicios;

namespace Matassi.Web.Areas.Web.Controllers
{
    public class LayoutController : Controller
    {
		[ChildActionOnly]
		public ActionResult ItemSeminuevos()
		{
			if (ServicioSistema<Seminuevo>.Get(s => s.Publicado == true).ToList().Count > 0)
				return new ContentResult { Content = "<li class=\"itemMenu\"><a href=\"/Seminuevo\">Seminuevos</a></li>" };
			return new ContentResult { Content = string.Empty };
		}
    }
}