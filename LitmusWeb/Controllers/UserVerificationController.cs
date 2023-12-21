using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UserVerificationController : Controller
    {
        readonly UserVerificationRepository AvRepository = new UserVerificationRepository();
        // GET: UserVerification
        public ActionResult Index()
        {
            List<UserVerification> AvEntity = new List<UserVerification>();
            AvEntity = AvRepository.GetUserVerificationList();
            List<Models.UserVerificationModel> AvModel = new List<UserVerificationModel>();
            foreach (var item in AvEntity)
            {
                UserVerificationModel temp = new UserVerificationModel
                {
                    Id = item.Id,
                    UnitCode = item.UnitCode,
                    UserCode = item.UserCode,
                    ActivationLink = item.ActivationLink,
                    ActivationCode = item.ActivationCode,
                    ActivationValidity = item.ActivationValidity,
                    ActivatedAt = item.ActivatedAt,
                    CreatedDate = item.CreatedDate,
                };

                AvModel.Add(temp);
            }
            return View(AvModel);
        }

        [HttpGet]
        public ActionResult Add(UserVerification userVerification)
        {
            return View();
        }

        [HttpPost]
        //public ActionResult AddPost(UserVerification userVerification)
        //{
        //    if(Session["userCode"] == null)
        //    {
        //        TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(GetDetailsByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        public ActionResult EditPost(Models.UserVerificationModel AvModel)
        {
            UserVerification AvEntity = new UserVerification()
            {
                Id = AvModel.Id,
                ActivationCode = AvModel.ActivationCode,
                ActivationValidity = AvModel.ActivationValidity
            };
            bool result = AvRepository.EditUserVerification(AvEntity);
            if (!result)
            {
                TempData["ErrorTitle"] = "Could not make changes to the selected record!";
                TempData["ErrorMessage"] = "Somthing went wrong while tried to modify the record.";
                return View("Error");
            }
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(GetDetailsByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        public ActionResult DeletePost(int id)
        {
            bool result = AvRepository.DeleteUserVerfication(id);
            if (!result)
            {
                TempData["ErrorTitle"] = "Could not delete the record!";
                TempData["ErrorMessage"] = "Somthing went wrong while trying to delete the record.";
                return View("Error");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            UserVerification AvEntity = new UserVerification();
            AvEntity = AvRepository.FindByPk(id);
            Models.UserVerificationModel AvModel = new UserVerificationModel()
            {
                Id = AvEntity.Id,
                UnitCode = AvEntity.UnitCode,
                UserCode = AvEntity.UserCode,
                ActivationLink = AvEntity.ActivationLink,
                ActivationValidity = AvEntity.ActivationValidity,
                ActivatedAt = AvEntity.ActivatedAt,
                CreatedDate = AvEntity.CreatedDate
            };
            return View(AvModel);
        }

        [HttpGet]
        public ActionResult VerifyUser()
        {
            return View();
        }

        [HttpPost]
        [ActionName("VerifyUser")]
        public ActionResult VerifyUserPost()
        {
            //UserVerification userVerification = new UserVerification();
            //MasterUser masterUser = new MasterUser();
            //bool result = AvRepository.VerifyUser(verificationForm["UserCode"], verificationForm["ActivationCode"]);
            //if (!result)
            //{
            //    return View("Error");
            //}
            //else
            //{
            //    Response.Write("Account verified");
            //    ViewBag.userCode = verificationForm["UserCode"];
            //    return RedirectToAction("Index", "Home");
            //}
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public Models.UserVerificationModel GetDetailsByID(int id)
        {
            UserVerification AvEntity = new UserVerification();
            AvEntity = AvRepository.FindByPk(id);
            Models.UserVerificationModel AvModel = new UserVerificationModel()
            {
                Id = AvEntity.Id,
                UnitCode = AvEntity.UnitCode,
                UserCode = AvEntity.UserCode,
                ActivationLink = AvEntity.ActivationLink,
                ActivationCode = AvEntity.ActivationCode,
                ActivationValidity = AvEntity.ActivationValidity,
                ActivatedAt = AvEntity.ActivatedAt,
                CreatedDate = AvEntity.CreatedDate
            };
            return AvModel;
        }
    }
}