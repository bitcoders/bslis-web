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
    public class SubMenuController : Controller
    {
        MasterSubMenuRepository SubMenuRepository = new MasterSubMenuRepository();
        // GET: SubMenu
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterSubMenu> SubMenuEntity = SubMenuRepository.GetMasterSubMenuList();

            List<MasterSubMenuModel> stoppageTypeModel = new List<MasterSubMenuModel>();
            foreach (var item in SubMenuEntity)
            {
                MasterSubMenuModel temp = new MasterSubMenuModel()
                {
                    Code = item.Code,
                    MasterMenuCode = item.MasterMenuCode,
                    Name = item.Name,
                    DisplayText = item.DisplayText,
                    DisplaySequence = item.DisplaySequence,
                    ControllerName = item.ControllerName,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                stoppageTypeModel.Add(temp);
            }
            return View(stoppageTypeModel);
        }

        // GET: SubMenu/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterSubMenuModel stoppageTypeModel = GetSubMenuModel(id);
            return View(stoppageTypeModel);
        }

        // GET: SubMenu/Create
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: SubMenu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSubMenuModel subMenuModel)
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
                MasterSubMenu SubMenuEntity = new MasterSubMenu()
                {
                    Code = subMenuModel.Code,
                    MasterMenuCode = subMenuModel.MasterMenuCode,
                    Name = subMenuModel.Name,
                    DisplayText = subMenuModel.DisplayText,
                    DisplaySequence = subMenuModel.DisplaySequence,
                    Description = subMenuModel.Description,
                    ControllerName = subMenuModel.ControllerName,
                    IsActive = subMenuModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                SubMenuRepository.AddMasterSubMenu(SubMenuEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubMenu/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterSubMenuModel stoppageTypeModel = GetSubMenuModel(id);
            return View(stoppageTypeModel);
        }

        // POST: SubMenu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSubMenuModel subMenuModel)
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
                MasterSubMenu SubMenuEntity = new MasterSubMenu()
                {
                    Code = subMenuModel.Code,
                    MasterMenuCode = subMenuModel.MasterMenuCode,
                    Name = subMenuModel.Name,
                    DisplayText = subMenuModel.DisplayText,
                    DisplaySequence = subMenuModel.DisplaySequence,
                    Description = subMenuModel.Description,
                    IsActive = subMenuModel.IsActive,
                    ControllerName = subMenuModel.ControllerName,
                    CreatedDate = subMenuModel.CreatedDate,
                    CreatedBy = subMenuModel.CreatedBy
                };
                bool result = SubMenuRepository.UpdateMasterSubMenu(SubMenuEntity);
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

        // GET: SubMenu/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterSubMenuModel stoppageTypeModel = GetSubMenuModel(id);
            return View(stoppageTypeModel);
        }

        // POST: SubMenu/Delete/5
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
                bool result = SubMenuRepository.DeleteMasterSubMenu(id);
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
        public MasterSubMenuModel GetSubMenuModel(int id)
        {
            MasterSubMenu SubMenuEntity = new MasterSubMenu();
            SubMenuEntity = SubMenuRepository.FindByCode(id);
            MasterSubMenuModel stoppageTypeModel = new MasterSubMenuModel()
            {
                Code = SubMenuEntity.Code,
                MasterMenuCode = SubMenuEntity.MasterMenuCode,
                Name = SubMenuEntity.Name,
                DisplayText = SubMenuEntity.DisplayText,
                DisplaySequence = SubMenuEntity.DisplaySequence,
                IsActive = SubMenuEntity.IsActive,
                Description = SubMenuEntity.Description,
                ControllerName = SubMenuEntity.ControllerName,
                CreatedDate = SubMenuEntity.CreatedDate,
                CreatedBy = SubMenuEntity.CreatedBy
            };
            return stoppageTypeModel;
        }
    }
}
