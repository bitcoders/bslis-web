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
    public class StoppageSubTypeController : Controller
    {

        MasterStoppageSubTypeRepository StoppageSubTypeRepository = new MasterStoppageSubTypeRepository();
        // GET: StoppageSubType
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterStoppageSubType> StoppageSubTypeEntity = new List<MasterStoppageSubType>();
            StoppageSubTypeEntity = StoppageSubTypeRepository.GetMasterStoppageSubTypeList();

            List<MasterStoppageSubTypeModel> stoppageSubTypeModel = new List<MasterStoppageSubTypeModel>();
            foreach (var item in StoppageSubTypeEntity)
            {
                MasterStoppageSubTypeModel temp = new MasterStoppageSubTypeModel()
                {
                    Code = item.Code,
                    MasterStoppageCode = item.MasterStoppageCode,
                    Name = item.Name,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    SubStoppageGroup = item.SubStoppageGroup,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                stoppageSubTypeModel.Add(temp);
            }
            return View(stoppageSubTypeModel);
        }

        // GET: StoppageSubType/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageSubTypeModel stoppageSubTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageSubTypeModel);
        }

        // GET: StoppageSubType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoppageSubType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterStoppageSubTypeModel stoppageSubTypeModel)
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
                MasterStoppageSubType StoppageSubTypeEntity = new MasterStoppageSubType()
                {
                    Code = stoppageSubTypeModel.Code,
                    MasterStoppageCode = stoppageSubTypeModel.MasterStoppageCode,
                    Name = stoppageSubTypeModel.Name,
                    Description = stoppageSubTypeModel.Description,
                    IsActive = stoppageSubTypeModel.IsActive,
                    SubStoppageGroup = stoppageSubTypeModel.SubStoppageGroup,
                    StoppageType = stoppageSubTypeModel.StoppageType,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                StoppageSubTypeRepository.AddMasterStoppageSubType(StoppageSubTypeEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: StoppageSubType/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageSubTypeModel stoppageSubTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageSubTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterStoppageSubTypeModel stoppageSubTypeModel)
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
                MasterStoppageSubType StoppageSubTypeEntity = new MasterStoppageSubType()
                {
                    Code = stoppageSubTypeModel.Code,
                    MasterStoppageCode = stoppageSubTypeModel.MasterStoppageCode,
                    Name = stoppageSubTypeModel.Name,
                    Description = stoppageSubTypeModel.Description,
                    IsActive = stoppageSubTypeModel.IsActive,
                    SubStoppageGroup = stoppageSubTypeModel.SubStoppageGroup,
                    StoppageType = stoppageSubTypeModel.StoppageType,
                    CreatedDate = stoppageSubTypeModel.CreatedDate,
                    CreatedBy = stoppageSubTypeModel.CreatedBy
                };
                bool result = StoppageSubTypeRepository.UpdateMasterStoppageSubType(StoppageSubTypeEntity);
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

        // GET: StoppageSubType/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterStoppageSubTypeModel stoppageSubTypeModel = GetStoppageSubTypeModel(id);
            return View(stoppageSubTypeModel);
        }

        // POST: StoppageSubType/Delete/5
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
                bool result = StoppageSubTypeRepository.DeleteMasterStoppageSubType(id);
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
        public MasterStoppageSubTypeModel GetStoppageSubTypeModel(int id)
        {
            MasterStoppageSubType StoppageSubTypeEntity = new MasterStoppageSubType();
            StoppageSubTypeEntity = StoppageSubTypeRepository.FindByCode(id);
            MasterStoppageSubTypeModel stoppageSubTypeModel = new MasterStoppageSubTypeModel()
            {
                Code = StoppageSubTypeEntity.Code,
                MasterStoppageCode = StoppageSubTypeEntity.MasterStoppageCode,
                Name = StoppageSubTypeEntity.Name,
                Description = StoppageSubTypeEntity.Description,
                IsActive = StoppageSubTypeEntity.IsActive,
                SubStoppageGroup = StoppageSubTypeEntity.SubStoppageGroup,
                StoppageType = StoppageSubTypeEntity.StoppageType,
                CreatedDate = StoppageSubTypeEntity.CreatedDate,
                CreatedBy = StoppageSubTypeEntity.CreatedBy
            };
            return stoppageSubTypeModel;
        }
    }
}
