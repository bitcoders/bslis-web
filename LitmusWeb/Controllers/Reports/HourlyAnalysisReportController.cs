using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace LitmusWeb.Controllers.Reports
{
    public class HourlyAnalysisReportController : Controller
    {
        // GET: HourlyAnalysisReport
        [HttpGet]
        [UnitBasedValuesFilter]
        public ActionResult Index()
        {
            HourlyAnalysisRepository Repository = new HourlyAnalysisRepository();
            List<HourlyAnalys> Entity = new List<HourlyAnalys>();
            int UnitCode = Convert.ToInt16(Session["BaseUnitCode"]);
            DateTime EntryDate = Convert.ToDateTime(ViewBag.EntryDate);
            int CrushingSeason = Convert.ToInt16(Session["CrushingSeason"]);
            Entity = Repository.GetHourlyAnalysisList(UnitCode, CrushingSeason, EntryDate);
            List<HourlyAnalysisModel> Model = new List<HourlyAnalysisModel>();
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
                    crushed_cane = item.crushed_cane
                };
                Model.Add(temp);
            }


            return View(Model);
        }
    }
}