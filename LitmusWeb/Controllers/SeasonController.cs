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
    public class SeasonController : Controller
    {
        MasterSeasonRepository repository = new MasterSeasonRepository();
        // GET: Season
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterSeason> masterSeasonEntity = repository.GetMasterSeasonList();
            List<MasterSeasonModel> masterSeasonModels = new List<MasterSeasonModel>();

            foreach (var item in masterSeasonEntity)
            {
                MasterSeasonModel temp = new MasterSeasonModel();
                temp.SeasonCode = item.SeasonCode;
                temp.SeasonYear = item.SeasonYear;
                temp.IsActive = item.IsActive;
                temp.CreatedDate = item.CreatedDate;
                temp.CreatedBy = item.CreatedBy;
                masterSeasonModels.Add(temp);
            }
            return View(masterSeasonModels);
        }

        // GET: Season/Details/5
        public ActionResult Details(int? id)
        {
            return View();

        }

        // GET: Season/Create
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Season/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSeasonModel seasonModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    MasterSeason masterSeasonEntity = new MasterSeason()
                    {
                        SeasonCode = seasonModel.SeasonCode,
                        SeasonYear = seasonModel.SeasonYear,
                        IsActive = seasonModel.IsActive,
                        CreatedDate = DateTime.Now,
                        CreatedBy = Session["UserCode"].ToString()
                    };
                    repository.AddSeason(masterSeasonEntity);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Season/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
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
            MasterSeasonModel msModel = null;
            try
            {
                MasterSeason MsEntity = repository.FindByCode(id);
                msModel = new MasterSeasonModel()
                {
                    id = MsEntity.id,
                    SeasonCode = MsEntity.SeasonCode,
                    SeasonYear = MsEntity.SeasonYear,
                    IsActive = MsEntity.IsActive,
                    CreatedBy = MsEntity.CreatedBy,
                    CreatedDate = MsEntity.CreatedDate
                };
                return View(msModel);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return View();
            }
        }

        // POST: Season/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSeasonModel msModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                MasterSeason msEntity = new MasterSeason()
                {
                    SeasonCode = msModel.SeasonCode,
                    SeasonYear = msModel.SeasonYear,
                    IsActive = msModel.IsActive,
                };

                bool result = repository.updateSeason(msEntity);
                if (result == false)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Season/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            MasterSeason season = new MasterSeason();
            season = repository.FindByCode(id);
            MasterSeasonModel model = new MasterSeasonModel()
            {
                id = season.id,
                SeasonCode = season.SeasonCode,
                SeasonYear = season.SeasonYear,
                IsActive = season.IsActive,
                CreatedDate = season.CreatedDate,
                CreatedBy = season.CreatedBy
            };
            return View(model);
        }

        // POST: Season/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection collection)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                // TODO: Add delete logic here
                bool result = repository.deleteSeason(Convert.ToInt32(collection["id"]));
                if (result == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
