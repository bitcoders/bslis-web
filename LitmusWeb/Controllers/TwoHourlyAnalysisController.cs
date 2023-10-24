using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class TwoHourlyAnalysisController : Controller
    {
        readonly SugarLabEntities Db;
        readonly TwoHourlyAnalysisRepository Repository = new TwoHourlyAnalysisRepository();
        public TwoHourlyAnalysisController()
        {
            Db = new SugarLabEntities();
        }
        // GET: TwoHourlyAnalys
        [TwoHourlyAnalysisFilter]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            List<TwoHourlyAnalys> TwoHourlyEntity = new List<TwoHourlyAnalys>();
            TwoHourlyEntity = Repository.GetTwoHourlyAnalysis(ViewBag.BaseUnit, ViewBag.CrushingSeason, Convert.ToDateTime(ViewBag.EntryDate));
            List<TwoHourlyAnalysisModel> TwoHourlyModel = new List<TwoHourlyAnalysisModel>();
            //PropertyInfo[] propertyInfos = typeof(TwoHourlyAnalysisModel).GetProperties();

            ViewBag.DayCompleted = false;
            // checking if all analysis entries for the two houlry analysis are done or not.
            // if all entries are done, we have to disable "Add New" button at index page.
            if (TwoHourlyEntity.Count == 12)
            {
                ViewBag.DayCompleted = true;
            }
            foreach (var i in TwoHourlyEntity)
            {
                TwoHourlyAnalysisModel Temp = new TwoHourlyAnalysisModel()
                {
                    Id = i.Id,
                    Unit_Code = i.Unit_Code,
                    season_code = i.season_code,
                    Entry_Date = i.Entry_Date,
                    entry_Time = i.entry_Time,
                    NM_Primary_Juice_Brix = i.NM_Primary_Juice_Brix,
                    NM_Primary_juice_pol = i.NM_Primary_juice_pol,
                    nm_primary_juice_purity = i.nm_primary_juice_purity,
                    nm_primary_juice_ph = i.nm_primary_juice_ph,
                    om_primary_juice_brix = i.om_primary_juice_brix,
                    om_primary_juice_pol = i.om_primary_juice_pol,
                    om_primary_juice_purity = i.om_primary_juice_purity,
                    om_primary_juice_ph = i.om_primary_juice_ph,
                    nm_mixed_juice_brix = i.nm_mixed_juice_brix,
                    nm_mixed_juice_pol = i.nm_mixed_juice_pol,
                    nm_mixed_juice_purity = i.nm_mixed_juice_purity,
                    nm_mixed_juice_ph = i.nm_mixed_juice_ph,
                    om_mixed_juice_brix = i.om_mixed_juice_brix,
                    om_mixed_juice_pol = i.om_mixed_juice_pol,
                    om_mixed_juice_purity = i.om_mixed_juice_purity,
                    om_mixed_juice_ph = i.om_mixed_juice_ph,
                    nm_last_juice_brix = i.nm_last_juice_brix,
                    nm_last_juice_pol = i.nm_last_juice_pol,
                    nm_last_juice_purity = i.nm_last_juice_purity,
                    om_last_juice_brix = i.om_last_juice_brix,
                    om_last_juice_pol = i.om_last_juice_pol,
                    om_last_juice_purity = i.om_last_juice_purity,
                    oliver_juice_brix = i.oliver_juice_brix,
                    oliver_juice_pol = i.oliver_juice_pol,
                    oliver_juice_ph = i.oliver_juice_ph,
                    oliver_juice_purity = i.oliver_juice_purity,
                    fcs_juice_brix = i.fcs_juice_brix,
                    fcs_juice_pol = i.fcs_juice_pol,
                    fcs_juice_ph = i.fcs_juice_ph,
                    fcs_juice_purity = i.fcs_juice_purity,
                    clear_juice_brix = i.clear_juice_brix,
                    clear_juice_pol = i.clear_juice_pol,
                    clear_juice_ph = i.clear_juice_ph,
                    clear_juice_purity = i.clear_juice_purity,
                    unsulphured_syrup_brix = i.unsulphured_syrup_brix,
                    unsulphured_syrup_pol = i.unsulphured_syrup_pol,
                    unsulphured_syrup_ph = i.unsulphured_syrup_ph,
                    unsulphured_syrup_purity = i.unsulphured_syrup_purity,
                    sulphured_syrup_brix = i.sulphured_syrup_brix,
                    sulphured_syrup_pol = i.sulphured_syrup_pol,
                    sulphured_syrup_ph = i.sulphured_syrup_ph,
                    sulphured_syrup_purity = i.sulphured_syrup_purity,
                    final_molasses_brix = i.final_molasses_brix,
                    final_molasses_pol = i.final_molasses_pol,
                    final_molasses_purity = i.final_molasses_purity,
                    final_molasses_temp = i.final_molasses_temp,
                    final_molasses_tanks = i.final_molasses_tanks,
                    nm_bagasse_pol = i.nm_bagasse_pol,
                    nm_bagasse_moisture = i.nm_bagasse_moisture,
                    om_bagasse_pol = i.om_bagasse_pol,
                    om_bagasse_moisture = i.om_bagasse_moisture,
                    pol_pressure_cake_sample1 = i.pol_pressure_cake_sample1,
                    pol_pressure_cake_sample2 = i.pol_pressure_cake_sample2,
                    pol_pressure_cake_sample3 = i.pol_pressure_cake_sample3,
                    pol_pressure_cake_sample4 = i.pol_pressure_cake_sample4,
                    pol_pressure_cake_sample5 = i.pol_pressure_cake_sample5,
                    pol_pressure_cake_sample6 = i.pol_pressure_cake_sample6,
                    pol_pressure_cake_average = i.pol_pressure_cake_average,
                    pol_pressure_cake_moisture = i.pol_pressure_cake_moisture,
                    pol_pressure_cake_composite = i.pol_pressure_cake_composite,

                    crtd_dt = i.crtd_dt,
                    crtd_by = i.crtd_by
                };

                TwoHourlyModel.Add(Temp);
            }
            return View(TwoHourlyModel);
        }

        [HttpGet]
        [TwoHourlyAnalysisFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Two Hourly Create")]
        [ValidationFilter("Create")]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TwoHourlyAnalysisFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Two Hourly Create")]
        [ActionName("Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(TwoHourlyAnalysisModel twoHourlyAnalysisModel)
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (twoHourlyAnalysisModel == null)
            {
                return RedirectToAction("Index");
            }
            TwoHourlyAnalys twoHourlyAnalysisEntity = new TwoHourlyAnalys()
            {
                Unit_Code = Convert.ToInt16(ViewBag.BaseUnit),
                season_code = ViewBag.CrushingSeason,
                Entry_Date = Convert.ToDateTime(ViewBag.EntryDate),
                entry_Time = Convert.ToInt16(ViewBag.EntryTime),
                NM_Primary_Juice_Brix = twoHourlyAnalysisModel.NM_Primary_Juice_Brix,
                NM_Primary_juice_pol = twoHourlyAnalysisModel.NM_Primary_juice_pol,
                nm_primary_juice_purity = twoHourlyAnalysisModel.nm_primary_juice_purity,
                nm_primary_juice_ph = twoHourlyAnalysisModel.nm_primary_juice_ph,
                om_primary_juice_brix = twoHourlyAnalysisModel.om_primary_juice_brix,
                om_primary_juice_pol = twoHourlyAnalysisModel.om_primary_juice_pol,
                om_primary_juice_purity = twoHourlyAnalysisModel.om_primary_juice_purity,
                om_primary_juice_ph = twoHourlyAnalysisModel.om_primary_juice_ph,
                nm_mixed_juice_brix = twoHourlyAnalysisModel.nm_mixed_juice_brix,
                nm_mixed_juice_pol = twoHourlyAnalysisModel.nm_mixed_juice_pol,
                nm_mixed_juice_purity = twoHourlyAnalysisModel.nm_mixed_juice_purity,
                nm_mixed_juice_ph = twoHourlyAnalysisModel.nm_mixed_juice_ph,
                om_mixed_juice_brix = twoHourlyAnalysisModel.om_mixed_juice_brix,
                om_mixed_juice_pol = twoHourlyAnalysisModel.om_mixed_juice_pol,
                om_mixed_juice_purity = twoHourlyAnalysisModel.om_mixed_juice_purity,
                om_mixed_juice_ph = twoHourlyAnalysisModel.om_mixed_juice_ph,
                nm_last_juice_brix = twoHourlyAnalysisModel.nm_last_juice_brix,
                nm_last_juice_pol = twoHourlyAnalysisModel.nm_last_juice_pol,
                nm_last_juice_purity = twoHourlyAnalysisModel.nm_last_juice_purity,
                om_last_juice_brix = twoHourlyAnalysisModel.om_last_juice_brix,
                om_last_juice_pol = twoHourlyAnalysisModel.om_last_juice_pol,
                om_last_juice_purity = twoHourlyAnalysisModel.om_last_juice_purity,
                oliver_juice_brix = twoHourlyAnalysisModel.oliver_juice_brix,
                oliver_juice_pol = twoHourlyAnalysisModel.oliver_juice_pol,
                oliver_juice_ph = twoHourlyAnalysisModel.oliver_juice_ph,
                oliver_juice_purity = twoHourlyAnalysisModel.oliver_juice_purity,
                fcs_juice_brix = twoHourlyAnalysisModel.fcs_juice_brix,
                fcs_juice_pol = twoHourlyAnalysisModel.fcs_juice_pol,
                fcs_juice_ph = twoHourlyAnalysisModel.fcs_juice_ph,
                fcs_juice_purity = twoHourlyAnalysisModel.fcs_juice_purity,
                clear_juice_brix = twoHourlyAnalysisModel.clear_juice_brix,
                clear_juice_pol = twoHourlyAnalysisModel.clear_juice_pol,
                clear_juice_ph = twoHourlyAnalysisModel.clear_juice_ph,
                clear_juice_purity = twoHourlyAnalysisModel.clear_juice_purity,
                unsulphured_syrup_brix = twoHourlyAnalysisModel.unsulphured_syrup_brix,
                unsulphured_syrup_pol = twoHourlyAnalysisModel.unsulphured_syrup_pol,
                unsulphured_syrup_ph = twoHourlyAnalysisModel.unsulphured_syrup_ph,
                unsulphured_syrup_purity = twoHourlyAnalysisModel.unsulphured_syrup_purity,
                sulphured_syrup_brix = twoHourlyAnalysisModel.sulphured_syrup_brix,
                sulphured_syrup_pol = twoHourlyAnalysisModel.sulphured_syrup_pol,
                sulphured_syrup_ph = twoHourlyAnalysisModel.sulphured_syrup_ph,
                sulphured_syrup_purity = twoHourlyAnalysisModel.sulphured_syrup_purity,
                final_molasses_brix = twoHourlyAnalysisModel.final_molasses_brix,
                final_molasses_pol = twoHourlyAnalysisModel.final_molasses_pol,
                final_molasses_purity = twoHourlyAnalysisModel.final_molasses_purity,
                final_molasses_temp = twoHourlyAnalysisModel.final_molasses_temp,
                final_molasses_tanks = twoHourlyAnalysisModel.final_molasses_tanks,
                nm_bagasse_pol = twoHourlyAnalysisModel.nm_bagasse_pol,
                nm_bagasse_moisture = twoHourlyAnalysisModel.nm_bagasse_moisture,
                om_bagasse_pol = twoHourlyAnalysisModel.om_bagasse_pol,
                om_bagasse_moisture = twoHourlyAnalysisModel.om_bagasse_moisture,
                pol_pressure_cake_sample1 = twoHourlyAnalysisModel.pol_pressure_cake_sample1,
                pol_pressure_cake_sample2 = twoHourlyAnalysisModel.pol_pressure_cake_sample2,
                pol_pressure_cake_sample3 = twoHourlyAnalysisModel.pol_pressure_cake_sample3,
                pol_pressure_cake_sample4 = twoHourlyAnalysisModel.pol_pressure_cake_sample4,
                pol_pressure_cake_sample5 = twoHourlyAnalysisModel.pol_pressure_cake_sample5,
                pol_pressure_cake_sample6 = twoHourlyAnalysisModel.pol_pressure_cake_sample6,
                pol_pressure_cake_average = twoHourlyAnalysisModel.pol_pressure_cake_average,
                pol_pressure_cake_moisture = twoHourlyAnalysisModel.pol_pressure_cake_moisture,
                pol_pressure_cake_composite = twoHourlyAnalysisModel.pol_pressure_cake_composite,
                crtd_dt = DateTime.Now,
                crtd_by = Session["UserCode"].ToString(),
            };
            if (Repository.AddTwoHourlyAnalysis(twoHourlyAnalysisEntity) == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [TwoHourlyAnalysisFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Two Hourly Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(int Id)
        {
            if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);

            TwoHourlyAnalysisModel Model = GetTwoHourlyAnalysis(Id);
            TempData["EditUnit"] = Model.Unit_Code;
            TempData["EditSeason"] = Model.season_code;
            TempData["EditEntryDate"] = Convert.ToDateTime(Model.Entry_Date);
            TempData["EditEntryTime"] = Model.entry_Time;
            TempData["CreatedBy"] = Model.crtd_by;
            TempData["CreatedDate"] = Model.crtd_dt;
            if (Model == null)
            {
                return RedirectToAction("Index");
            }
            return View(Model);
        }

        [HttpPost]
        [ActionName(name: "Edit")]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Two Hourly Edit")]
        [ValidationFilter("update")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(TwoHourlyAnalysisModel model)
        {
            TwoHourlyAnalys Entity = new TwoHourlyAnalys()
            {
                Id = model.Id,
                Unit_Code = Convert.ToInt32(TempData["EditUnit"]),
                season_code = Convert.ToInt32(TempData["EditSeason"]),
                Entry_Date = Convert.ToDateTime(TempData["EditEntryDate"]),
                entry_Time = Convert.ToInt32(TempData["EditEntryTime"]),
                NM_Primary_Juice_Brix = model.NM_Primary_Juice_Brix,
                NM_Primary_juice_pol = model.NM_Primary_juice_pol,
                nm_primary_juice_purity = model.nm_primary_juice_purity,
                nm_primary_juice_ph = model.nm_primary_juice_ph,
                om_primary_juice_brix = model.om_primary_juice_brix,
                om_primary_juice_pol = model.om_primary_juice_pol,
                om_primary_juice_purity = model.om_primary_juice_purity,
                om_primary_juice_ph = model.om_primary_juice_ph,
                nm_mixed_juice_brix = model.nm_mixed_juice_brix,
                nm_mixed_juice_pol = model.nm_mixed_juice_pol,
                nm_mixed_juice_purity = model.nm_mixed_juice_purity,
                nm_mixed_juice_ph = model.nm_mixed_juice_ph,
                om_mixed_juice_brix = model.om_mixed_juice_brix,
                om_mixed_juice_pol = model.om_mixed_juice_pol,
                om_mixed_juice_purity = model.om_mixed_juice_purity,
                om_mixed_juice_ph = model.om_mixed_juice_ph,
                nm_last_juice_brix = model.nm_last_juice_brix,
                nm_last_juice_pol = model.nm_last_juice_pol,
                nm_last_juice_purity = model.nm_last_juice_purity,
                om_last_juice_brix = model.om_last_juice_brix,
                om_last_juice_pol = model.om_last_juice_pol,
                om_last_juice_purity = model.om_last_juice_purity,
                oliver_juice_brix = model.oliver_juice_brix,
                oliver_juice_pol = model.oliver_juice_pol,
                oliver_juice_ph = model.oliver_juice_ph,
                oliver_juice_purity = model.oliver_juice_purity,
                fcs_juice_brix = model.fcs_juice_brix,
                fcs_juice_pol = model.fcs_juice_pol,
                fcs_juice_ph = model.fcs_juice_ph,
                fcs_juice_purity = model.fcs_juice_purity,
                clear_juice_brix = model.clear_juice_brix,
                clear_juice_pol = model.clear_juice_pol,
                clear_juice_ph = model.clear_juice_ph,
                clear_juice_purity = model.clear_juice_purity,
                unsulphured_syrup_brix = model.unsulphured_syrup_brix,
                unsulphured_syrup_pol = model.unsulphured_syrup_pol,
                unsulphured_syrup_ph = model.unsulphured_syrup_ph,
                unsulphured_syrup_purity = model.unsulphured_syrup_purity,
                sulphured_syrup_brix = model.sulphured_syrup_brix,
                sulphured_syrup_pol = model.sulphured_syrup_pol,
                sulphured_syrup_ph = model.sulphured_syrup_ph,
                sulphured_syrup_purity = model.sulphured_syrup_purity,
                final_molasses_brix = model.final_molasses_brix,
                final_molasses_pol = model.final_molasses_pol,
                final_molasses_purity = model.final_molasses_purity,
                final_molasses_temp = model.final_molasses_temp,
                final_molasses_tanks = model.final_molasses_tanks,
                nm_bagasse_pol = model.nm_bagasse_pol,
                nm_bagasse_moisture = model.nm_bagasse_moisture,
                om_bagasse_pol = model.om_bagasse_pol,
                om_bagasse_moisture = model.om_bagasse_moisture,
                pol_pressure_cake_sample1 = model.pol_pressure_cake_sample1,
                pol_pressure_cake_sample2 = model.pol_pressure_cake_sample2,
                pol_pressure_cake_sample3 = model.pol_pressure_cake_sample3,
                pol_pressure_cake_sample4 = model.pol_pressure_cake_sample4,
                pol_pressure_cake_sample5 = model.pol_pressure_cake_sample5,
                pol_pressure_cake_sample6 = model.pol_pressure_cake_sample6,
                pol_pressure_cake_average = model.pol_pressure_cake_average,
                pol_pressure_cake_moisture = model.pol_pressure_cake_moisture,
                pol_pressure_cake_composite = model.pol_pressure_cake_composite,
                crtd_by = TempData["CreatedBy"].ToString(),
                crtd_dt = Convert.ToDateTime(TempData["CreatedDate"])
            };
            bool result = Repository.UpdateTwoHourlyAnalysis(Entity);
            if (result == false)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [NonAction]
        [ValidationFilter("view")]
        private TwoHourlyAnalysisModel GetTwoHourlyAnalysis(int id)
        {
            int unitCode = Convert.ToInt16(Session["BaseUnitCode"]);
            int seasonCode = Convert.ToInt16(Session["CrushingSeason"]);
            DateTime EntryDate = Convert.ToDateTime(ViewBag.EntryDate);
            TwoHourlyAnalys Entity = new TwoHourlyAnalys();
            Entity = Repository.GetTwoHourlyAnalysisDetails(id, unitCode, seasonCode, EntryDate);
            if (Entity != null)
            {
                TwoHourlyAnalysisModel Model = new TwoHourlyAnalysisModel()
                {
                    Id = Entity.Id,
                    Unit_Code = Entity.Unit_Code,
                    season_code = Entity.season_code,
                    Entry_Date = Entity.Entry_Date,
                    entry_Time = Entity.entry_Time,
                    NM_Primary_Juice_Brix = Entity.NM_Primary_Juice_Brix,
                    NM_Primary_juice_pol = Entity.NM_Primary_juice_pol,
                    nm_primary_juice_purity = Entity.nm_primary_juice_purity,
                    nm_primary_juice_ph = Entity.nm_primary_juice_ph,
                    om_primary_juice_brix = Entity.om_primary_juice_brix,
                    om_primary_juice_pol = Entity.om_primary_juice_pol,
                    om_primary_juice_purity = Entity.om_primary_juice_purity,
                    om_primary_juice_ph = Entity.om_primary_juice_ph,
                    nm_mixed_juice_brix = Entity.nm_mixed_juice_brix,
                    nm_mixed_juice_pol = Entity.nm_mixed_juice_pol,
                    nm_mixed_juice_purity = Entity.nm_mixed_juice_purity,
                    nm_mixed_juice_ph = Entity.nm_mixed_juice_ph,
                    om_mixed_juice_brix = Entity.om_mixed_juice_brix,
                    om_mixed_juice_pol = Entity.om_mixed_juice_pol,
                    om_mixed_juice_purity = Entity.om_mixed_juice_purity,
                    om_mixed_juice_ph = Entity.om_mixed_juice_ph,
                    nm_last_juice_brix = Entity.nm_last_juice_brix,
                    nm_last_juice_pol = Entity.nm_last_juice_pol,
                    nm_last_juice_purity = Entity.nm_last_juice_purity,
                    om_last_juice_brix = Entity.om_last_juice_brix,
                    om_last_juice_pol = Entity.om_last_juice_pol,
                    om_last_juice_purity = Entity.om_last_juice_purity,
                    oliver_juice_brix = Entity.oliver_juice_brix,
                    oliver_juice_pol = Entity.oliver_juice_pol,
                    oliver_juice_ph = Entity.oliver_juice_ph,
                    oliver_juice_purity = Entity.oliver_juice_purity,
                    fcs_juice_brix = Entity.fcs_juice_brix,
                    fcs_juice_pol = Entity.fcs_juice_pol,
                    fcs_juice_ph = Entity.fcs_juice_ph,
                    fcs_juice_purity = Entity.fcs_juice_purity,
                    clear_juice_brix = Entity.clear_juice_brix,
                    clear_juice_pol = Entity.clear_juice_pol,
                    clear_juice_ph = Entity.clear_juice_ph,
                    clear_juice_purity = Entity.clear_juice_purity,
                    unsulphured_syrup_brix = Entity.unsulphured_syrup_brix,
                    unsulphured_syrup_pol = Entity.unsulphured_syrup_pol,
                    unsulphured_syrup_ph = Entity.unsulphured_syrup_ph,
                    unsulphured_syrup_purity = Entity.unsulphured_syrup_purity,
                    sulphured_syrup_brix = Entity.sulphured_syrup_brix,
                    sulphured_syrup_pol = Entity.sulphured_syrup_pol,
                    sulphured_syrup_ph = Entity.sulphured_syrup_ph,
                    sulphured_syrup_purity = Entity.sulphured_syrup_purity,
                    final_molasses_brix = Entity.final_molasses_brix,
                    final_molasses_pol = Entity.final_molasses_pol,
                    final_molasses_purity = Entity.final_molasses_purity,
                    final_molasses_temp = Entity.final_molasses_temp,
                    final_molasses_tanks = Entity.final_molasses_tanks,
                    nm_bagasse_pol = Entity.nm_bagasse_pol,
                    nm_bagasse_moisture = Entity.nm_bagasse_moisture,
                    om_bagasse_pol = Entity.om_bagasse_pol,
                    om_bagasse_moisture = Entity.om_bagasse_moisture,
                    pol_pressure_cake_sample1 = Entity.pol_pressure_cake_sample1,
                    pol_pressure_cake_sample2 = Entity.pol_pressure_cake_sample2,
                    pol_pressure_cake_sample3 = Entity.pol_pressure_cake_sample3,
                    pol_pressure_cake_sample4 = Entity.pol_pressure_cake_sample4,
                    pol_pressure_cake_sample5 = Entity.pol_pressure_cake_sample5,
                    pol_pressure_cake_sample6 = Entity.pol_pressure_cake_sample6,
                    pol_pressure_cake_average = Entity.pol_pressure_cake_average,
                    pol_pressure_cake_moisture = Entity.pol_pressure_cake_moisture,
                    pol_pressure_cake_composite = Entity.pol_pressure_cake_composite,
                    crtd_dt = Entity.crtd_dt,
                    crtd_by = Entity.crtd_by
                };
                return Model;
            }
            return null;
        }
    }
}