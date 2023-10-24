using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models.CustomModels;
using System;
using System.Net.Http;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class DailyReportApiController : ApiController
    {

        private readonly CommonRepository _commonReportRepo;
        public DailyReportApiController()
        {
            _commonReportRepo = new CommonRepository();
        }

        [HttpPost]
        [Route("api/DailyReportApi/getProcessedDateRange")]
        public HttpResponseMessage getProcessedDateRange([FromBody] ApiParameterModel param)
        {
            try
            {
                var minDate = _commonReportRepo.getMinProcessedDate(param.unit_code, param.season_code);
                var maxDate = _commonReportRepo.getMaxProcessedDate(param.unit_code, param.season_code);

                DailyReportsApiResponse response = new DailyReportsApiResponse()
                {
                    minDate = minDate.Date,
                    maxDate = maxDate.Date
                };
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public class DailyReportsApiResponse
    {
        public DateTime minDate { get; set; }
        public DateTime maxDate { get; set; }
    }
}