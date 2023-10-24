using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    [CustomAuthorizationFilter("Developer,Define Reports")]
    public class ExcelReportTemplateController : ApiController
    {
        ExcelReportTemplateRepository templateRepository = new ExcelReportTemplateRepository();
        /// <summary>
        /// get the template definition filtered by the Datatype
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetExcelTemplateDefinitionFilteredByDataType([FromBody] ApiExcelTemplateParameters parameters)
        {
            if (parameters == null || ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = templateRepository.excelReportTemplates(parameters.ReportSchemaCode, parameters.DatatypeCode);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            var genericResult = new { HttpStatusCode.OK, result };
            return Request.CreateResponse(genericResult);
        }


        // Add new row
        [HttpPost]
        [Route("api/ExcelReportTemplate/Add")]
        public HttpResponseMessage AddNewRecord(ExcelReportTemplateModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            bool result = false;
            ExcelReportTemplate Entity = new ExcelReportTemplate()
            {
                //Id = model.Id,
                ReportCode = model.ReportCode,
                DataType = model.DataType,
                CellFrom = model.CellFrom,
                CellTo = model.CellTo,
                Value = model.Value,
                Bold = model.Bold,
                Italic = model.Italic,
                FontColor = model.FontColor,
                BackGroundColor = model.BackGroundColor,
                NumerFormat = model.NumerFormat,
                HelpText = model.HelpText
            };
            try
            {
                result = templateRepository.Add(Entity);
                if (result == true)
                {
                    return Request.CreateResponse(new { HttpStatusCode.Created, result });
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return Request.CreateResponse(new { HttpStatusCode.Forbidden, result });
        }

        //Edit Row Item
        [HttpPost]
        [Route("api/ExcelReportTemplate/Edit")]
        public HttpResponseMessage Edit([FromBody] ExcelReportTemplateModel parameters)
        {
            int statusCode = 200;
            if (parameters == null || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            ExcelReportTemplate entity = new ExcelReportTemplate()
            {
                Id = parameters.Id,
                DataType = parameters.DataType,
                CellFrom = parameters.CellFrom,
                CellTo = parameters.CellTo,
                Value = parameters.Value,
                Bold = parameters.Bold,
                Italic = parameters.Italic,
                FontColor = parameters.FontColor,
                BackGroundColor = parameters.BackGroundColor,
                NumerFormat = parameters.NumerFormat,
                HelpText = parameters.HelpText
            };
            bool status = templateRepository.Update(entity);
            if (status)
            {
                statusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            var genericResult = new { statusCode };
            return Request.CreateResponse(genericResult);
        }

        // Get details of row defined in template
        [HttpPost]
        [Route("api/ExcelReportTemplate/GetDetailsOfTemplateRowById")]
        public HttpResponseMessage GetDetailsOfTemplateRow([FromBody] ApiExcelTemplateRowDetailsById param)
        {
            int statusCode = 200;
            if (param == null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                var genericResult = new { statusCode };
                return Request.CreateResponse(genericResult);
            }
            ExcelReportTemplate entity = new ExcelReportTemplate();
            entity = templateRepository.excelReportTemplateRowDefinition(param.Id);
            if (entity == null)
            {
                statusCode = (int)HttpStatusCode.NoContent;
                var genericResult = new { statusCode };
                return Request.CreateResponse(genericResult);
            }
            else
            {
                ExcelReportTemplateModel model = new ExcelReportTemplateModel()
                {
                    Id = entity.Id,
                    ReportCode = entity.ReportCode,
                    DataType = entity.DataType,
                    CellFrom = entity.CellFrom,
                    CellTo = entity.CellTo,
                    Value = entity.Value,
                    Bold = entity.Bold,
                    Italic = entity.Italic,
                    FontColor = entity.FontColor,
                    BackGroundColor = entity.BackGroundColor,
                    NumerFormat = entity.NumerFormat,
                    HelpText = entity.HelpText
                };
                statusCode = (int)HttpStatusCode.OK;
                var genericResponse = new { statusCode, model };
                return Request.CreateResponse(genericResponse);
            }


        }


    }
}
