using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repositories.ReportScheduler;
using DataAccess.CustomModels;
using DataAccess.Repositories;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;

namespace LitmusWeb.Controllers.ReportScheduler
{
    public class ProcessedDateForReportController : Controller
    {
        private ProcessedDatesForReportRepository _repo;
        private int crushing_season;
        public ProcessedDateForReportController()
        {
             _repo = new ProcessedDatesForReportRepository();
        }
        // GET: ProcessedDateForReport
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                int unit_code = (int)Session["BaseUnitCode"];
                GetUnitDefaultValues(unit_code);

                var result = _repo.GetProcessedDatesForreport(unit_code, crushing_season);
                if(result != null)
                {
                    List<usp_select_ProcessedDatesForReportModel> dataList = new List<usp_select_ProcessedDatesForReportModel>();
                    foreach (var r in result)
                    {
                        usp_select_ProcessedDatesForReportModel d = new usp_select_ProcessedDatesForReportModel()
                        {
                            Id = r.ID,
                            UnitCode = r.UnitCode,
                            Name = r.Name,
                            SeasonCode = r.SeasonCode,
                            ProcessDate = r.ProcessDate,
                            FirstProcessedAt = r.FirstProcessedAt,
                            FirstProcessedBy = r.FirstProcessedBy,
                            RecentProcessedAt = r.RecentProcessedAt,
                            RecentProcessedBy = r.RecentProcessedBy,
                            ProcessCount = r.ProcessCount,
                            IsFinalizedForReport = r.IsFinalizedForReport,
                            DataFinalizedBy = r.DataFinalizedBy,
                            DataFinalizedAt = r.DataFinalizedAt,
                            ReportStatusCode = r.ReportStatusCode,
                            Value = r.Value,
                            Description = r.Description,
                        };
                        dataList.Add(d);
                    }
                    return View(dataList);
                }

            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
            return View();
        }

        [NonAction]
        private void GetUnitDefaultValues(int unitCode)
        {
            MasterUnitRepository unitRepository = new MasterUnitRepository();
            var unitValues = unitRepository.FindUnitByPk(unitCode);
            crushing_season = unitValues.CrushingSeason;
        }
    }
}