using LitmusWeb.Models;
using System.Web.Mvc;
using System.Collections.Generic;
namespace LitmusWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult InternalServerError(ErrorViewModel error)
        {
            return View(error);
        }
        public ActionResult Forbidden(string ErrorMessage = "")
        {
            ViewBag.ErrorMessage = ErrorMessage;
            return View();
        }
        public ActionResult Maintenance()
        {
            return View();
        }
        public ActionResult PageException(string error = "")
        {
            @ViewBag.ErrorMessage = error;
            return View();
        }

        public ActionResult ModelError()
        {
            ErrorViewModel errorModel = (ErrorViewModel)TempData["Model"];
            return View(errorModel);
        }

    }
}