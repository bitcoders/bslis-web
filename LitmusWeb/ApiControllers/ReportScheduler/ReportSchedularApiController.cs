using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Repositories.ReportScheduler;
using DataAccess.CustomModels;
using LitmusWeb.Models.CustomModels;

namespace LitmusWeb.ApiControllers.ReportScheduler
{
    public class ReportSchedularApiController : ApiController
    {
        private ProcessedDatesForReportRepository _repo;
        public ReportSchedularApiController()
        {
                _repo = new ProcessedDatesForReportRepository();
        }
        [HttpPost]
        [Route("api/ReportSchedularApi/ApproveReportToSchedule")]
        [ActionName("ApproveReportToSchedule")]
        public HttpResponseMessage ApproveReportToSchedule(ApiParamaApproveReport param)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            try
            {
                
                
                   var result = _repo.FinalizeReportById(param.id, param.user_code);
                if(result != null)
                {
                    response.status_code = result.status_code;
                    response.status_message = result.status_message;
                    return Request.CreateResponse(HttpStatusCode.OK,response);
                }
                else
                {
                    response.status_code = (int)HttpStatusCode.NoContent;
                    response.status_message = "Report approval failed!";
                    return Request.CreateResponse(HttpStatusCode.NoContent,response);
                }
                
                
            }
            catch(Exception ex)
            {
                response.status_code = (int)HttpStatusCode.InternalServerError;
                response.status_message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
