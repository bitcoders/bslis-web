using DataAccess.Repositories;
using System;
using System.Web.Mvc;
namespace LitmusWeb.Filters
{
    public class MeltingsFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["UserCode"] != null)
            {
                int BaseUnit = Convert.ToInt16(filterContext.HttpContext.Session.Contents["BaseUnitCode"]);
                MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
                MasterParameterSubCategoryRepository subCategoryRepository = new MasterParameterSubCategoryRepository();

                var masterUnit = masterUnitRepository.FindUnitByPk(BaseUnit);
                // 2 is master code in MasterParameterCategories for Molasses
                var MeltingsList = subCategoryRepository.GetMasterParameterCategoriesByParameterMasterCode(3);
                int cropDays = Convert.ToInt32(masterUnit.EntryDate.Subtract(masterUnit.CrushingStartDate).TotalDays) + 1;

                filterContext.Controller.ViewBag.BaseUnit = BaseUnit;
                filterContext.Controller.ViewBag.CrushingSeason = masterUnit.CrushingSeason;
                filterContext.Controller.ViewBag.CropDay = cropDays;
                filterContext.Controller.ViewBag.EntryDate = masterUnit.EntryDate.ToShortDateString();
                filterContext.Controller.ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
                filterContext.Controller.ViewBag.UnitName = masterUnit.Name;
                filterContext.Controller.ViewBag.MolassesList = MeltingsList;
            }
        }
    }
}