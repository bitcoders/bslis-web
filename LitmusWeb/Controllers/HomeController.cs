using DataAccess;
using System;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{

    public class HomeController : Controller
    {
        LitmusRepository litmusRepository = new LitmusRepository();
        // GET: Home


        public ActionResult Index()
        {
            if (Session["UserCode"] != null)
            {
                return RedirectToAction("Index", "Litmus");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection loginForm)
        {
            if (Session["UserCode"] != null)
            {
                return RedirectToAction("Index", "Litmus");
            }
            string code = loginForm["txtCode"];
            string password = loginForm["txtPassword"];
            // this will store user code in viewbag and will use this at login page to show user code
            // in textbox in case of login failed.
            ViewBag.userCode = code;


            MasterUser masterUser = litmusRepository.ValidateUser(code, password);
            if (masterUser == null)
            {
                ViewBag.ErrorMessage = "Invalid Credentials, please try again";
                return View("Login");
            }
            else
            {
                Session["UserCode"] = masterUser.Code;
                Session["BaseUnitCode"] = masterUser.UnitCode;
                Session["UserName"] = masterUser.FirstName;
                Session["UserRole"] = masterUser.Role;
                Session["ServerName"] = litmusRepository.GetServerDetails();
                Session["UserImage"] = masterUser.UserImageUrl;
                // Load default parameter of units if user is validated and save them into
                // session varialbe
                MasterUnit masterUnit = new MasterUnit();
                masterUnit = litmusRepository.GetUnitSettings(Convert.ToInt16(masterUser.UnitCode));

                Session["UnitName"] = masterUnit.Name;
                Session["CrushingSeason"] = masterUnit.CrushingSeason;
                Session["EntryDate"] = masterUnit.EntryDate;
                Session["ProcessDate"] = masterUnit.ProcessDate;



                if (ViewBag.LastPage != null)
                {
                    return Redirect(ViewBag.LastPage);
                }
                return RedirectToAction("Index", "Litmus");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            //if (Session["UserCode"] != null)
            //{
            //    RedirectToAction("Index", "Litmus");
            //}
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection loginForm)
        {
            if (Session["UserCode"] != null)
            {
                return RedirectToAction("Index", "Litmus");
            }
            string code = loginForm["txtCode"];
            string password = loginForm["txtPassword"];
            // this will store user code in viewbag and will use this at login page to show user code
            // in textbox in case of login failed.
            ViewBag.userCode = code;


            MasterUser masterUser = litmusRepository.ValidateUser(code, password);
            if (masterUser == null)
            {
                ViewBag.ErrorMessage = "Invalid Credentials, please try again";
                return View("Login");
            }
            else
            {
                Session["UserCode"] = masterUser.Code;
                Session["BaseUnitCode"] = masterUser.UnitCode;  /// this is working unit code, cant change array name because it was change in the middle of development, it we change it now, it will crash the site at multiple pages.
                Session["UserName"] = masterUser.FirstName;
                Session["UserRole"] = masterUser.Role;
                Session["ServerName"] = litmusRepository.GetServerDetails();
                Session["UserImage"] = masterUser.UserImageUrl;
                // Load default parameter of units if user is validated and save them into
                // session varialbe
                MasterUnit masterUnit = new MasterUnit();
                masterUnit = litmusRepository.GetUnitSettings(Convert.ToInt16(masterUser.UnitCode));

                Session["UnitName"] = masterUnit.Name;
                Session["CrushingSeason"] = masterUnit.CrushingSeason;
                Session["EntryDate"] = masterUnit.EntryDate;
                Session["ProcessDate"] = masterUnit.ProcessDate;


                if (ViewBag.LastPage != null)
                {
                    return Redirect(ViewBag.LastPage);
                }
                return RedirectToAction("Index", "Litmus");
            }
        }


    }
}