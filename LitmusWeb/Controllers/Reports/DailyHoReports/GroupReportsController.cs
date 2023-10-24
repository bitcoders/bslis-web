using DataAccess;
using DataAccess.CustomModels.Reports;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository.HoReports.DailyReports;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LitmusWeb.Filters;
namespace LitmusWeb.Controllers.Reports.DailyHoReports
{

    [CustomAuthorizationFilter("Super Admin", "Group Reports")]
    public class GroupReportsController : Controller
    {
        // GET: DailyReports
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public ActionResult DailyHoReports()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            MasterUnitRepository unitRepo = new MasterUnitRepository();
            UnitSeasonsRepository unitSeasonRepo = new UnitSeasonsRepository();
            MasterSeasonRepository seasonRepository = new MasterSeasonRepository();
            ReportDetailsRepository reportRepository = new ReportDetailsRepository();
            List<ReportDetail> Entity = new List<ReportDetail>();
            List<MasterSeason> seasons = new List<MasterSeason>();
            List<MasterSeasonModel> seasonModel = new List<MasterSeasonModel>();
            Entity = reportRepository.GetExcelTemplateReportDetails(true).Where(x => x.ReportCategory == "Daily").ToList();
            seasons = seasonRepository.GetMasterSeasonList();
            List<ReportDetail> reportDetails = new List<ReportDetail>();

            if (Entity == null)
            {
                return View();
            }
            List<ReportDetailsModel> rModel = new List<ReportDetailsModel>();
            foreach (var e in Entity)
            {
                ReportDetailsModel temp = new ReportDetailsModel()
                {
                    Code = e.Code,
                    Name = e.Name,
                    Description = e.Description,
                    Formats = e.Formats,
                    ReportCategory = e.ReportCategory,
                    ReportSchemaCode = e.ReportSchemaCode,
                    CreatedAt = e.CreatedAt,
                    CreatedBy = e.CreatedBy
                };
                rModel.Add(temp);
            }
            if (seasons.Count > 0)
            {
                foreach (var x in seasons)
                {
                    MasterSeasonModel temp = new MasterSeasonModel()
                    {
                        SeasonCode = x.SeasonCode,
                        SeasonYear = x.SeasonYear,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate
                    };
                    seasonModel.Add(temp);
                }
            }

            DailyReportViewModel viewModel = new DailyReportViewModel()
            {
                reportDetailsModel = rModel,
                masterSeasonsModel = seasonModel
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "DailyHoReports")]
        public ActionResult DailyReportsPost(FormCollection formData)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            DailyCrushingReport reportRepo = new DailyCrushingReport();

            //int unitCode = Convert.ToInt32(formData["reportUnit"]);
            int unitCode = 1;
            int seasonCode = Convert.ToInt32(formData["season_code"]);
            DateTime ReportDate = Convert.ToDateTime(formData["txtReportDate"]);
            int reportCode = Convert.ToInt32(formData["report_name"]);
            ReportParameterModel paramModel = new ReportParameterModel()
            {
                UnitCode = unitCode,
                SeasonCode = seasonCode,
                ReportDate = ReportDate,
                ReportCode = reportCode

            };
            //string filePath;
            //string fileName;
            //filePath = filePath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/HO Reports/");


            //fileName = reportRepo.GeneratePdf(filePath,paramModel);
            //return File(fileName, "application/pdf");


            string filename = reportRepo.GenerateExcel(paramModel);
            return File(filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}