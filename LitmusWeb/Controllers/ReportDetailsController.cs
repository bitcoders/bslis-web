using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Developer", "Define Reports")]
    public class ReportDetailsController : Controller
    {
        ReportDetailsRepository repository = new ReportDetailsRepository();
        // GET: ReportDetails
        public ActionResult Index()
        {
            List<ReportDetail> Entity = new List<ReportDetail>();
            Entity = repository.getReportDetailsList();
            if (Entity == null)
            {
                return View();
            }

            List<ReportDetailsModel> Model = new List<ReportDetailsModel>();
            foreach (var e in Entity)
            {
                ReportDetailsModel temp = new ReportDetailsModel()
                {
                    Code = e.Code,
                    Name = e.Name,
                    Description = e.Description,
                    Formats = e.Formats,
                    CreatedAt = e.CreatedAt,
                    CreatedBy = e.CreatedBy
                };
                Model.Add(temp);
            }
            return View(Model);
        }
    }
}