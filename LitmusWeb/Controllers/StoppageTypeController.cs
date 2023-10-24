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
    public class StoppageTypeController : Controller
    {
        MasterStoppageTypeRepository StoppageTypeRepository = new MasterStoppageTypeRepository();
        // GET: StoppageTypes
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterStoppageType> StoppageTypeEntity = new List<MasterStoppageType>();
            StoppageTypeEntity = StoppageTypeRepository.GetMasterStoppageTypeList();

            List<MasterStoppageTypeModel> stoppageTypeModel = new List<MasterStoppageTypeModel>();
            foreach (var item in StoppageTypeEntity)
            {
                MasterStoppageTypeModel temp = new MasterStoppageTypeModel()
                {
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                stoppageTypeModel.Add(temp);
            }
            return View(stoppageTypeModel);
        }

        // GET: StoppageTypes/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageTypeModel stoppageTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageTypeModel);
        }

        // GET: StoppageTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoppageTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterStoppageType stoppageTypeModel)
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
                MasterStoppageType StoppageTypeEntity = new MasterStoppageType()
                {
                    Code = stoppageTypeModel.Code,
                    Name = stoppageTypeModel.Name,
                    Description = stoppageTypeModel.Description,
                    IsActive = stoppageTypeModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                StoppageTypeRepository.AddMasterStoppageType(StoppageTypeEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StoppageTypes/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageTypeModel stoppageTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageTypeModel);
        }

        // POST: StoppageTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterStoppageType stoppageTypeModel)
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
                MasterStoppageType StoppageTypeEntity = new MasterStoppageType()
                {
                    Code = stoppageTypeModel.Code,
                    Name = stoppageTypeModel.Name,
                    Description = stoppageTypeModel.Description,
                    IsActive = stoppageTypeModel.IsActive,
                    CreatedDate = stoppageTypeModel.CreatedDate,
                    CreatedBy = stoppageTypeModel.CreatedBy
                };
                bool result = StoppageTypeRepository.UpdateMasterStoppageType(StoppageTypeEntity);
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

        // GET: StoppageTypes/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageTypeModel stoppageTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageTypeModel);
        }

        // POST: StoppageTypes/Delete/5
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
                bool result = StoppageTypeRepository.DeleteMasterStoppageType(id);
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
        public MasterStoppageTypeModel GetStoppageSubTypeModel(int id)
        {
            MasterStoppageType StoppageTypeEntity = new MasterStoppageType();
            StoppageTypeEntity = StoppageTypeRepository.FindByCode(id);
            MasterStoppageTypeModel stoppageTypeModel = new MasterStoppageTypeModel()
            {
                Code = StoppageTypeEntity.Code,
                Name = StoppageTypeEntity.Name,
                Description = StoppageTypeEntity.Description,
                IsActive = StoppageTypeEntity.IsActive,
                CreatedDate = StoppageTypeEntity.CreatedDate,
                CreatedBy = StoppageTypeEntity.CreatedBy
            };
            return stoppageTypeModel;
        }
    }
}
