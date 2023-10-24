using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class ReportSchemaController : ApiController
    {
        ReportSchemaRepository schemaRepository = new ReportSchemaRepository();
        [HttpPost]
        [Route("api/ReportSchema/GetSchemaDetailsList")]
        public HttpResponseMessage GetSchemaDetailsList()
        {
            int statusCode;
            List<ReportSchema> reportSchemas = new List<ReportSchema>();
            List<ReportSchemaModel> reportSchemaList = new List<ReportSchemaModel>();
            try
            {
                reportSchemas = schemaRepository.GetReportSchemaList();
                if (reportSchemas != null)
                {

                    foreach (var item in reportSchemas)
                    {
                        ReportSchemaModel temp = new ReportSchemaModel()
                        {
                            Code = item.Code,
                            SysObjectName = item.SysObjectName,
                            SysObjectDescripton = item.SysObjectDescripton,
                            SearchKeywords = item.SearchKeywords,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            CreatedBy = item.CreatedBy,
                            UpdatedDate = item.UpdatedDate,
                            UpdatedBy = item.UpdatedBy
                        };
                        reportSchemaList.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }


            statusCode = (int)System.Net.HttpStatusCode.OK;
            var genericResponse = new { statusCode, reportSchemaList };
            return Request.CreateResponse(genericResponse);
        }

        [HttpPost]
        [Route("api/ReportSchema/AddReportSchema")]
        public HttpResponseMessage AddReportSchema([FromBody] ReportSchemaModel reportSchemaModel)
        {
            int statusCode;
            if (reportSchemaModel == null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                var genericResponse = new { statusCode };
                return Request.CreateResponse(genericResponse);
            }
            else
            {
                try
                {
                    ReportSchema reportSchema = new ReportSchema()
                    {
                        Code = reportSchemaModel.Code,
                        SysObjectName = reportSchemaModel.SysObjectName,
                        SysObjectDescripton = reportSchemaModel.SysObjectDescripton,
                        SearchKeywords = reportSchemaModel.SearchKeywords,
                        IsActive = reportSchemaModel.IsActive,
                        CreatedBy = reportSchemaModel.CreatedBy,
                        CreatedDate = reportSchemaModel.CreatedDate,
                        UpdatedBy = reportSchemaModel.UpdatedBy,
                        UpdatedDate = reportSchemaModel.UpdatedDate,
                        SchemaType = reportSchemaModel.SchemaType
                    };
                    schemaRepository.AddReportSchema(reportSchema);
                    statusCode = (int)HttpStatusCode.Created;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                    statusCode = (int)HttpStatusCode.BadRequest;
                }

                var genericResponse = new { statusCode };
                return Request.CreateResponse(genericResponse);
            }
        }
    }
}
