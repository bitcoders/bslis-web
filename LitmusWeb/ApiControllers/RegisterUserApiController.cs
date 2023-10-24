using DataAccess;
using LitmusWeb.Models.CustomModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class RegisterUserApiController : ApiController
    {
        [HttpPost]
        [Route("api/RegisterUserApi/PasswordReset")]
        [ActionName(name: "PasswordReset")]
        public HttpResponseMessage PasswordReset([FromBody] ApiDoubleStringParam param)
        {
            ApiResponseModel response = new ApiResponseModel();
            if (param.FirstString == string.Empty || param.SecondString == string.Empty || param == null)
            {
                response.ErrorCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMessage = "Invalid request!";
            }

            /// first check either token exists in Password reset table or not, 
            /// also check token validity and IsActive flag.
            /// 
            PasswordReset pReset = new PasswordReset();
            RegisteredDevice rDevice = new RegisteredDevice();
            using (SugarLabEntities db = new SugarLabEntities())
            {
                pReset = db.PasswordResets.Where(x => x.Token == param.FirstString
                && DateTime.Now >= x.ValidFrom
                && DateTime.Now <= x.ValidTill
                && x.IsActive == true
                ).FirstOrDefault();
                // if token is valid, get user details based on it
                if (pReset != null)
                {

                    rDevice = db.RegisteredDevices.Where(x => x.UserCode == pReset.UserCode
                    && x.IsActive == true
                    && DateTime.Now >= x.ValidFrom
                    && DateTime.Now <= x.ValidTill
                    ).FirstOrDefault();

                    if (rDevice != null)
                    {
                        // update user password in 'RegisteredDevice' table and return true
                        try
                        {
                            rDevice.UserPassword = param.SecondString;
                            db.SaveChanges();

                            db.PasswordResets.Remove(pReset);
                            db.SaveChanges();
                            response.ErrorCode = (int)HttpStatusCode.OK;
                            response.ErrorMessage = "Password changed successfully!";
                        }
                        catch (Exception ex)
                        {
                            new Exception(ex.Message);
                            response.ErrorCode = (int)HttpStatusCode.InternalServerError;
                            response.ErrorMessage = "Error occured while resetting the password!";
                        }
                    }
                    else
                    {
                        response.ErrorCode = (int)HttpStatusCode.NotFound;
                        response.ErrorMessage = "Either user is not registed or is not active. Contact administrator!";
                    }
                }
                else
                {
                    response.ErrorCode = (int)HttpStatusCode.NotFound;
                    response.ErrorMessage = "The reset link is expired, re-send request using 'forgot password' option!";
                }
            }
            return Request.CreateResponse(response);
        }
    }
}
