using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matassi.Web.Areas.Web.Controllers
{
    public class ErrorController : Controller
    {
		// GET: /Error/HttpError404
		public ActionResult HttpError404(string message)
		{
			//return View("HttpError404", message);
			//return View("HttpError404", model: message);
			return RedirectToAction("Inicio", "Home");
		}
	}
}