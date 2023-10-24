using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Super Admin")]
    public class DepartmentController : Controller
    {
        // GET: Department
        MasterDepartmentRepository deptRepository = new MasterDepartmentRepository();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterDepartment> masterDepartment = new List<MasterDepartment>();
            masterDepartment = deptRepository.GetMasterDepartmentList();
            List<MasterDepartmentModel> deptModel = new List<MasterDepartmentModel>();
            foreach (var m in masterDepartment)
            {
                MasterDepartmentModel temp = new MasterDepartmentModel
                {
                    Code = m.Code,
                    Name = m.Name,
                    IsActive = m.IsActive,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy
                };
                deptModel.Add(temp);
            }
            return View(deptModel);
        }

        [HttpGet]
        public ActionResult Create()
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
        public ActionResult Create(Models.MasterDepartmentModel deptModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                MasterDepartment masterDepartment = new MasterDepartment()
                {
                    Code = deptModel.Code,
                    Name = deptModel.Name,
                    IsActive = deptModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                bool result = deptRepository.addMasterDepartment(masterDepartment);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View(); // if ModelState is  invalid, return to same page and show validation errors

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterDepartment masterDepartment = deptRepository.FindByCode(id);
            MasterDepartmentModel deptModel = new MasterDepartmentModel()
            {
                Code = masterDepartment.Code,
                Name = masterDepartment.Name,
                IsActive = masterDepartment.IsActive,
                CreatedDate = masterDepartment.CreatedDate,
                CreatedBy = masterDepartment.CreatedBy
            };

            return View(deptModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.MasterDepartmentModel deptModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                MasterDepartment masterDepartment = new MasterDepartment()
                {
                    Code = deptModel.Code,
                    Name = deptModel.Name,
                    IsActive = deptModel.IsActive,
                    CreatedDate = deptModel.CreatedDate,
                    CreatedBy = deptModel.CreatedBy
                };

                bool result = deptRepository.updateDepartment(masterDepartment);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterDepartment masterDepartment = deptRepository.FindByCode(id);
            MasterDepartmentModel deptModel = new MasterDepartmentModel()
            {
                Code = masterDepartment.Code,
                Name = masterDepartment.Name,
                IsActive = masterDepartment.IsActive,
                CreatedDate = masterDepartment.CreatedDate,
                CreatedBy = masterDepartment.CreatedBy
            };

            return View(deptModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteDept(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                bool result = deptRepository.deleteDepartment(id);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}