using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class HourlySummaryController : ApiController
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
                EntryDate = UnitDefaultValues.EntryDate;
                CrushingSeason = UnitDefaultValues.CrushingSeason;
            }
        }

        [HttpPost]
        [ActionName(name: "GetSummary")]
        [Route("api/HourlySummary/GetSummary")]
        public  IHttpActionResult GetHourlyLastEntry([FromBody] ApiParamUnitCode apiParam)
        {
            SetUnitDefaultValues(apiParam.unit_code);
            HourlyAnalys summary = new HourlyAnalys();
            summary =  Repository.GetLastAnalysisDetailsForEntryDate(apiParam.unit_code, CrushingSeason, EntryDate);
            if (summary == null)
            {
                return  Content(HttpStatusCode.NoContent, "No Content");
            }
            return Ok(summary);
        }


        [ActionName(name: "GetHourlyDataForPeriod")]
        public IHttpActionResult GetHourlyDataForPeriod(int unit_code)
        {
            SetUnitDefaultValues(unit_code);
            List<func_hourly_data_for_period_Result> Result = new List<func_hourly_data_for_period_Result>();
            Result = Repository.GetHourlyAnalysisSummaryForPeriod(unit_code, CrushingSeason);
            if (Result == null)
            {
                return Content(HttpStatusCode.NoContent, "No Content");
            }
            return Ok(Result);
        }

        [HttpPost]
        [Route("api/HourlySummary/GetHourlyDataForDate")]
        [ActionName(name: "GetHourlyDataForDate")]
        public IHttpActionResult GetHourlyDataForDate([FromBody] ApiParamUnitCode param)
        {
            SetUnitDefaultValues(param.unit_code);
            List<func_hourly_data_for_period_Result> Result = new List<func_hourly_data_for_period_Result>();
            Result = Repository.GetHourlyAnalysisSummaryForPeriod(param.unit_code, CrushingSeason);

            if (Result.Count == 0)
            {

                return Content(HttpStatusCode.NoContent, "No Content");
            }
            var ResultForDate = Result.Where(x => x.entry_date == EntryDate && x.unit_code == param.unit_code).FirstOrDefault();
            if (ResultForDate == null)
            {
                return Content(HttpStatusCode.NoContent, "No Content");
            }
            return Ok(ResultForDate);
        }

        [HttpPost]
        [Route("api/HourlyAnalyses/GetSyrupDiversionFactor")]
        public IHttpActionResult GetSyrupDiversionFactor(ReportParamUnitSeasonDate param)
        {
            usp_cane_diversion_factor_Result result = new usp_cane_diversion_factor_Result();
            if(param != null)
            {
                result = Repository.GetSyrupDiversionCaneFactor(param.unitCode, param.seasonCode, param.reportate);
                var response = new { HttpStatusCode.OK, result };
                return Ok(response);
            }
            else
            {
                return Ok(new { HttpStatusCode.NotFound, result } );
            }
            

        }
    }
}
