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
    public class ExcelDataTypeController : Controller
    {

        ExcelReportDataTypeRepository repository = new ExcelReportDataTypeRepository();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                RedirectToAction("Login", "Home");
            }
            List<ExcelReportDataType> excelReportDataTypes = new List<ExcelReportDataType>();
            excelReportDataTypes = repository.ExcelReportDataTypes();
            List<ExcelReportDataTypeModel> model = new List<ExcelReportDataTypeModel>();
            if (excelReportDataTypes != null)
            {
                foreach (var item in excelReportDataTypes)
                {
                    ExcelReportDataTypeModel temp = new ExcelReportDataTypeModel()
                    {
                        Code = item.Code,
                        Name = item.Name,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy
                    };
                    model.Add(temp);
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                RedirectToAction("Login", "Home");
            }
            ViewBag.NextCode = repository.GetNextCode();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Create")]
        public ActionResult CreatePost(ExcelReportDataTypeModel excelReportDataType)
        {
            if (Session["UserCode"] == null)
            {
                RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            ExcelReportDataType entity = new ExcelReportDataType()
            {
                Code = excelReportDataType.Code,
                Name = excelReportDataType.Name,
                CreatedBy = Session["UserCode"].ToString(),
                CreatedAt = DateTime.Now
            };

            bool result = repository.Add(entity);
            if (result == false)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ExcelReportDataType excelReportDataType = new ExcelReportDataType();
            excelReportDataType = repository.GetExcelReportDataType(id);

            if (excelReportDataType != null)
            {
                ExcelReportDataTypeModel model = new ExcelReportDataTypeModel()
                {
                    Code = excelReportDataType.Code,
                    Name = excelReportDataType.Name
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        public ActionResult EditPost(ExcelReportDataTypeModel model)
        {
            if (Session["UserCode"] == null)
            {
                return View("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                ExcelReportDataType Entity = new ExcelReportDataType()
                {
                    Code = model.Code,
                    Name = model.Name
                };
                if (!repository.Edit(Entity))
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                new Exception(Ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ExcelReportDataType Entity = new ExcelReportDataType();
            Entity = repository.GetExcelReportDataType(id);
            if (Entity == null)
            {
                return View("Index");
            }
            ExcelReportDataTypeModel Model = new ExcelReportDataTypeModel()
            {
                Code = Entity.Code,
                Name = Entity.Name,
                CreatedAt = Entity.CreatedAt,
                CreatedBy = Entity.CreatedBy
            };

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        public ActionResult DeletePost(ExcelReportDataTypeModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (repository.Delete(model.Code))
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
    }
}