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
    public class CaneVarietyApiController : ApiController
    {
        CaneVarietyRepository cvRepo;
        WebApiHeader header;
        public CaneVarietyApiController()
        {
            cvRepo = new CaneVarietyRepository();
        }
        [HttpPost]
        [ActionName(name: "SearchCaneVariety")]
        [Route("api/CaneVarietyApi/SearchCaneVariety")]
        public HttpResponseMessage SearchCaneVariety([FromBody] ApiSingleStringParam StringParam)
        {
            List<CaneVarietyModel> model = new List<CaneVarietyModel>();
            List<CaneVariety> cv = new List<CaneVariety>();
            if (StringParam == null)
            {
                header = new WebApiHeader()
                {
                    statusCode = HttpStatusCode.BadRequest,
                    statusMessage = "Failed: invalid request!"
                };
            }
            else
            {
                cv = cvRepo.SearchCaneVariety(StringParam.StringParam);
                if (cv != null)
                {
                    foreach (CaneVariety v in cv)
                    {
                        CaneVarietyModel temp = new CaneVarietyModel()
                        {
                            Id = v.Id,
                            Code = v.Code,
                            Name = v.Name,
                            IsActive = v.IsActive,
                            Description = v.Description
                        };
                        model.Add(temp);
                    }
                    header = new WebApiHeader()
                    {
                        statusCode = HttpStatusCode.OK,
                        statusMessage = "Success: cane varities found."
                    };
                }
                else
                {
                    header = new WebApiHeader()
                    {
                        statusCode = HttpStatusCode.NoContent,
                        statusMessage = "Failed: No cane variety found!"
                    };
                }
            }

            var response = new { header, model };
            return Request.CreateResponse(response);
        }
    }
}
