using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Developer", "Define Reports")]
    public class ReportDetailsController : Controller
    {
        ReportDetailsRepository repository = new ReportDetailsRepository();
        // GET: ReportDetails
        public ActionResult Index()
        {
            List<ReportDetail> Entity = new List<ReportDetail>();
            Entity = repository.getReportDetailsList();
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
                    IsActive = e.IsActive,
                    CreatedAt = e.CreatedAt,
                    CreatedBy = e.CreatedBy
                    ,
                    ReportSchemaCode = e.ReportSchemaCode,
                    ReportCategory = e.ReportCategory,
                    IsTemplateBased = e.IsTemplateBased,
                    TemplatePath = e.TemplatePath,
                    TemplateFileName = e.TemplateFileName,
                    NoOfPages = e.NoOfPages,
                    AdminOnly = e.AdminOnly,
                    FileGenerationLocation = e.FileGenerationLocation,
                    AllowAutoGenerate = e.AllowAutoGenerate == null ? false : (bool)e.AllowAutoGenerate,
                };
                Model.Add(temp);
            }
            return View(Model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                ReportDetail e = repository.GetReportDetailById(id);
                if (e != null)
                {
                    ReportDetailsModel model = new ReportDetailsModel()
                    {
                        Code = e.Code
                        ,
                        Name = e.Name
                        ,
                        Description = e.Description
                        ,
                        Formats = e.Formats
                        ,
                        IsActive = e.IsActive
                        ,
                        CreatedAt = e.CreatedAt
                        ,
                        CreatedBy = e.CreatedBy
                        ,
                        ReportSchemaCode = e.ReportSchemaCode
                        ,
                        ReportCategory = e.ReportCategory
                        ,
                        IsTemplateBased = e.IsTemplateBased
                        ,
                        TemplatePath = e.TemplatePath
                        ,
                        TemplateFileName = e.TemplateFileName
                        ,
                        NoOfPages = e.NoOfPages
                        ,
                        AdminOnly = e.AdminOnly
                        ,
                        FileGenerationLocation = e.FileGenerationLocation
                        ,
                        AllowAutoGenerate = e.AllowAutoGenerate == null ? false : (bool)e.AllowAutoGenerate,
                    };
                    TempData["report_code"] = e.Code;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult EditPost(ReportDetailsModel model)
        {
            if (!ModelState.IsValid && TempData["report_code"] == null)
            {
                return View(model);
            }

            try
            {
                int report_code;
                bool blnConversion = int.TryParse(TempData["report_code"].ToString(), out report_code);
                if (blnConversion)
                {
                    ReportDetail rd = new ReportDetail()
                    {
                        Code = report_code,
                        Name = model.Name,
                        Description = model.Description,
                        Formats = model.Formats,
                        IsActive = model.IsActive,
                        ReportSchemaCode = model.ReportSchemaCode,
                        ReportCategory = model.ReportCategory,
                        IsTemplateBased = model.IsTemplateBased,
                        TemplatePath = model.TemplatePath,
                        TemplateFileName = model.TemplateFileName,
                        NoOfPages = model.NoOfPages,
                        AdminOnly = model.AdminOnly,
                        FileGenerationLocation = model.FileGenerationLocation,
                        AllowAutoGenerate = model.AllowAutoGenerate
                    };
                    bool result = repository.Edit(rd);
                    if (result)
                    {
                        return RedirectToAction("Edit", "ReportDetails");
                    }
                }
                return View(model);

            }
            catch (Exception e)
            {
                new Exception(e.Message);
                return View(model);
            }
        }
    }
}