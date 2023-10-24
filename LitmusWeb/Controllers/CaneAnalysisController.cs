using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using a = LitmusWeb.Models.CustomModels;
using b = DataAccess.CustomModels;


namespace LitmusWeb.Controllers
{
    public class CaneAnalysisController : Controller
    {

        CaneAnalysisRepository caneRepo;
        public CaneAnalysisController()
        {
            caneRepo = new CaneAnalysisRepository();
        }

        // GET: CaneAnalysis
        public ActionResult Index()
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<a.CaneAnalysisViewModel> model = new List<a.CaneAnalysisViewModel>();
            List<b.CaneAnalysisViewModel> entity = new List<b.CaneAnalysisViewModel>();
            entity = caneRepo.GetCaneAnalysisViewModelList(Convert.ToInt32(Session["BaseUnitCode"]), Convert.ToInt32(Session["CrushingSeason"]));
            if (entity != null)
            {
                foreach (var x in entity)
                {
                    a.CaneAnalysisViewModel temp = new a.CaneAnalysisViewModel()
                    {
                        Id = x.Id,
                        UnitCode = x.UnitCode,
                        SampleDate = x.SampleDate,
                        AnalysisDate = x.AnalysisDate,
                        ZoneCode = x.ZoneCode,
                        ZoneName = x.ZoneName,
                        VillageCode = x.VillageCode,
                        VillageName = x.VillageName,
                        GrowerCode = x.GrowerCode,
                        GrowerName = x.GrowerName,
                        RelativeName = x.RelativeName,
                        VarietyCode = x.VarietyCode,
                        VarietyName = x.VarietyName,
                        CaneType = x.CaneType,
                        LandPosition = x.LandPosition,
                        FieldCondition = x.FieldCondition,
                        JuicePercent = x.JuicePercent,
                        Brix = x.Brix,
                        Pol = x.Pol,
                        Purity = x.Purity,
                        PolInCaneToday = x.PolInCaneToday,
                        RecoveryByWCapToday = x.RecoveryByWCapToday,
                        RecoveryByMolPurityToday = x.RecoveryByMolPurityToday,
                        PreviousSeasonHarvestingDate = x.PreviousSeasonHarvestingDate,
                        SeasonCode = x.SeasonCode,

                    };
                    model.Add(temp);
                }
            }
            return View(model);
        }

        [HttpGet]
        [CustomAuthorizationFilter("Developer", "Super Admin", "Unit Admin", "Cane Analysis")]
        public ActionResult Add()
        {
            CaneAnalysisModel m = new CaneAnalysisModel();

            return View(m);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Developer", "Super Admin", "Unit Admin", "Cane Analysis")]
        [ValidationFilter("Add")]
        public ActionResult AddPost(CaneAnalysisModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View();
            }

            CaneAnalys entity = new CaneAnalys()
            {
                UnitCode = Convert.ToInt32(Session["BaseUnitCode"]),
                SampleDate = model.SampleDate,
                AnalysisDate = model.AnalysisDate,
                ZoneCode = model.ZoneCode,
                VillageCode = model.VillageCode,
                GrowerCode = model.GrowerCode,
                VarietyCode = model.VarietyCode,
                CaneType = model.CaneType,
                LandPosition = model.LandPosition,
                FieldCondition = model.FieldCondition,
                JuicePercent = model.JuicePercent,
                Brix = model.Brix,
                Pol = model.Pol,
                Purity = model.Purity,
                PolInCaneToday = model.PolInCaneToday,
                RecoveryByWCapToday = model.RecoveryByWCapToday,
                RecoveryByMolPurityToday = model.RecoveryByMolPurityToday,
                PreviousSeasonHarvestingDate = model.PreviousSeasonHarvestingDate,
                CreatedAt = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString(),
                SeasonCode = Convert.ToInt32(Session["CrushingSeason"]),
                IrrigatedDays = model.IrrigatedDays,
                MinTemperature = model.MinTemperature,
                MaxTemperature = model.MaxTemperature,
                Humidity = model.Humidity
            };
            string responseMessage = caneRepo.Add(entity);
            if (responseMessage.Split(':')[0] == "Success")
            {
                WriteCookie(model.SampleDate.Date, model.AnalysisDate.Date, model.PreviousSeasonHarvestingDate);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("PageError", responseMessage);
                return View();
            }
        }


        [HttpGet]
        public ActionResult GetExcelReport()
        {
            a.ReportParamUnitSeasonDate model = new a.ReportParamUnitSeasonDate();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GetExcelReport")]
        public ActionResult GetExcelReportPost(a.ReportParamUnitSeasonDate m)
        {
            a.ReportParamUnitSeasonDate model = new a.ReportParamUnitSeasonDate();
            CaneAnalysisReportRepository cRepo = new CaneAnalysisReportRepository();
            string filePath = Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/CaneAnalysis/");
            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            int seasonCode = Convert.ToInt32(Session["CrushingSeason"]);
            string fileName = cRepo.ExcelReport_CaneAnalysisBySampleDate(unitCode, seasonCode, m.reportate, filePath);
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        [HttpGet]
        public ActionResult Edit(CaneAnalys model)
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            // check if user's current unit code and session unit code matches
            if (Convert.ToInt16(Session["BaseUnitCode"]) == model.UnitCode)
            {
                return View();
            }
            return RedirectToAction("Forbidden", "Error");

        }

        public void WriteCookie(DateTime sampleDate, DateTime analysisDate, DateTime prvSeasonHarvestDate)
        {
            HttpCookie caneAnalysisCookie = new HttpCookie("caneAnalysisCookie");
            caneAnalysisCookie.Values.Set("sampleDate", sampleDate.ToShortDateString());
            caneAnalysisCookie.Values.Set("analysisDate", analysisDate.ToShortDateString());
            caneAnalysisCookie.Values.Set("prvSeasonHarvestingDate", prvSeasonHarvestDate.ToShortDateString());
            //caneAnalysisCookie["sampleDate"] = sampleDate.ToShortDateString();
            //caneAnalysisCookie["analysisDate"] = analysisDate.ToShortDateString();
            caneAnalysisCookie.Expires = DateTime.Now.AddHours(10);

            Response.Cookies.Add(caneAnalysisCookie);
        }
    }
}