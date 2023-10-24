using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UnitController : Controller
    {
        MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
        // GET: Unit
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterUnit> mu = masterUnitRepository.GetMasterUnitList();
            List<MasterUnitModel> muModel = new List<MasterUnitModel>();
            if (mu == null)
            {
                return View();
            }

            foreach (var item in mu)
            {
                Models.MasterUnitModel temp = new MasterUnitModel()
                {
                    CompanyCode = item.CompanyCode,
                    Code = item.Code,
                    Name = item.Name,
                    Address = item.Address,
                    CrushingSeason = item.CrushingSeason,
                    ReportStartTime = item.ReportStartTime,
                    CrushingStartDate = item.CrushingStartDate,
                    CrushingEndDate = item.CrushingEndDate,
                    DayHours = item.DayHours,
                    EntryDate = item.EntryDate,
                    ProcessDate = item.ProcessDate,
                    NewMillCapacity = Convert.ToInt32(item.NewMillCapacity),
                    OldMillCapacity = Convert.ToInt32(item.OldMillCapacity),
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    Createdy = item.Createdy,
                    CrushingStartTime = item.CrushingStartTime,
                    CrushingStartDateTime = item.CrushingStartDateTime,
                    CrushingEndDateTime = item.CrushingEndDateTime,
                    ReportStartHourMinute = item.ReportStartHourMinute,
                    AllowedModificationDays = item.AllowedModificationDays
                };
                muModel.Add(temp);
            }
            return View(muModel);
        }

        [CustomAuthorizationFilter("Super Admin", "Developer")]
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterCompanyRepository masterCompanyRepository = new MasterCompanyRepository();
            ViewBag.CompanyList = masterCompanyRepository.GetMasterCompaniesList();
            MasterUnitModel model = new MasterUnitModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult Create(MasterUnitModel mum)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                DataAccess.MasterUnit masterUnit = new MasterUnit()
                {
                    CompanyCode = mum.CompanyCode,
                    Code = mum.Code,
                    Name = mum.Name,
                    Address = mum.Address,
                    CrushingSeason = mum.CrushingSeason,
                    ReportStartTime = mum.ReportStartTime,
                    CrushingStartDate = mum.CrushingStartDate,
                    CrushingEndDate = mum.CrushingEndDate,
                    DayHours = mum.DayHours,
                    EntryDate = mum.EntryDate,
                    ProcessDate = mum.ProcessDate,
                    NewMillCapacity = mum.NewMillCapacity,
                    OldMillCapacity = mum.OldMillCapacity,
                    IsActive = mum.IsActive,
                    CreatedDate = mum.CreatedDate,
                    Createdy = mum.Createdy,
                    CrushingStartTime = mum.CrushingStartTime,
                    CrushingEndTime = mum.CrushingEndTime,
                    CrushingStartDateTime = mum.CrushingStartDateTime,
                    CrushingEndDateTime = mum.CrushingEndDateTime,
                    ReportStartHourMinute = mum.ReportStartHourMinute
                };
                bool result = masterUnitRepository.AddMasterUnit(masterUnit);
                if (!result)
                {
                    return RedirectToAction("Error"); ;
                }
                return RedirectToAction("Index", "Unit");
            }
            return View();

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            MasterUnit mu = masterUnitRepository.FindUnitByPk(id);
            Models.MasterUnitModel mum = new MasterUnitModel()
            {
                Id = mu.Id,
                CompanyCode = mu.CompanyCode,
                Code = mu.Code,
                Name = mu.Name,
                Address = mu.Address,
                CrushingSeason = mu.CrushingSeason,
                ReportStartTime = mu.ReportStartTime,
                CrushingStartDate = mu.CrushingStartDate,
                CrushingEndDate = mu.CrushingEndDate,
                DayHours = mu.DayHours,
                EntryDate = mu.EntryDate,
                ProcessDate = mu.ProcessDate,
                NewMillCapacity = mu.NewMillCapacity,
                OldMillCapacity = mu.OldMillCapacity,
                AllowedModificationDays = mu.AllowedModificationDays,
                CrushingStartTime = mu.CrushingStartTime,
                CrushingEndTime = mu.CrushingEndTime,
                CrushingStartDateTime = mu.CrushingStartDateTime,
                CrushingEndDateTime = mu.CrushingEndDateTime,
                ReportStartHourMinute = mu.ReportStartHourMinute,
                IsActive = mu.IsActive,
                CreatedDate = mu.CreatedDate,
                Createdy = mu.Createdy,

            };

            return View(mum);
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterUnitRepository mur = new MasterUnitRepository();
            MasterUnit mu = mur.FindUnitByPk(id);
            Models.MasterUnitModel mum = new MasterUnitModel()
            {
                Id = mu.Id,
                CompanyCode = mu.CompanyCode,
                Code = mu.Code,
                Name = mu.Name,
                Address = mu.Address,
                CrushingSeason = mu.CrushingSeason,
                ReportStartTime = mu.ReportStartTime,
                CrushingStartDate = mu.CrushingStartDate,
                CrushingEndDate = mu.CrushingEndDate,
                DayHours = mu.DayHours,
                EntryDate = mu.EntryDate,
                ProcessDate = mu.ProcessDate,
                NewMillCapacity = mu.NewMillCapacity,
                OldMillCapacity = mu.OldMillCapacity,
                AllowedModificationDays = mu.AllowedModificationDays,
                IsActive = mu.IsActive,
                CreatedDate = mu.CreatedDate,
                Createdy = mu.Createdy,
                CrushingStartTime = mu.CrushingStartTime,
                CrushingEndTime = mu.CrushingEndTime,
                CrushingStartDateTime = mu.CrushingStartDateTime,
                CrushingEndDateTime = mu.CrushingEndDateTime,
                ReportStartHourMinute = mu.ReportStartHourMinute
            };


            return View(mum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult EditPost(Models.MasterUnitModel mum)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            //MasterUnit unit = masterUnitRepository.FindUnitByPk(mum.Code);
            //if (ModelState.IsValid)
            //{
            //    unit.Id = unit.Id;
            //    unit.CompanyCode = mum.CompanyCode;
            //    unit.Code = unit.Code;
            //    unit.Name = mum.Name;

            //    unit.Address = mum.Address;
            //    unit.CrushingSeason = mum.CrushingSeason;
            //    unit.CrushingStartDate = mum.CrushingStartDate;
            //    unit.CrushingEndDate = mum.CrushingEndDate;
            //    unit.DayHours = mum.DayHours;
            //    unit.EntryDate = mum.EntryDate;
            //    unit.ProcessDate = mum.ProcessDate;
            //    unit.NewMillCapacity = mum.NewMillCapacity;
            //    unit.OldMillCapacity = mum.OldMillCapacity;
            //    unit.IsActive = mum.IsActive;
            //    unit.CreatedDate = unit.CreatedDate;
            //    unit.Createdy = unit.Createdy;
            //    unit.ReportStartTime = mum.ReportStartTime;
            //    unit.CrushingStartTime = mum.CrushingStartTime;
            //    unit.CrushingEndTime = mum.CrushingEndTime;
            //    unit.AllowedModificationDays = mum.AllowedModificationDays;
            //    unit.UpdatedBy = Session["UserCode"].ToString();
            //    unit.UpdatedDate = DateTime.Now;

            //    bool result = masterUnitRepository.UpdateMasterUnit(unit);
            //    if (!result)
            //    {
            //        return View("Error");
            //    }
            //}
            if (ModelState.IsValid)
            {
                MasterUnit unit = new MasterUnit()
                {
                    Code = mum.Code,
                    Name = mum.Name,
                    Address = mum.Address,
                    CrushingSeason = mum.CrushingSeason,
                    CrushingStartDate = mum.CrushingStartDateTime.Value.Date,
                    CrushingEndDate = mum.CrushingEndDateTime.Value.Date,
                    DayHours = mum.DayHours,
                    EntryDate = mum.EntryDate,
                    ProcessDate = mum.ProcessDate,
                    NewMillCapacity = mum.NewMillCapacity,
                    OldMillCapacity = mum.OldMillCapacity,
                    IsActive = mum.IsActive,
                    CreatedDate = mum.CreatedDate,
                    Createdy = mum.Createdy,
                    ReportStartTime = mum.ReportStartTime,
                    CrushingStartTime = mum.CrushingStartTime,
                    CrushingEndTime = mum.CrushingEndTime,
                    AllowedModificationDays = mum.AllowedModificationDays,
                    CrushingStartDateTime = mum.CrushingStartDateTime,
                    CrushingEndDateTime = mum.CrushingEndDateTime,
                    ReportStartHourMinute = mum.ReportStartHourMinute,
                    UpdatedBy = Session["UserCode"].ToString(),
                    UpdatedDate = DateTime.Now
                };
                bool result = masterUnitRepository.UpdateMasterUnit(unit);
                if (!result)
                {
                    return View("Error");
                }
            }
            return RedirectToAction("Details", "Unit", new { id = mum.Code });
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            return View(GetMasterUnitByCode(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult DeleteUnit(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool result = masterUnitRepository.DeleteMasterUnit(id);
            if (result != true)
            {
                return Redirect("Error");
            }
            return RedirectToAction("Index", "Unit");
        }
        [NonAction]
        private MasterUnitModel GetMasterUnitByCode(int id)
        {
            MasterUnitRepository mur = new MasterUnitRepository();
            MasterUnit mu = mur.FindUnitByPk(id);
            Models.MasterUnitModel mum = new MasterUnitModel()
            {
                Id = mu.Id,
                Code = mu.Code,
                Name = mu.Name,
                Address = mu.Address,
                CrushingSeason = mu.CrushingSeason,
                ReportStartTime = mu.ReportStartTime,
                CrushingStartDate = mu.CrushingStartDate,
                CrushingEndDate = mu.CrushingEndDate,
                DayHours = mu.DayHours,
                EntryDate = mu.EntryDate,
                ProcessDate = mu.ProcessDate,
                NewMillCapacity = mu.NewMillCapacity,
                OldMillCapacity = mu.OldMillCapacity,
                AllowedModificationDays = mu.AllowedModificationDays,
                CrushingStartTime = mu.CrushingStartTime,
                CrushingEndTime = mu.CrushingEndTime,
                CrushingStartDateTime = mu.CrushingStartDateTime,
                CrushingEndDateTime = mu.CrushingEndDateTime,
                ReportStartHourMinute = mu.ReportStartHourMinute,
                IsActive = mu.IsActive,
                CreatedDate = mu.CreatedDate,
                Createdy = mu.Createdy
            };
            return mum;
        }
    }
}