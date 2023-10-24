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
    public class StoppageApiController : ApiController
    {
        HourlyAnalysisRepository Repository = new HourlyAnalysisRepository();
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate;
        [System.Web.Mvc.NonAction]
        private void SetUnitDefaultValues(int unit_code)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(unit_code);
            if (UnitDefaultValues != null)
            {
                BaseUnitCode = unit_code;
                EntryDate = UnitDefaultValues.EntryDate.AddDays(-1);
                CrushingSeason = UnitDefaultValues.CrushingSeason;
            }
        }

        [HttpPost]
        [Route("api/StoppageApi/PostStoppageSummary")]
        [ActionName("PostStoppageSummary")]
        public async Task<HttpResponseMessage> PostStoppageSummary([FromBody] ApiParamUnitCode apiParam)
        {
            SetUnitDefaultValues(apiParam.unit_code);
            StoppageRepository repository = new StoppageRepository();
            proc_stoppageSummaryForDate_Result StoppageSummary = new proc_stoppageSummaryForDate_Result();
            StoppageSummary = await Task.FromResult(repository.GetStoppageSummaryForDay(apiParam.unit_code, CrushingSeason, EntryDate));
            if (StoppageSummary == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }
            StoppageHeadWiseSummaryModel stoppageHeadWiseSummary = new StoppageHeadWiseSummaryModel()
            {
                unit_code = StoppageSummary.unit_code,
                report_date = StoppageSummary.report_date,
                season_code = StoppageSummary.season_code,
                NoCane = StoppageSummary.NoCane,
                Mechanical = StoppageSummary.Mechanical,
                CoGen = StoppageSummary.CoGen,
                GeneralCleaning = StoppageSummary.GeneralCleaning,
                Festival = StoppageSummary.Festival,
                Weather = StoppageSummary.Weather,
                Process = StoppageSummary.Process,
                Miscellaneous = StoppageSummary.Miscellaneous,
                Unknown = StoppageSummary.Unknown,
                TotalDuration = StoppageSummary.TotalDuration,
                nm_total_duration = StoppageSummary.nm_total_duration,
                om_total_duration = StoppageSummary.om_total_duration,
                NoCaneGrossDuration = StoppageSummary.NoCaneGrossDuration,
                MechanicalGrossDuration = StoppageSummary.MechanicalGrossDuration,
                CoGenGrossDuration = StoppageSummary.CoGenGrossDuration,
                GeneralCleaningGrossDuration = StoppageSummary.GeneralCleaningGrossDuration,
                FestivalGrossDuration = StoppageSummary.FestivalGrossDuration,
                WeatherGrossDuration = StoppageSummary.WeatherGrossDuration,
                ProcessGrossDuration = StoppageSummary.ProcessGrossDuration,
                MiscellaneousGrossDuration = StoppageSummary.MiscellaneousGrossDuration,
                UnknownGrossDuration = StoppageSummary.UnknownGrossDuration,
                TotalGrossDuration = StoppageSummary.TotalGrossDuration,
                nm_gross_duration = StoppageSummary.nm_gross_duration,
                om_gross_duration = StoppageSummary.om_gross_duration,
                actual_minutes_of_crushing = StoppageSummary.actual_minutes_of_crushing,
                actual_gross_minutes_of_crushing = StoppageSummary.actual_minutes_of_crushing,
                td_NoCane = StoppageSummary.td_NoCane,
                td_Mechanical = StoppageSummary.td_Mechanical,
                td_CoGen = StoppageSummary.td_CoGen,
                td_GeneralCleaning = StoppageSummary.td_GeneralCleaning,
                td_Festival = StoppageSummary.td_Festival,
                td_Weather = StoppageSummary.td_Weather,
                td_Process = StoppageSummary.td_Process,
                td_Miscellaneous = StoppageSummary.td_Miscellaneous,
                td_Unknown = StoppageSummary.td_Unknown,
                td_TotalDuration = StoppageSummary.td_TotalDuration,
                td_nm_total_duration = StoppageSummary.td_nm_total_duration,
                td_om_total_duration = StoppageSummary.td_om_total_duration,
                td_NoCaneGrossDuration = StoppageSummary.td_NoCaneGrossDuration,
                td_MechanicalGrossDuration = StoppageSummary.td_MechanicalGrossDuration,
                td_CoGenGrossDuration = StoppageSummary.td_CoGenGrossDuration,
                td_GeneralCleaningGrossDuration = StoppageSummary.td_GeneralCleaningGrossDuration,
                td_FestivalGrossDuration = StoppageSummary.td_FestivalGrossDuration,
                td_WeatherGrossDuration = StoppageSummary.td_WeatherGrossDuration,
                td_ProcessGrossDuration = StoppageSummary.td_ProcessGrossDuration,
                td_MiscellaneousGrossDuration = StoppageSummary.td_MiscellaneousGrossDuration,
                td_UnknownGrossDuration = StoppageSummary.td_UnknownGrossDuration,
                td_TotalGrossDuration = StoppageSummary.td_TotalGrossDuration,
                td_nm_gross_duration = StoppageSummary.td_nm_gross_duration,
                td_om_gross_duration = StoppageSummary.td_om_gross_duration,
                td_actual_minutes_of_crushing = StoppageSummary.td_actual_minutes_of_crushing,
                td_actual_gross_minutes_of_crushing = StoppageSummary.td_actual_gross_minutes_of_crushing
            };

            var GenericResponse = new { stoppageHeadWiseSummary };

            return Request.CreateResponse(HttpStatusCode.OK, stoppageHeadWiseSummary);
        }
        private string ConvertMinutesToHours(decimal minutes)
        {
            //int x = Convert.ToDateTime(minutes / 60).Hour;

            decimal hours = minutes / 60;
            int min = Convert.ToInt32(minutes % 60);
            return Math.Truncate(hours).ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0');
        }
    }
}
