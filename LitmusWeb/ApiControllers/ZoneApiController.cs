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
    public class ZoneApiController : ApiController
    {
        WebApiHeader header;
        ZoneRepository zRepo;
        public ZoneApiController()
        {
            zRepo = new ZoneRepository();

        }

        [HttpPost]
        [ActionName(name: "GetZones")]
        [Route("api/ZoneApi/GetZones")]
        public HttpResponseMessage GetZones([FromBody] ApiParamUnitCode param)
        {
            List<ZoneModel> model = new List<ZoneModel>();
            List<Zone> z = new List<Zone>();
            if (param != null)
            {
                z = zRepo.GetZoness(param.unit_code);
                if (z != null)
                {
                    foreach (Zone x in z)
                    {
                        ZoneModel temp = new ZoneModel()
                        {
                            Id = x.Id,
                            UnitCode = x.UnitCode,
                            Code = x.Code,
                            Name = x.Name,
                            Description = x.Description
                        };
                        model.Add(temp);
                    }
                }
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.OK,
                    statusMessage = "Success: zone found."
                };
            }
            else
            {
                header = new WebApiHeader
                {
                    statusCode = HttpStatusCode.NoContent,
                    statusMessage = "Failed: No zone found!"
                };
            }

            var response = new { header, model };
            return Request.CreateResponse(response);
        }
    }
}
