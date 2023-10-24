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
    public class ParameterCategoryController : Controller
    {
        MasterParameterCategoryRepository ParameterCategRepository = new MasterParameterCategoryRepository();
        // GET: ParameterCategory
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterParameterCategory> parameterCategEntity = new List<MasterParameterCategory>();
            parameterCategEntity = ParameterCategRepository.GetMasterParameterCategoryList();

            List<MasterParameterCategoryModel> parameterCategModel = new List<MasterParameterCategoryModel>();
            foreach (var item in parameterCategEntity)
            {
                MasterParameterCategoryModel temp = new MasterParameterCategoryModel()
                {
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                parameterCategModel.Add(temp);
            }
            return View(parameterCategModel);
        }

        // GET: ParameterCategory/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterCategoryModel parameterCategModel = GetMasterParameterCategoryModel(id);
            return View(parameterCategModel);
        }

        // GET: ParameterCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParameterCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterParameterCategoryModel parameterCategModel)
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
                MasterParameterCategory parameterCategEntity = new MasterParameterCategory()
                {
                    Code = parameterCategModel.Code,
                    Name = parameterCategModel.Name,
                    Description = parameterCategModel.Description,
                    IsActive = parameterCategModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                ParameterCategRepository.AddMasterParameterCategory(parameterCategEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParameterCategory/Edit/5
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterCategoryModel parameterCategModel = GetMasterParameterCategoryModel(id);
            return View(parameterCategModel);
        }

        // POST: ParameterCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Edit(MasterParameterCategoryModel parameterCategModel)
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
                MasterParameterCategory parameterCategEntity = new MasterParameterCategory()
                {
                    Code = parameterCategModel.Code,
                    Name = parameterCategModel.Name,
                    Description = parameterCategModel.Description,
                    IsActive = parameterCategModel.IsActive,
                    CreatedDate = parameterCategModel.CreatedDate,
                    CreatedBy = parameterCategModel.CreatedBy
                };
                bool result = ParameterCategRepository.UpdateMasterParameterCategory(parameterCategEntity);
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

        // GET: ParameterCategory/Delete/5
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterParameterCategoryModel parameterCategModel = GetMasterParameterCategoryModel(id);
            return View(parameterCategModel);
        }

        // POST: ParameterCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        [CustomAuthorizationFilter("Super Admin")]
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
                bool result = ParameterCategRepository.DeleteMasterParameterCategory(id);
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
        public MasterParameterCategoryModel GetMasterParameterCategoryModel(int id)
        {
            MasterParameterCategory parameterCategEntity = new MasterParameterCategory();
            parameterCategEntity = ParameterCategRepository.FindByCode(id);
            MasterParameterCategoryModel parameterCategModel = new MasterParameterCategoryModel()
            {
                Code = parameterCategEntity.Code,
                Name = parameterCategEntity.Name,
                Description = parameterCategEntity.Description,
                IsActive = parameterCategEntity.IsActive,
                CreatedDate = parameterCategEntity.CreatedDate,
                CreatedBy = parameterCategEntity.CreatedBy
            };
            return parameterCategModel;
        }
    }
}
