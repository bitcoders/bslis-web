using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Developer", "Define Reports", "Admin")]
    public class ExcelReportTemplateController : Controller
    {
        // GET: ExcelReportTemplate
        readonly ExcelReportTemplateRepository templateRepository = new ExcelReportTemplateRepository();
        readonly ReportDetailsRepository reportDetailsRepository = new ReportDetailsRepository();
        public ActionResult Index()
        {
            List<ReportDetail> Entity = new List<ReportDetail>();
            List<ReportDetailsModel> Model = new List<ReportDetailsModel>();
            try
            {
                Entity = templateRepository.reportDetails();
                if (Entity != null)
                {
                    foreach (var item in Entity)
                    {
                        ReportDetailsModel temp = new ReportDetailsModel()
                        {
                            Code = item.Code,
                            Name = item.Name,
                            Description = item.Description,
                            Formats = item.Formats,
                            IsActive = item.IsActive,
                            CreatedAt = item.CreatedAt,
                            CreatedBy = item.CreatedBy
                        };
                        Model.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return View(Model);
        }


        [HttpGet]
        public ActionResult ExcelTemplate(int id)
        {
            if (Session["UserCode"] == null)
            {
                return View();
            }
            List<ExcelReportTemplate> entity = new List<ExcelReportTemplate>();

            int reportSchemaCode = (int)reportDetailsRepository.GetReportDetails(id).ReportSchemaCode;
            entity = templateRepository.excelReportTemplates(id);
            ViewBag.WorkingReportCode = id;
            ViewBag.reportSchemaCode = reportSchemaCode;
            if (entity == null)
            {
                return View();
            }

            List<ExcelReportTemplateModel> model = new List<ExcelReportTemplateModel>();
            foreach (var item in entity)
            {
                ExcelReportTemplateModel temp = new ExcelReportTemplateModel()
                {
                    Id = item.Id,
                    ReportCode = item.ReportCode,
                    DataType = item.DataType,
                    CellFrom = item.CellFrom,
                    CellTo = item.CellTo,
                    Value = item.Value,
                    Bold = item.Bold,
                    Italic = item.Italic,
                    FontColor = item.FontColor,
                    BackGroundColor = item.BackGroundColor,
                    NumerFormat = item.NumerFormat,
                    HelpText = item.HelpText,
                    DataTypeName = item.ExcelReportDataType.Name,
                    ReportName = item.ReportDetail.Name,
                    ReportDetails_ReportCode = item.ReportDetail.Code
                };
                model.Add(temp);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DownloadReportData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "DownloadReportData")]
        public ActionResult DownloadReportDataPost(FormCollection formCollection)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (formCollection == null || formCollection["txtDateFrom"].Trim() == "")
            {
                return View();
            }
            SetUnitDefaultValues();
            string fileName = "";
            string reportType = formCollection["selectReportType"].ToString();

            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            int seasonCode = Convert.ToInt32(ViewBag.CrushingSeason);
            DateTime fromDate = Convert.ToDateTime(formCollection["txtDateFrom"]).Date;
            DateTime toDate;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + reportType + ".xlsx");
            string filepath = Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "//RawData//");

            ExcelReportGeneratorRepository excelRepo = new ExcelReportGeneratorRepository();
            switch (reportType.ToLower())
            {
                case "0":
                    break;
                case "periodical":
                    if (formCollection["txtDateFrom"] == null || formCollection["txtDateTo"].Trim() == "")
                    {
                        return View();
                    }
                    toDate = Convert.ToDateTime(formCollection["txtDateTo"]).Date;
                    fileName = excelRepo.ExcelPeriodicalRawData(unitCode, seasonCode, fromDate, toDate, filepath);
                    break;
                case "daily":
                    fileName = excelRepo.ExcelReportDataForDate(unitCode, seasonCode, fromDate, filepath);
                    break;
            }
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet]

        public ActionResult ListExcelFileTemplate()
        {
            List<ReportDetailsModel> model = new List<ReportDetailsModel>();
            List<ReportDetail> reportDetails = new List<ReportDetail>();
            reportDetails = reportDetailsRepository.GetExcelTemplateReportDetails();
            if (reportDetails != null)
            {
                foreach (var i in reportDetails)
                {
                    ReportDetailsModel temp = new ReportDetailsModel()
                    {
                        Code = i.Code,
                        Name = i.Name,
                        Description = i.Description,
                        IsActive = i.IsActive,
                        ReportCategory = i.ReportCategory,
                        TemplatePath = i.TemplatePath,
                        TemplateFileName = i.TemplateFileName,
                        NoOfPages = i.NoOfPages
                    };
                    model.Add(temp);
                }
            }
            return View(model);
        }



        [HttpGet]

        public ActionResult UploadExcelFileTemplate()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            ReportDetailsModel model = new ReportDetailsModel();
            return View(model);
        }


        /// <summary>
        /// save excel file based template details and upload excel file to be used as a template
        /// </summary>
        /// <param name="reportDetailsModel"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "UploadExcelFileTemplate")]
        public ActionResult UploadExcelFileTemplatePost(ReportDetailsModel reportDetailsModel, HttpPostedFileBase file)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (reportDetailsModel == null)
            {
                return View();
            }

            switch (reportDetailsModel.ReportCategory.ToLower())
            {
                case "daily":
                    reportDetailsModel.ReportSchemaCode = 9;
                    break;
                case "periodical":
                    reportDetailsModel.ReportSchemaCode = 1;
                    break;
                default:
                    break;
            }
            string _filename = Path.GetFileName(file.FileName);
            if (!_filename.EndsWith(".xlsx"))
            {
                return RedirectToAction("Forbidden", "Error", new { ErrorMessage = "File extention not supported. Upload .xlsx files only." });
            }
            string upload_path = Path.Combine(Server.MapPath("~/ReportTemplates/ExcelReportTemplates/"), _filename);

            file.SaveAs(upload_path);
            ReportDetail entity = new ReportDetail()
            {
                Code = reportDetailsModel.Code,
                Name = reportDetailsModel.Name,
                Description = reportDetailsModel.Description,
                Formats = "Excel",
                IsActive = true,
                CreatedAt = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString(),
                ReportSchemaCode = reportDetailsModel.ReportSchemaCode,
                ReportCategory = reportDetailsModel.ReportCategory,
                IsTemplateBased = true,
                TemplatePath = reportDetailsModel.TemplatePath,
                TemplateFileName = _filename,
                NoOfPages = reportDetailsModel.NoOfPages
            };
            reportDetailsRepository.AddReportDetail(entity);

            return RedirectToAction("UploadExcelFileTemplate");
        }
        private void SetUnitDefaultValues()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Index", "Home");
            }
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            MasterStoppageTypeRepository stoppageRepository = new MasterStoppageTypeRepository();

            var UnitDefaultValues = UnitRepository.FindUnitByPk(Convert.ToInt16(Session["BaseUnitCode"]));

            TempData["BaseUnitCode"] = Session["BaseUnitCode"];
            ViewBag.UnitName = UnitDefaultValues.Name;
            ViewBag.EntryDate = UnitDefaultValues.EntryDate;
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
        }
    }
}