using DataAccess;
using DataAccess.CustomModels.Reports;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using iText.Layout.Element;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.WebSockets;

namespace LitmusWeb.ApiControllers
{
    public class DashBoardApiController : ApiController
    {
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate, ProcessDate, CrushingStartDate;


        [System.Web.Mvc.NonAction]
        private void SetUnitDefaultValues(int unit_code)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(unit_code);
            BaseUnitCode = unit_code;
            EntryDate = UnitDefaultValues.EntryDate;
            ProcessDate = UnitDefaultValues.ProcessDate;
            CrushingSeason = UnitDefaultValues.CrushingSeason;
            CrushingStartDate = UnitDefaultValues.CrushingStartDate;
        }

        [HttpPost]
        [ActionName(name: "GetDashBoardAlers")]
        [Route("api/DashBoardApi/GetDashBoardAlers")]
        public async Task<HttpResponseMessage> GetDashboardCriticalAlerts([FromBody] ApiParamUnitCode param)
        {
            SetUnitDefaultValues(param.unit_code);
            HourlyAnalysisRepository hourlyAnalysisRepository = new HourlyAnalysisRepository();
            HourlyAnalys hourlyAnalysEntity = new HourlyAnalys();
            hourlyAnalysEntity = await hourlyAnalysisRepository.GetLatestHourlyAnalysesDetails(param.unit_code, CrushingSeason);
            HourlyAnalysisModel hourlyAnalysisModel = new HourlyAnalysisModel()
            {
                id = hourlyAnalysEntity.id,
                unit_code = hourlyAnalysEntity.unit_code,
                season_code = hourlyAnalysEntity.season_code,
                entry_Date = hourlyAnalysEntity.entry_Date,
                entry_time = hourlyAnalysEntity.entry_time,
                new_mill_juice = hourlyAnalysEntity.new_mill_juice,
                old_mill_juice = hourlyAnalysEntity.old_mill_juice,
                juice_total = hourlyAnalysEntity.juice_total,
                new_mill_water = hourlyAnalysEntity.new_mill_water,
                old_mill_water = hourlyAnalysEntity.old_mill_water,
                sugar_bags_L31 = hourlyAnalysEntity.sugar_bags_L31,
                sugar_bags_L30 = hourlyAnalysEntity.sugar_bags_L30,
                sugar_bags_L_total = hourlyAnalysEntity.sugar_bags_L_total,
                sugar_bags_M30 = hourlyAnalysEntity.sugar_bags_M30,
                sugar_bags_M31 = hourlyAnalysEntity.sugar_bags_M31,
                sugar_bags_M_total = hourlyAnalysEntity.sugar_bags_M_total,
                sugar_bags_S30 = hourlyAnalysEntity.sugar_bags_S30,
                sugar_bags_S31 = hourlyAnalysEntity.sugar_bags_S31,
                sugar_bags_S_total = hourlyAnalysEntity.sugar_bags_S_total,
                sugar_Biss = hourlyAnalysEntity.sugar_Biss,
                sugar_raw = hourlyAnalysEntity.sugar_raw,
                sugar_bags_total = hourlyAnalysEntity.sugar_bags_total,
                cooling_trace = hourlyAnalysEntity.cooling_trace,
                cooling_pol = hourlyAnalysEntity.cooling_pol,
                cooling_ph = hourlyAnalysEntity.cooling_ph,
                crtd_dt = hourlyAnalysEntity.crtd_dt,
                crtd_by = hourlyAnalysEntity.crtd_by,
                standing_truck = hourlyAnalysEntity.standing_truck,
                standing_trippler = hourlyAnalysEntity.standing_trippler,
                standing_trolley = hourlyAnalysEntity.standing_trolley,
                standing_cart = hourlyAnalysEntity.standing_cart,
                un_crushed_cane = hourlyAnalysEntity.un_crushed_cane,
                crushed_cane = hourlyAnalysEntity.crushed_cane
            };

            string alertMessage = "";
            if (EntryDate < hourlyAnalysisModel.entry_Date)
            {
                alertMessage = "It looks like selected Unit is uploading or modifying Analyses data of Previous Days!";
                var genericData = new { alertMessage, hourlyAnalysisModel };
                return Request.CreateResponse(HttpStatusCode.OK, genericData);
            }
            var x = new { alertMessage, hourlyAnalysisModel };
            return Request.CreateResponse(HttpStatusCode.OK, x);
        }


        [HttpPost]
        [ActionName(name: "GetHourlyData")]
        [Route("api/DashBoardApi/GetHourlyData")]
        public HttpResponseMessage GetHourlyData([FromBody] DashboardApiParam param )
        {
            List<DashboardModel> modelList = new List<DashboardModel>();
            try
            {
                DashboardRepository dRepo = new DashboardRepository();
                var data = dRepo.GetHourlyDataSummary(param.user_code, param.company_code, param.unit_code, param.season_code, param.entry_date);
                if(data != null)
                {
                    foreach(var d in data)
                    {
                        DashboardModel m = new DashboardModel()
                        {
                            UnitCode = d.UnitCode,
                            Id = d.Id,
                            EntryDate = d.EntryDate,
                            EntryTime = d.EntryTime,
                            UnitName = d.UnitName,
                            CompanyCode = d.CompanyCode,
                            CompanyName = d.CompanyName,
                            CaneCrushed = d.CaneCrushed,
                            SugarBagsTotal = d.SugarBagsTotal,
                            WaterTotal = d.WaterTotal,
                            JuiceTotal = d.JuiceTotal,
                            CaneDiverted = d.CaneDiverted,
                            DivertedSyrup = d.DivertedSyrup
                        };
                        modelList.Add(m);
                    }
                }
                var unitList = modelList.Select(x=> new { x.UnitCode, x.UnitName}).Distinct().ToList();
                List<UnitWiseHourlDataResult> resultList = new List<UnitWiseHourlDataResult>();
               // List<HourlyData> hourlyData = new List<HourlyData>();
                foreach( var x in unitList)
                {
                    List<HourlyData> hourlyData = data.Where(w => w.UnitCode == x.UnitCode).Select(d => new HourlyData
                    {
                        hour = d.EntryTime.ToString(),
                        value = d.CaneCrushed.ToString()
                    }).ToList();

                    UnitWiseHourlDataResult temp = new UnitWiseHourlDataResult()
                    {
                        unitName = x.UnitName,
                        hourlyValues = hourlyData
                    };
                    resultList.Add(temp);
                }
                return Request.CreateResponse(HttpStatusCode.OK, resultList);
            }
            catch(Exception ex) {
                new Exception(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,$"Error {ex.Message}");
            }
        }

        [HttpPost]
        [ActionName(name: "GetDashboardDaysummary")]
        [Route("api/DashBoardApi/GetDashboardDaysummary")]
        public HttpResponseMessage GetDashboardDaysummary([FromBody] DashboardProcessedDataApiParam param)
        {
            DashboardRepository dRepo = new DashboardRepository();
            List<DashboardProcessedData> modelList = new List<DashboardProcessedData>();
            try
            {
                var data = dRepo.GetProcessedDataSummary(param.user_code, param.company_code, param.season_code, param.history_days);
                if (data != null)
                {
                    foreach (var d in data)
                    {
                        DashboardProcessedData temp = new DashboardProcessedData()
                        {
                            id = d.id,
                            entry_date = d.entry_date,
                            unit_code = d.unit_code,
                            unit_name = d.unit_name,
                            company_code = d.company_code,
                            company_name = d.company_name,
                            cane_crushed = d.cane_crushed,
                            estimated_sugar_percent_cane = d.estimated_sugar_percent_cane,
                            estimated_recovery_on_syrup = d.estimated_recovery_on_syrup,
                            estimated_sugar_percent_on_b_heavy = d.estimated_sugar_percent_on_b_heavy,
                            estimated_sugar_percent_on_c_heavy = d.estimated_sugar_percent_on_c_heavy,
                            estimated_sugar_percent_on_raw_sugar = d.estimated_sugar_percent_on_raw_sugar,
                            total_losses_percent_cane = d.total_losses_percent_cane,
                            unknwon_loss_percent_cane = d.unknwon_loss_percent_cane,
                            steam_percent_cane = d.steam_percent_cane,
                            total_bagasse_percent_cane = d.total_bagasse_percent_cane,
                            estimated_molasses_percent_cane = d.estimated_molasses_percent_cane,
                            total_sugar_bagged = d.total_sugar_bagged,
                            fiber_percent_cane = d.fiber_percent_cane,
                            pol_in_cane = d.pol_in_cane,
                        };
                        modelList.Add(temp);
                    }
                    var unitList = modelList.Select(x => new { x.unit_code, x.unit_name }).Distinct().ToList();
                    List<UnitWiseResult> resultList = new List<UnitWiseResult>();
                    foreach (var x in unitList)
                    {
                        List<Result> result = data.Where(w => w.unit_code == x.unit_code).Select(d => new Result
                        {
                            entry_date = d.entry_date,
                            value = d.cane_crushed.ToString(),
                            cane_crushed = d.cane_crushed,
                            estimated_sugar_percent_cane = d.estimated_sugar_percent_cane,
                            estimated_sugar_percent_on_b_heavy = d.estimated_sugar_percent_on_b_heavy,
                            esimated_recovery_on_syrup = d.estimated_recovery_on_syrup,
                            total_losses_percent = d.total_losses_percent_cane,
                            unknwown_loss = d.unknwon_loss_percent_cane,
                            steam_percent_cane = d.steam_percent_cane,
                            total_bagasse_percent_cane = d.total_bagasse_percent_cane,
                            estimated_molasses_percent_cane = d.estimated_molasses_percent_cane,
                            total_sugar_bagged = d.total_sugar_bagged,
                            fiber_percent_cane = d.fiber_percent_cane,
                            pol_in_cane = d.pol_in_cane,
                        }).ToList();

                        UnitWiseResult temp = new UnitWiseResult()
                        {
                            unit_name = x.unit_name,
                            results = result
                        };
                        resultList.Add(temp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, resultList);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error {ex.Message}");
            }
            
        }
    }
}
