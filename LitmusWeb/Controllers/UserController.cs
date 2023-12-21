using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UserController : Controller
    {
        MasterUserRepository MasterUserRepository = new MasterUserRepository();
        UserVerificationRepository UserVerificationRepository = new UserVerificationRepository();
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterUser> masterUsers = MasterUserRepository.GetMasterUserList();
            List<Models.MasterUserModel> masterUserModel = new List<MasterUserModel>();
            foreach (var item in masterUsers)
            {
                Models.MasterUserModel temp = new MasterUserModel
                {
                    CompanyCode = item.CompanyCode,
                    UnitCode = item.UnitCode,
                    Code = item.Code,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Password = item.Password,
                    Salt = item.Salt,
                    Email = item.Email,
                    MobileNo = item.MobileNo,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy,
                    IsDeleted = item.IsDeleted,
                    UnitRights = item.UnitRights
                };

                masterUserModel.Add(temp);
            }
            return View(masterUserModel);
        }
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            // get the list of available units so that we can show unit name checkbox
            // to assign unit rights to the user.
            GetUnitList();
            GetRolesList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        public async Task<ActionResult> Create(Models.MasterUserModel masterUserModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {

                CryptographyRepository crypto = new CryptographyRepository();

                string tempSalt = crypto.GenerateSalt();
                string tempPassword = crypto.GenerateHashedString(masterUserModel.Password, tempSalt);
                DataAccess.MasterUser masterUser = new MasterUser()
                {
                    CompanyCode = masterUserModel.CompanyCode,
                    UnitCode = masterUserModel.UnitCode,
                    Code = masterUserModel.Code,
                    FirstName = masterUserModel.FirstName,
                    LastName = masterUserModel.LastName,
                    Salt = tempSalt,
                    Password = tempPassword,
                    IsActive = masterUserModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString(),
                    IsDeleted = masterUserModel.IsDeleted,
                    Email = masterUserModel.Email,
                    MobileNo = masterUserModel.MobileNo,
                    EmailVerified = false,
                    MobileVerified = false,
                    EmailNewLetter = masterUserModel.EmailNewLetter,
                    MobileAlert = masterUserModel.MobileAlert,
                    UnitRights = masterUserModel.UnitRights,
                    BaseUnit = masterUserModel.BaseUnit,
                    Role = masterUserModel.Role,
                    DashboardUnits = masterUserModel.DashboardUnits,
                    EntryAllowedSeasons = masterUserModel.EntryAllowedSeasons,
                    ModificationAllowedForSeasons = masterUserModel.ModificationAllowedForSeasons,
                    ViewAllowedForSeasons = masterUserModel.ViewAllowedForSeasons
                };

                string randomUrlCode = crypto.GenerateSalt(5, 6);
                Guid randomString = Guid.NewGuid();
                DataAccess.UserVerification VerificationModel = new UserVerification()
                {
                    UnitCode = masterUserModel.UnitCode,
                    UserCode = masterUserModel.Code,
                    ActivationCode = crypto.GenerateSalt(2, 4),
                    Token = randomString.ToString(),
                    //ActivationLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/UserVerification/verifyUser/" + masterUserModel.Code + "/" + randomUrlCode,
                    ActivationLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/UserVerification/verifyUser/" + randomString.ToString(),
                    ActivationValidity = DateTime.Now.AddMinutes(60),
                    ActivatedAt = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    
                };
                bool result = MasterUserRepository.addMasterUser(masterUser);
                bool resultVerification = UserVerificationRepository.AddUserVerification(VerificationModel);
                if (!result || !resultVerification)
                {
                    TempData["ErrorTitle"] = "Failed To register user";
                    TempData["ErrorMessage"] = "Something wrong went while registering the user";
                    return View("Error");
                }
                // send varification email and then return to index page
                EmailRepository emailRepository = new EmailRepository();
                string emailResult = await emailRepository.SendVerificationMail(masterUserModel.Email
                    , masterUserModel.FirstName, VerificationModel.ActivationLink
                    , VerificationModel.ActivationCode, masterUserModel.Code, masterUserModel.Password);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Edit(string id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetUnitList();
            GetRolesList();
            MasterUser masterUser = new MasterUser();

            masterUser = MasterUserRepository.FindByUserCode(id);

            MasterUserUpdateModel masterUserModel = new MasterUserUpdateModel()
            {
                CompanyCode = masterUser.CompanyCode,
                UnitCode = masterUser.UnitCode,
                Code = masterUser.Code,
                FirstName = masterUser.FirstName,
                LastName = masterUser.LastName,
                IsActive = masterUser.IsActive,
                CreatedDate = masterUser.CreatedDate,
                CreatedBy = masterUser.CreatedBy,
                IsDeleted = masterUser.IsDeleted,
                Email = masterUser.Email,
                MobileNo = masterUser.MobileNo,
                EmailVerified = masterUser.EmailVerified,
                MobileVerified = masterUser.MobileVerified,
                EmailNewLetter = masterUser.EmailNewLetter,
                MobileAlert = masterUser.MobileAlert,
                UnitRights = masterUser.UnitRights,
                BaseUnit = masterUser.BaseUnit,
                Role = masterUser.Role,
                DashboardUnits = masterUser.DashboardUnits,
                ViewAllowedForSeasons = masterUser.ViewAllowedForSeasons,
                ModificationAllowedForSeasons = masterUser.ModificationAllowedForSeasons,
                EntryAllowedSeasons = masterUser.EntryAllowedSeasons,
            };
            return View(masterUserModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        public ActionResult EditPost(Models.MasterUserUpdateModel mum)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetUnitList();
            GetRolesList();
            if (!ModelState.IsValid)
            {
                return View(mum);
            }

            MasterUser tempMasterUser = new MasterUser();
            tempMasterUser = MasterUserRepository.FindByUserCode(mum.Code);

            MasterUser masterUser = new MasterUser()
            {

                CompanyCode = tempMasterUser.CompanyCode,
                UnitCode = tempMasterUser.UnitCode,
                Code = tempMasterUser.Code,
                FirstName = tempMasterUser.FirstName,
                LastName = tempMasterUser.LastName,
                IsActive = mum.IsActive,
                CreatedDate = tempMasterUser.CreatedDate,
                CreatedBy = tempMasterUser.CreatedBy,
                IsDeleted = mum.IsDeleted,
                Email = mum.Email,
                Password = tempMasterUser.Password,
                Salt = tempMasterUser.Salt,
                MobileNo = mum.MobileNo,
                EmailVerified = tempMasterUser.EmailVerified,
                MobileVerified = tempMasterUser.MobileVerified,
                EmailNewLetter = mum.EmailNewLetter,
                MobileAlert = mum.MobileAlert,
                UnitRights = mum.UnitRights,
                Role = mum.Role,
                BaseUnit = mum.BaseUnit,
                DashboardUnits = mum.DashboardUnits,
                EntryAllowedSeasons = mum.EntryAllowedSeasons,
                ViewAllowedForSeasons = mum.ViewAllowedForSeasons,
                ModificationAllowedForSeasons = mum.ModificationAllowedForSeasons

            };

            MasterUserRepository repo = new MasterUserRepository();
            bool result = repo.updateUser(masterUser);
            if (!result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("Details", "User", new { id = mum.Code });

        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                TempData["ErrorTitle"] = "User Code can't be null";
                TempData["ErrorMessage"] = "It seems you are searching for a user invalid user code!";
                return View("error");
            }
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            return View(GetUserByCode(id));
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Delete(string id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetUnitList();
            return View(GetUserByCode(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult DeletePost(string id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (!MasterUserRepository.deleteUser(id))
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorTitle"] = "Failed to delete User";
                TempData["ErrorMessage"] = ex.Message;
                return View("Error");
            }

        }


        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult ChangePassword(string id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterUser masterUser = new MasterUser();
            masterUser = MasterUserRepository.FindByPk(id);
            MasterUserPasswordModel model = new MasterUserPasswordModel()
            {
                Code = masterUser.Code,
                Password = masterUser.Password,
                Salt = masterUser.Salt
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "ChangePassword")]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult ChangePasswordPost(MasterUserPasswordModel model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            CryptographyRepository crypto = new CryptographyRepository();
            if (!ModelState.IsValid)
            {
                return View();
            }
            string tempSalt = crypto.GenerateSalt();
            string tempPassword = crypto.GenerateHashedString(model.Password, tempSalt);

            MasterUser masterUser = new MasterUser()
            {
                Code = model.Code,
                Password = tempPassword,
                Salt = tempSalt
            };
            if (!MasterUserRepository.changePassword(masterUser))
            {
                return View("Error");
            }
            return RedirectToAction("Index");



        }


        /// A method to return MasterUserModel, since there are multiple methods which
        /// require MasterUserModel, so that a single function is created.
        /// Ravi Bhushan - 02-Aug-2019 16:06
        [NonAction]
        private Models.MasterUserModel GetUserByCode(string id)
        {
            MasterUser masterUser = MasterUserRepository.FindByPk(id);
            Models.MasterUserModel userModel = new MasterUserModel()
            {
                Id = masterUser.Id,
                CompanyCode = masterUser.CompanyCode,
                UnitCode = masterUser.UnitCode,
                Code = masterUser.Code,
                FirstName = masterUser.FirstName,
                LastName = masterUser.LastName,
                IsActive = masterUser.IsActive,
                CreatedDate = masterUser.CreatedDate,
                CreatedBy = masterUser.CreatedBy,
                IsDeleted = masterUser.IsDeleted,
                Email = masterUser.Email,
                MobileNo = masterUser.MobileNo,
                EmailVerified = masterUser.EmailVerified,
                MobileVerified = masterUser.MobileVerified,
                EmailNewLetter = masterUser.EmailNewLetter,
                MobileAlert = masterUser.MobileAlert,
                UnitRights = masterUser.UnitRights,
                Role = masterUser.Role,
                BaseUnit = masterUser.BaseUnit
            };
            return userModel;
        }

        [NonAction]
        private void GetUnitList()
        {
            MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
            ViewBag.UnitList = masterUnitRepository.GetMasterUnitList();
        }

        [NonAction]
        private void GetRolesList()
        {
            MasterRoleRepository roleRepository = new MasterRoleRepository();
            ViewBag.MasterRolesList = roleRepository.GetMasterRolesList();
        }


    }
}