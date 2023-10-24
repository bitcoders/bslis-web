using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UnitDailyParameterController : Controller
    {

        readonly MasterUnitRepository Repository = new MasterUnitRepository();
        // GET: UnitDailyParameter
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]

        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterUnit Entity = new MasterUnit();
            Entity = Repository.FindUnitByPk(Convert.ToInt32(Session["BaseUnitCode"]));
            if (Entity == null)
            {
                return View();
            }
            MasterUnitModelForUnitAdmin Model = new MasterUnitModelForUnitAdmin()
            {
                Code = Entity.Code,
                CrushingSeason = Entity.CrushingSeason,
                CrushingStartDate = Entity.CrushingStartDate,
                CrushingEndDate = Entity.CrushingEndDate,
                DayHours = Entity.DayHours,
                EntryDate = Entity.EntryDate,
                ProcessDate = Entity.ProcessDate
            };
            return View(Model);
        }


        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]

        public ActionResult Edit()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterUnit Entity = new MasterUnit();
            Entity = Repository.FindUnitByPk(Convert.ToInt32(Session["BaseUnitCode"]));
            if (Entity == null)
            {
                return View();
            }
            MasterUnitModelForUnitAdmin Model = new MasterUnitModelForUnitAdmin()
            {
                Code = Entity.Code,
                CrushingSeason = Entity.CrushingSeason,
                CrushingStartDate = Entity.CrushingStartDate,
                CrushingEndDate = Entity.CrushingEndDate,
                DayHours = Entity.DayHours,
                EntryDate = Entity.EntryDate,
                ProcessDate = Entity.ProcessDate
            };
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]

        public async Task<ActionResult> EditPost(MasterUnitModelForUnitAdmin Model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            MasterUnit Entity = new MasterUnit()
            {
                Code = Model.Code,
                CrushingSeason = Model.CrushingSeason,
                CrushingStartDate = Model.CrushingStartDate,
                CrushingEndDate = Model.CrushingEndDate,
                DayHours = Model.DayHours,
                EntryDate = Model.EntryDate,
                ProcessDate = Model.ProcessDate
            };
            bool result = await Repository.UpdateUnitDailyParameter(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}