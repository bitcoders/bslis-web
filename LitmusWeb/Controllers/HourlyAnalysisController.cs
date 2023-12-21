using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;


namespace LitmusWeb.Controllers
{
    public class HourlyAnalysisController : Controller
    {
        HourlyAnalysisRepository Repository = new HourlyAnalysisRepository();
        // GET: HourlyAnalys
        [UnitBasedValuesFilter]

        public ActionResult Index()
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                ViewBag.unit_code = Convert.ToInt16(Session["BaseUnitCode"]);
                ViewBag.season_code = Convert.ToInt16(Session["CrushingSeason"]);

                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            string BaseUnitcode = Session["BaseUnitCode"].ToString();
            int SeasonCode = Convert.ToInt16(Session["CrushingSeason"]);
            
            DateTime EntryDate = DateTime.Parse(ViewBag.EntryDate);
            
            List<HourlyAnalys> Entity = new List<HourlyAnalys>();
            Entity = Repository.GetHourlyAnalysisList(Convert.ToInt32(BaseUnitcode), SeasonCode, Convert.ToDateTime(EntryDate));

            ViewBag.DayCompleted = false;
            // checking if all analysis entries for the day are done or not.
            // if all entries are done, we have to disable "Add New" button at index page.
            if (Entity.Count == 24)
            {
                ViewBag.DayCompleted = true;
            }
            List<HourlyAnalysisModel> model = new List<HourlyAnalysisModel>();
            foreach (var item in Entity)
            {
                HourlyAnalysisModel temp = new HourlyAnalysisModel()
                {
                    id = item.id,
                    unit_code = item.unit_code,
                    season_code = item.season_code,
                    entry_Date = item.entry_Date,
                    entry_time = item.entry_time,
                    new_mill_juice = item.new_mill_juice,
                    old_mill_juice = item.old_mill_juice,
                    juice_total = item.juice_total,
                    new_mill_water = item.new_mill_water,
                    old_mill_water = item.old_mill_water,
                    water_total = item.water_total,
                    sugar_bags_L31 = item.sugar_bags_L31,
                    sugar_bags_L30 = item.sugar_bags_L30,
                    sugar_bags_L_total = item.sugar_bags_L_total,
                    sugar_bags_M31 = item.sugar_bags_M31,
                    sugar_bags_M30 = item.sugar_bags_M30,
                    sugar_bags_M_total = item.sugar_bags_M_total,
                    sugar_bags_S31 = item.sugar_bags_S31,
                    sugar_bags_S30 = item.sugar_bags_S30,
                    sugar_bags_S_total = item.sugar_bags_S_total,
                    sugar_Biss = item.sugar_Biss,
                    sugar_raw = item.sugar_raw,
                    sugar_bags_total = item.sugar_bags_total,
                    cooling_trace = item.cooling_trace,
                    cooling_pol = item.cooling_pol,
                    cooling_ph = item.cooling_ph,
                    crtd_dt = item.crtd_dt,
                    crtd_by = item.crtd_by,
                    updt_dt = item.updt_dt,
                    updt_by = item.updt_by,
                    standing_truck = item.standing_truck,
                    standing_trippler = item.standing_trippler,
                    standing_trolley = item.standing_trolley,
                    standing_cart = item.standing_cart,
                    un_crushed_cane = item.un_crushed_cane,
                    crushed_cane = item.crushed_cane,
                    cane_diverted_for_syrup = item.cane_diverted_for_syrup,
                    diverted_syrup_quantity = item.diverted_syrup_quantity,

                };
                model.Add(temp);
            }
            return View(model);
        }

        [UnitBasedValuesFilter]
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Create")]
        [ValidationFilter("Create")]
        public ActionResult Create()
        {
            //if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            //{
            //    TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
            //    return RedirectToAction("Index", "Home");
            //}

            HourlyAnalysisModel model = new HourlyAnalysisModel();
            return View(model);
        }

        [UnitBasedValuesFilter]
        [HttpPost]
        [ActionName(name: "Create")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(HourlyAnalysisModel model)
        {
            //if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            //{
            //    TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
            //    return RedirectToAction("Index", "Home");
            //}
            HourlyAnalys hourlyAnalysisEntity = new HourlyAnalys()
            {
                unit_code = Convert.ToInt16(Session["BaseUnitCode"]),
                season_code = Convert.ToInt16(Session["CrushingSeason"]),
                entry_Date = Convert.ToDateTime(ViewBag.EntryDate),
                entry_time = ViewBag.EntryTime,
                new_mill_juice = model.new_mill_juice,
                old_mill_juice = model.old_mill_juice,
                juice_total = model.juice_total,
                new_mill_water = model.new_mill_water,
                old_mill_water = model.old_mill_water,
                water_total = model.water_total,
                sugar_bags_L31 = model.sugar_bags_L31,
                sugar_bags_L30 = model.sugar_bags_L30,
                sugar_bags_L_total = model.sugar_bags_L_total,
                sugar_bags_M31 = model.sugar_bags_M31,
                sugar_bags_M30 = model.sugar_bags_M30,
                sugar_bags_M_total = model.sugar_bags_M_total,
                sugar_bags_S31 = model.sugar_bags_S31,
                sugar_bags_S30 = model.sugar_bags_S30,
                sugar_bags_S_total = model.sugar_bags_S_total,
                sugar_Biss = model.sugar_Biss,
                sugar_raw = model.sugar_raw,
                sugar_bags_total = model.sugar_bags_total,
                cooling_trace = model.cooling_trace,
                cooling_pol = model.cooling_pol,
                cooling_ph = model.cooling_ph,
                crtd_dt = DateTime.Now,
                crtd_by = Session["UserCode"].ToString(),
                updt_dt = null,
                updt_by = null,
                standing_truck = model.standing_truck,
                standing_trippler = model.standing_trippler,
                standing_trolley = model.standing_trolley,
                standing_cart = model.standing_cart,
                crushed_cane = model.crushed_cane,
                un_crushed_cane = model.un_crushed_cane,
                cane_diverted_for_syrup = model.cane_diverted_for_syrup,
                diverted_syrup_quantity = model.diverted_syrup_quantity,
                export_sugar = model.export_sugar,
            };
            bool result = Repository.CreateHourlyAnalysis(hourlyAnalysisEntity);
            if (result == false)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ValidationFilter("view")]
        public ActionResult Details(int id)
        {
            //if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            //{
            //    TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
            //    return RedirectToAction("Index", "Home");
            //}
            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            HourlyAnalysisModel Model = GetHourlyAnalysis(id, unitCode);
            return View(Model);
        }

        [HttpGet]
        [UnitBasedValuesFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Edit")]
        [ValidationFilter("Update")]
        public ActionResult Edit(int id)
        {
            //if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            //{
            //    TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
            //    return RedirectToAction("Index", "Home");
            //}
            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            HourlyAnalysisModel Model = GetHourlyAnalysis(id, unitCode);
            TempData["EditUnit"] = Model.unit_code;
            TempData["EditSeason"] = Model.season_code;
            TempData["EditEntryDate"] = Model.entry_Date.ToShortDateString();
            TempData["EditEntryTime"] = Model.entry_time;
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitBasedValuesFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Edit")]
        [ActionName(name: "Edit")]
        [ValidationFilter("Update")]
        public ActionResult EditPost(HourlyAnalysisModel Model)
        {
            //if (Session["UserCode"] == null)
            //{
            //    TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
            //    return RedirectToAction("Index", "Home");
            //}
            if (ModelState.IsValid)
            {
                HourlyAnalys hourlyAnalysis = new HourlyAnalys()
                {
                    id = Model.id,
                    unit_code = Convert.ToInt16(TempData["EditUnit"]),
                    season_code = Convert.ToInt16(TempData["EditSeason"]),
                    entry_Date = Convert.ToDateTime(TempData["EditEntryDate"]),
                    entry_time = Convert.ToInt16(TempData["EditEntryTime"]),
                    new_mill_juice = Model.new_mill_juice,
                    old_mill_juice = Model.old_mill_juice,
                    juice_total = Model.juice_total,
                    new_mill_water = Model.new_mill_water,
                    old_mill_water = Model.old_mill_water,
                    water_total = Model.water_total,
                    sugar_bags_L31 = Model.sugar_bags_L31,
                    sugar_bags_L30 = Model.sugar_bags_L30,
                    sugar_bags_L_total = Model.sugar_bags_L_total,
                    sugar_bags_M31 = Model.sugar_bags_M31,
                    sugar_bags_M30 = Model.sugar_bags_M30,
                    sugar_bags_M_total = Model.sugar_bags_M_total,
                    sugar_bags_S31 = Model.sugar_bags_S31,
                    sugar_bags_S30 = Model.sugar_bags_S30,
                    sugar_bags_S_total = Model.sugar_bags_S_total,
                    sugar_Biss = Model.sugar_Biss,
                    sugar_raw = Model.sugar_raw,
                    sugar_bags_total = Model.sugar_bags_total,
                    cooling_trace = Model.cooling_trace,
                    cooling_pol = Model.cooling_pol,
                    cooling_ph = Model.cooling_ph,
                    standing_truck = Model.standing_truck,
                    standing_trippler = Model.standing_trippler,
                    standing_trolley = Model.standing_trolley,
                    standing_cart = Model.standing_cart,
                    un_crushed_cane = Model.un_crushed_cane,
                    crushed_cane = Model.crushed_cane,
                    //crtd_by = Model.crtd_by,
                    //crtd_dt = Model.crtd_dt,
                    updt_by = Session["UserCode"].ToString(),
                    updt_dt = DateTime.Now,
                    cane_diverted_for_syrup = Model.cane_diverted_for_syrup,
                    diverted_syrup_quantity = Model.diverted_syrup_quantity,
                    export_sugar = Model.export_sugar,
                };
                bool result = Repository.UpdateHourlyAnalysis(hourlyAnalysis);
                if (result == false)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [NonAction]
        [UnitBasedValuesFilter]
        [ValidationFilter("view")]
        private HourlyAnalysisModel GetHourlyAnalysis(int id, int unitCode)
        {
            HourlyAnalys Entity = new HourlyAnalys();
            Entity = Repository.GetHourlyAnalysisById(id, unitCode);
            HourlyAnalysisModel Model = new HourlyAnalysisModel()
            {
                id = Entity.id,
                unit_code = Entity.unit_code,
                season_code = Entity.season_code,
                entry_Date = Entity.entry_Date,
                entry_time = Entity.entry_time,
                new_mill_juice = Entity.new_mill_juice,
                old_mill_juice = Entity.old_mill_juice,
                juice_total = Entity.juice_total,
                new_mill_water = Entity.new_mill_water,
                old_mill_water = Entity.old_mill_water,
                water_total = Entity.water_total,
                sugar_bags_L31 = Entity.sugar_bags_L31,
                sugar_bags_L30 = Entity.sugar_bags_L30,
                sugar_bags_L_total = Entity.sugar_bags_L_total,
                sugar_bags_M31 = Entity.sugar_bags_M31,
                sugar_bags_M30 = Entity.sugar_bags_M30,
                sugar_bags_M_total = Entity.sugar_bags_M_total,
                sugar_bags_S31 = Entity.sugar_bags_S31,
                sugar_bags_S30 = Entity.sugar_bags_S30,
                sugar_bags_S_total = Entity.sugar_bags_S_total,
                sugar_Biss = Entity.sugar_Biss,
                sugar_raw = Entity.sugar_raw,
                sugar_bags_total = Entity.sugar_bags_total,
                cooling_trace = Entity.cooling_trace,
                cooling_pol = Entity.cooling_pol,
                cooling_ph = Entity.cooling_ph,
                crtd_dt = Entity.crtd_dt,
                crtd_by = Entity.crtd_by,
                updt_dt = Entity.updt_dt,
                updt_by = Entity.updt_by,
                standing_truck = Entity.standing_truck,
                standing_trippler = Entity.standing_trippler,
                standing_trolley = Entity.standing_trolley,
                standing_cart = Entity.standing_cart,
                un_crushed_cane = Entity.un_crushed_cane,
                crushed_cane = Entity.crushed_cane,
                cane_diverted_for_syrup = Entity.cane_diverted_for_syrup,
                diverted_syrup_quantity = Entity.diverted_syrup_quantity,
                export_sugar = Entity.export_sugar
            };
            return Model;
        }


    }
}