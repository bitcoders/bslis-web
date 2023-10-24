using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Filters;
using LitmusWeb.Models.CustomModels;
using System;
using System.Web.Mvc;
namespace LitmusWeb.Controllers.Reports
{
    [CustomAuthorizationFilter("Super Admin", "Developer", "Raw Data dowload")]
    public class GenerateRawDataController : Controller
    {
        // GET: GenerateRawData
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost(RawDataDownloadParameters param)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            AnalysisRawDataExcelRepository repository = new AnalysisRawDataExcelRepository();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=RawData.xlsx");

            string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ReportDownloads/" + Session["BaseUnitCode"].ToString() + "/RawData/");
            //string fileName = repository.FetchRawData(7, 1, 20, Convert.ToDateTime("2020-01-01"), Convert.ToDateTime("2020-04-10"), filepath, false);
            string fileName = repository.FetchRawData(param.analysisDataCode, Convert.ToInt32(Session["BaseUnitCode"]), Convert.ToInt32(Session["CrushingSeason"]), param.fromDate, param.toDate, filepath, false);


            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}