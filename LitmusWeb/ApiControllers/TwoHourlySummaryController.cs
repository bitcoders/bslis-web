using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models.CustomModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class TwoHourlySummaryController : ApiController
    {
        TwoHourlyAnalysSummaryRepository Repository = new TwoHourlyAnalysSummaryRepository();
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate;

        [HttpPost]
        [ActionName(name: "GetSummary")]
        [Route("api/TwoHourlySummary")]
        public IHttpActionResult GetSummary([FromBody] ApiParamUnitCode apiParam)
        {
            List<func_two_hourly_transaction_summary_Result> summary = new List<func_two_hourly_transaction_summary_Result>();
            //return Repository.GetTwoHourlySummaryForDate(1, 20, DateTime.Parse("2019-08-24"));
            SetUnitDefaultValues(apiParam.unit_code);
            summary = Repository.GetTwoHourlySummaryForDateList(apiParam.unit_code, CrushingSeason, EntryDate);
            if (summary.Count == 0)
            {
                return Content(HttpStatusCode.NoContent, "No content");
            }
            else
            {
                return Ok(summary);
            }
        }

        [ActionName(name: "TwoHourlySummaryByHours")]
        public HttpResponseMessage GetTwoHourlySummaryByHours(int unitCode)
        {
            List<func_two_hourly_transaction_summary_for_hours_Result> results = new List<func_two_hourly_transaction_summary_for_hours_Result>();
            SetUnitDefaultValues(unitCode);
            var shiftA = new func_two_hourly_transaction_summary_for_hours_Result();
            var shiftB = new func_two_hourly_transaction_summary_for_hours_Result();
            var shiftC = new func_two_hourly_transaction_summary_for_hours_Result();

            //results = Repository.GetTwo_Hourly_Transaction_Summary_For_Hours_Results(unitCode, CrushingSeason, reportDate, startHour, endHour);
            shiftA = Repository.GetTwo_Hourly_Transaction_Summary_For_Hours_Results(unitCode, CrushingSeason, EntryDate, 10, 16);
            shiftB = Repository.GetTwo_Hourly_Transaction_Summary_For_Hours_Results(unitCode, CrushingSeason, EntryDate, 18, 24);
            shiftC = Repository.GetTwo_Hourly_Transaction_Summary_For_Hours_Results(unitCode, CrushingSeason, EntryDate, 2, 8);

            results.Add(shiftA);
            results.Add(shiftB);
            results.Add(shiftC);

            if (results.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(results));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
        }
        [System.Web.Mvc.NonAction]
        private void SetUnitDefaultValues(int id)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(id);
            BaseUnitCode = id;
            EntryDate = UnitDefaultValues.EntryDate;
            CrushingSeason = UnitDefaultValues.CrushingSeason;
        }
    }
}
