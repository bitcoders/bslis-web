using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
namespace LitmusWeb.ApiControllers
{
    public class DashBoardApiController : ApiController
    {
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate, ProcessDate, CrushingStartDate;


        [System.Web.Mvc.NonAction]
        private void SetUnitDefaultValues(int unit_code)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(unit_code);
            BaseUnitCode = unit_code;
            EntryDate = UnitDefaultValues.EntryDate;
            ProcessDate = UnitDefaultValues.ProcessDate;
            CrushingSeason = UnitDefaultValues.CrushingSeason;
            CrushingStartDate = UnitDefaultValues.CrushingStartDate;
        }

        [HttpPost]
        [ActionName(name: "GetDashBoardAlers")]
        [Route("api/DashBoardApi/GetDashBoardAlers")]
        public async Task<HttpResponseMessage> GetDashboardCriticalAlerts([FromBody] ApiParamUnitCode param)
        {
            SetUnitDefaultValues(param.unit_code);
            HourlyAnalysisRepository hourlyAnalysisRepository = new HourlyAnalysisRepository();
            HourlyAnalys hourlyAnalysEntity = new HourlyAnalys();
            hourlyAnalysEntity = await hourlyAnalysisRepository.GetLatestHourlyAnalysesDetails(param.unit_code, CrushingSeason);
            HourlyAnalysisModel hourlyAnalysisModel = new HourlyAnalysisModel()
            {
                id = hourlyAnalysEntity.id,
                unit_code = hourlyAnalysEntity.unit_code,
                season_code = hourlyAnalysEntity.season_code,
                entry_Date = hourlyAnalysEntity.entry_Date,
                entry_time = hourlyAnalysEntity.entry_time,
                new_mill_juice = hourlyAnalysEntity.new_mill_juice,
                old_mill_juice = hourlyAnalysEntity.old_mill_juice,
                juice_total = hourlyAnalysEntity.juice_total,
                new_mill_water = hourlyAnalysEntity.new_mill_water,
                old_mill_water = hourlyAnalysEntity.old_mill_water,
                sugar_bags_L31 = hourlyAnalysEntity.sugar_bags_L31,
                sugar_bags_L30 = hourlyAnalysEntity.sugar_bags_L30,
                sugar_bags_L_total = hourlyAnalysEntity.sugar_bags_L_total,
                sugar_bags_M30 = hourlyAnalysEntity.sugar_bags_M30,
                sugar_bags_M31 = hourlyAnalysEntity.sugar_bags_M31,
                sugar_bags_M_total = hourlyAnalysEntity.sugar_bags_M_total,
                sugar_bags_S30 = hourlyAnalysEntity.sugar_bags_S30,
                sugar_bags_S31 = hourlyAnalysEntity.sugar_bags_S31,
                sugar_bags_S_total = hourlyAnalysEntity.sugar_bags_S_total,
                sugar_Biss = hourlyAnalysEntity.sugar_Biss,
                sugar_raw = hourlyAnalysEntity.sugar_raw,
                sugar_bags_total = hourlyAnalysEntity.sugar_bags_total,
                cooling_trace = hourlyAnalysEntity.cooling_trace,
                cooling_pol = hourlyAnalysEntity.cooling_pol,
                cooling_ph = hourlyAnalysEntity.cooling_ph,
                crtd_dt = hourlyAnalysEntity.crtd_dt,
                crtd_by = hourlyAnalysEntity.crtd_by,
                standing_truck = hourlyAnalysEntity.standing_truck,
                standing_trippler = hourlyAnalysEntity.standing_trippler,
                standing_trolley = hourlyAnalysEntity.standing_trolley,
                standing_cart = hourlyAnalysEntity.standing_cart,
                un_crushed_cane = hourlyAnalysEntity.un_crushed_cane,
                crushed_cane = hourlyAnalysEntity.crushed_cane
            };

            string alertMessage = "";
            if (EntryDate < hourlyAnalysisModel.entry_Date)
            {
                alertMessage = "It looks like selected Unit is uploading or modifying Analyses data of Previous Days!";
                var genericData = new { alertMessage, hourlyAnalysisModel };
                return Request.CreateResponse(HttpStatusCode.OK, genericData);
            }
            var x = new { alertMessage, hourlyAnalysisModel };
            return Request.CreateResponse(HttpStatusCode.OK, x);
        }

    }
}
