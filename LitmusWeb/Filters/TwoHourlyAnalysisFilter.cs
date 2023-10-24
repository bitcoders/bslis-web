using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LitmusWeb.Filters
{
    public class TwoHourlyAnalysisFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["BaseUnitCode"] != null)
            {
                MasterUnitRepository x = new MasterUnitRepository();
                int BaseUnit = Convert.ToInt16(filterContext.HttpContext.Session.Contents["BaseUnitCode"]);
                MasterUnit mu = new MasterUnit();
                mu = x.FindUnitByPk(Convert.ToUInt16(BaseUnit));
                TwoHourlyAnalysisRepository twoHourlyAnalysisRepository = new TwoHourlyAnalysisRepository();
                var twoHourlyAnalysis = twoHourlyAnalysisRepository.GetTwoHourlyAnalysis(BaseUnit, mu.CrushingSeason, mu.EntryDate);
                int cropDays = Convert.ToInt32(mu.EntryDate.Subtract(mu.CrushingStartDate).TotalDays) + 1;
                int OldMillCapacity = Convert.ToInt32(mu.OldMillCapacity);
                //get the last entry time of analysis and add 2 (hours in it as its a bi-hourly entry)
                int EntryTime = mu.ReportStartTime;

                if (twoHourlyAnalysis.Count == 0)
                {
                    if (mu.CrushingStartDate == mu.EntryDate)
                    {
                        EntryTime = Convert.ToInt32(mu.CrushingStartDateTime.Value.Hour);
                        // check even odd time
                        if ((EntryTime % 2) > 0)
                        {
                            EntryTime = Convert.ToInt32(mu.CrushingStartDateTime.Value.Hour) + 1; // if mill start at 13 time should be 14
                        }
                        else
                        {
                            EntryTime = Convert.ToInt32(mu.CrushingStartDateTime.Value.Hour) + 2; // if mill start at 14 time should be 16
                        }

                    }
                    else
                    {
                        EntryTime = EntryTime + 2;
                    }
                }
                else
                {
                    EntryTime = (from t in twoHourlyAnalysis select t.entry_Time).LastOrDefault() + 2;
                }

                // if entry time is 24 (i.e. 12 AM) then the next entry time will be 2 am
                // if we dont use the below if condition it will return 26Hrs which is invalid
                if (EntryTime > 24)
                {
                    EntryTime = 2;
                }
                filterContext.Controller.ViewBag.BaseUnit = BaseUnit;
                filterContext.Controller.ViewBag.CrushingSeason = mu.CrushingSeason;
                filterContext.Controller.ViewBag.CropDay = cropDays;
                filterContext.Controller.ViewBag.EntryDate = mu.EntryDate.ToShortDateString();
                filterContext.Controller.ViewBag.UnitName = mu.Name;
                filterContext.Controller.ViewBag.EntryTime = EntryTime;
                filterContext.Controller.ViewBag.OldMillCapacity = OldMillCapacity;
            }
        }
    }
}