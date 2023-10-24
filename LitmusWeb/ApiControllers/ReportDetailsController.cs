using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class ReportDetailsController : ApiController
    {
        readonly ReportDetailsRepository reportDetailsRepository = new ReportDetailsRepository();
        [HttpPost]
        [Route("api/ReportDetails/GetReportDetailsByCode")]

        public HttpResponseMessage GetReportDetailsByCode([FromBody] ApiReportDetails param)
        {
            var result = reportDetailsRepository.GetReportDetails(param.workingCode);
            int statusCode = 200;

            if (result != null)
            {
                statusCode = (int)HttpStatusCode.OK;
                var response = new { statusCode, result };
                return Request.CreateResponse(response);
            }
            else
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                var response = new { statusCode };
                return Request.CreateResponse(response);
            }
        }
        [HttpPost]
        [Route("api/ReportDetails/PostReportDetailsByCode")]

        public HttpResponseMessage PostReportDetails([FromBody] ReportDetailsModel parameter)
        {
            ReportDetail entity = new ReportDetail()
            {
                Code = parameter.Code,
                Name = parameter.Name,
                Description = parameter.Description,
                ReportSchemaCode = parameter.ReportSchemaCode

            };
            if (reportDetailsRepository.EditReportDetail(entity))
            {
                return Request.CreateResponse((int)HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse((int)HttpStatusCode.Forbidden);
            }
        }
    }

}
