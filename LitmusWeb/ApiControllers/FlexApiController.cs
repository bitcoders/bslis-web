using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class FlexApiController : ApiController
    {
        FlexRepository fRepo;
        WebApiHeader header;
        public FlexApiController()
        {
            fRepo = new FlexRepository();
        }


        [HttpPost]
        [ActionName(name: "GetFlexSubMasterByCode")]
        [Route("api/FlexApiController/GetFlexSubMasterByCode")]
        public HttpResponseMessage GetFlexSubMasterByCode([FromBody] ApiSingleIntegerParam param)
        {
            List<FlexSubMasterModel> model = new List<FlexSubMasterModel>();


            List<FlexSubMaster> f = new List<FlexSubMaster>();
            f = fRepo.GetFlexSubMasterByCode(param.IntegerParam);
            if (f.Count > 0)
            {
                foreach (FlexSubMaster x in f)
                {
                    FlexSubMasterModel temp = new FlexSubMasterModel()
                    {
                        Code = x.Code,
                        FlexMasterCode = x.FlexMasterCode,
                        Value = x.Value,
                        DataTypeOfValue = x.DataTypeOfValue,
                        IsActive = x.IsActive,
                        Description = x.Description
                    };
                    model.Add(temp);
                }

                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.OK,
                    statusMessage = "Success!"
                };
            }
            else
            {
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.NotFound,
                    statusMessage = "Sucess: No data exist!"
                };
            }



            var response = new { header, model };

            return Request.CreateResponse(response);
        }


        [HttpPost]
        [ActionName(name: "GetFlexSubMaster")]
        [Route("api/FlexApiController/GetFlexSubMaster")]
        public HttpResponseMessage GetFlexSubMaster()
        {
            List<FlexSubMasterModel> model = new List<FlexSubMasterModel>();


            List<FlexSubMaster> f = new List<FlexSubMaster>();
            f = fRepo.GetActiveFlexMasters();
            if (f.Count > 0)
            {
                foreach (FlexSubMaster x in f)
                {
                    FlexSubMasterModel temp = new FlexSubMasterModel()
                    {
                        Code = x.Code,
                        FlexMasterCode = x.FlexMasterCode,
                        Value = x.Value,
                        DataTypeOfValue = x.DataTypeOfValue,
                        IsActive = x.IsActive,
                        Description = x.Description
                    };
                    model.Add(temp);
                }

                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.OK,
                    statusMessage = "Success!"
                };
            }
            else
            {
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.NotFound,
                    statusMessage = "Sucess: No data exist!"
                };
            }



            var response = new { header, model };

            return Request.CreateResponse(response);
        }
    }
}
