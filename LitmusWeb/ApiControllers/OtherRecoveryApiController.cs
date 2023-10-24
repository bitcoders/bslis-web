
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace LitmusWeb.ApiControllers
{
    public class OtherRecoveryApiController : ApiController
    {
        [HttpPost]
        [Route("api/OtherRecoveriesApi/PostOtherRecovery")]
        [ActionName("OtherRecovery")]
        public HttpResponseMessage GetOtherRecoveryByUnitCode([FromBody] ApiParamUnitCode param)
        {
            WebApiHeader status = new WebApiHeader();
            OtherRecoveryModel otherRecovery;
            if (param == null)
            {
                status.statusCode = HttpStatusCode.BadRequest;
                status.statusMessage = "Invalid input!";
                otherRecovery = new OtherRecoveryModel();
            }
            else
            {
                OtherRecoveryRepository recoveryRepo = new OtherRecoveryRepository();
                OtherRecovery r = new OtherRecovery();
                r = recoveryRepo.GetOtherRecoveriesByUnitCode(param.unit_code);
                if (r == null)
                {
                    status.statusCode = HttpStatusCode.NotFound;
                    status.statusMessage = "No data found!";
                    otherRecovery = new OtherRecoveryModel();
                }
                else
                {
                    status.statusCode = HttpStatusCode.OK;
                    status.statusMessage = "Success!";
                    otherRecovery = new OtherRecoveryModel()
                    {
                        Id = r.Id,
                        UnitCode = r.UnitCode,
                        SeasonCode = r.SeasonCode,
                        TransDate = r.TransDate,
                        NonSugarInCHeavyFinalMolasses = r.NonSugarInCHeavyFinalMolasses,
                        CHeavyFinalMolasses = r.CHeavyFinalMolasses,
                        LossInCHeavyFinalMolasses = r.LossInCHeavyFinalMolasses,
                        LossInCHeavyPercentCane = r.LossInCHeavyPercentCane,
                        EstimatedSugarPercentOnCHeavy = r.EstimatedSugarPercentOnCHeavy
                    ,
                        RawSugarGainQtl = r.RawSugarGainQtl
                    ,
                        EstimatedSugarPercentOnRawSugar = r.EstimatedSugarPercentOnRawSugar
                    ,
                        NonSugarInBHeavyFinalMolasses = r.NonSugarInBHeavyFinalMolasses
                    ,
                        BHeavyFinalMolasses = r.BHeavyFinalMolasses
                    ,
                        LossInBHeavyFinalMolasses = r.LossInBHeavyFinalMolasses
                    ,
                        LossInBHeavyFinalMolassesPercentCane = r.LossInBHeavyFinalMolassesPercentCane
                    ,
                        EstimatedSugarPercentOnBHeavy = r.EstimatedSugarPercentOnBHeavy
                    ,
                        SyrupDiversion = r.SyrupDiversion
                    ,
                        EstimatedRecoveryOnSyrup = r.EstimatedRecoveryOnSyrup
                    };
                }
            }

            var response = new { status, otherRecovery };
            return Request.CreateResponse(response);
        }



        [HttpPost]
        [Route("api/OtherRecoveriesApi/PostOtherRecoveryPreviousDay")]
        [ActionName("PostOtherRecoveryPreviousDay")]
        public HttpResponseMessage GetOtherRecoveryPreviousDayByUnitCode([FromBody] ApiParamUnitCode param)
        {
            WebApiHeader status = new WebApiHeader();
            AllRecoveriesViewModel otherRecovery;
            if (param == null)
            {
                status.statusCode = HttpStatusCode.BadRequest;
                status.statusMessage = "Invalid input!";
                otherRecovery = new AllRecoveriesViewModel();
            }
            else
            {
                SummarizedReportsRepository summRepo = new SummarizedReportsRepository();
                proc_summarized_report_Result r = new proc_summarized_report_Result();

                r = summRepo.SummarizedReportForPreviousDay(param.unit_code);
                if (r == null)
                {
                    status.statusCode = HttpStatusCode.NotFound;
                    status.statusMessage = "No data found!";
                    otherRecovery = new AllRecoveriesViewModel();
                }
                else
                {
                    status.statusCode = HttpStatusCode.OK;
                    status.statusMessage = "Success!";

                    OtherRecoveryRepository otherRecoveryRepo = new OtherRecoveryRepository();
                    DailyAnalysisRepository dailyAnalysisRepo = new DailyAnalysisRepository();

                    OtherRecovery o = new OtherRecovery();
                    DailyAnalys d = new DailyAnalys();

                    o = otherRecoveryRepo.GetOtherRecoveriesPreviousDayByUnitCode(param.unit_code);
                    d = dailyAnalysisRepo.dailyAnalysesForPreviousDate(param.unit_code);


                    otherRecovery = new AllRecoveriesViewModel
                    {
                        estimated_sugar_on_b_heavy_on_date = r.od_estimated_sugar_percent_on_b_heavy,
                        estimated_sugar_on_b_heavy_to_date = r.td_estimated_sugar_percent_on_b_heavy,
                        estimated_sugar_on_c_heavy_on_date = r.od_estimated_sugar_percent_on_c_heavy,
                        estimated_sugar_on_c_heavy_to_date = r.td_estimated_sugar_percent_on_c_heavy,
                        estimated_sugar_on_raw_sugar_on_date = r.od_estimated_sugar_percent_on_raw_sugar,
                        estimated_sugar_on_raw_sugar_to_date = r.td_estimated_sugar_percent_on_raw_sugar,
                        estimated_sugar_on_syrup_on_date = r.od_estimated_sugar_percent_on_syrup,
                        estimated_sugar_on_syrup_to_date = r.td_estimated_sugar_percent_on_syrup,
                        LossInCHeavyPercentCane = o.LossInCHeavyPercentCane,
                        LossInBHeavyPercentCane = o.LossInBHeavyFinalMolassesPercentCane,
                        RawSugarGain = d.RawSugarGainDate,
                        SyrupDiversion = d.QuintalSyrupDiversion
                    };
                }
            }

            var response = new { status, otherRecovery };
            return Request.CreateResponse(response);
        }

    }
}
