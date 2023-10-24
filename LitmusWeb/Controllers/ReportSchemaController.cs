using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Developer", "Report Schema Definition")]
    public class ReportSchemaController : Controller
    {
        ReportSchemaRepository rsRepository = new ReportSchemaRepository();
        [HttpGet]
        // GET: ReportSchema
        public ActionResult Index()
        {
            List<ReportSchema> reportSchema = new List<ReportSchema>();
            List<ReportSchemaModel> Model = new List<ReportSchemaModel>();
            reportSchema = rsRepository.GetReportSchemaList();

            if (reportSchema == null)
            {
                return View();
            }
            if (reportSchema != null)
            {
                foreach (var item in reportSchema)
                {
                    ReportSchemaModel temp = new ReportSchemaModel()
                    {
                        Code = item.Code,
                        SysObjectName = item.SysObjectName,
                        SysObjectDescripton = item.SysObjectDescripton,
                        SearchKeywords = item.SearchKeywords,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedBy = item.UpdatedBy,
                        SchemaType = item.SchemaType
                    };
                    Model.Add(temp);
                }
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Create")]
        public ActionResult CreatePost(ReportSchemaModel m)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid || m == null)
            {
                return View();
            }
            ReportSchema rs = new ReportSchema()
            {
                Code = m.Code,
                SysObjectName = m.SysObjectName,
                SysObjectDescripton = m.SysObjectDescripton,
                SearchKeywords = m.SearchKeywords,
                IsActive = m.IsActive,
                CreatedDate = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString(),
                UpdatedDate = DateTime.Now,
                UpdatedBy = Session["UserCode"].ToString(),
                SchemaType = m.SchemaType
            };
            bool result = rsRepository.AddReportSchema(rs);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}