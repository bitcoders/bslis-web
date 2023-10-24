using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static iText.IO.Util.IntHashtable;

namespace LitmusWeb.Controllers
{
    public class DailyAnalysisController : Controller
    {
        readonly DailyAnalysisRepository Repository = new DailyAnalysisRepository();
        readonly MasterParameterSubCategoryRepository SubParameterRepository = new MasterParameterSubCategoryRepository();
        // GET: DailyAnalys
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }   
            SetUnitDefaultValues();

            ViewBag.MaterialSentOut = SubParameterRepository.GetMasterParameterCategoriesByParameterMasterCode(7);
            ViewBag.SugarProduces = SubParameterRepository.GetMasterParameterCategoriesByParameterMasterCode(8);


            DailyAnalys Entity = new DailyAnalys();
            Entity = Repository.GetDailyAnalysisDetailsByDate(Convert.ToInt16(TempData["BaseUnitCode"])
                                                    , Convert.ToInt16(ViewBag.CrushingSeason)
                                                    , ViewBag.EntryDate);

            if (Entity == null)
            {
                /* InitialState TempData defines that
                 there is no value exist in table for the 
                 crushing season and entry date.
                 By this we will set 0 to all input filds, 
                 otherwise we will set the values from database
                 for the respective input filed*/
                //TempData["InitialState"] = "New";
                ViewBag.InitialState = "New";
                DailyAnalysisModel dm = new DailyAnalysisModel();
                return View(dm);
            }
            else
            {
                //TempData["InitialState"] = "Update";
                ViewBag.InitialState = "Update";
            }

            DailyAnalysisModel Model = new DailyAnalysisModel()
            {
                id = Entity.id,
                unit_code = Entity.unit_code,
                season_code = Entity.season_code,
                entry_date = Entity.entry_date,
                trans_time = Entity.trans_time,
                cane_early = Entity.cane_early,
                cane_general = Entity.cane_general,
                cane_rejected = Entity.cane_rejected,
                cane_gate = Entity.cane_gate,
                cane_centre = Entity.cane_centre,
                cane_burnt = Entity.cane_burnt,
                cane_crushed = Entity.cane_crushed,
                total_juice = Entity.total_juice,
                sp_gravity = Entity.sp_gravity,
                total_water = Entity.total_water,
                press_cake = Entity.press_cake,
                molasses_sent_out = Entity.molasses_sent_out,
                biss_sugar = Entity.biss_sugar,
                biss_molasses = Entity.biss_molasses,
                scrap_sugar = Entity.scrap_sugar,
                scrap_molasses = Entity.scrap_molasses,
                moist_sugar = Entity.moist_sugar,
                moist_molasses = Entity.moist_molasses,
                raw_sugar = Entity.raw_sugar,
                raw_molasses = Entity.raw_molasses,
                other_sugar = Entity.other_sugar,
                other_molasses = Entity.other_molasses,
                store_sulpher = Entity.store_sulpher,
                store_phosphoric = Entity.store_phosphoric,
                store_lime = Entity.store_lime,
                store_viscosity_reducer = Entity.store_viscosity_reducer,
                store_biocide = Entity.store_biocide,
                store_color_reducer = Entity.store_color_reducer,
                store_megnafloe = Entity.store_megnafloe,
                store_lub_oil = Entity.store_lub_oil,
                store_lub_grease = Entity.store_lub_grease,
                store_boiler_chemical = Entity.store_boiler_chemical,
                icumsa_l31 = Entity.icumsa_l31,
                icumsa_l30 = Entity.icumsa_l30,
                icumsa_m31 = Entity.icumsa_m31,
                icumsa_m30 = Entity.icumsa_m30,
                icumsa_s31 = Entity.icumsa_s31,
                icumsa_s30 = Entity.icumsa_s30,
                foreign_matter_l31 = Entity.foreign_matter_l31,
                foreign_matter_l30 = Entity.foreign_matter_l30,
                foreign_matter_m31 = Entity.foreign_matter_m31,
                foreign_matter_m30 = Entity.foreign_matter_m30,
                foreign_matter_s31 = Entity.foreign_matter_s31,
                foreign_matter_s30 = Entity.foreign_matter_s30,
                retention_l31 = Entity.retention_l31,
                retention_l30 = Entity.retention_l30,
                retention_m31 = Entity.retention_m31,
                retention_m30 = Entity.retention_m30,
                retention_s31 = Entity.retention_s31,
                retention_s30 = Entity.retention_s30,
                moisture_l31 = Entity.moisture_l31,
                moisture_l30 = Entity.moisture_l30,
                moisture_m31 = Entity.moisture_m31,
                moisture_m30 = Entity.moisture_m30,
                moisture_s31 = Entity.moisture_s31,
                moisture_s30 = Entity.moisture_s30,
                etp_ph = Entity.etp_ph,
                etp_tss = Entity.etp_tss,
                etp_cod = Entity.etp_cod,
                etp_bod = Entity.etp_bod,
                etp_water_flow = Entity.etp_water_flow,
                calcium_mixed_juice = Entity.calcium_mixed_juice,
                calcium_clear_juice = Entity.calcium_clear_juice,
                phosphate_mixed_juice = Entity.phosphate_mixed_juice,
                phosphate_clear_juice = Entity.phosphate_clear_juice,
                rs_primary_juice = Entity.rs_primary_juice,
                rs_mixed_juice = Entity.rs_mixed_juice,
                rs_clear_juice = Entity.rs_clear_juice,
                unknown_losses = Entity.unknown_losses,
                dirt_correction = Entity.dirt_correction,
                total_operating_tube_well = Entity.total_operating_tube_well,
                exhaust_condensate_recovery = Entity.exhaust_condensate_recovery,
                T_c_massecuite_pan = Entity.T_c_massecuite_pan,
                ph_injection_inlet = Entity.ph_injection_inlet,
                ph_injection_outlet = Entity.ph_injection_outlet,
                average_vaccume_pan = Entity.average_vaccume_pan,
                average_vaccume_evap = Entity.average_vaccume_evap,
                Exhaust_steam_press_lp = Entity.Exhaust_steam_press_lp,
                Exhaust_steam_press_hp = Entity.Exhaust_steam_press_hp,
                boiler_steam_press_lp = Entity.boiler_steam_press_lp,
                boiler_steam_press_hp = Entity.boiler_steam_press_hp,
                ph_boiler_feed_water = Entity.ph_boiler_feed_water,
                bagasse_baed = Entity.bagasse_baed,
                power_from_grid = Entity.power_from_grid,
                power_export_grid = Entity.power_export_grid,
                
                filter_water = Entity.filter_water,
                pan_water = Entity.pan_water,
                cf_water = Entity.cf_water,
                bagasse_sold = Entity.bagasse_sold,
                bagasse_stock = Entity.bagasse_stock,
                nm_p_index = Entity.nm_p_index,
                nm_pry_ext = Entity.nm_pry_ext,
                b_heavy_final_molasses_trs = Entity.b_heavy_final_molasses_trs,
                b_heavy_final_molasses_rs = Entity.b_heavy_final_molasses_rs,
                temp_max = Entity.temp_max,
                temp_min = Entity.temp_min,
                humidity = Entity.humidity,
                rain_fall = Entity.rain_fall,
                iu_primary_juice = Entity.iu_primary_juice,
                iu_mixed_juice = Entity.iu_mixed_juice,
                iu_clear_juice = Entity.iu_clear_juice,
                live_steam_generation = Entity.live_steam_generation,
                live_steam_consumption = Entity.live_steam_consumption,
                power_turbines = Entity.power_turbines,
                bleeding_in_process = Entity.bleeding_in_process,
                bleeding_acf = Entity.bleeding_acf,
                ata3_cogen = Entity.ata3_cogen,
                d_sulpher_heating = Entity.d_sulpher_heating,
                drain_pipe_loss = Entity.drain_pipe_loss,
                exhaust_steam_generation = Entity.exhaust_steam_generation,
                exhaust_steam_consumption = Entity.exhaust_steam_consumption,
                steam_per_ton_cane = Entity.steam_per_ton_cane,
                steam_per_qtl_sugar = Entity.steam_per_qtl_sugar,
                power_from_sugar = Entity.power_from_sugar,
                power_dg_set = Entity.power_dg_set,
                power_import_cogen = Entity.power_import_cogen,
                total_power = Entity.total_power,
                power_per_ton_cane = Entity.power_per_ton_cane,
                power_per_qtl_sugar = Entity.power_per_qtl_sugar,
                om_p_index = Entity.om_p_index,
                om_pry_ext = Entity.om_pry_ext,
                steam_percent_cane = Entity.steam_percent_cane,
                cane_farm = Entity.cane_farm,
                boiler_water = Entity.boiler_water,
                daily_hour_worked = Entity.daily_hour_worked,
                home_consumption = Entity.home_consumption,
                power_generation_from_coGen = Entity.power_generation_from_coGen,
                bagasse_consumed_qtl = Entity.bagasse_consumed_qtl,
                bagacillo_to_boiling_house = Entity.bagacillo_to_boiling_house,
                Steam_Fuel_Ratio = Entity.Steam_Fuel_Ratio,
                
                Temp_Injection_Water_Inlet = Entity.Temp_Injection_Water_Inlet,
                Temp_Injection_Water_Outlet = Entity.Temp_Injection_Water_Outlet,
                etp_water_consumption_at_plant = Entity.etp_water_consumption_at_plant,
                store_washing_soda = Entity.store_washing_soda,
                store_hydrolic_acid = Entity.store_hydrolic_acid,
                store_de_scaling_chemical = Entity.store_de_scaling_chemical,
                store_seed_slurry = Entity.store_seed_slurry,
                store_anti_fomer = Entity.store_anti_fomer,
                store_chemical_for_brs_cleaning = Entity.store_chemical_for_brs_cleaning,
                icumsa_raw_sugar = Entity.icumsa_raw_sugar,
                foreign_matter_raw_sugar = Entity.foreign_matter_raw_sguar,
                moisture_raw_sugar = Entity.moisture_raw_sugar,
                retention_raw_sugar = Entity.retention_raw_sugar,
                crtd_dt = Entity.crtd_dt,
                crtd_by = Entity.crtd_by,
                PowerToDitillery = Entity.PowerToDitillery,
                
                gross_biss_sugar = Entity.gross_biss_sugar,
                gross_scrap_sugar = Entity.gross_scrap_sugar,
                gross_moist_sugar = Entity.gross_moist_sugar,
                gross_raw_sugar = Entity.gross_raw_sugar,
                gross_other_sugar = Entity.gross_other_sugar,
                water_pan_a = Entity.water_pan_a,
                water_pan_b = Entity.water_pan_b,
                water_pan_c = Entity.water_pan_c,
                OverTimeEnggReplacement = Entity.OverTimeEnggReplacement,
                OverTimeEnggExtra = Entity.OverTimeEnggExtra,
                OverTimeMfgReplacement = Entity.OverTimeMfgReplacement,
                OverTimeMfgExtra = Entity.OverTimeMfgExtra,
                MolassesSentOutBHeavy = Entity.MolassesSentOutBHeavy,
                MolassesSentOutCHeavy = Entity.MolassesSentOutCHeavy,
                StoreProcessChemicalQuantity = Entity.StoreProcessChemicalQuantity,
                StoreProcessChemicalAmount = Entity.StoreProcessChemicalAmount,
                StoreProcessChemicalPerBagRate = Entity.StoreProcessChemicalPerBagRate,
                StoreProcessChemicalPerTonCaneRate = Entity.StoreProcessChemicalPerTonCaneRate,
                StoreBoilerChemicalQuantity = Entity.StoreBoilerChemicalQuantity,
                StoreBoilerChemicalAmount = Entity.StoreBoilerChemicalAmount,
                StoreBoilerChemicalPerBagRate = Entity.StoreBoilerChemicalPerBagRate,
                StoreBoilerChemicalPerTonCaneRate = Entity.StoreBoilerChemicalPerTonCaneRate,
                StoreGreaseOilQuantity = Entity.StoreGreaseOilQuantity,
                StoreGreaseOilAmount = Entity.StoreGreaseOilAmount,
                StoreGreaseOilPerBagRate = Entity.StoreGreaseOilPerBagRate,
                StoreGreaseOilPerTonCaneRate = Entity.StoreGreaseOilPerTonCaneRate
                ,
                QuintalSyrupDiversion = Entity.QuintalSyrupDiversion
                ,
                SugarPol = Entity.SugarPol
                ,
                CHeavyBrix = Entity.CHeavyBrix
                ,
                CHeavyPol = Entity.CHeavyPol
                ,
                CHeavyPurity = Entity.CHeavyPol > 0 ? Math.Round(((Entity.CHeavyPol * 100) / Entity.CHeavyBrix), 2) : 0
                ,
                RawSugarGainDate = Entity.RawSugarGainDate
                ,
                MaterialSentOut = Entity.MaterialSentOut
                ,
                SugarProduced = Entity.SugarProduced
                , PowerExportFromDistillery = Entity.PowerExportFromDistillery
                , PowerExportFromCogen = Entity.PowerExportFromCogen
                
                
                , mill_house_trace = Entity.mill_house_trace
                , mill_house_ph = Entity.mill_house_ph
                , mill_house_pol = Entity.mill_house_pol
                , AshSold = Entity.AshSold
                , under_oliver_filter_trace = Entity.under_oliver_filter_trace
                , under_oliver_filter_ph = Entity.under_oliver_filter_pH
                , under_oliver_filter_pol = Entity.under_oliver_filter_pol
                , c_heavy_final_molasses_trs = Entity.c_heavy_final_molasses_trs
                , c_heavy_final_molasses_rs = Entity.c_heavy_final_molasses_rs
                , syrup_trs = Entity.syrup_trs
                , syrup_rs = Entity.syrup_rs
                , power_export_from_sugar = Entity.power_export_from_sugar
                , power_generate_from_sugar = Entity.power_generate_from_sugar
                ,
                AlcoholFromCaneSyrup = Entity.AlcoholFromCaneSyrup
                ,
                AbsoluteAlcoholFromSyrupDayRecovery = Entity.AbsoluteAlcoholFromSyrupDayRecovery
                ,
                AbsoluteAlcoholFromSyrupToDateRecovery = Entity.AbsoluteAlcoholFromSyrupToDateRecovery
                ,
                RectifiedSpirit = Entity.RectifiedSpirit,
                RectifiedSpiritDayRecover = Entity.RectifiedSpiritDayRecover,
                RectifiedSpiritToDateRecovery = Entity.RectifiedSpiritToDateRecovery,
                Ethanol = Entity.Ethanol,
                EthanolDayRecovery = Entity.EthanolDayRecovery,
                EthanolToDateRecovery = Entity.EthanolToDateRecovery,
                AbsoluteAlcoholFromBHeavy = Entity.AbsoluteAlcoholFromBHeavy,
                AbsoluteAlcoholBHeavyDayRecovery = Entity.AbsoluteAlcoholBHeavyDayRecovery,
                AbsoluteAlcoholBHeavyToDateRecovery = Entity.AbsoluteAlcoholBHeavyToDateRecovery,
                AlcoholFromCHeavy = Entity.AlcoholFromCHeavy,
                AbsoluteAlcoholCHeavyDayRecovery = Entity.AbsoluteAlcoholCHeavyDayRecovery,
                AbsoluteAlcoholCHeavyToDateRecovery = Entity.AbsoluteAlcoholCHeavyToDateRecovery,
                AbsoluteAlcohol = Entity.AbsoluteAlcohol,
                AbsoluteAlcoholDayRecovery = Entity.AbsoluteAlcoholDayRecovery,
                AbsoluteAlcoholToDateRecovery = Entity.AbsoluteAlcoholToDateRecovery
                , refinery_water_consumption = Entity.refinery_water_consumption
                , fiberized_pol_in_cane = Entity.fiberized_pol_in_cane
                , dextran_primary_juice = Entity.dextran_primary_juice
                , dextran_mixed_juice = Entity.dextran_mixed_juice
                ,Remarks = Entity.Remarks
            };
            TempData["Crtd_dt"] = Model.crtd_dt;
            TempData["Crtd_By"] = Model.crtd_by;
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Index")]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("create")]
        public ActionResult IndexPost(DailyAnalysisModel Model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                List<Errors> errors = new List<Errors>();
                errorViewModel.ErrorTitle = "Errior while Modifying Stoppage Data";
                //foreach (var x in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList())
                foreach (var x in ModelState.Values.SelectMany(e => e.Errors).Select(y => y.ErrorMessage))
                {
                    Errors t = new Errors()
                    {
                        ErrorMessage = x.ToString()
                    };
                    errors.Add(t);
                }
                errorViewModel.ErrorMessage = errors;
                TempData["Model"] = errorViewModel;
                return RedirectToAction("ModelError", "Error");
            }
            SetUnitDefaultValues(); //setting all default values of base unit
            DailyAnalys Entity = new DailyAnalys()
            {
                unit_code = Convert.ToInt16(TempData["BaseUnitCode"]),
                season_code = ViewBag.CrushingSeason,
                entry_date = ViewBag.EntryDate,
                trans_time = DateTime.Now.ToShortTimeString(),
                cane_early = Model.cane_early,
                cane_general = Model.cane_general,
                cane_rejected = Model.cane_rejected,
                cane_burnt = Model.cane_burnt,
                cane_gate = Model.cane_gate,
                cane_centre = Model.cane_centre,
                cane_crushed = Model.cane_crushed,
                total_juice = Model.total_juice,
                sp_gravity = Model.sp_gravity,
                total_water = Model.total_water,
                press_cake = Model.press_cake,
                molasses_sent_out = Model.molasses_sent_out,
                biss_sugar = Model.biss_sugar,
                biss_molasses = Model.biss_molasses,
                scrap_sugar = Model.scrap_sugar,
                scrap_molasses = Model.scrap_molasses,
                moist_sugar = Model.moist_sugar,
                moist_molasses = Model.moist_molasses,
                raw_sugar = Model.raw_sugar,
                raw_molasses = Model.raw_molasses,
                other_sugar = Model.other_sugar,
                other_molasses = Model.other_molasses,
                store_sulpher = Model.store_sulpher,
                store_phosphoric = Model.store_phosphoric,
                store_lime = Model.store_lime,
                store_viscosity_reducer = Model.store_viscosity_reducer,
                store_biocide = Model.store_biocide,
                store_color_reducer = Model.store_color_reducer,
                store_megnafloe = Model.store_megnafloe,
                store_lub_oil = Model.store_lub_oil,
                store_lub_grease = Model.store_lub_grease,
                store_boiler_chemical = Model.store_boiler_chemical,
                store_washing_soda = Model.store_washing_soda,
                store_hydrolic_acid = Model.store_hydrolic_acid,
                store_de_scaling_chemical = Model.store_de_scaling_chemical,
                store_seed_slurry = Model.store_seed_slurry,
                store_anti_fomer = Model.store_anti_fomer,
                store_chemical_for_brs_cleaning = Model.store_chemical_for_brs_cleaning,
                icumsa_l31 = Model.icumsa_l31,
                icumsa_l30 = Model.icumsa_l30,
                icumsa_m31 = Model.icumsa_m31,
                icumsa_m30 = Model.icumsa_m30,
                icumsa_s31 = Model.icumsa_s31,
                icumsa_s30 = Model.icumsa_s30,
                icumsa_raw_sugar = Model.icumsa_raw_sugar,
                foreign_matter_l31 = Model.foreign_matter_l31,
                foreign_matter_l30 = Model.foreign_matter_l30,
                foreign_matter_m31 = Model.foreign_matter_m31,
                foreign_matter_m30 = Model.foreign_matter_m30,
                foreign_matter_s31 = Model.foreign_matter_s31,
                foreign_matter_s30 = Model.foreign_matter_s30,
                foreign_matter_raw_sguar = Model.foreign_matter_raw_sugar,
                retention_l31 = Model.retention_l31,
                retention_l30 = Model.retention_l30,
                retention_m31 = Model.retention_m31,
                retention_m30 = Model.retention_m30,
                retention_s31 = Model.retention_s31,
                retention_s30 = Model.retention_s30,
                retention_raw_sugar = Model.retention_raw_sugar,
                moisture_l31 = Model.moisture_l31,
                moisture_l30 = Model.moisture_l30,
                moisture_m31 = Model.moisture_m31,
                moisture_m30 = Model.moisture_m30,
                moisture_s31 = Model.moisture_s31,
                moisture_s30 = Model.moisture_s30,
                moisture_raw_sugar = Model.moisture_raw_sugar,
                etp_ph = Model.etp_ph,
                etp_tss = Model.etp_tss,
                etp_cod = Model.etp_cod,
                etp_bod = Model.etp_bod,
                etp_water_flow = Model.etp_water_flow,
                etp_water_consumption_at_plant = Model.etp_water_consumption_at_plant,
                calcium_mixed_juice = Model.calcium_mixed_juice,
                calcium_clear_juice = Model.calcium_clear_juice,
                phosphate_mixed_juice = Model.phosphate_mixed_juice,
                phosphate_clear_juice = Model.phosphate_clear_juice,
                rs_primary_juice = Model.rs_primary_juice,
                rs_mixed_juice = Model.rs_mixed_juice,
                rs_clear_juice = Model.rs_clear_juice,
                unknown_losses = Model.unknown_losses,
                dirt_correction = Model.dirt_correction,
                total_operating_tube_well = Model.total_operating_tube_well,
                exhaust_condensate_recovery = Model.exhaust_condensate_recovery,
                T_c_massecuite_pan = Model.T_c_massecuite_pan,
                ph_injection_inlet = Model.ph_injection_inlet,
                ph_injection_outlet = Model.ph_injection_outlet,
                Temp_Injection_Water_Inlet = Model.Temp_Injection_Water_Inlet,
                Temp_Injection_Water_Outlet = Model.Temp_Injection_Water_Outlet,
                average_vaccume_pan = Model.average_vaccume_pan,
                average_vaccume_evap = Model.average_vaccume_evap,
                Exhaust_steam_press_lp = Model.Exhaust_steam_press_lp,
                Exhaust_steam_press_hp = Model.Exhaust_steam_press_hp,
                boiler_steam_press_lp = Model.boiler_steam_press_lp,
                boiler_steam_press_hp = Model.boiler_steam_press_hp,
                ph_boiler_feed_water = Model.ph_boiler_feed_water,
                Temp_Boiler_feed_water = Model.Temp_Boiler_feed_water,
                bagasse_baed = Model.bagasse_baed,

                filter_water = Model.filter_water,
                pan_water = Model.pan_water,
                cf_water = Model.cf_water,
                bagasse_sold = Model.bagasse_sold,
                bagasse_stock = Model.bagasse_stock,
                bagasse_consumed_qtl = Model.bagasse_consumed_qtl,
                bagacillo_to_boiling_house = Model.bagacillo_to_boiling_house,
                Steam_Fuel_Ratio = Model.Steam_Fuel_Ratio,

                nm_p_index = Model.nm_p_index,
                nm_pry_ext = Model.nm_pry_ext,
                b_heavy_final_molasses_trs = Model.b_heavy_final_molasses_trs,
                b_heavy_final_molasses_rs = Model.b_heavy_final_molasses_rs,
                temp_max = Model.temp_max,
                temp_min = Model.temp_min,
                humidity = Model.humidity,
                rain_fall = Model.rain_fall,
                iu_primary_juice = Model.iu_primary_juice,
                iu_mixed_juice = Model.iu_mixed_juice,
                iu_clear_juice = Model.iu_clear_juice,
                live_steam_generation = Model.live_steam_generation,
                live_steam_consumption = Model.live_steam_consumption,
                power_turbines = Model.power_turbines,
                bleeding_in_process = Model.bleeding_in_process,
                bleeding_acf = Model.bleeding_acf,
                ata3_cogen = Model.ata3_cogen,
                d_sulpher_heating = Model.d_sulpher_heating,
                drain_pipe_loss = Model.drain_pipe_loss,
                exhaust_steam_generation = Model.exhaust_steam_generation,
                exhaust_steam_consumption = Model.exhaust_steam_consumption,
                steam_per_ton_cane = Model.steam_per_ton_cane,
                steam_per_qtl_sugar = Model.steam_per_qtl_sugar,
                power_from_sugar = Model.power_from_sugar,
                power_from_grid = Model.power_from_grid,
                power_export_grid = Model.power_export_grid,
                power_dg_set = Model.power_dg_set,
                power_import_cogen = Model.power_import_cogen,
                home_consumption = Model.home_consumption,
                power_generation_from_coGen = Model.power_generation_from_coGen,

                total_power = Model.total_power,
                power_per_ton_cane = Model.power_per_ton_cane,
                power_per_qtl_sugar = Model.power_per_qtl_sugar,
                om_p_index = Model.om_p_index,
                om_pry_ext = Model.om_pry_ext,
                steam_percent_cane = Model.steam_percent_cane,
                cane_farm = Model.cane_farm,
                boiler_water = Model.boiler_water,
                daily_hour_worked = 24,
                crtd_dt = DateTime.Now,
                crtd_by = Session["UserCode"].ToString(),

               
                gross_biss_sugar = Model.gross_biss_sugar,
                gross_scrap_sugar = Model.gross_scrap_sugar,
                gross_moist_sugar = Model.gross_moist_sugar,
                gross_raw_sugar = Model.gross_raw_sugar,
                gross_other_sugar = Model.gross_other_sugar,
                water_pan_a = Model.water_pan_a,
                water_pan_b = Model.water_pan_b,
                water_pan_c = Model.water_pan_c,
                OverTimeEnggReplacement = Model.OverTimeEnggReplacement,
                OverTimeEnggExtra = Model.OverTimeEnggExtra,
                OverTimeMfgReplacement = Model.OverTimeMfgReplacement,
                OverTimeMfgExtra = Model.OverTimeMfgExtra,
                MolassesSentOutCHeavy = Model.MolassesSentOutCHeavy,
                MolassesSentOutBHeavy = Model.MolassesSentOutBHeavy,
                StoreProcessChemicalQuantity = Model.StoreProcessChemicalQuantity,
                StoreProcessChemicalAmount = Model.StoreProcessChemicalAmount,
                StoreProcessChemicalPerBagRate = Model.StoreProcessChemicalPerBagRate,
                StoreProcessChemicalPerTonCaneRate = Model.StoreProcessChemicalPerTonCaneRate,
                StoreBoilerChemicalQuantity = Model.StoreBoilerChemicalQuantity,
                StoreBoilerChemicalAmount = Model.StoreBoilerChemicalAmount,
                StoreBoilerChemicalPerBagRate = Model.StoreBoilerChemicalPerBagRate,
                StoreBoilerChemicalPerTonCaneRate = Model.StoreBoilerChemicalPerTonCaneRate,
                StoreGreaseOilQuantity = Model.StoreGreaseOilQuantity,
                StoreGreaseOilAmount = Model.StoreGreaseOilAmount,
                StoreGreaseOilPerBagRate = Model.StoreGreaseOilPerBagRate,
                StoreGreaseOilPerTonCaneRate = Model.StoreGreaseOilPerTonCaneRate
                ,
                QuintalSyrupDiversion = Model.QuintalSyrupDiversion
                ,
                SugarPol = Model.SugarPol

                ,
                CHeavyBrix = Model.CHeavyBrix
                ,
                CHeavyPol = Model.CHeavyPol
                ,
                CHeavyPurity = Model.CHeavyPol > 0 ? Math.Round(((Model.CHeavyPol * 100) / Model.CHeavyBrix), 2) : 0
                ,
                RawSugarGainDate = Model.RawSugarGainDate
                ,
                MaterialSentOut = Model.MaterialSentOut
                ,
                SugarProduced = Model.SugarProduced
                , 
                PowerExportFromDistillery = Model.PowerExportFromDistillery
                ,
                PowerExportFromCogen = Model.PowerExportFromCogen
                ,
                PowerToDitillery = Model.PowerToDitillery,

                AlcoholFromCaneSyrup = Model.AlcoholFromCaneSyrup,
                AbsoluteAlcoholFromSyrupDayRecovery = Model.AbsoluteAlcoholFromSyrupDayRecovery,
                AbsoluteAlcoholFromSyrupToDateRecovery = Model.AbsoluteAlcoholFromSyrupToDateRecovery,
                RectifiedSpirit = Model.RectifiedSpirit,
                RectifiedSpiritDayRecover = Model.RectifiedSpiritDayRecover,
                RectifiedSpiritToDateRecovery = Model.RectifiedSpiritToDateRecovery,
                Ethanol = Model.Ethanol,
                EthanolDayRecovery = Model.EthanolDayRecovery,
                EthanolToDateRecovery = Model.EthanolToDateRecovery,
                AbsoluteAlcoholFromBHeavy = Model.AbsoluteAlcoholFromBHeavy,
                AbsoluteAlcoholBHeavyDayRecovery = Model.AbsoluteAlcoholBHeavyDayRecovery,
                AbsoluteAlcoholBHeavyToDateRecovery = Model.AbsoluteAlcoholBHeavyToDateRecovery,
                AlcoholFromCHeavy = Model.AlcoholFromCHeavy,
                AbsoluteAlcoholCHeavyDayRecovery = Model.AbsoluteAlcoholCHeavyDayRecovery,
                AbsoluteAlcoholCHeavyToDateRecovery = Model.AbsoluteAlcoholCHeavyToDateRecovery,
                AbsoluteAlcohol = Model.AbsoluteAlcohol,
                AbsoluteAlcoholDayRecovery = Model.AbsoluteAlcoholDayRecovery,
                AbsoluteAlcoholToDateRecovery = Model.AbsoluteAlcoholToDateRecovery
                ,
                mill_house_trace = Model.mill_house_trace
                ,
                mill_house_ph = Model.mill_house_ph
                ,
                mill_house_pol = Model.mill_house_pol
                ,
                under_oliver_filter_trace = Model.under_oliver_filter_trace
                ,
                under_oliver_filter_pH = Model.under_oliver_filter_ph
                ,
                under_oliver_filter_pol = Model.under_oliver_filter_pol
                ,
                c_heavy_final_molasses_trs = Model.c_heavy_final_molasses_trs
                ,
                c_heavy_final_molasses_rs = Model.c_heavy_final_molasses_rs
                ,
                syrup_trs = Model.syrup_trs
                ,
                syrup_rs = Model.syrup_rs
                , power_export_from_sugar = Model.power_export_from_sugar
                , power_generate_from_sugar = Model.power_generate_from_sugar
                , refinery_water_consumption = Model.refinery_water_consumption
                , fiberized_pol_in_cane = Model.fiberized_pol_in_cane
                , dextran_primary_juice = Model.dextran_primary_juice
                , dextran_mixed_juice = Model.dextran_mixed_juice
                , AshSold = Model.AshSold
                , Remarks = Model.Remarks
            };
            bool result = Repository.AddDailyAnalysis(Entity);
            if (!result)
            {
                return View("Error");
            }
                return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("create")]
        public ActionResult Edit(DailyAnalysisModel Model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                List<Errors> errors = new List<Errors>();
                errorViewModel.ErrorTitle = "Errior while Modifying Stoppage Data";
                //foreach (var x in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList())
                foreach(var x in ModelState.Values.SelectMany(e=>e.Errors).Select(y=>y.ErrorMessage))
                {
                    Errors t = new Errors()
                    {
                        ErrorMessage = x.ToString()
                    };
                    errors.Add(t);
                }
                errorViewModel.ErrorMessage = errors;
                TempData["Model"] = errorViewModel;
                return RedirectToAction("ModelError","Error");
            }
            SetUnitDefaultValues(); //setting all default values of base unit
            DailyAnalys temp = Repository.GetDailyAnalysisDetailsByDate(Convert.ToInt16(TempData["BaseUnitCode"]), ViewBag.CrushingSeason, ViewBag.EntryDate);

            temp.unit_code = Convert.ToInt16(TempData["BaseUnitCode"]);
            temp.season_code = ViewBag.CrushingSeason;
            temp.entry_date = ViewBag.EntryDate;
            temp.trans_time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            temp.cane_early = Model.cane_early;
            temp.cane_general = Model.cane_general;
            temp.cane_rejected = Model.cane_rejected;
            temp.cane_burnt = Model.cane_burnt;
            temp.cane_gate = Model.cane_gate;
            temp.cane_centre = Model.cane_centre;
            temp.cane_crushed = Model.cane_crushed;
            temp.total_juice = Model.total_juice;
            temp.sp_gravity = Model.sp_gravity;
            temp.total_water = Model.total_water;
            temp.press_cake = Model.press_cake;
            temp.molasses_sent_out = Model.molasses_sent_out;
            temp.biss_sugar = Model.biss_sugar;
            temp.biss_molasses = Model.biss_molasses;
            temp.scrap_sugar = Model.scrap_sugar;
            temp.scrap_molasses = Model.scrap_molasses;
            temp.moist_sugar = Model.moist_sugar;
            temp.moist_molasses = Model.moist_molasses;
            temp.raw_sugar = Model.raw_sugar;
            temp.raw_molasses = Model.raw_molasses;
            temp.other_sugar = Model.other_sugar;
            temp.other_molasses = Model.other_molasses;
            temp.store_sulpher = Model.store_sulpher;
            temp.store_phosphoric = Model.store_phosphoric;
            temp.store_lime = Model.store_lime;
            temp.store_viscosity_reducer = Model.store_viscosity_reducer;
            temp.store_biocide = Model.store_biocide;
            temp.store_color_reducer = Model.store_color_reducer;
            temp.store_megnafloe = Model.store_megnafloe;
            temp.store_lub_oil = Model.store_lub_oil;
            temp.store_lub_grease = Model.store_lub_grease;
            temp.store_boiler_chemical = Model.store_boiler_chemical;
            temp.store_washing_soda = Model.store_washing_soda;
            temp.store_hydrolic_acid = Model.store_hydrolic_acid;
            temp.store_de_scaling_chemical = Model.store_de_scaling_chemical;
            temp.store_seed_slurry = Model.store_seed_slurry;
            temp.store_anti_fomer = Model.store_anti_fomer;
            temp.store_chemical_for_brs_cleaning = Model.store_chemical_for_brs_cleaning;
            temp.icumsa_l31 = Model.icumsa_l31;
            temp.icumsa_l30 = Model.icumsa_l30;
            temp.icumsa_m31 = Model.icumsa_m31;
            temp.icumsa_m30 = Model.icumsa_m30;
            temp.icumsa_s31 = Model.icumsa_s31;
            temp.icumsa_s30 = Model.icumsa_s30;
            temp.icumsa_raw_sugar = Model.icumsa_raw_sugar;
            temp.foreign_matter_l31 = Model.foreign_matter_l31;
            temp.foreign_matter_l30 = Model.foreign_matter_l30;
            temp.foreign_matter_m31 = Model.foreign_matter_m31;
            temp.foreign_matter_m30 = Model.foreign_matter_m30;
            temp.foreign_matter_s31 = Model.foreign_matter_s31;
            temp.foreign_matter_s30 = Model.foreign_matter_s30;
            temp.foreign_matter_raw_sguar = Model.foreign_matter_raw_sugar;
            temp.retention_l31 = Model.retention_l31;
            temp.retention_l30 = Model.retention_l30;
            temp.retention_m31 = Model.retention_m31;
            temp.retention_m30 = Model.retention_m30;
            temp.retention_s31 = Model.retention_s31;
            temp.retention_s30 = Model.retention_s30;
            temp.retention_raw_sugar = Model.retention_raw_sugar;
            temp.moisture_l31 = Model.moisture_l31;
            temp.moisture_l30 = Model.moisture_l30;
            temp.moisture_m31 = Model.moisture_m31;
            temp.moisture_m30 = Model.moisture_m30;
            temp.moisture_s31 = Model.moisture_s31;
            temp.moisture_s30 = Model.moisture_s30;
            temp.moisture_raw_sugar = Model.moisture_raw_sugar;
            temp.etp_ph = Model.etp_ph;
            temp.etp_tss = Model.etp_tss;
            temp.etp_cod = Model.etp_cod;
            temp.etp_bod = Model.etp_bod;
            temp.etp_water_flow = Model.etp_water_flow;
            temp.etp_water_consumption_at_plant = Model.etp_water_consumption_at_plant;
            temp.calcium_mixed_juice = Model.calcium_mixed_juice;
            temp.calcium_clear_juice = Model.calcium_clear_juice;
            temp.phosphate_mixed_juice = Model.phosphate_mixed_juice;
            temp.phosphate_clear_juice = Model.phosphate_clear_juice;
            temp.rs_primary_juice = Model.rs_primary_juice;
            temp.rs_mixed_juice = Model.rs_mixed_juice;
            temp.rs_clear_juice = Model.rs_clear_juice;
            temp.unknown_losses = Model.unknown_losses;
            temp.dirt_correction = Model.dirt_correction;
            temp.total_operating_tube_well = Model.total_operating_tube_well;
            temp.exhaust_condensate_recovery = Model.exhaust_condensate_recovery;
            temp.T_c_massecuite_pan = Model.T_c_massecuite_pan;
            temp.ph_injection_inlet = Model.ph_injection_inlet;
            temp.ph_injection_outlet = Model.ph_injection_outlet;
            temp.Temp_Injection_Water_Inlet = Model.Temp_Injection_Water_Inlet;
            temp.Temp_Injection_Water_Outlet = Model.Temp_Injection_Water_Outlet;
            temp.average_vaccume_pan = Model.average_vaccume_pan;
            temp.average_vaccume_evap = Model.average_vaccume_evap;
            temp.Exhaust_steam_press_lp = Model.Exhaust_steam_press_lp;
            temp.Exhaust_steam_press_hp = Model.Exhaust_steam_press_hp;
            temp.boiler_steam_press_lp = Model.boiler_steam_press_lp;
            temp.boiler_steam_press_hp = Model.boiler_steam_press_hp;
            temp.ph_boiler_feed_water = Model.ph_boiler_feed_water;
            temp.Temp_Boiler_feed_water = Model.Temp_Boiler_feed_water;
            temp.bagasse_baed = Model.bagasse_baed;
            temp.filter_water = Model.filter_water;
            temp.pan_water = Model.pan_water;
            temp.cf_water = Model.cf_water;
            temp.bagasse_sold = Model.bagasse_sold;
            temp.bagasse_stock = Model.bagasse_stock;
            temp.bagasse_consumed_qtl = Model.bagasse_consumed_qtl;
            temp.bagacillo_to_boiling_house = Model.bagacillo_to_boiling_house;
            temp.Steam_Fuel_Ratio = Model.Steam_Fuel_Ratio;
            temp.AshSold = Model.AshSold;
            temp.nm_p_index = Model.nm_p_index;
            temp.nm_pry_ext = Model.nm_pry_ext;
            temp.b_heavy_final_molasses_trs = Model.b_heavy_final_molasses_trs;
            temp.b_heavy_final_molasses_rs = Model.b_heavy_final_molasses_rs;
            temp.temp_max = Model.temp_max;
            temp.temp_min = Model.temp_min;
            temp.humidity = Model.humidity;
            temp.rain_fall = Model.rain_fall;
            temp.iu_primary_juice = Model.iu_primary_juice;
            temp.iu_mixed_juice = Model.iu_mixed_juice;
            temp.iu_clear_juice = Model.iu_clear_juice;
            temp.live_steam_generation = Model.live_steam_generation;
            temp.live_steam_consumption = Model.live_steam_consumption;
            temp.power_turbines = Model.power_turbines;
            temp.bleeding_in_process = Model.bleeding_in_process;
            temp.bleeding_acf = Model.bleeding_acf;
            temp.ata3_cogen = Model.ata3_cogen;
            temp.d_sulpher_heating = Model.d_sulpher_heating;
            temp.drain_pipe_loss = Model.drain_pipe_loss;
            temp.exhaust_steam_generation = Model.exhaust_steam_generation;
            temp.exhaust_steam_consumption = Model.exhaust_steam_consumption;
            temp.steam_per_ton_cane = Model.steam_per_ton_cane;
            temp.steam_per_qtl_sugar = Model.steam_per_qtl_sugar;
            temp.power_from_sugar = Model.power_from_sugar;
            temp.power_from_grid = Model.power_from_grid;
            //temp.power_export_grid = Model.power_export_grid;
            temp.power_export_grid = Convert.ToDecimal(Model.PowerExportFromDistillery) + Convert.ToDecimal(Model.PowerExportFromCogen);
            temp.power_dg_set = Model.power_dg_set;
            temp.power_import_cogen = Model.power_import_cogen;
            temp.home_consumption = Model.home_consumption;
            temp.power_generation_from_coGen = Model.power_generation_from_coGen;
            temp.total_power = Model.total_power;
            temp.power_per_ton_cane = Model.power_per_ton_cane;
            temp.power_per_qtl_sugar = Model.power_per_qtl_sugar;
            temp.om_p_index = Model.om_p_index;
            temp.om_pry_ext = Model.om_pry_ext;
            temp.steam_percent_cane = Model.steam_percent_cane;
            temp.cane_farm = Model.cane_farm;
            temp.boiler_water = Model.boiler_water;
            temp.daily_hour_worked = 24;
            temp.crtd_dt = Convert.ToDateTime(TempData["Crtd_dt"]);
            temp.crtd_by = TempData["Crtd_By"].ToString();
            temp.updt_dt = DateTime.Now;
            temp.updt_by = Session["UserCode"].ToString();
            temp.PowerToDitillery = Model.PowerToDitillery;
            
            temp.gross_biss_sugar = Model.gross_biss_sugar;
            temp.gross_scrap_sugar = Model.gross_scrap_sugar;
            temp.gross_moist_sugar = Model.gross_moist_sugar;
            temp.gross_raw_sugar = Model.gross_raw_sugar;
            temp.gross_other_sugar = Model.gross_other_sugar;
            temp.water_pan_a = Model.water_pan_a;
            temp.water_pan_b = Model.water_pan_b;
            temp.water_pan_c = Model.water_pan_c;
            temp.OverTimeEnggReplacement = Model.OverTimeEnggReplacement;
            temp.OverTimeEnggExtra = Model.OverTimeEnggExtra;
            temp.OverTimeMfgReplacement = Model.OverTimeMfgReplacement;
            temp.OverTimeMfgExtra = Model.OverTimeMfgExtra;
            temp.MolassesSentOutBHeavy = Model.MolassesSentOutBHeavy;
            temp.MolassesSentOutCHeavy = Model.MolassesSentOutCHeavy;

            temp.StoreProcessChemicalQuantity = Model.StoreProcessChemicalQuantity;
            temp.StoreProcessChemicalAmount = Model.StoreProcessChemicalAmount;
            temp.StoreProcessChemicalPerBagRate = Model.StoreProcessChemicalPerBagRate;
            temp.StoreProcessChemicalPerTonCaneRate = Model.StoreProcessChemicalPerTonCaneRate;
            temp.StoreBoilerChemicalQuantity = Model.StoreBoilerChemicalQuantity;
            temp.StoreBoilerChemicalAmount = Model.StoreBoilerChemicalAmount;
            temp.StoreBoilerChemicalPerBagRate = Model.StoreBoilerChemicalPerBagRate;
            temp.StoreBoilerChemicalPerTonCaneRate = Model.StoreBoilerChemicalPerTonCaneRate;
            temp.StoreGreaseOilQuantity = Model.StoreGreaseOilQuantity;
            temp.StoreGreaseOilAmount = Model.StoreGreaseOilAmount;
            temp.StoreGreaseOilPerBagRate = Model.StoreGreaseOilPerBagRate;
            temp.StoreGreaseOilPerTonCaneRate = Model.StoreGreaseOilPerTonCaneRate;
            temp.QuintalSyrupDiversion = Model.QuintalSyrupDiversion;
            temp.SugarPol = Model.SugarPol;
            temp.CHeavyBrix = Model.CHeavyBrix;
            temp.CHeavyPol = Model.CHeavyPol;
            temp.CHeavyPurity = Model.CHeavyPol > 0 ? Math.Round(((Model.CHeavyPol * 100) / Model.CHeavyBrix), 2) : 0;
            temp.RawSugarGainDate = Model.RawSugarGainDate;
            temp.MaterialSentOut = Model.MaterialSentOut;
            temp.SugarProduced = Model.SugarProduced;
            temp.PowerExportFromDistillery = Model.PowerExportFromDistillery;
            temp.PowerExportFromCogen = Model.PowerExportFromCogen;
            

            temp.mill_house_trace = Model.mill_house_trace;
            temp.mill_house_ph = Model.mill_house_ph;
            temp.mill_house_pol = Model.mill_house_pol;
            temp.under_oliver_filter_trace = Model.under_oliver_filter_trace;
            temp.under_oliver_filter_pH = Model.under_oliver_filter_ph;
            temp.under_oliver_filter_pol = Model.under_oliver_filter_pol;
            temp.c_heavy_final_molasses_trs = Model.c_heavy_final_molasses_trs;
            temp.c_heavy_final_molasses_rs = Model.c_heavy_final_molasses_rs;
            temp.syrup_trs = Model.syrup_trs;
            temp.syrup_rs = Model.syrup_rs;
            temp.power_export_from_sugar = Model.power_export_from_sugar;
            temp.power_generate_from_sugar = Model.power_generate_from_sugar;
            
            temp.AlcoholFromCaneSyrup = Model.AlcoholFromCaneSyrup;
            temp.AbsoluteAlcoholFromSyrupDayRecovery = Model.AbsoluteAlcoholFromSyrupDayRecovery;
            temp.AbsoluteAlcoholFromSyrupToDateRecovery = Model.AbsoluteAlcoholFromSyrupToDateRecovery;
            temp.RectifiedSpirit = Model.RectifiedSpirit;
            temp.RectifiedSpiritDayRecover = Model.RectifiedSpiritDayRecover;
            temp.RectifiedSpiritToDateRecovery = Model.RectifiedSpiritToDateRecovery;
            temp.Ethanol = Model.Ethanol;
            temp.EthanolDayRecovery = Model.EthanolDayRecovery;
            temp.EthanolToDateRecovery = Model.EthanolToDateRecovery;
            temp.AbsoluteAlcoholFromBHeavy = Model.AbsoluteAlcoholFromBHeavy;
            temp.AbsoluteAlcoholBHeavyDayRecovery = Model.AbsoluteAlcoholBHeavyDayRecovery;
            temp.AbsoluteAlcoholBHeavyToDateRecovery = Model.AbsoluteAlcoholBHeavyToDateRecovery;
            temp.AlcoholFromCHeavy = Model.AlcoholFromCHeavy;
            temp.AbsoluteAlcoholCHeavyDayRecovery = Model.AbsoluteAlcoholCHeavyDayRecovery;
            temp.AbsoluteAlcoholCHeavyToDateRecovery = Model.AbsoluteAlcoholCHeavyToDateRecovery;
            temp.AbsoluteAlcohol = Model.AbsoluteAlcohol;
            temp.AbsoluteAlcoholDayRecovery = Model.AbsoluteAlcoholDayRecovery;
            temp.AbsoluteAlcoholToDateRecovery = Model.AbsoluteAlcoholToDateRecovery;
            temp.refinery_water_consumption = Model.refinery_water_consumption;
            temp.fiberized_pol_in_cane = Model.fiberized_pol_in_cane;
            temp.dextran_primary_juice = Model.dextran_primary_juice;
            temp.dextran_mixed_juice = Model.dextran_mixed_juice;
            temp.Remarks = Model.Remarks;

            bool result = Repository.EditDailyAnalysis(temp);
            if (!result)
            {
                
                return View("Error");
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// A function wich set some default values in viewbag so that we can use them 
        /// in other Action methods
        /// </summary>
        [NonAction]
        private void SetUnitDefaultValues()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Index", "Home");
            }
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            MasterStoppageTypeRepository stoppageRepository = new MasterStoppageTypeRepository();

            var UnitDefaultValues = UnitRepository.FindUnitByPk(Convert.ToInt16(Session["BaseUnitCode"]));

            TempData["BaseUnitCode"] = Session["BaseUnitCode"];
            ViewBag.UnitName = UnitDefaultValues.Name;
            ViewBag.EntryDate = UnitDefaultValues.EntryDate;
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
        }
    }
}