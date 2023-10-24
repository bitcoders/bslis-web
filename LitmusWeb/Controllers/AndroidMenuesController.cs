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
    public class AndroidMenuesController : Controller
    {
        // GET: AndroidMenues
        // show list of exiting android menues
        AndroidMenueRepository androidMenueRepository = new AndroidMenueRepository();
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }

            List<AndroidMenue> androidMenueList = new List<AndroidMenue>();

            androidMenueList = androidMenueRepository.AndroidMenues();
            List<AndroidMenuesModel> androidMenuesModels = new List<AndroidMenuesModel>();
            if (androidMenueList.Count > 0)
            {

                foreach (var item in androidMenueList)
                {
                    AndroidMenuesModel temp = new AndroidMenuesModel()
                    {
                        Code = item.Code,
                        Name = item.Name,
                        DisplayText = item.DisplayText,
                        IconUrl = item.IconUrl,
                        ControllerName = item.ControllerName,
                        ActionName = item.ActionName,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt,
                        CreatedBy = item.CreatedBy,
                        ApiUrl = item.ApiUrl,
                        ReportHeader = item.ReportHeader
                    };
                    androidMenuesModels.Add(temp);
                }
            }
            return View(androidMenuesModels);
        }
        /// <summary>
        /// Add android device httpget
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Add")]
        public ActionResult AddPost(AndroidMenuesModel androidMenuesModel)
        {

            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            if (androidMenuesModel == null || !ModelState.IsValid)
            {
                return View();
            }


            AndroidMenue androidMenue = new AndroidMenue()
            {
                Code = androidMenuesModel.Code,
                Name = androidMenuesModel.Name,
                DisplayText = androidMenuesModel.DisplayText,
                IconUrl = androidMenuesModel.IconUrl,
                ControllerName = androidMenuesModel.ControllerName,
                ActionName = androidMenuesModel.ActionName,
                IsActive = androidMenuesModel.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString(),
                ApiUrl = androidMenuesModel.ApiUrl,
                ReportHeader = androidMenuesModel.ReportHeader
            };

            if (!androidMenueRepository.Add(androidMenue))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }

            AndroidMenue androidMenue = new AndroidMenue();

            androidMenue = androidMenueRepository.FindAndroidMenueById(id);
            if (androidMenue == null)
            {
                return View();
            }
            AndroidMenuesModel androidMenuesModels = new AndroidMenuesModel()
            {
                Code = androidMenue.Code,
                Name = androidMenue.Name,
                DisplayText = androidMenue.DisplayText,
                IconUrl = androidMenue.IconUrl,
                ControllerName = androidMenue.ControllerName,
                ActionName = androidMenue.ActionName,
                IsActive = androidMenue.IsActive,
                CreatedAt = androidMenue.CreatedAt,
                CreatedBy = androidMenue.CreatedBy,
                ApiUrl = androidMenue.ApiUrl,
                ReportHeader = androidMenue.ReportHeader
            };
            return View(androidMenuesModels);
        }

        [HttpPost]
        [ActionName(name: "Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(AndroidMenuesModel androidMenuesModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            if (androidMenuesModel == null || !ModelState.IsValid)
            {
                return View();
            }
            AndroidMenue androidMenue = new AndroidMenue()
            {
                Code = androidMenuesModel.Code,
                Name = androidMenuesModel.Name,
                DisplayText = androidMenuesModel.DisplayText,
                IconUrl = androidMenuesModel.IconUrl,
                ControllerName = androidMenuesModel.ControllerName,
                ActionName = androidMenuesModel.ActionName,
                IsActive = androidMenuesModel.IsActive,
                CreatedAt = androidMenuesModel.CreatedAt,
                CreatedBy = androidMenuesModel.CreatedBy,
                ApiUrl = androidMenuesModel.ApiUrl,
                ReportHeader = androidMenuesModel.ReportHeader
            };

            if (!androidMenueRepository.Edit(androidMenue))
            {
                return View("Error");
            }
            return RedirectToAction("Edit", new { id = androidMenuesModel.Code });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }

            AndroidMenue androidMenue = new AndroidMenue();

            androidMenue = androidMenueRepository.FindAndroidMenueById(id);
            if (androidMenue == null)
            {
                return View();
            }
            AndroidMenuesModel androidMenuesModels = new AndroidMenuesModel()
            {
                Code = androidMenue.Code,
                Name = androidMenue.Name,
                DisplayText = androidMenue.DisplayText,
                IconUrl = androidMenue.IconUrl,
                ControllerName = androidMenue.ControllerName,
                ActionName = androidMenue.ActionName,
                IsActive = androidMenue.IsActive,
                CreatedAt = androidMenue.CreatedAt,
                CreatedBy = androidMenue.CreatedBy,
                ApiUrl = androidMenue.ApiUrl,
                ReportHeader = androidMenue.ReportHeader
            };
            return View(androidMenuesModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        public ActionResult DeletePost(AndroidMenuesModel androidMenuesModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            if (androidMenuesModel == null || !ModelState.IsValid)
            {
                return View();
            }
            AndroidMenue androidMenue = new AndroidMenue()
            {
                Code = androidMenuesModel.Code,
                Name = androidMenuesModel.Name,
                DisplayText = androidMenuesModel.DisplayText,
                IconUrl = androidMenuesModel.IconUrl,
                ControllerName = androidMenuesModel.ControllerName,
                ActionName = androidMenuesModel.ActionName,
                IsActive = androidMenuesModel.IsActive,
                CreatedAt = androidMenuesModel.CreatedAt,
                CreatedBy = androidMenuesModel.CreatedBy,
                ApiUrl = androidMenuesModel.ApiUrl,
                ReportHeader = androidMenuesModel.ReportHeader
            };

            if (!androidMenueRepository.Edit(androidMenue))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}