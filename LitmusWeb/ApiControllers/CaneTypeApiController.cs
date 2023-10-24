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
    public class CaneTypeApiController : ApiController
    {
        CaneTypeRepository cRepo;
        WebApiHeader header;
        public CaneTypeApiController()
        {
            cRepo = new CaneTypeRepository();
        }

        [HttpPost]
        [ActionName(name: "GetCaneTypes")]
        [Route("api/CaneType/GetCaneTypes")]
        public HttpResponseMessage GetCaneTypes()
        {
            List<CaneTypeModel> model = new List<CaneTypeModel>();
            List<CaneType> ct = new List<CaneType>();
            ct = cRepo.GetCaneTypes();
            if (ct.Count > 0)
            {
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.OK,
                    statusMessage = "Success: Cane type found."
                };
                foreach (CaneType c in ct)
                {
                    CaneTypeModel temp = new CaneTypeModel()
                    {
                        Id = c.Id,
                        Code = c.Code,
                        Name = c.Name,
                        IsActive = c.IsActive,
                        Description = c.Description
                    };
                    model.Add(temp);
                }
            }
            else
            {
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.NoContent,
                    statusMessage = "Failed: No cane type found!"
                };
            }

            var response = new { header, model };
            return Request.CreateResponse(response);
        }
    }
}
