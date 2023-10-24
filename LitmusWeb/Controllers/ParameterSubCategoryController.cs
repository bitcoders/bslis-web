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
    public class ParameterSubCategoryController : Controller
    {
        MasterParameterSubCategoryRepository ParameterSubCategRepository = new MasterParameterSubCategoryRepository();
        // GET: ParameterSubCategory
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterParameterSubCategory> parameterSubCategEntity = new List<MasterParameterSubCategory>();
            parameterSubCategEntity = ParameterSubCategRepository.GetMasterParameterSubCategoryList();

            List<MasterParameterSubCategoryModel> parameterSubCategModel = new List<MasterParameterSubCategoryModel>();
            foreach (var item in parameterSubCategEntity)
            {
                MasterParameterSubCategoryModel temp = new MasterParameterSubCategoryModel()
                {
                    MasterCategory = item.MasterCategory,
                    MasterCategoryName = item.MasterParameterCategory.Name,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                parameterSubCategModel.Add(temp);
            }
            return View(parameterSubCategModel);
        }

        // GET: ParameterSubCategory/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterSubCategoryModel parameterSubCategModel = GetMasterParameterSubCategoryModel(id);
            return View(parameterSubCategModel);
        }

        // GET: ParameterSubCategory/Create
        public ActionResult Create()
        {
            MasterParameterSubCategoryModel model = new MasterParameterSubCategoryModel();
            return View(model);
        }

        // POST: ParameterSubCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterParameterSubCategoryModel parameterSubCategModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            // if model state is not valid, return to the same view i.e. create view
            //08-08-2019
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // TODO: Add insert logic here
                MasterParameterSubCategory parameterSubCategEntity = new MasterParameterSubCategory()
                {
                    MasterCategory = parameterSubCategModel.MasterCategory,
                    Code = parameterSubCategModel.Code,
                    Name = parameterSubCategModel.Name,
                    Description = parameterSubCategModel.Description,
                    IsActive = parameterSubCategModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                ParameterSubCategRepository.AddMasterParameterSubCategory(parameterSubCategEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParameterSubCategory/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterSubCategoryModel parameterSubCategModel = GetMasterParameterSubCategoryModel(id);
            return View(parameterSubCategModel);
        }

        // POST: ParameterSubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterParameterSubCategoryModel parameterSubCategModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            // return to the same view if Model state is invalid. 08-08-2019
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // TODO: Add update logic here
                MasterParameterSubCategory parameterSubCategEntity = new MasterParameterSubCategory()
                {
                    MasterCategory = parameterSubCategModel.MasterCategory,
                    Code = parameterSubCategModel.Code,
                    Name = parameterSubCategModel.Name,
                    Description = parameterSubCategModel.Description,
                    IsActive = parameterSubCategModel.IsActive,
                    CreatedDate = parameterSubCategModel.CreatedDate,
                    CreatedBy = parameterSubCategModel.CreatedBy
                };
                bool result = ParameterSubCategRepository.UpdateMasterParameterSubCategory(parameterSubCategEntity);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParameterSubCategory/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterSubCategoryModel parameterSubCategModel = GetMasterParameterSubCategoryModel(id);
            return View(parameterSubCategModel);
        }

        // POST: ParameterSubCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        public ActionResult DeletePost(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // TODO: Add delete logic here
                bool result = ParameterSubCategRepository.DeleteMasterParameterSubCategory(id);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [NonAction]
        public MasterParameterSubCategoryModel GetMasterParameterSubCategoryModel(int id)
        {
            MasterParameterSubCategory parameterSubCategEntity = new MasterParameterSubCategory();
            parameterSubCategEntity = ParameterSubCategRepository.FindByCode(id);
            MasterParameterSubCategoryModel parameterSubCategModel = new MasterParameterSubCategoryModel()
            {
                MasterCategory = parameterSubCategEntity.MasterCategory,
                Code = parameterSubCategEntity.Code,
                Name = parameterSubCategEntity.Name,
                Description = parameterSubCategEntity.Description,
                IsActive = parameterSubCategEntity.IsActive,
                CreatedDate = parameterSubCategEntity.CreatedDate,
                CreatedBy = parameterSubCategEntity.CreatedBy
            };
            return parameterSubCategModel;
        }
    }
}
