using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LitmusWeb.Controllers.Reports
{
    public class PeriodicalReportController : Controller
    {
        ReportDetailsRepository reportRepository = new ReportDetailsRepository();
        ExcelReportGeneratorRepository excelRepository;
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<ReportDetail> entity = new List<ReportDetail>();
            entity = reportRepository.getReportDetailsList().Where(x => x.ReportCategory == "Periodical" || x.ReportCategory == "periodical").ToList();
            if (entity == null)
            {
                return View();
            }
            List<ReportDetailsModel> models = new List<ReportDetailsModel>();
            foreach (var x in entity)
            {
                ReportDetailsModel temp = new ReportDetailsModel()
                {
                    Code = x.Code,
                    Name = x.Name,
                    Description = x.Description,
                    Formats = x.Formats,
                    IsTemplateBased = x.IsTemplateBased,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                };
                models.Add(temp);
            }
            return View(models);

        }
        [HttpPost]
        [ActionName(name: "Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost(FormCollection formData)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Index", "Home");
            }
            SetUnitDefaultValues();

            if (formData == null)
            {
                return View();
            }
            ReportDetailsRepository reportDetailsRepository = new ReportDetailsRepository();
            int formCode = Convert.ToInt32(formData["report_name"]);
            var reportDetails = reportDetailsRepository.GetReportDetails(formCode);
            string filepath = "";
            string fileName = "";

            try
            {
                excelRepository = new ExcelReportGeneratorRepository();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + reportDetails.Name + ".xlsx");

                filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "//" + reportDetails.Name + "//");
                if (reportDetails.IsTemplateBased == false)
                {
                    fileName = excelRepository.ExcelReportFile(formCode, Convert.ToInt16(Session["BaseUnitCode"]),
                                                            Convert.ToInt16(ViewBag.CrushingSeason)
                                                            , Convert.ToDateTime(formData["txtReportFromDate"])
                                                            , Convert.ToDateTime(formData["txtReportToDate"])
                                                            , filepath, reportDetails.Name);
                }
                else
                {
                    //============= Excel file using excel template
                    // in arguments templatepath is the path of excel template
                    // and 'filepath' is where the output file will be generated
                    string templatePath = Server.MapPath("~/ReportTemplates/ExcelReportTemplates/" + reportDetails.TemplateFileName);
                    fileName = excelRepository.createCopyFromExcelTemplate(formCode, Convert.ToInt16(Session["BaseUnitCode"]),
                                                            Convert.ToInt16(ViewBag.CrushingSeason)
                                                            , Convert.ToDateTime(formData["txtReportFromDate"])
                                                            , Convert.ToDateTime(formData["txtReportToDate"])
                                                            , reportDetails.TemplateFileName
                                                            , templatePath, filepath);
                }



            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [NonAction]
        private void SetUnitDefaultValues()
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
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();

            ViewBag.CropDay = cropDays;
        }
    }
}