using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
//using DataAccess.CustomModels;
using LitmusWeb.Models.CustomModels;
using DataAccess.Repositories;

namespace LitmusWeb.Controllers
{
    public class HourlyAnalysisController : Controller
    {
        HourlyAnalysisRepository Repository = new HourlyAnalysisRepository();
        // GET: HourlyAnalys


        #region Commented code before applying MillControl Data on index page 
        //public ActionResult Index()
        //{
        //    if (Session["UserCode"] == null || Session["BaseUnitCode"] == null)
        //    {
        //        ViewBag.unit_code = Convert.ToInt16(Session["BaseUnitCode"]);
        //        ViewBag.season_code = Convert.ToInt16(Session["CrushingSeason"]);

        //        TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
        //        return RedirectToAction("Index", "Home");
        //    }

        //    string BaseUnitcode = Session["BaseUnitCode"].ToString();
        //    int SeasonCode = Convert.ToInt16(Session["CrushingSeason"]);

        //    DateTime EntryDate = DateTime.Parse(ViewBag.EntryDate);

        //    List<HourlyAnalys> Entity = new List<HourlyAnalys>();
        //    Entity = Repository.GetHourlyAnalysisList(Convert.ToInt32(BaseUnitcode), SeasonCode, Convert.ToDateTime(EntryDate));

        //    ViewBag.DayCompleted = false;
        //    // checking if all analysis entries for the day are done or not.
        //    // if all entries are done, we have to disable "Add New" button at index page.
        //    if (Entity.Count == 24)
        //    {
        //        ViewBag.DayCompleted = true;
        //    }
        //    List<HourlyAnalysisModel> model = new List<HourlyAnalysisModel>();
        //    foreach (var item in Entity)
        //    {
        //        HourlyAnalysisModel temp = new HourlyAnalysisModel()
        //        {
        //            id = item.id,
        //            unit_code = item.unit_code,
        //            season_code = item.season_code,
        //            entry_Date = item.entry_Date,
        //            entry_time = item.entry_time,
        //            new_mill_juice = item.new_mill_juice,
        //            old_mill_juice = item.old_mill_juice,
        //            juice_total = item.juice_total,
        //            new_mill_water = item.new_mill_water,
        //            old_mill_water = item.old_mill_water,
        //            water_total = item.water_total,
        //            sugar_bags_L31 = item.sugar_bags_L31,
        //            sugar_bags_L30 = item.sugar_bags_L30,
        //            sugar_bags_L_total = item.sugar_bags_L_total,
        //            sugar_bags_M31 = item.sugar_bags_M31,
        //            sugar_bags_M30 = item.sugar_bags_M30,
        //            sugar_bags_M_total = item.sugar_bags_M_total,
        //            sugar_bags_S31 = item.sugar_bags_S31,
        //            sugar_bags_S30 = item.sugar_bags_S30,
        //            sugar_bags_S_total = item.sugar_bags_S_total,
        //            sugar_Biss = item.sugar_Biss,
        //            sugar_raw = item.sugar_raw,
        //            sugar_bags_total = item.sugar_bags_total,
        //            cooling_trace = item.cooling_trace,
        //            cooling_pol = item.cooling_pol,
        //            cooling_ph = item.cooling_ph,
        //            crtd_dt = item.crtd_dt,
        //            crtd_by = item.crtd_by,
        //            updt_dt = item.updt_dt,
        //            updt_by = item.updt_by,
        //            standing_truck = item.standing_truck,
        //            standing_trippler = item.standing_trippler,
        //            standing_trolley = item.standing_trolley,
        //            standing_cart = item.standing_cart,
        //            un_crushed_cane = item.un_crushed_cane,
        //            crushed_cane = item.crushed_cane,
        //            cane_diverted_for_syrup = item.cane_diverted_for_syrup,
        //            diverted_syrup_quantity = item.diverted_syrup_quantity,

        //        };
        //        model.Add(temp);
        //    }
        //    return View(model);
        //}
        #endregion
        [UnitBasedValuesFilter]
        [HttpGet]
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

            //List<HourlyAnalys> Entity = new List<HourlyAnalys>();
            //Entity = Repository.GetHourlyAnalysisList(Convert.ToInt32(BaseUnitcode), SeasonCode, Convert.ToDateTime(EntryDate));
            List<HourlyAnalysesViewModel> Entity = new List<HourlyAnalysesViewModel>();
            var data = Repository.GetHourlyAnalysesWithMillControlDataList(Convert.ToInt32(BaseUnitcode), Convert.ToDateTime(EntryDate));
            if(data.Count > 0)
            {
                foreach(var d in data)
                {
                    HourlyAnalysesViewModel temp = new HourlyAnalysesViewModel();
                    HourlyAnalys hm = new HourlyAnalys()
                    {
                        id = d.hourlyAnalysesModel.id,
                        unit_code = d.hourlyAnalysesModel.unit_code,
                        season_code = d.hourlyAnalysesModel.season_code,
                        entry_Date = d.hourlyAnalysesModel.entry_Date,
                        entry_time = d.hourlyAnalysesModel.entry_time,
                        new_mill_juice = d.hourlyAnalysesModel.new_mill_juice,
                        old_mill_juice = d.hourlyAnalysesModel.old_mill_juice,
                        juice_total = d.hourlyAnalysesModel.juice_total,
                        new_mill_water = d.hourlyAnalysesModel.new_mill_water,
                        old_mill_water = d.hourlyAnalysesModel.old_mill_water,
                        water_total = d.hourlyAnalysesModel.water_total,
                        sugar_bags_L31 = d.hourlyAnalysesModel.sugar_bags_L31,
                        sugar_bags_L30 = d.hourlyAnalysesModel.sugar_bags_L30,
                        sugar_bags_L_total = d.hourlyAnalysesModel.sugar_bags_L_total,
                        sugar_bags_M31 = d.hourlyAnalysesModel.sugar_bags_M31,
                        sugar_bags_M30 = d.hourlyAnalysesModel.sugar_bags_M30,
                        sugar_bags_M_total = d.hourlyAnalysesModel.sugar_bags_M_total,
                        sugar_bags_S31 = d.hourlyAnalysesModel.sugar_bags_S31,
                        sugar_bags_S30 = d.hourlyAnalysesModel.sugar_bags_S30,
                        sugar_bags_S_total = d.hourlyAnalysesModel.sugar_bags_S_total,
                        sugar_Biss = d.hourlyAnalysesModel.sugar_Biss,
                        sugar_raw = d.hourlyAnalysesModel.sugar_raw,
                        sugar_bags_total = d.hourlyAnalysesModel.sugar_bags_total,
                        cooling_trace = d.hourlyAnalysesModel.cooling_trace,
                        cooling_pol = d.hourlyAnalysesModel.cooling_pol,
                        cooling_ph = d.hourlyAnalysesModel.cooling_ph,
                        standing_truck = d.hourlyAnalysesModel.standing_truck,
                        standing_trippler = d.hourlyAnalysesModel.standing_trippler,
                        standing_trolley = d.hourlyAnalysesModel.standing_trolley,
                        standing_cart = d.hourlyAnalysesModel.standing_cart,
                        un_crushed_cane = d.hourlyAnalysesModel.un_crushed_cane,
                        crushed_cane = d.hourlyAnalysesModel.crushed_cane,
                        cane_diverted_for_syrup = d.hourlyAnalysesModel.cane_diverted_for_syrup,
                        diverted_syrup_quantity = d.hourlyAnalysesModel.diverted_syrup_quantity,
                        export_sugar = d.hourlyAnalysesModel.export_sugar,
                    };
                    HourlyAnalysesMillControlData mcm = new HourlyAnalysesMillControlData()
                    {
                        Id = d.MillControlModel.Id,
                        HourlyAnalysesNo = d.MillControlModel.HourlyAnalysesNo,
                        unit_code = d.MillControlModel.unit_code,
                        season_code = d.MillControlModel.season_code,
                        entry_date = d.MillControlModel.entry_date,
                        entry_time = d.MillControlModel.entry_time,
                        imbibition_water_temp = d.MillControlModel.imbibition_water_temp,
                        exhaust_steam_temp = d.MillControlModel.exhaust_steam_temp,
                        mill_biocide_dosing = d.MillControlModel.mill_biocide_dosing,
                        mill_washing = d.MillControlModel.mill_washing,
                        mill_steaming = d.MillControlModel.mill_steaming,
                        sugar_bags_temp = d.MillControlModel.sugar_bags_temp,
                        molasses_inlet_temp = d.MillControlModel.molasses_inlet_temp,
                        molasses_outlet_temp = d.MillControlModel.molasses_outlet_temp,
                        mill_hydraulic_pressure_one = d.MillControlModel.mill_hydraulic_pressure_one,
                        mill_hydraulic_pressure_two = d.MillControlModel.mill_hydraulic_pressure_two,
                        mill_hydraulic_pressure_three = d.MillControlModel.mill_hydraulic_pressure_three,
                        mill_hydraulic_pressure_four = d.MillControlModel.mill_hydraulic_pressure_four,
                        mill_hydraulic_pressure_five = d.MillControlModel.mill_hydraulic_pressure_five,
                        mill_load_one = d.MillControlModel.mill_load_one,
                        mill_load_two = d.MillControlModel.mill_load_two,
                        mill_load_three = d.MillControlModel.mill_load_three,
                        mill_load_four = d.MillControlModel.mill_load_four,
                        mill_load_five = d.MillControlModel.mill_load_five,
                        mill_rpm_one = d.MillControlModel.mill_rpm_one,
                        mill_rpm_two = d.MillControlModel.mill_rpm_two,
                        mill_rpm_three = d.MillControlModel.mill_rpm_three,
                        mill_rpm_four = d.MillControlModel.mill_rpm_four,
                        mill_rpm_five = d.MillControlModel.mill_rpm_five
                    };
                    temp.hourlyAnalysesModel = hm;
                    temp.MillControlModel = mcm;
                    Entity.Add(temp);
                }
            }
            

            ViewBag.DayCompleted = false;
            // checking if all analysis entries for the day are done or not.
            // if all entries are done, we have to disable "Add New" button at index page.
            if (data.Count == 24)
            {
                ViewBag.DayCompleted = true;
            }
            return View(Entity);
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

            //HourlyAnalysisModel model = new HourlyAnalysisModel();
            //HourlyAnalysesViewModel model = new HourlyAnalysesViewModel();
            HourlyAnalysesViewModel m = new HourlyAnalysesViewModel();
            List<SelectListItem> yesNoList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" }
            };

            // Set the SelectList to your model property
            ViewBag.YesNoList = new SelectList(yesNoList, "Value", "Text", selectedValue: "false");
            
            return View(m);
        }

        [UnitBasedValuesFilter]
        [HttpPost]
        [ActionName(name: "Create")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(HourlyAnalysesViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            DataAccess.CustomModels.HourlyAnalysesViewModel ham = new DataAccess.CustomModels.HourlyAnalysesViewModel()
            {
                hourlyAnalysesModel = new HourlyAnalys()
                {
                    unit_code = Convert.ToInt16(Session["BaseUnitCode"]),
                    season_code = Convert.ToInt16(Session["CrushingSeason"]),
                    entry_Date = Convert.ToDateTime(ViewBag.EntryDate),
                    entry_time = ViewBag.EntryTime,
                    new_mill_juice = model.hourlyAnalysesModel.new_mill_juice,
                    old_mill_juice = model.hourlyAnalysesModel.old_mill_juice,
                    juice_total = model.hourlyAnalysesModel.juice_total,
                    new_mill_water = model.hourlyAnalysesModel.new_mill_water,
                    old_mill_water = model.hourlyAnalysesModel.old_mill_water,
                    water_total = model.hourlyAnalysesModel.water_total,
                    sugar_bags_L31 = model.hourlyAnalysesModel.sugar_bags_L31,
                    sugar_bags_L30 = model.hourlyAnalysesModel.sugar_bags_L30,
                    sugar_bags_L_total = model.hourlyAnalysesModel.sugar_bags_L_total,
                    sugar_bags_M31 = model.hourlyAnalysesModel.sugar_bags_M31,
                    sugar_bags_M30 = model.hourlyAnalysesModel.sugar_bags_M30,
                    sugar_bags_M_total = model.hourlyAnalysesModel.sugar_bags_M_total,
                    sugar_bags_S31 = model.hourlyAnalysesModel.sugar_bags_S31,
                    sugar_bags_S30 = model.hourlyAnalysesModel.sugar_bags_S30,
                    sugar_bags_S_total = model.hourlyAnalysesModel.sugar_bags_S_total,
                    sugar_Biss = model.hourlyAnalysesModel.sugar_Biss,
                    sugar_raw = model.hourlyAnalysesModel.sugar_raw,
                    sugar_bags_total = model.hourlyAnalysesModel.sugar_bags_total,
                    cooling_trace = model.hourlyAnalysesModel.cooling_trace,
                    cooling_pol = model.hourlyAnalysesModel.cooling_pol,
                    cooling_ph = model.hourlyAnalysesModel.cooling_ph,
                    crtd_dt = DateTime.Now,
                    crtd_by = Session["UserCode"].ToString(),
                    updt_dt = null,
                    updt_by = null,
                    standing_truck = model.hourlyAnalysesModel.standing_truck,
                    standing_trippler = model.hourlyAnalysesModel.standing_trippler,
                    standing_trolley = model.hourlyAnalysesModel.standing_trolley,
                    standing_cart = model.hourlyAnalysesModel.standing_cart,
                    crushed_cane = model.hourlyAnalysesModel.crushed_cane,
                    un_crushed_cane = model.hourlyAnalysesModel.un_crushed_cane,
                    cane_diverted_for_syrup = model.hourlyAnalysesModel.cane_diverted_for_syrup,
                    diverted_syrup_quantity = model.hourlyAnalysesModel.diverted_syrup_quantity,
                    export_sugar = model.hourlyAnalysesModel.export_sugar
                },
                MillControlModel = new HourlyAnalysesMillControlData()
                {
                    imbibition_water_temp = model.MillControlModel.imbibition_water_temp,
                    exhaust_steam_temp = model.MillControlModel.exhaust_steam_temp,
                    mill_biocide_dosing = model.MillControlModel.mill_biocide_dosing,
                    mill_washing = model.MillControlModel.mill_washing,
                    mill_steaming = model.MillControlModel.mill_steaming,
                    sugar_bags_temp = model.MillControlModel.sugar_bags_temp,
                    molasses_inlet_temp = model.MillControlModel.molasses_inlet_temp,
                    molasses_outlet_temp = model.MillControlModel.molasses_outlet_temp,
                    mill_hydraulic_pressure_one = model.MillControlModel.mill_hydraulic_pressure_one,
                    mill_hydraulic_pressure_two = model.MillControlModel.mill_hydraulic_pressure_two,
                    mill_hydraulic_pressure_three = model.MillControlModel.mill_hydraulic_pressure_three,
                    mill_hydraulic_pressure_four = model.MillControlModel.mill_hydraulic_pressure_four,
                    mill_hydraulic_pressure_five = model.MillControlModel.mill_hydraulic_pressure_five,
                    mill_load_one = model.MillControlModel.mill_load_one,
                    mill_load_two = model.MillControlModel.mill_load_two,
                    mill_load_three = model.MillControlModel.mill_load_three,
                    mill_load_four = model.MillControlModel.mill_load_four,
                    mill_load_five = model.MillControlModel.mill_load_five,
                    mill_rpm_one = model.MillControlModel.mill_rpm_one,
                    mill_rpm_two = model.MillControlModel.mill_rpm_two,
                    mill_rpm_three = model.MillControlModel.mill_rpm_three,
                    mill_rpm_four = model.MillControlModel.mill_rpm_four,
                    mill_rpm_five = model.MillControlModel.mill_rpm_five
                }

            };




           // DataAccess.CustomModels.HourlyAnalysesViewModel ham = new DataAccess.CustomModels.HourlyAnalysesViewModel();
            //ham.hourlyAnalysesModel = hourlyAnalysisEntity;
            //ham.MillControlModel = ctlData;

            bool result = Repository.CreateHourlyAnalysis(ham);
            //if (result == false)
            //{
            //    return View("Error");
            //}

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
            HourlyAnalysisModel Model = GetHourlyAnalysis(id);
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
            
            HourlyAnalysisModel Model = GetHourlyAnalysis(id);
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
        private HourlyAnalysisModel GetHourlyAnalysis(int id)
        {
            HourlyAnalys Entity = new HourlyAnalys();
            int unitCode = (int)Session["BaseUnitCode"];
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

        #region Mill Control Data (tab-2)
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Edit")]
        public HourlyAnalysesViewModel GetMillControlData(int id, int unitCode)
        {
            try
            {
                var result = Repository.GetMillControlDataById(id, unitCode);
                if (result != null)
                {
                    HourlyAnalysesViewModel ham = new HourlyAnalysesViewModel()
                    {
                        hourlyAnalysesModel = new HourlyAnalys()
                        {
                            cooling_trace = result.hourlyAnalysesModel.cooling_trace  ,
                            cooling_pol = result.hourlyAnalysesModel.cooling_pol,
                            cooling_ph = result.hourlyAnalysesModel.cooling_ph,
                        },
                        MillControlModel = new HourlyAnalysesMillControlData()
                        {
                            imbibition_water_temp = result.MillControlModel.imbibition_water_temp,
                            exhaust_steam_temp = result.MillControlModel.exhaust_steam_temp,
                            mill_biocide_dosing = result.MillControlModel.mill_biocide_dosing,
                            mill_washing = result.MillControlModel.mill_washing,
                            mill_steaming = result.MillControlModel.mill_steaming,
                            sugar_bags_temp = result.MillControlModel.sugar_bags_temp,
                            molasses_inlet_temp = result.MillControlModel.molasses_inlet_temp,
                            molasses_outlet_temp = result.MillControlModel.molasses_outlet_temp,
                            mill_hydraulic_pressure_one = result.MillControlModel.mill_hydraulic_pressure_one,
                            mill_hydraulic_pressure_two = result.MillControlModel.mill_hydraulic_pressure_two,
                            mill_hydraulic_pressure_three = result.MillControlModel.mill_hydraulic_pressure_three,
                            mill_hydraulic_pressure_four = result.MillControlModel.mill_hydraulic_pressure_four,
                            mill_hydraulic_pressure_five = result.MillControlModel.mill_hydraulic_pressure_five,
                            mill_load_one = result.MillControlModel.mill_load_one,
                            mill_load_two = result.MillControlModel.mill_load_two,
                            mill_load_three = result.MillControlModel.mill_load_three,
                            mill_load_four = result.MillControlModel.mill_load_four,
                            mill_load_five = result.MillControlModel.mill_load_five,
                            mill_rpm_one = result.MillControlModel.mill_rpm_one,
                            mill_rpm_two = result.MillControlModel.mill_rpm_two,
                            mill_rpm_three = result.MillControlModel.mill_rpm_three,
                            mill_rpm_four = result.MillControlModel.mill_rpm_four,
                            mill_rpm_five = result.MillControlModel.mill_rpm_five
                        }
                    };
                    return ham;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
            return null;
        }

        public List<HourlyAnalysesMillControlDataModel> MillControlDataList(int unit_code, int season_code, DateTime entry_date)
        {
            List<HourlyAnalysesMillControlDataModel> list = new List<HourlyAnalysesMillControlDataModel>();
            try
            {
                var result = Repository.GetMillControlDataByUnit(unit_code,season_code, entry_date);
                if (result.Count > 0)
                {
                    foreach(var r in result)
                    {
                        HourlyAnalysesMillControlDataModel m = new HourlyAnalysesMillControlDataModel()
                        {
                            Id = r.Id,
                            HourlyAnalysesNo = r.HourlyAnalysesNo,
                            unit_code = r.unit_code,
                            season_code = r.season_code,
                            entry_date = r.entry_date,
                            entry_time = r.entry_time,
                            imbibition_water_temp = r.imbibition_water_temp,
                            exhaust_steam_temp = r.exhaust_steam_temp,
                            mill_biocide_dosing = r.mill_biocide_dosing,
                            mill_washing = r.mill_washing,
                            mill_steaming = r.mill_steaming,
                            sugar_bags_temp = r.sugar_bags_temp,
                            molasses_inlet_temp = r.molasses_inlet_temp,
                            molasses_outlet_temp = r.molasses_outlet_temp,
                            mill_hydraulic_pressure_one = r.mill_hydraulic_pressure_one,
                            mill_hydraulic_pressure_two = r.mill_hydraulic_pressure_two,
                            mill_hydraulic_pressure_three = r.mill_hydraulic_pressure_three,
                            mill_hydraulic_pressure_four = r.mill_hydraulic_pressure_four,
                            mill_hydraulic_pressure_five = r.mill_hydraulic_pressure_five,
                            mill_load_one = r.mill_load_one,
                            mill_load_two = r.mill_load_two,
                            mill_load_three = r.mill_load_three,
                            mill_load_four = r.mill_load_four,
                            mill_load_five = r.mill_load_five,
                            mill_rpm_one = r.mill_rpm_one,
                            mill_rpm_two = r.mill_rpm_two,
                            mill_rpm_three = r.mill_rpm_three,
                            mill_rpm_four = r.mill_rpm_four,
                            mill_rpm_five = r.mill_rpm_five
                        };
                        list.Add(m);
                    }
                    
                    return list;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
            return null;
        }

        [HttpGet]
        public ActionResult EditMillControlData(int id)
        {
            if(Session["BaseUnitCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            HourlyAnalysesViewModel m = new HourlyAnalysesViewModel();
            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            TempData["ID"] = id;
            m = GetMillControlData(id, unitCode);

            List<SelectListItem> yesNoList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" }
            };

            // Set the SelectList to your model property
            ViewBag.millWashingYesNoList = new SelectList(yesNoList, "Value", "Text", m.MillControlModel.mill_washing.ToString());
            ViewBag.millSteamingYesNoList = new SelectList(yesNoList, "Value", "Text", m.MillControlModel.mill_steaming.ToString());
            ViewBag.millBiocideDosingYesNoList = new SelectList(yesNoList, "Value", "Text", m.MillControlModel.mill_biocide_dosing.ToString());


            return View(m);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Hourly Edit")]
        [ActionName("EditMillControlData")]
        public ActionResult PostEditMillControlData(HourlyAnalysesViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if(TempData["ID"] != null)
                {
                    DataAccess.CustomModels.HourlyAnalysesViewModel data = new DataAccess.CustomModels.HourlyAnalysesViewModel()
                    {
                        hourlyAnalysesModel = new HourlyAnalys()
                        {
                            cooling_trace = model.hourlyAnalysesModel.cooling_trace,
                            cooling_ph = model.hourlyAnalysesModel.cooling_ph,
                            cooling_pol = model.hourlyAnalysesModel.cooling_pol
                        },
                        MillControlModel = new HourlyAnalysesMillControlData()
                        {
                            Id = (int)TempData["ID"],
                            unit_code = (int)Session["BaseUnitCode"],
                            season_code = (int)Session["CrushingSeason"],
                            imbibition_water_temp = model.MillControlModel.imbibition_water_temp,
                            exhaust_steam_temp = model.MillControlModel.exhaust_steam_temp,
                            mill_biocide_dosing = model.MillControlModel.mill_biocide_dosing,
                            mill_washing = model.MillControlModel.mill_washing,
                            mill_steaming = model.MillControlModel.mill_steaming,
                            sugar_bags_temp = model.MillControlModel.sugar_bags_temp,
                            molasses_inlet_temp = model.MillControlModel.molasses_inlet_temp,
                            molasses_outlet_temp = model.MillControlModel.molasses_outlet_temp,
                            mill_hydraulic_pressure_one = model.MillControlModel.mill_hydraulic_pressure_one,
                            mill_hydraulic_pressure_two = model.MillControlModel.mill_hydraulic_pressure_two,
                            mill_hydraulic_pressure_three = model.MillControlModel.mill_hydraulic_pressure_three,
                            mill_hydraulic_pressure_four = model.MillControlModel.mill_hydraulic_pressure_four,
                            mill_hydraulic_pressure_five = model.MillControlModel.mill_hydraulic_pressure_five,
                            mill_load_one = model.MillControlModel.mill_load_one,
                            mill_load_two = model.MillControlModel.mill_load_two,
                            mill_load_three = model.MillControlModel.mill_load_three,
                            mill_load_four = model.MillControlModel.mill_load_four,
                            mill_load_five = model.MillControlModel.mill_load_five,
                            mill_rpm_one = model.MillControlModel.mill_rpm_one,
                            mill_rpm_two = model.MillControlModel.mill_rpm_two,
                            mill_rpm_three = model.MillControlModel.mill_rpm_three,
                            mill_rpm_four = model.MillControlModel.mill_rpm_four,
                            mill_rpm_five = model.MillControlModel.mill_rpm_five
                        }
                    };
                    bool result = Repository.UpdateMillcontrolData(data);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
                
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return View(model);
        }

        #endregion




    }
}