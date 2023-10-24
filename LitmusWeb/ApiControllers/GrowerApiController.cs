using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class GrowerApiController : ApiController
    {
        GrowerRepository gRepo;
        WebApiHeader header;
        public GrowerApiController()
        {
            gRepo = new GrowerRepository();
        }

        [HttpPost]
        [ActionName(name: "GetGrowerDetailsByCode")]
        [Route("api/GrowerApi/GetGrower")]
        public HttpResponseMessage PostGetGrowerByCode([FromBody] GrowerApiParams param)
        {
            GrowerModel model = new GrowerModel();
            if (param == null)
            {
                header = new WebApiHeader
                {
                    statusCode = HttpStatusCode.BadRequest,
                    statusMessage = "Invalid input"
                };
            }
            else
            {
                Grower g = new Grower();
                g = gRepo.GetGrowerByCode(param.unitCode, param.villageCode, param.growerCode);
                if (g != null)
                {
                    model.Code = g.Code;
                    model.Name = g.Name;
                    model.RelativeName = g.RelativeName;
                    model.MobileNo = g.MobileNo;
                    model.Uid = g.Uid;
                }
                header = new WebApiHeader
                {
                    statusCode = HttpStatusCode.Found,
                    statusMessage = "Sucess: grower found"
                };
            }
            var response = new { header, model };
            return Request.CreateResponse(response);
        }
    }
}
