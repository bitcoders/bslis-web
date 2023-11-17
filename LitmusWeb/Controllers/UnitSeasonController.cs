using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UnitSeasonController : Controller
    {

        UnitSeasonsRepository unitSeasonsRepository = new UnitSeasonsRepository();


        // GET: UnitSeason
        [CustomAuthorizationFilter("Super Admin", "Unit Seasons")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<UnitSeasonModel> model = new List<UnitSeasonModel>();
            List<UnitSeason> entity = new List<UnitSeason>();
            entity = unitSeasonsRepository.unitSeasonsList();
            foreach (var item in entity)
            {
                UnitSeasonModel temp = new UnitSeasonModel()
                {
                    id = item.id,
                    Code = item.Code,
                    Season = item.Season,
                    CrushingStartDateTime = item.CrushingStartDateTime,
                    CrushingEndDateTime = item.CrushingEndDateTime,
                    NewMillCapacity = item.NewMillCapacity,
                    OldMillCapacity = item.OldMillCapacity,
                    ReportStartHourMinute = item.ReportStartHourMinute,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy
                };
                model.Add(temp);
            }
            return View(model);
        }

        [CustomAuthorizationFilter("Super Admin", "Unit Seasons")]
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            UnitSeasonModel unitSeason = new UnitSeasonModel();
            return View(unitSeason);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        [CustomAuthorizationFilter("Super Admin", "Unit Seasons")]
        public ActionResult CreatePost(UnitSeasonModel model)
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

            UnitSeason unitSeason = new UnitSeason()
            {
                id = System.Guid.NewGuid(),
                Code = model.Code,
                Season = model.Season,
                CrushingStartDateTime = model.CrushingStartDateTime,
                CrushingEndDateTime = model.CrushingEndDateTime,
                NewMillCapacity = model.NewMillCapacity,
                OldMillCapacity = model.OldMillCapacity,
                ReportStartHourMinute = model.ReportStartHourMinute,
                CreatedAt = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString()
            };
            bool result = unitSeasonsRepository.CreateUnitSeason(unitSeason);

            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Seasons")]
        public ActionResult Edit(System.Guid id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return View();
            }
            UnitSeason entity = new UnitSeason();
            try
            {
                entity = unitSeasonsRepository.UnitSeason(id);
                UnitSeasonModel model = new UnitSeasonModel()
                {
                    id = entity.id,
                    Code = entity.Code,
                    Season = entity.Season,
                    CrushingStartDateTime = entity.CrushingStartDateTime,
                    CrushingEndDateTime = entity.CrushingEndDateTime,
                    NewMillCapacity = entity.NewMillCapacity,
                    OldMillCapacity = entity.OldMillCapacity,
                    ReportStartHourMinute = entity.ReportStartHourMinute,
                    DisableDailyProcess = entity.DisableDailyProcess,
                    DisableUpdate = entity.DisableUpdate,
                    DisableAdd  = entity.DisableAdd,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy
                };
                return View(model);
            }
            catch (Exception ex)
            {

                ErrorViewModel error = new ErrorViewModel()
                {
                    ErrorTitle = "Exception occured",
                    //ErrorMessage = new List<string>()
                    //{
                    //    ex.Message
                    //}
                };
                ViewData["ErrorMessage"] = error;
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        [CustomAuthorizationFilter("Super Admin", "Unit Seasons")]

        public ActionResult EditPost(UnitSeasonModel formData)
        {
            if (formData == null)
            {
                return View();
            }
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            try
            {
                UnitSeason unitSeason = new UnitSeason()
                {
                    id = formData.id,
                    Code = formData.Code,
                    Season = formData.Season,
                    CrushingStartDateTime = formData.CrushingStartDateTime,
                    CrushingEndDateTime = formData.CrushingEndDateTime,
                    NewMillCapacity = formData.NewMillCapacity,
                    OldMillCapacity = formData.OldMillCapacity,
                    ReportStartHourMinute = formData.ReportStartHourMinute,
                    DisableDailyProcess = formData.DisableDailyProcess,
                    DisableAdd = formData.DisableAdd,
                    DisableUpdate = formData.DisableUpdate
                    //CreatedAt = formData.CreatedAt,
                    //CreatedBy = formData.CreatedBy
                };
                unitSeasonsRepository.EditUnitSeason(unitSeason);

            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    ErrorTitle = "Error occured while updating the record!",
                    //ErrorMessage = new List<string>()
                    //{
                    //    ex.Message
                    //}
                };
                ViewData["ErrorMessage"] = error;
                return View("Error");
            }


            return RedirectToAction("Index");
        }
    }
}