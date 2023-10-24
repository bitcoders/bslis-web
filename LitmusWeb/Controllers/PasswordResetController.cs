
using DataAccess.CustomModels;
using DataAccess.Repositories;
using LitmusWeb.Models.CustomModels;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    public class PasswordResetController : Controller
    {
        // GET: PasswordReset
        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [ActionName(name: "PasswordReset")]
        public ActionResult PasswordResetPost(PasswordResetViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                RegisteredDevicesRepository rRepo = new RegisteredDevicesRepository();

                PasswordResetModel pm = new PasswordResetModel()
                {
                    UserCode = m.UserCode
                };
                bool result = rRepo.ResetPasswordRequestForDevice(pm);
                if (result == true)
                {
                    return RedirectToAction("Index", "Litmus");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult PasswordRecovery()
        {
            var url = Url.RequestContext.RouteData.Values["id"];
            ViewBag.token = url;
            return View();
        }

        [HttpPost]
        [ActionName(name: "PasswordRecovery")]
        [ValidateAntiForgeryToken]
        public ActionResult PassworRecoveryPost(PasswordChangeViewModel m)
        {
            if (!ModelState.IsValid || m == null)
            {
                return View(m);
            }
            //RegisteredDevicesRepository rRepo = new RegisteredDevicesRepository();
            var url = Url.RequestContext.RouteData.Values["id"];
            ViewBag.token = url;
            //if (rRepo.ResetPassword(url.ToString(), m.Password) == true)
            //{
            //    ViewBag.status = "sucess";
            //}
            //else
            //{
            //    ViewBag.status = "false";
            //}
            return View();
        }

    }
}