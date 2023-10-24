using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using DataAccess.Repositories.ReportsRepository.DailyReportsPartial;
using DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace LitmusWeb.Controllers.Reports
{
    public class DailyReportsController : Controller
    {
        ReportDetailsRepository reportRepository = new ReportDetailsRepository();
        MasterSeasonRepository seasonRepository = new MasterSeasonRepository();
        // GET: DailyReports
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            SetUnitDefaultValues();
            List<ReportDetail> Entity = new List<ReportDetail>();
            List<MasterSeason> seasons = new List<MasterSeason>();
            List<MasterSeasonModel> seasonModel = new List<MasterSeasonModel>();
            Entity = reportRepository.getReportDetailsList().Where(x => x.ReportCategory == "Daily" && x.AdminOnly == false).ToList();
            seasons = seasonRepository.GetMasterSeasonList();
            if (Entity == null)
            {
                return View();
            }
            List<ReportDetailsModel> Model = new List<ReportDetailsModel>();
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
                Model.Add(temp);
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
                reportDetailsModel = Model,
                masterSeasonsModel = seasonModel
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Index")]
        public async Task<ActionResult> IndexPost(FormCollection formData)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }



            if (formData == null || Convert.ToInt32(formData["report_name"]) == 0 || formData["txtReportDate"].ToString() == string.Empty)
            {
                string errorMessage = "Report date or report type was not selected. Please go back to the report page and select a date and report type!\n" +
                    "Still facing problem, check your session please.";
                return RedirectToAction("PageException", "Error", new { error = errorMessage });
            }



            string userSelectedSeason = formData["season_code"].ToString();
            int selectedUnitForReport = formData["reportUnit"] == null ? Convert.ToInt16(Session["BaseUnitCode"]) : Convert.ToInt16(formData["reportUnit"]);
            SetUnitDefaultValues(userSelectedSeason);
            ReportDetailsRepository reportDetailsRepository = new ReportDetailsRepository();

            DailyReportsRepository D;
            ComparitiveReportExcel comparitiveReportExcel;
            DailyCrushReportExcel dcr;
            ComparitiveReportAllUnitsExcel GroupComparitive;
            DailyManufacturingExcelReport DMR;
            ProductionExcelReport productionExcelReport;
            WestUpExcelReport westUpExcelReport;
            RgSixExcelReport rgSixExcelReport;
            ExcelReportGeneratorRepository excelRepository;


            int formCode = Convert.ToInt32(formData["report_name"]);

            var reportDetails = reportDetailsRepository.GetReportDetails(formCode);
            string filepath = "";
            string fileName = "";

            switch (formCode)
            {
                case 1:
                    D = new PlantPerformance();

                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/PlantPerformance/");
                    fileName = D.GeneratePdf(selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    // fileName = comparitiveReportExcel.ExcelReportFile(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    //fileName = await  comparitiveReportExcel.ExcelReportFile(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);

                    break;
                case 2:
                    comparitiveReportExcel = new ComparitiveReportExcel();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ComparitiveReport.xlsx");
                    //filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/PlantPerformance/");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/ComparitiveReport/");
                    //fileName = D.GeneratePdf(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    fileName = await comparitiveReportExcel.ExcelReportFile(selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 3:
                    dcr = new DailyCrushReportExcel();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=DailyCrushReport.xlsx");
                    //filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/PlantPerformance/");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/DailyCrushingReport/");
                    //fileName = D.GeneratePdf(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    fileName = await dcr.ExcelReportFile(selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 4:
                    GroupComparitive = new ComparitiveReportAllUnitsExcel();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=GroupComparitive.xlsx");
                    //filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/PlantPerformance/");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/GroupComparitive/");
                    //fileName = D.GeneratePdf(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    fileName = GroupComparitive.ExcelReportFile(selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 5:
                    DMR = new DailyManufacturingExcelReport();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=DailyManufacturingReport.xlsx");
                    //filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/PlantPerformance/");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/DMR/");
                    //fileName = D.GeneratePdf(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    fileName = DMR.ExcelReportFile(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 6:
                    productionExcelReport = new ProductionExcelReport();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Production.xlsx");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/Production/");
                    fileName = productionExcelReport.ExcelReportFile(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 7:
                    westUpExcelReport = new WestUpExcelReport();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=WestUP.xlsx");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/WestUP/");
                    fileName = westUpExcelReport.ExcelReportFile(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;
                case 8:
                    rgSixExcelReport = new RgSixExcelReport();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=RgSix.xlsx");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "/RGSix/");
                    fileName = rgSixExcelReport.ExcelReportFile(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath);
                    break;

                /// ========================== Ravi B. 19-04-2020 ====================================
                /// I have created a common class to generate excel report named "DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports.ExcelReportsGeneratorRepository"
                /// Using this repository we can minify the code and we do not need to use switch case.
                /// For now I am use this for newly created reports, at later stage will try to use this class for above all 8 switch cases.
                /// 
                default:
                    excelRepository = new ExcelReportGeneratorRepository();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + reportDetails.Name + ".xlsx");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + selectedUnitForReport.ToString() + "//" + reportDetails.Name + "//");

                    if (reportDetails.IsTemplateBased == true)
                    {
                        fileName = excelRepository.GenerateSingleDateExcelReportBasedOnExcelTemplate(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]));
                    }
                    else
                    {
                        fileName = excelRepository.ExcelReportFile(formCode, selectedUnitForReport, Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(formData["txtReportDate"]), filepath, reportDetails.Name);
                    }


                    break;
            }

            if (formCode == 1)
            {
                return File(fileName, "application/pdf");
            }
            else
            {
                return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        /// <summary>
        /// A function wich set some default values in viewbag so that we can use them 
        /// in other Action methods
        /// </summary>
        [NonAction]
        private void SetUnitDefaultValues(string userSelectedSeason = null)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Login", "Home");
            }
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            MasterStoppageTypeRepository stoppageRepository = new MasterStoppageTypeRepository();

            var UnitDefaultValues = UnitRepository.FindUnitByPk(Convert.ToInt16(Session["BaseUnitCode"]));
            int cropDays = Convert.ToInt32(UnitDefaultValues.EntryDate.Subtract(UnitDefaultValues.CrushingStartDate).TotalDays);

            TempData["BaseUnitCode"] = Session["BaseUnitCode"];
            ViewBag.UnitName = UnitDefaultValues.Name;
            ViewBag.EntryDate = UnitDefaultValues.EntryDate.ToString("dd-MMM-yyyy");
            ViewBag.CrushingSeason = userSelectedSeason == null ? UnitDefaultValues.CrushingSeason.ToString() : userSelectedSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();

            ViewBag.CropDay = cropDays;
        }

    }
}