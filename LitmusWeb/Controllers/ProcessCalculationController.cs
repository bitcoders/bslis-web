using CaptchaMvc.HtmlHelpers;
using DataAccess;
using DataAccess.CustomModels;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
    [UnitSeasonFilter]
    public class ProcessCalculationController : Controller
    {
        DateTime processDate;
        ProcessCalculationRepository Repository = new ProcessCalculationRepository();
        LedgerDataRepository LedgerDataRepository = new LedgerDataRepository();
        // GET: ProcessCalculation
        [ValidationFilter("create")]

        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            ledger_data Entity = new ledger_data();
            int baseUnitcode = Convert.ToInt16(Session["BaseUnitCode"]);
            GetUnitDefaultValues(baseUnitcode);
            Entity = LedgerDataRepository.GetLedgerDataForTheDate(baseUnitcode, ViewBag.CrushingSeason, Convert.ToDateTime(ViewBag.ProcessDate));
            if (Entity == null)
            {
                return View();
            }
            LedgerDataModel Model = new LedgerDataModel()
            {
                estimated_sugar_percent_cane = Entity.estimated_sugar_percent_cane,
                estimated_molasses_percent_cane = Entity.estimated_molasses_percent_cane,
                fiber_percent_cane = Entity.fiber_percent_cane
            };
            return View(Model);
        }
        [HttpPost]
        [ActionName(name: "Index")]
        [ValidateAntiForgeryToken]
        [ValidationFilter("create")]
        public ActionResult IndexPost(FormCollection formData)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (this.IsCaptchaValid("Captcha is valid"))
            {


                ViewBag.CaptchaErrorMessage = "Success";
                int baseUnitcode = Convert.ToInt16(Session["BaseUnitCode"]);
                GetUnitDefaultValues(baseUnitcode);

                bool result = Repository.ProcessCalculation(baseUnitcode, ViewBag.CrushingSeason, processDate.ToString());
                if (!result)
                {
                    return View("Error");
                }
                ledger_data Entity = new ledger_data();
                Entity = LedgerDataRepository.GetLedgerDataForTheDate(baseUnitcode, ViewBag.CrushingSeason, processDate);
                if (Entity == null)
                {
                    return View("Error");
                }
                LedgerDataModel Model = new LedgerDataModel()
                {
                    estimated_sugar_percent_cane = Entity.estimated_sugar_percent_cane,
                    estimated_molasses_percent_cane = Entity.estimated_molasses_percent_cane,
                    fiber_percent_cane = Entity.fiber_percent_cane
                };
                return View(Model);
            }
            else
            {
                ViewBag.CaptchaErrorMessage = "Captch mismatch, try again!";
            }
            return View();
        }



        [HttpGet]
        [ValidationFilter("Admin")]
        [CustomAuthorizationFilter("Super Admin", "Process Date Range")]
        public ActionResult ProcessForDateRange()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationFilter("Admin")]
        [CustomAuthorizationFilter("Super Admin", "Process Date Range")]
        [ActionName(name: "ProcessForDateRange")]
        public ActionResult ProcessForDateRangePost(FormCollection formData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ProcessCalculationModel model = new ProcessCalculationModel()
            {
                UnitCode = Convert.ToInt32(formData["unit_code"]),
                SeasonCode = Convert.ToInt32(formData["season_code"]),
                FromDate = formData["txtFromDate"].ToString(),
                ToDate = formData["txtToDate"].ToString()
            };

            bool result = Repository.ProcessCalculationForDateRange(model);
            if (!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Litmus");
        }

        [NonAction]
        private void GetUnitDefaultValues(int unitCode)
        {
            MasterUnitRepository unitRepository = new MasterUnitRepository();
            var unitValues = unitRepository.FindUnitByPk(unitCode);
            ViewBag.CrushingSeason = unitValues.CrushingSeason;
            processDate = unitValues.ProcessDate;
            ViewBag.ProcessDate = unitValues.ProcessDate.ToShortDateString();
        }

    }
}
