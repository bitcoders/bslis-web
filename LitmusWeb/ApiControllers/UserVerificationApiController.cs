using DataAccess.Repositories;
using LitmusWeb.Models.CustomModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace LitmusWeb.ApiControllers
{
    public class UserVerificationApiController : ApiController
    {
        [HttpPost]
        [Route("api/UserVerificationApi/VerifyUserByToken")]
        [ActionName(name: "VerifyUserByToken")]

        public HttpResponseMessage VerifyUserByToken([FromBody] ApiSingleStringParam param)
        {
            ApiResponseModel status = new ApiResponseModel();
            if (param == null)
            {
                status.ErrorCode = (int)HttpStatusCode.BadRequest;
                status.ErrorMessage = "Invalid input!";
            }
            else
            {
                UserVerificationRepository uRepo = new UserVerificationRepository();
                if (uRepo.VerifyUserByToken(param.StringParam) == true)
                {
                    status.ErrorCode = (int)HttpStatusCode.OK;
                    status.ErrorMessage = "Email verified sucessfully!";
                }
                else
                {
                    status.ErrorCode = (int)HttpStatusCode.Forbidden;
                    status.ErrorMessage = "Email verification failed!";
                }
            }
            var response = new { status };
            return Request.CreateResponse(response);
        }
    }
}
