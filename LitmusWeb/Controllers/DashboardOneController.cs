using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class DashboardOneController : Controller
    {
        // GET: DashboardOne
        public ActionResult Index()
        {
            return View("Management");
        }
    }
}