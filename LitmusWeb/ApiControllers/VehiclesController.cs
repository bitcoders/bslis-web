using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Collections.Generic;
using System.Web.Http;
namespace LitmusWeb.ApiControllers
{
    public class VehiclesController : ApiController
    {
        readonly SugarLabEntities db;
        MasterVehicleRepository Repository;
        public VehiclesController()
        {
            db = new SugarLabEntities();
            Repository = new MasterVehicleRepository();
        }
        [HttpPost]
        [ActionName(name: "GetVehicleDetails")]
        [Route("api/Vehicles/GetVehicleDetails")]
        public List<MasterVehicleModel> GetVehicleList([FromBody] ApiParamUnitCode param)
        {
            List<MasterVehicle> VehicleEntity = new List<MasterVehicle>();
            VehicleEntity = Repository.GetMasterVehicleByUnitList(param.unit_code);
            List<Models.MasterVehicleModel> VehicleModels = new List<MasterVehicleModel>();
            foreach (var item in VehicleEntity)
            {
                MasterVehicleModel temp = new MasterVehicleModel()
                {
                    CompanyCode = item.CompanyCode,
                    UnitCode = item.UnitCode,
                    Code = item.Code,
                    Name = item.Name,
                    MinimumWeight = item.MinimumWeight,
                    MaximumWeight = item.MaximumWeight,
                    AverageWeight = item.AverageWeight,
                    IsActive = item.IsActive
                };
                VehicleModels.Add(temp);
            }
            return VehicleModels;

        }
    }
}
