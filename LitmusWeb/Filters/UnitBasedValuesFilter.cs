using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using System;
using System.Web.Mvc;


namespace LitmusWeb.Filters
{
    public class UnitBasedValuesFilter : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["BaseUnitCode"] != null)
            {
                int BaseUnit = Convert.ToInt16(filterContext.HttpContext.Session.Contents["BaseUnitCode"].ToString());
                MasterUnitRepository x = new MasterUnitRepository();
                var mu = x.FindUnitByPk(BaseUnit);

                HourlyAnalysisRepository hourlyAnalysisRepository = new HourlyAnalysisRepository();
                var hourlyAnalysis = hourlyAnalysisRepository.GetLastAnalysisDetailsForEntryDate(BaseUnit, mu.CrushingSeason, Convert.ToDateTime(mu.EntryDate));
                int cropDays = Convert.ToInt32(mu.EntryDate.Subtract(mu.CrushingStartDate).TotalDays) + 1;
                // add 1 to the previous entry time, so that entry will be made for the next hour only.
                int EntryTime = mu.ReportStartTime;

                if (mu.CrushingStartDate == mu.EntryDate)
                {
                    //EntryTime = Convert.ToInt16(mu.CrushingStartTime);
                    EntryTime = Convert.ToInt32(mu.CrushingStartDateTime.Value.Hour);
                }

                if (hourlyAnalysis == null)
                {
                    EntryTime = EntryTime + 1;
                }
                else
                {
                    EntryTime = hourlyAnalysis.entry_time + 1;
                }


                if (EntryTime > 24)
                {
                    EntryTime = 1;
                }

                if (EntryTime == Convert.ToInt16(mu.CrushingStartDateTime.Value.Hour))
                {
                    filterContext.Controller.ViewBag.DayCompleted = true;
                }

                filterContext.HttpContext.Session.Add("CrushingSeason", mu.CrushingSeason);
                filterContext.Controller.ViewBag.CrushingSeason = mu.CrushingSeason;
                filterContext.Controller.ViewBag.EntryDate = mu.EntryDate.ToShortDateString();
                filterContext.Controller.ViewBag.CropDay = cropDays;
                filterContext.Controller.ViewBag.ProcessDate = mu.ProcessDate.ToShortDateString();
                filterContext.Controller.ViewBag.UnitName = mu.Name;
                filterContext.Controller.ViewBag.EntryTime = EntryTime;
                filterContext.Controller.ViewBag.OldMillCapacity = mu.OldMillCapacity;
            }
        }
    }
}