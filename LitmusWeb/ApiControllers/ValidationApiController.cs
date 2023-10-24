using DataAccess.CustomModels;
using DataAccess.Repositories;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class ValidationApiController : ApiController
    {
        ValidationRepository validationRepo = new ValidationRepository();

        [HttpPost]
        [ActionName(name: "validateDataBeforeFinalProcess")]
        [Route("api/ValidationApi/validateDataBeforeFinalProcess")]
        public HttpResponseMessage validateDataBeforeFinalProcess([FromBody] ValidationBeforeProcessParameter param)
        {
            List<ValidationModel> validationModels = new List<ValidationModel>();
            WebApiHeader webApiHeader = new WebApiHeader();
            try
            {

                ValidationModel a = new ValidationModel(); // validation for hourly analyses
                ValidationModel b = new ValidationModel(); // validation object for Two Hourly Analyses
                ValidationModel c = new ValidationModel(); // Validation object for daily Analyses
                ValidationModel d = new ValidationModel(); // validation object for Stoppage Analyses

                a = validationRepo.validateHourlyDataBeforeProcess(param.unit_code, param.season_code, param.process_date);
                ValidationModel hourlyValidation = new ValidationModel()
                {
                    validated = a.validated,
                    validationMessage = a.validationMessage
                };
                validationModels.Add(hourlyValidation);
                b = validationRepo.validateTwoHourlyDataBeforeProcess(param.unit_code, param.season_code, param.process_date);

                ValidationModel twohourlyValidation = new ValidationModel()
                {
                    validated = b.validated,
                    validationMessage = b.validationMessage
                };
                validationModels.Add(twohourlyValidation);

                c = validationRepo.validateDailyAnalysesBeforeProcess(param.unit_code, param.season_code, param.process_date);

                ValidationModel dailyAnalysesValidation = new ValidationModel()
                {
                    validated = c.validated,
                    validationMessage = c.validationMessage
                };
                validationModels.Add(dailyAnalysesValidation);
                d = validationRepo.validateStoppageDataBeforeProcess(param.unit_code, param.season_code, param.process_date);

                ValidationModel stoppageAnalysesValidation = new ValidationModel()
                {
                    validated = d.validated,
                    validationMessage = d.validationMessage
                };
                validationModels.Add(stoppageAnalysesValidation);

                //validationModels.Add(validationRepo.validateHourlyDataBeforeProcess(param.unit_code, param.season_code, param.process_date));
                //validationModels.Add(validationRepo.validateTwoHourlyDataBeforeProcess(param.unit_code, param.season_code, param.process_date));
                //validationModels.Add(validationRepo.validateDailyAnalysesBeforeProcess(param.unit_code, param.season_code, param.process_date));
                //validationModels.Add(validationRepo.validateStoppageDataBeforeProcess(param.unit_code, param.season_code, param.process_date));

                List<ValidationMessageModel> vmModel = new List<ValidationMessageModel>();
                foreach (var i in validationModels)
                {
                    ValidationMessageModel tempVmModel = new ValidationMessageModel()
                    {
                        validated = i.validated,
                        validationMessage = i.validationMessage
                    };
                    vmModel.Add(tempVmModel);
                }
                webApiHeader.statusCode = HttpStatusCode.OK;
                webApiHeader.statusMessage = HttpStatusCode.OK.ToString();
            }
            catch (Exception ex)
            {
                webApiHeader.statusCode = HttpStatusCode.NoContent;
                webApiHeader.statusMessage = ex.Message;
            }
            var response = new { webApiHeader, validationModels };

            return Request.CreateResponse(response);
        }
    }
}