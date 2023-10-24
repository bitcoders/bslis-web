using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class KeySampleController : Controller
    {
        readonly KeySampleAnalysRepository Repository = new KeySampleAnalysRepository();
        // GET: Melting
        [HttpGet]
        [KeySampleFilter]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            List<KeySampleAnalys> Entity = new List<KeySampleAnalys>();
            Entity = Repository.GetKeySampleAnalysListByDate(
                Convert.ToDateTime(ViewBag.EntryDate),
                Convert.ToInt16(ViewBag.BaseUnit)
                );

            List<KeySampleAnalysisModel> Model = new List<KeySampleAnalysisModel>();
            foreach (var i in Entity)
            {
                KeySampleAnalysisModel Temp = new KeySampleAnalysisModel()
                {
                    id = i.id,
                    unit_code = i.unit_code,
                    season_code = i.season_code,
                    entry_date = i.entry_date,
                    entry_time = i.entry_time,
                    pan_number = i.pan_number,
                    sugar_stage = i.sugar_stage,
                    sugar_stage_name = i.MasterParameterSubCategory.Name,
                    brix = i.brix,
                    pol = i.pol,
                    purity = i.purity,
                    crtd_dt = i.crtd_dt,
                    crtd_by = i.crtd_by,
                };
                Model.Add(Temp);
            }
            return View(Model);
        }

        [HttpGet]
        [KeySampleFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Key Sample Create")]
        [ValidationFilter("Create")]
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
        [ActionName(name: "Create")]
        [KeySampleFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Key Sample Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(KeySampleAnalysisModel keySampleAnalysisModel)
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
            KeySampleAnalys Entity = new KeySampleAnalys()
            {

                unit_code = ViewBag.BaseUnit,
                season_code = ViewBag.CrushingSeason,
                entry_date = Convert.ToDateTime(ViewBag.EntryDate),
                entry_time = keySampleAnalysisModel.entry_time,
                pan_number = keySampleAnalysisModel.pan_number,
                sugar_stage = keySampleAnalysisModel.sugar_stage,
                brix = keySampleAnalysisModel.brix,
                pol = keySampleAnalysisModel.pol,
                purity = keySampleAnalysisModel.purity,
                crtd_dt = Convert.ToDateTime(DateTime.Now),
                crtd_by = Session["UserCode"].ToString()
            };
            bool result = Repository.AddKeySample(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [KeySampleFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Key Sample Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            int BaseUnit = Convert.ToInt32(ViewBag.BaseUnit);
            int CrushingSeason = Convert.ToInt16(ViewBag.CrushingSeason);
            DateTime EntryDate = Convert.ToDateTime(ViewBag.EntryDate);

            var Entity = Repository.GetKeySampleAnalysById(BaseUnit, CrushingSeason, id, EntryDate);

            TempData["Id"] = Entity.id;
            TempData["BaseUnit"] = Entity.unit_code;
            TempData["CrushingSeason"] = Entity.season_code;
            TempData["EntryDate"] = Entity.entry_date;
            TempData["CreatedBy"] = Entity.crtd_by;
            TempData["CreatedDate"] = Entity.crtd_dt;


            KeySampleAnalysisModel Model = new KeySampleAnalysisModel()
            {
                id = Entity.id,
                unit_code = Entity.unit_code,
                season_code = Entity.season_code,
                entry_date = Entity.entry_date,
                entry_time = Entity.entry_time,
                pan_number = Entity.pan_number,
                sugar_stage = Entity.sugar_stage,
                brix = Entity.brix,
                pol = Entity.pol,
                purity = Entity.purity,
                crtd_dt = Entity.crtd_dt,
                crtd_by = Entity.crtd_by,
            };
            return View(Model);
        }

        [HttpPost]
        [KeySampleFilter]
        [ActionName(name: "Edit")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Key Sample Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(KeySampleAnalysisModel Model)
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
            KeySampleAnalys Entity = new KeySampleAnalys()
            {
                id = Convert.ToInt32(TempData["Id"]),
                unit_code = Convert.ToInt16(TempData["BaseUnit"]),
                season_code = Convert.ToInt32(TempData["CrushingSeason"]),
                entry_date = DateTime.Parse(TempData["EntryDate"].ToString()),
                entry_time = Model.entry_time,
                pan_number = Model.pan_number,
                sugar_stage = Model.sugar_stage,
                brix = Model.brix,
                pol = Model.pol,
                purity = Model.purity,
                crtd_dt = DateTime.Parse(TempData["CreatedDate"].ToString()),
                crtd_by = TempData["CreatedBy"].ToString(),
            };
            bool result = Repository.EditKeySample(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}