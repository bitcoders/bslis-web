using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using DataAccess.Repositories.AnalysisRepositories;
using DataAccess.CustomModels;
using LitmusWeb.Models.CustomModels;

namespace LitmusWeb.ApiControllers
{
    public class HourlyAnalysesAPIController : ApiController
    {
        private HourlyAnalysisRepository _repo;
        public HourlyAnalysesAPIController()
        {
             _repo = new HourlyAnalysisRepository();
        }
        [HttpPost]  
        [Route("api/HourlyAnalyses/Delete")]
        [ActionName("Delete")]
        //public HttpResponseMessage Delete([FromBody] int unit_code, string user_code,  int line_id)
        //{
        //    try
        //    {
        //       var result =  _repo.DeleteHourlyAnalysis(unit_code, user_code, line_id);
        //        if(result.success)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { Success = true, StatusCode = HttpStatusCode.OK, Message=result.message });
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { Success = false, StatusCode = HttpStatusCode.OK, Message = result.message});
        //        }
        //    }
        //    catch(Exception ex)
        //    { 
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, new  ApiResponse { Success = false, StatusCode=HttpStatusCode.InternalServerError, Message=ex.Message });
        //    }
        //}
        public HttpResponseMessage Delete()
        {
            try
            {
                int unit_code = int.Parse(Request.Headers.GetValues("unit_code").FirstOrDefault());
                string user_code = Request.Headers.GetValues("user_code").FirstOrDefault();
                int line_id = int.Parse(Request.Headers.GetValues("line_id").FirstOrDefault());

                var result = _repo.DeleteHourlyAnalysis(unit_code, user_code, line_id);
                if (result.success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { Success = true, StatusCode = HttpStatusCode.OK, Message = result.message });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { Success = false, StatusCode = HttpStatusCode.OK, Message = result.message });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiResponse { Success = false, StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
