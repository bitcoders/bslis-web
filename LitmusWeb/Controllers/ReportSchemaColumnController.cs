using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Developer", "Report Schema Definition")]
    public class ReportSchemaColumnController : Controller
    {

        ReportSchemaColumRepository rscRepository = new ReportSchemaColumRepository();
        // GET: ReportSchemaColumn
        public ActionResult Index(int id)
        {
            List<ReportSchemaColumn> entity = new List<ReportSchemaColumn>();
            entity = rscRepository.GetReportDataColumnsList(id);
            List<ReportSchemaColumnModel> Model = new List<ReportSchemaColumnModel>();
            if (entity != null)
            {
                foreach (var item in entity)
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
                        UpdatedBy = item.UpdatedBy,
                        UpdatedDate = item.UpdatedDate
                    };
                    Model.Add(temp);
                }
            }
            return View(Model);
        }
    }
}