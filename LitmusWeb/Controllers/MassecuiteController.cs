using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class MassecuiteController : Controller
    {
        MassecuiteAnalysisRepository Repository = new MassecuiteAnalysisRepository();
        // GET: Massecuite
        [HttpGet]
        [MassecuiteFilter]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MassecuiteAnalys> Entity = new List<MassecuiteAnalys>();

            //getting the list of Massecuite Analysis based on current parameter for the unit and user
            //24-08-2019 22:06
            Entity = Repository.GetMassecuiteDetails(
                Convert.ToInt16(ViewBag.BaseUnit)
                , Convert.ToInt16(ViewBag.CrushingSeason)
                , Convert.ToDateTime(ViewBag.EntryDate)
                );
            List<MassecuiteAnalysModel> Model = new List<MassecuiteAnalysModel>();
            foreach (var i in Entity)
            {
                MassecuiteAnalysModel temp = new MassecuiteAnalysModel();
                temp.id = i.id;
                temp.unit_code = i.unit_code;
                temp.season_code = i.season_code;
                temp.m_param_type_code = i.m_param_type_code;
                temp.MasterCategoryName = i.MasterParameterSubCategory.Name;
                temp.m_entry_date = i.m_entry_date;
                temp.m_entry_time = i.m_entry_time;
                temp.m_hl = i.m_hl;
                temp.m_pan_no = i.m_pan_no;
                temp.m_start_time = i.m_start_time;
                temp.m_drop_time = i.m_drop_time;
                temp.m_crystal_no = i.m_crystal_no;
                temp.m_brix = i.m_brix;
                temp.m_pol = i.m_pol;
                temp.m_purity = i.m_purity;
                temp.crtd_dt = i.crtd_dt;
                temp.crtd_by = i.crtd_by;
                temp.m_icumsa_unit = i.m_icumsa_unit;
                Model.Add(temp);
            }
            return View(Model);
        }

        [HttpGet]
        [MassecuiteFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Massecuite Create")]
        [ValidationFilter("Create")]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [MassecuiteFilter]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Massecuite Create")]
        [ValidationFilter("Create")]
        public ActionResult Create(MassecuiteAnalysModel Model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            MassecuiteAnalys Entity = new MassecuiteAnalys()
            {
                unit_code = ViewBag.BaseUnit,
                season_code = ViewBag.CrushingSeason,
                m_param_type_code = Model.m_param_type_code,
                m_entry_date = Convert.ToDateTime(ViewBag.EntryDate),
                m_entry_time = Model.m_entry_time,
                m_hl = Model.m_hl,
                m_pan_no = Model.m_pan_no,
                m_start_time = Model.m_start_time,
                m_drop_time = Model.m_drop_time,
                m_crystal_no = Model.m_crystal_no,
                m_brix = Model.m_brix,
                m_pol = Model.m_pol,
                m_purity = Model.m_purity,
                m_icumsa_unit = Model.m_icumsa_unit,
                crtd_dt = Convert.ToDateTime(DateTime.Now),
                crtd_by = Session["UserCode"].ToString()
            };
            bool result = Repository.AddMassecuite(Entity);
            if (result != true)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        [MassecuiteFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Massecuite Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MassecuiteAnalys Entity = new MassecuiteAnalys();
            int BaseUnitCode = Convert.ToInt16(ViewBag.BaseUnit);
            int CrushingSeason = Convert.ToInt16(ViewBag.CrushingSeason);
            Entity = Repository.GetMassecuiteDetailsById(id, BaseUnitCode, CrushingSeason);
            if (Entity == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            TempData["CreatedDate"] = Entity.crtd_dt;
            TempData["CreatedBy"] = Entity.crtd_by;
            TempData["EntryDate"] = Entity.m_entry_date;
            TempData["EntryTime"] = Entity.m_entry_time;
            TempData["UnitCode"] = Entity.unit_code;
            TempData["CrushingSeason"] = Entity.season_code;
            TempData["id"] = Entity.id;
            MassecuiteAnalysModel massecuiteAnalysisModel = new MassecuiteAnalysModel()
            {
                unit_code = Entity.unit_code,
                season_code = Entity.season_code,
                m_param_type_code = Entity.m_param_type_code,
                m_entry_date = Entity.m_entry_date,
                m_entry_time = Entity.m_entry_time,
                m_hl = Entity.m_hl,
                m_pan_no = Entity.m_pan_no,
                m_start_time = Entity.m_start_time,
                m_drop_time = Entity.m_drop_time,
                m_crystal_no = Entity.m_crystal_no,
                m_brix = Entity.m_brix,
                m_pol = Entity.m_pol,
                m_purity = Entity.m_purity,
                m_icumsa_unit = Entity.m_icumsa_unit,
                crtd_dt = Entity.crtd_dt,
                crtd_by = Entity.crtd_by
            };
            return View(massecuiteAnalysisModel);
        }

        [HttpPost]
        [MassecuiteFilter]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Edit")]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Massecuite Edit")]
        [ValidationFilter("update")]
        public ActionResult EditPost(MassecuiteAnalysModel Model)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.CreatedDate = Model.crtd_dt;
            ViewBag.CreatedBy = Model.crtd_by;
            MassecuiteAnalys Entity = new MassecuiteAnalys()
            {
                id = Convert.ToInt64(TempData["id"]),
                unit_code = Convert.ToInt16(TempData["UnitCode"]),
                season_code = Convert.ToInt16(TempData["CrushingSeason"]),
                m_param_type_code = Model.m_param_type_code,
                m_entry_date = Convert.ToDateTime(TempData["EntryDate"]),
                m_entry_time = TimeSpan.Parse(TempData["EntryTime"].ToString()),
                m_hl = Model.m_hl,
                m_pan_no = Model.m_pan_no,
                m_start_time = Model.m_start_time,
                m_drop_time = Model.m_drop_time,
                m_crystal_no = Model.m_crystal_no,
                m_brix = Model.m_brix,
                m_pol = Model.m_pol,
                m_purity = Model.m_purity,
                m_icumsa_unit = Model.m_icumsa_unit,
                crtd_dt = Convert.ToDateTime(TempData["CreatedDate"]),
                crtd_by = TempData["CreatedBy"].ToString(),
            };
            bool result = Repository.Edit(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}