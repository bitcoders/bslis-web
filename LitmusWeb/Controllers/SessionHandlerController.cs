using DataAccess.Repositories;
using System;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    public class SessionHandlerController : Controller
    {
        // GET: SessionHandler
        MasterUnitRepository repository = new MasterUnitRepository();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateSessionBaseUnit(int id)
        {
            try
            {
                var masterunits = repository.FindUnitByPk(id);
                Session["BaseUnitCode"] = id;

                Session["UnitName"] = masterunits.Name;
                return RedirectToAction("Index", "Litmus");
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return View();
            }
        }




    }
}