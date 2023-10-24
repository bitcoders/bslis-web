using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace LitmusWeb.Controllers
{
    [CustomAuthorizationFilter("Super Admin", "Mobile Device Admin")]
    public class RegisteredDevicesController : Controller
    {
        RegisteredDevicesRepository devicesRepository = new RegisteredDevicesRepository();
        CryptographyRepository cryptoRepository = new CryptographyRepository();

        // GET: RegisteredDevices
        [HttpGet]
        public ActionResult Index()
        {

            List<RegisteredDevice> registeredDevice = new List<RegisteredDevice>();
            registeredDevice = devicesRepository.GetRegisteredDevicesList();
            if (registeredDevice == null)
            {
                return View();
            }
            List<RegisteredDevicesModel> model = new List<RegisteredDevicesModel>();
            foreach (var item in registeredDevice)
            {
                RegisteredDevicesModel temp = new RegisteredDevicesModel()
                {
                    Code = item.Code,
                    Gender = item.Gender,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    ImageUrl = item.ImageUrl,
                    UserCode = item.UserCode,
                    UserPassword = item.UserPassword,
                    Salt = item.Salt,
                    DeviceTypeCode = item.DeviceTypeCode,
                    DeviceToken = item.DeviceToken,
                    AccessToken = item.AccessToken,
                    UnitRights = item.UnitRights,
                    MenuRights = item.MenuRights,
                    IsActive = item.IsActive,
                    ValidFrom = item.ValidFrom,
                    ValidTill = item.ValidTill,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    DepartmentCode = item.DepartmentCode,
                    DeviceType = item.MasterDeviceType.DeviceType,
                    DepartmentName = item.MasterDepartment.Name,
                    Email = item.Email

                };
                model.Add(temp);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            GetDepartmentList();
            NameInitialList();
            RegisteredDevicesModel model = new RegisteredDevicesModel();
            return View(model);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        ///Register new device/User for Mobile App
        public ActionResult AddPost(RegisteredDevicesModel model)
        {
            ///return to login page if session user code is null
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetDepartmentList();
            NameInitialList();
            // return to same page if model is empty or null
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }

            string saltString = cryptoRepository.GenerateSalt(30);
            RegisteredDevice registeredDevice = new RegisteredDevice()
            {
                Code = model.Code,
                Gender = model.Gender,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                UserCode = model.UserCode,
                UserPassword = model.UserPassword,
                DeviceTypeCode = model.DeviceTypeCode,
                DeviceToken = model.DeviceToken,
                AccessToken = model.AccessToken,
                UnitRights = model.UnitRights,
                MenuRights = model.MenuRights,
                IsActive = model.IsActive,
                ValidFrom = model.ValidFrom,
                ValidTill = model.ValidTill,
                CreatedAt = DateTime.Now,
                CreatedBy = Session["UserCode"].ToString(),
                DepartmentCode = model.DepartmentCode,
                Email = model.Email,
                Salt = saltString
            };
            if (!devicesRepository.Add(registeredDevice))
            {
                return View("Error");
            }
            EmailRepository emailRepository = new EmailRepository();
            string emailResult = emailRepository.SendDeviceRegistrationMail(model.Email, model.LastName, model.UserCode, model.UserPassword).Result;
            return RedirectToAction("Index");

        }
        [HttpGet]
        //Edit existing Registered User or device 
        public ActionResult Edit(string id)
        {

            ///return to login page if session user code is null
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetDepartmentList();
            NameInitialList();
            RegisteredDevice device = new RegisteredDevice();
            device = devicesRepository.FindRegisteredDeviceByCode(id);
            if (device != null)
            {
                RegisteredDevicesUpdateModel model = new RegisteredDevicesUpdateModel()
                {
                    Code = device.Code,
                    Gender = device.Gender,
                    FirstName = device.FirstName,
                    LastName = device.LastName,
                    ImageUrl = device.ImageUrl,
                    UserCode = device.UserCode,
                    DeviceTypeCode = device.DeviceTypeCode,
                    UnitRights = device.UnitRights,
                    MenuRights = device.MenuRights,
                    IsActive = device.IsActive,
                    ValidFrom = device.ValidFrom,
                    ValidTill = device.ValidTill,
                    DepartmentCode = device.DepartmentCode,
                    Email = device.Email
                };
                return View(model);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        public ActionResult EditPost(RegisteredDevicesUpdateModel model)
        {

            ///return to login page if session user code is null
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            GetDepartmentList();
            NameInitialList();
            // return to same page if model is empty or null
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }

            RegisteredDevice registeredDevice = new RegisteredDevice()
            {
                Code = model.Code,
                Gender = model.Gender,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                DeviceTypeCode = model.DeviceTypeCode,
                AccessToken = model.AccessToken,
                UnitRights = model.UnitRights,
                MenuRights = model.MenuRights,
                IsActive = model.IsActive,
                ValidFrom = model.ValidFrom,
                ValidTill = model.ValidTill,
                DepartmentCode = model.DepartmentCode,
                Email = model.Email
            };
            if (!devicesRepository.Update(registeredDevice))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ActionName(name: "Delete")]
        public ActionResult DetailsPost(int id)
        {
            return View();
        }

        [NonAction]
        private void GetUnitDetails()
        {
            MasterUnitRepository unitRepository = new MasterUnitRepository();
            List<MasterUnit> masterUnit = new List<MasterUnit>();
            masterUnit = unitRepository.GetMasterUnitList();
            List<MasterUnitApiUnitRights> masterUnitApiUnitRights = new List<MasterUnitApiUnitRights>();
            foreach (var item in masterUnit)
            {
                MasterUnitApiUnitRights temp = new MasterUnitApiUnitRights()
                {
                    Code = item.Code,
                    Name = item.Name
                };
                masterUnitApiUnitRights.Add(temp);
            }
            ViewBag.UnitList = masterUnitApiUnitRights;
        }
        [NonAction]
        private void AndroidMenu()
        {
            AndroidMenueRepository androidMenueRepository = new AndroidMenueRepository();
            List<AndroidMenue> androidMenues = new List<AndroidMenue>();
            androidMenues = androidMenueRepository.AndroidMenues();
            List<AndroidMenuesModel> androidMenuesModels = new List<AndroidMenuesModel>();
            if (androidMenues.Count <= 0)
            {
                ViewBag.AndroidMenuList = "";
            }
            else
            {
                foreach (var item in androidMenues)
                {
                    AndroidMenuesModel temp = new AndroidMenuesModel()
                    {
                        Code = item.Code,
                        Name = item.Name,
                    };
                    androidMenuesModels.Add(temp);
                }
                ViewBag.AndroidMenuList = androidMenuesModels;
            }
        }

        private void GetDepartmentList()
        {

            MasterDepartmentRepository masterDepartmentRepository = new MasterDepartmentRepository();
            List<MasterDepartment> masterDepartment = new List<MasterDepartment>();
            masterDepartment = masterDepartmentRepository.GetMasterDepartmentList();
            if (masterDepartment.Count <= 0)
            {
                ViewBag.DepartmentList = "";
            }
            else
            {
                List<MasterDepartmentModel> masterDepartmentModels = new List<MasterDepartmentModel>();
                foreach (var item in masterDepartment)
                {
                    MasterDepartmentModel temp = new MasterDepartmentModel()
                    {
                        Code = item.Code,
                        Name = item.Name
                    };
                    masterDepartmentModels.Add(temp);
                }
                ViewBag.DepartmentList = masterDepartmentModels;
            }

        }

        private void NameInitialList()
        {
            List<string> Initials = new List<string>();
            Initials.Add("Mr.");
            Initials.Add("Mrs.");
            Initials.Add("Miss.");
            ViewBag.NameInitials = Initials;
        }
    }
}