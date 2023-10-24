using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class UserMasterApiController : ApiController
    {
        readonly MasterUserRepository UserRepository = new MasterUserRepository();
        readonly MasterUnitRepository UnitRepository = new MasterUnitRepository();
        readonly MasterSeasonRepository seasonRepository = new MasterSeasonRepository();
        ApiResponseModel apiResponse;
        public UserMasterApiController()
        {
            apiResponse = new ApiResponseModel();
        }
        [HttpPost]
        [Route("api/UserMasterApi/UnitRightsList")]
        [ActionName(name: "UnitRightsList")]
        public async Task<HttpResponseMessage> PostUnitRightsList(ApiSingleStringParam param)
        {


            if (param == null || param.StringParam.Length == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            var masterUser = UserRepository.FindByUserCode(param.StringParam);

            if (masterUser == null && masterUser.UnitRights == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");

            }
            List<int> UsersUnitRightsList = new List<int>();

            UsersUnitRightsList = await Task.FromResult(masterUser.UnitRights.Split(',').Select(int.Parse).ToList());


            ///Creating a list of unit details based on unit rights 
            List<MasterUnit> unitList = new List<MasterUnit>();
            foreach (int unitCode in UsersUnitRightsList)
            {
                MasterUnit temp = new MasterUnit();
                temp = await Task.FromResult(UnitRepository.FindUnitByPk(unitCode));
                if (temp != null)
                {
                    unitList.Add(temp);
                }
            }
            List<MasterUnitApiUnitRights> UnitViewModel = new List<MasterUnitApiUnitRights>();
            foreach (var item in unitList)
            {
                MasterUnitApiUnitRights temp = new MasterUnitApiUnitRights()
                {
                    Id = item.Id,
                    CompanyCode = item.CompanyCode,
                    Code = item.Code,
                    Name = item.Name,
                };
                UnitViewModel.Add(temp);
            }
            var GenericResult = new { UnitViewModel };
            return Request.CreateResponse(HttpStatusCode.OK, GenericResult);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostChangeWorkingUnit([FromBody] MasterUserRolesModel param)
        {
            if (param == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            MasterUser masterUser = new MasterUser()
            {
                Code = param.Code,
                UnitCode = param.UnitCode,
            };
            if (await Task.FromResult(UserRepository.UpdateUserWorkingUnit(masterUser)) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "User's working unit not changed");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Sucess! User's Working is Unit Changed");
        }


        [HttpPost]
        [Route("api/UserMasterApi/SeasonModificationRights")]
        [ActionName(name: "SeasonModificationRights")]
        public async Task<HttpResponseMessage> SeasonRightsForModification(ApiSingleStringParam param)
        {
            List<MasterSeasonModel> allowedSeasons = new List<MasterSeasonModel>();
            if (param == null)
            {
                apiResponse.ErrorCode = (int)HttpStatusCode.BadRequest;
                apiResponse.ErrorMessage = "Invalid input!";
                var response = new { apiResponse, allowedSeasons };
                return Request.CreateResponse(response);
            }
            MasterUser user = new MasterUser();
            user = UserRepository.FindByUserCode(param.StringParam);

            if (user == null)
            {
                apiResponse.ErrorCode = (int)HttpStatusCode.NotFound;
                apiResponse.ErrorMessage = "No mofication rights are assigned to the user!";
                var response = new { apiResponse, allowedSeasons };
                return Request.CreateResponse(response);
            }
            else
            {
                List<int> allowedSeasonForModification = await Task.FromResult(user.ModificationAllowedForSeasons.Split(',').Select(int.Parse).ToList());

                // creating list of season rights based on season codes
                List<MasterSeason> season = new List<MasterSeason>();
                season = seasonRepository.GetMasterSeasonList();

                if (season.Count > 0)
                {
                    foreach (var s in season)
                    {

                        // add season to list if it exists in allowed season in unit rights
                        if (allowedSeasonForModification.Contains(s.SeasonCode))
                        {
                            MasterSeasonModel temp = new MasterSeasonModel()
                            {
                                SeasonCode = s.SeasonCode,
                                SeasonYear = s.SeasonYear
                            };
                            allowedSeasons.Add(temp);
                        }

                    }
                    apiResponse.ErrorCode = (int)HttpStatusCode.OK;
                    apiResponse.ErrorMessage = "Success";
                }
                else
                {
                    apiResponse.ErrorCode = (int)HttpStatusCode.NotFound;
                    apiResponse.ErrorMessage = "No mofication rights are assigned to the user!";
                }
                var response = new { apiResponse, allowedSeasons };
                return Request.CreateResponse(response);
            }


        }
        //[HttpPost]
        //[Route("api/UserMasterApi/seasonRightsList")]
        //public async Task<HttpResponseMessage> PostSeasonRights([FromBody] ApiSingleStringParam param)
        //{
        //    var masterUser = UserRepository.FindByUserCode(param.StringParam);
        //    if(masterUser == null)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
        //    }
        //    List<int> UserSeasonRightsList = new List<int>();
        //    UserSeasonRightsList = await Task.FromResult(masterUser.SeasonRights.Split(',').Select(int.Parse).ToList());
        //    List<MasterSeason> masterSeasons = new List<MasterSeason>();
        //    foreach(var item in UserSeasonRightsList)
        //    {
        //        MasterSeason temp = new MasterSeason();
        //        temp = await Task.FromResult(seasonRepository.FindByCode(item));
        //        if(temp != null)
        //        {
        //            masterSeasons.Add(temp);
        //        }
        //    }
        //    List<MasterSeasonModel> model = new List<MasterSeasonModel>();
        //    foreach(var i in masterSeasons)
        //    {
        //        MasterSeasonModel temp = new MasterSeasonModel()
        //        {
        //            id = i.id,
        //            SeasonCode = i.SeasonCode,
        //            SeasonYear = i.SeasonYear,
        //            IsActive = i.IsActive,
        //            CreatedDate = i.CreatedDate,
        //            CreatedBy = i.CreatedBy
        //        };
        //        model.Add(temp);
        //    }
        //    var genericResult = new { model };
        //    return Request.CreateResponse(HttpStatusCode.OK, genericResult);
        //}
    }
}
