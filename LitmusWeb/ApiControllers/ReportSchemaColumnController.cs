using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LitmusWeb.ApiControllers
{
    public class ReportSchemaColumnController : ApiController
    {
        ReportSchemaColumRepository rscReository = new ReportSchemaColumRepository();
        [HttpPost]
        [Route("api/ReportSchmeaColumn/ColumnList")]
        [ActionName("ColumnList")]
        public HttpResponseMessage PostReportSchemaColumnList([FromBody] ApiExcelTemplateParameters param)
        {
            int statusCode = 200;
            if (param == null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                return Request.CreateResponse(new { statusCode });
            }

            List<ReportSchemaColumn> reportSchemaColumns = new List<ReportSchemaColumn>();
            List<ReportSchemaColumnModel> Model = new List<ReportSchemaColumnModel>();

            reportSchemaColumns = rscReository.GetReportDataColumnsList(param.ReportSchemaCode);
            if (reportSchemaColumns != null)
            {
                foreach (var item in reportSchemaColumns)
                {
                    ReportSchemaColumnModel temp = new ReportSchemaColumnModel()
                    {
                        Code = item.Code,
                        SchemaCode = item.SchemaCode,
                        ColumnText = item.ColumnText,
                        SearchKeyword = item.SearchKeyword,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedBy = item.UpdatedBy
                    };
                    Model.Add(temp);
                }
            }
            var genericResponse = new { statusCode, Model };
            return Request.CreateResponse(genericResponse);
        }


        [HttpPost]
        [Route("api/ReportSchmeaColumn/Add")]
        [ActionName("AddColumn")]
        public HttpResponseMessage PostAddSchemaColumn([FromBody] ReportSchemaColumnModel param)
        {
            bool status = false;
            int statusCode = 200;
            if (param == null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                return Request.CreateResponse(statusCode);
            }

            ReportSchemaColumn entity = new ReportSchemaColumn()
            {
                SchemaCode = param.SchemaCode,
                ColumnText = param.ColumnText,
                SearchKeyword = param.SearchKeyword,
                IsActive = param.IsActive,
                CreatedDate = DateTime.Now,
                CreatedBy = param.CreatedBy,
            };
            try
            {
                status = rscReository.AddSchemaColumn(entity);
                if (status)
                {
                    statusCode = (int)HttpStatusCode.Created;
                }
                else
                {
                    statusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            var genericResponse = new { statusCode };
            return Request.CreateResponse(genericResponse);

        }

    }
}
