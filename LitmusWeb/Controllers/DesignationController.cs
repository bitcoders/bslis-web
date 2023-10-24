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
    public class DesignationController : Controller
    {
        // GET: Designation
        MasterDesignationRepository desigRepository = new MasterDesignationRepository();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterDesignation> masterDesignation = new List<MasterDesignation>();
            masterDesignation = desigRepository.GetMasterDesignationList();
            List<MasterDesignationModel> desigModel = new List<MasterDesignationModel>();
            foreach (var m in masterDesignation)
            {
                MasterDesignationModel temp = new MasterDesignationModel
                {
                    Code = m.Code,
                    DepartmentCode = m.DepartmentCode,
                    Name = m.Name,
                    IsActive = m.IsActive,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy
                };
                desigModel.Add(temp);
            }
            return View(desigModel);
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
        public ActionResult Create(Models.MasterDesignationModel desigModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                MasterDesignation masterDesignation = new MasterDesignation()
                {
                    Code = desigModel.Code,
                    DepartmentCode = desigModel.DepartmentCode,
                    Name = desigModel.Name,
                    IsActive = desigModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                bool result = desigRepository.addMasterDesignation(masterDesignation);
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
            MasterDesignation masterDesignation = desigRepository.FindByPk(id);
            MasterDesignationModel desigModel = new MasterDesignationModel()
            {
                Code = masterDesignation.Code,
                DepartmentCode = masterDesignation.DepartmentCode,
                Name = masterDesignation.Name,
                IsActive = masterDesignation.IsActive,
                CreatedDate = masterDesignation.CreatedDate,
                CreatedBy = masterDesignation.CreatedBy
            };

            return View(desigModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.MasterDesignationModel desigModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                MasterDesignation masterDesignation = new MasterDesignation()
                {
                    Code = desigModel.Code,
                    DepartmentCode = desigModel.DepartmentCode,
                    Name = desigModel.Name,
                    IsActive = desigModel.IsActive,
                    CreatedDate = desigModel.CreatedDate,
                    CreatedBy = desigModel.CreatedBy
                };

                bool result = desigRepository.updateDesignation(masterDesignation);
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
            MasterDesignation masterDesignation = desigRepository.FindByPk(id);
            MasterDesignationModel desigModel = new MasterDesignationModel()
            {
                Code = masterDesignation.Code,
                DepartmentCode = masterDesignation.DepartmentCode,
                Name = masterDesignation.Name,
                IsActive = masterDesignation.IsActive,
                CreatedDate = masterDesignation.CreatedDate,
                CreatedBy = masterDesignation.CreatedBy
            };

            return View(desigModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Deletedesig(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                bool result = desigRepository.deleteDesignation(id);
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