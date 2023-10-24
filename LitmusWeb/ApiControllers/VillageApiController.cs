using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class VillageApiController : ApiController
    {
        VillageRepository vRepo;
        WebApiHeader header;
        public VillageApiController()
        {
            vRepo = new VillageRepository();
        }

        [HttpPost]
        [ActionName(name: "GetVillageByCode")]
        [Route("api/VillageApi/GetVillageByCode")]
        public HttpResponseMessage GetVillageByCode([FromBody] VillageApiParams param)
        {
            VillageModel model = new VillageModel();
            Village v = new Village();
            if (param != null)
            {

                v = vRepo.GetVillageByCode(param.unitCode, param.villageCode);
                if (v != null)
                {
                    header = new WebApiHeader
                    {
                        statusCode = HttpStatusCode.OK,
                        statusMessage = "Success: village found."
                    };
                    model.Id = v.Id;
                    model.UnitCode = v.UnitCode;
                    model.Code = v.Code;
                    model.Name = v.Name;
                    model.IsActive = v.IsActive;
                }
                else
                {
                    header = new WebApiHeader
                    {
                        statusCode = HttpStatusCode.NoContent,
                        statusMessage = "Failed: village code does not exist."
                    };
                }
            }
            var response = new { header, model };
            return Request.CreateResponse(response);
        }
    }
}
