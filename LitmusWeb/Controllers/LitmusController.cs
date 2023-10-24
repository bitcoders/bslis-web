using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{

    public class LitmusController : Controller
    {
        // GET: Litmus
        readonly MasterUserRepository Repository = new MasterUserRepository();
        readonly MasterUnitRepository UnitRepository = new MasterUnitRepository();
        [UnitBasedValuesFilter]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            // getting user details from MasterUsers table.
            // purpose of this to get the 'UnitRights' column value.
            var UserDetails = Repository.FindByPk(Session["UserCode"].ToString());
            List<MasterUnitModel> UnitListModel = new List<MasterUnitModel>();
            //var unitRights = UserDetails.UnitRights;
            var dashboardRights = UserDetails.DashboardUnits;

            // converting unit rights field to a list so that i can get unit details 
            // by performing foreach loop using this list.

            List<MasterUnit> masterUnitEntity = new List<MasterUnit>();
            if (UserDetails != null && UserDetails.UnitRights != null)
            {
                masterUnitEntity = UnitRepository.GetMasterUnitDetailsByUnitCodes(UserDetails.DashboardUnits);
                foreach (var item in masterUnitEntity)
                {
                    MasterUnitModel temp = new MasterUnitModel()
                    {
                        Code = item.Code,
                        Name = item.Name,
                        EntryDate = item.EntryDate,
                        CrushingSeason = item.CrushingSeason
                    };
                    UnitListModel.Add(temp);
                }
            }
            return View(UnitListModel);
        }


    }
}