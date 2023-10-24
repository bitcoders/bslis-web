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
    public class MasterMenuController : Controller
    {
        MasterMenuRepository MenuRepository = new MasterMenuRepository();

        // GET: MasterMenu
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterMenu> menuEntity = new List<MasterMenu>();
            menuEntity = MenuRepository.GetMasterMenuList();

            List<MasterMenuModel> menuModel = new List<MasterMenuModel>();
            foreach (var item in menuEntity)
            {
                MasterMenuModel temp = new MasterMenuModel()
                {
                    Code = item.Code,
                    Name = item.Name,
                    DisplayText = item.DisplayText,
                    DisplaySequence = item.DisplaySequence,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    ControllerName = item.ControllerName,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                menuModel.Add(temp);
            }
            return View(menuModel);
        }

        // GET: MasterMenu/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {

            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterMenuModel menuModel = GetMasterMenuModel(id);
            return View(menuModel);
        }

        // GET: MasterMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterMenu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterMenuModel menuModel)
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
                MasterMenu menuEntity = new MasterMenu()
                {
                    Code = menuModel.Code,
                    Name = menuModel.Name,
                    DisplayText = menuModel.DisplayText,
                    DisplaySequence = menuModel.DisplaySequence,
                    Description = menuModel.Description,
                    IsActive = menuModel.IsActive,
                    ControllerName = menuModel.ControllerName,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                MenuRepository.AddMasterMenu(menuEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterMenu/Edit/5
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Edit")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterMenuModel menuModel = GetMasterMenuModel(id);
            return View(menuModel);
        }

        // POST: MasterMenu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Edit")]
        public ActionResult Edit(MasterMenuModel menuModel)
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
                MasterMenu menuEntity = new MasterMenu()
                {
                    Code = menuModel.Code,
                    Name = menuModel.Name,
                    DisplayText = menuModel.DisplayText,
                    DisplaySequence = menuModel.DisplaySequence,
                    Description = menuModel.Description,
                    IsActive = menuModel.IsActive,
                    ControllerName = menuModel.ControllerName,
                    CreatedDate = menuModel.CreatedDate,
                    CreatedBy = menuModel.CreatedBy
                };
                bool result = false;
                result = MenuRepository.UpdateMasterMenu(menuEntity);
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

        // GET: MasterMenu/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterMenuModel menuModel = GetMasterMenuModel(id);
            return View(menuModel);
        }

        // POST: MasterMenu/Delete/5
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
                bool result = MenuRepository.DeleteMasterMenu(id);
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
        public MasterMenuModel GetMasterMenuModel(int id)
        {
            MasterMenu menuEntity = new MasterMenu();
            menuEntity = MenuRepository.FindByCode(id);
            MasterMenuModel menuModel = new MasterMenuModel()
            {
                Code = menuEntity.Code,
                Name = menuEntity.Name,
                DisplayText = menuEntity.DisplayText,
                DisplaySequence = menuEntity.DisplaySequence,
                Description = menuEntity.Description,
                IsActive = menuEntity.IsActive,
                ControllerName = menuEntity.ControllerName,
                CreatedDate = menuEntity.CreatedDate,
                CreatedBy = menuEntity.CreatedBy
            };
            return menuModel;
        }
    }
}
