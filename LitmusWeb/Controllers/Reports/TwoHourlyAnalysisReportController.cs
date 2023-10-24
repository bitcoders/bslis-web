using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers.Reports
{
    public class TwoHourlyAnalysisReportController : Controller
    {

        private int crushingSeason;
        private DateTime entryDate;
        private int unitCode;
        public int CrushingSeason { get => crushingSeason; set => crushingSeason = value; }
        public DateTime EntryDate { get => entryDate; set => entryDate = value; }
        public int UnitCode { get => unitCode; set => unitCode = value; }

        // GET: TwoHourlyAnalysisReport
        [HttpGet]
        public ActionResult Index()
        {
            SetUnitDefaultValues();
            TwoHourlyAnalysisRepository reportRepository = new TwoHourlyAnalysisRepository();
            List<TwoHourlyAnalys> Entity = reportRepository.GetTwoHourlyAnalysis(UnitCode, CrushingSeason, EntryDate);
            List<TwoHourlyAnalysisModel> models = new List<TwoHourlyAnalysisModel>();

            if (Entity == null)
            {
                ViewBag.ErrorMessage = "No data found for the date.";
                return View();
            }
            foreach (var x in Entity)
            {
                TwoHourlyAnalysisModel tempModel = new TwoHourlyAnalysisModel()
                {
                    Unit_Code = x.Unit_Code,
                    season_code = x.season_code,
                    Entry_Date = x.Entry_Date,
                    entry_Time = x.entry_Time,
                    NM_Primary_Juice_Brix = x.NM_Primary_Juice_Brix,
                    NM_Primary_juice_pol = x.NM_Primary_juice_pol,
                    nm_primary_juice_purity = x.nm_primary_juice_purity,
                    nm_primary_juice_ph = x.nm_primary_juice_ph,
                    om_primary_juice_brix = x.om_primary_juice_brix,
                    om_primary_juice_purity = x.om_primary_juice_purity,
                    om_primary_juice_ph = x.om_primary_juice_ph,
                    nm_mixed_juice_brix = x.nm_mixed_juice_brix,
                    nm_mixed_juice_pol = x.nm_mixed_juice_pol,
                    nm_mixed_juice_ph = x.nm_mixed_juice_ph,
                    nm_mixed_juice_purity = x.nm_mixed_juice_purity,
                    om_mixed_juice_brix = x.om_mixed_juice_brix,
                    om_mixed_juice_pol = x.om_mixed_juice_pol,
                    om_mixed_juice_purity = x.om_mixed_juice_purity,
                    om_mixed_juice_ph = x.om_mixed_juice_ph,
                    nm_last_juice_brix = x.nm_last_juice_brix,
                    nm_last_juice_pol = x.nm_last_juice_pol,
                    nm_last_juice_purity = x.nm_last_juice_purity,
                    om_last_juice_brix = x.om_last_juice_brix,
                    om_last_juice_pol = x.om_last_juice_pol,
                    om_last_juice_purity = x.om_last_juice_purity,
                    oliver_juice_brix = x.oliver_juice_brix,
                    oliver_juice_pol = x.oliver_juice_pol,
                    oliver_juice_purity = x.oliver_juice_purity,
                    oliver_juice_ph = x.oliver_juice_ph,
                    fcs_juice_brix = x.fcs_juice_brix,
                    fcs_juice_pol = x.fcs_juice_pol,
                    fcs_juice_ph = x.fcs_juice_ph,
                    fcs_juice_purity = x.fcs_juice_purity,
                    clear_juice_brix = x.clear_juice_brix,
                    clear_juice_pol = x.clear_juice_pol,
                    clear_juice_ph = x.clear_juice_ph,
                    clear_juice_purity = x.clear_juice_purity,
                    unsulphured_syrup_brix = x.unsulphured_syrup_brix,
                    unsulphured_syrup_pol = x.unsulphured_syrup_pol,
                    unsulphured_syrup_ph = x.unsulphured_syrup_ph,
                    unsulphured_syrup_purity = x.unsulphured_syrup_purity,
                    sulphured_syrup_brix = x.sulphured_syrup_brix,
                    sulphured_syrup_pol = x.sulphured_syrup_pol,
                    sulphured_syrup_ph = x.sulphured_syrup_ph,
                    sulphured_syrup_purity = x.sulphured_syrup_purity,
                    final_molasses_brix = x.final_molasses_brix,
                    final_molasses_pol = x.final_molasses_pol,
                    final_molasses_purity = x.final_molasses_purity,
                    final_molasses_temp = x.final_molasses_temp,
                    final_molasses_tanks = x.final_molasses_tanks,
                    nm_bagasse_pol = x.nm_bagasse_pol,
                    nm_bagasse_moisture = x.nm_bagasse_moisture,
                    om_bagasse_pol = x.om_bagasse_pol,
                    om_bagasse_moisture = x.om_bagasse_moisture,
                    pol_pressure_cake_sample1 = x.pol_pressure_cake_sample1,
                    pol_pressure_cake_sample2 = x.pol_pressure_cake_sample2,
                    pol_pressure_cake_sample3 = x.pol_pressure_cake_sample3,
                    pol_pressure_cake_sample4 = x.pol_pressure_cake_sample4,
                    pol_pressure_cake_sample5 = x.pol_pressure_cake_sample5,
                    pol_pressure_cake_sample6 = x.pol_pressure_cake_sample6,
                    pol_pressure_cake_average = x.pol_pressure_cake_average,
                    pol_pressure_cake_moisture = x.pol_pressure_cake_moisture,
                    pol_pressure_cake_composite = x.pol_pressure_cake_composite,
                    crtd_dt = x.crtd_dt,
                    crtd_by = x.crtd_by,

                };
                models.Add(tempModel);
            }
            return View(models);
        }
        public void SetUnitDefaultValues()
        {
            if (Session["BaseUnitCode"] == null)
            {
                RedirectToLoginPage();
            }
            UnitCode = Convert.ToInt16(Session["BaseUnitCode"]);
            MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
            var MasterUnit = masterUnitRepository.FindUnitByPk(unitCode);
            CrushingSeason = MasterUnit.CrushingSeason;
            EntryDate = MasterUnit.EntryDate;
        }
        public ActionResult RedirectToLoginPage()
        {
            return RedirectToAction("Home", "Index");
        }
    }


}