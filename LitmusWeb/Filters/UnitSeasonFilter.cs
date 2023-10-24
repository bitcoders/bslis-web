using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace LitmusWeb.Filters
{
    public class UnitSeasonFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int currentUnit = Convert.ToInt32(filterContext.HttpContext.Session.Contents["BaseUnitCode"]);
            int currentSeason = Convert.ToInt32(filterContext.HttpContext.Session.Contents["CrushingSeason"]);

            UnitSeasonsRepository usRepo = new UnitSeasonsRepository();
            UnitSeason entity = new UnitSeason();
            entity = usRepo.UnitSeasonByUnitCodeAndSeasonCode(currentUnit, currentSeason);
            UnitSeasonModel model;
            if (entity != null)
            {
                model = new UnitSeasonModel()
                {
                    id = entity.id,
                    Code = entity.Code,
                    Season = entity.Season,
                    CrushingStartDateTime = entity.CrushingStartDateTime,
                    CrushingEndDateTime = entity.CrushingEndDateTime,
                    NewMillCapacity = entity.NewMillCapacity,
                    OldMillCapacity = entity.OldMillCapacity,
                    ReportStartHourMinute = entity.ReportStartHourMinute,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    DisableDailyProcess = entity.DisableDailyProcess,
                    DisableAdd = entity.DisableAdd,
                    DisableUpdate = entity.DisableUpdate
                };

                if (model.DisableDailyProcess == true)
                {
                    string message = "Daily process is disabled by the administrator for season code " + currentSeason;
                    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    var url = urlHelper.Action("Forbidden", "Error", new { errorMessage = message });
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.Redirect(url);
                    filterContext.HttpContext.Response.End();
                }
            }

        }
    }
}