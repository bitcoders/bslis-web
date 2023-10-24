using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    public class MeltingController : Controller
    {
        readonly MeltingAnalysisRepository Repository = new MeltingAnalysisRepository();
        // GET: Melting
        [HttpGet]
        [MeltingsFilter]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            List<MeltingAnalys> Entity = new List<MeltingAnalys>();
            Entity = Repository.GetMeltingAnalysesListByDate(
                Convert.ToDateTime(ViewBag.EntryDate),
                Convert.ToInt16(ViewBag.BaseUnit)
                );

            List<MeltingAnalysModel> Model = new List<MeltingAnalysModel>();
            foreach (var i in Entity)
            {
                MeltingAnalysModel Temp = new MeltingAnalysModel()
                {
                    id = i.id,
                    unit_code = i.unit_code,
                    season_code = i.season_code,
                    entry_date = i.entry_date,
                    entry_time = i.entry_time,
                    m_code = i.m_code,
                    melting_name = i.MasterParameterSubCategory.Name,
                    brix = i.brix,
                    pol = i.pol,
                    purity = i.purity,
                    icumsa_unit = i.icumsa_unit,
                    crtd_dt = i.crtd_dt,
                    crtd_by = i.crtd_by,
                    updt_dt = i.updt_dt,
                    updt_by = i.updt_by
                };
                Model.Add(Temp);
            }
            return View(Model);
        }

        [HttpGet]
        [MeltingsFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Meltings Create")]
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
        [ActionName(name: "Create")]
        [MeltingsFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Meltings Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(MeltingAnalysModel meltingAnalysisModel)
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
            MeltingAnalys Entity = new MeltingAnalys()
            {
                unit_code = ViewBag.BaseUnit,
                season_code = ViewBag.CrushingSeason,
                entry_date = Convert.ToDateTime(ViewBag.EntryDate),
                entry_time = meltingAnalysisModel.entry_time,
                m_code = meltingAnalysisModel.m_code,
                brix = meltingAnalysisModel.brix,
                pol = meltingAnalysisModel.pol,
                purity = meltingAnalysisModel.purity,
                icumsa_unit = meltingAnalysisModel.icumsa_unit,
                crtd_dt = Convert.ToDateTime(DateTime.Now),
                crtd_by = Session["UserCode"].ToString()
            };
            bool result = Repository.AddMeltingAnalysis(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [MeltingsFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Meltings Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            int BaseUnit = Convert.ToInt32(ViewBag.BaseUnit);
            int CrushingSeason = Convert.ToInt16(ViewBag.CrushingSeason);
            DateTime EntryDate = Convert.ToDateTime(ViewBag.EntryDate);

            var Entity = Repository.GetMeltingAnalysisById(id, BaseUnit, CrushingSeason, EntryDate);

            TempData["Id"] = Entity.id;
            TempData["BaseUnit"] = Entity.unit_code;
            TempData["CrushingSeason"] = Entity.season_code;
            TempData["EntryDate"] = Entity.entry_date;
            TempData["CreatedBy"] = Entity.crtd_by;
            TempData["CreatedDate"] = Entity.crtd_dt;


            MeltingAnalysModel Model = new MeltingAnalysModel()
            {
                id = Entity.id,
                unit_code = Entity.unit_code,
                season_code = Entity.season_code,
                entry_date = Entity.entry_date,
                entry_time = Entity.entry_time,
                m_code = Entity.m_code,
                brix = Entity.brix,
                pol = Entity.pol,
                purity = Entity.purity,
                icumsa_unit = Entity.icumsa_unit,
                crtd_dt = Entity.crtd_dt,
                crtd_by = Entity.crtd_by,
            };
            return View(Model);
        }

        [HttpPost]
        [MeltingsFilter]
        [ActionName(name: "Edit")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Meltings Edit")]
        [ValidationFilter("update")]
        public ActionResult Edit(MeltingAnalysModel Model)
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
            MeltingAnalys Entity = new MeltingAnalys()
            {
                id = Convert.ToInt32(TempData["Id"]),
                unit_code = Convert.ToInt16(TempData["BaseUnit"]),
                season_code = Convert.ToInt32(TempData["CrushingSeason"]),
                entry_date = DateTime.Parse(TempData["EntryDate"].ToString()),
                entry_time = Model.entry_time,
                m_code = Model.m_code,
                brix = Model.brix,
                pol = Model.pol,
                purity = Model.purity,
                icumsa_unit = Model.icumsa_unit,
                crtd_dt = DateTime.Parse(TempData["CreatedDate"].ToString()),
                crtd_by = TempData["CreatedBy"].ToString(),
                updt_by = Session["UserCode"].ToString(),
                updt_dt = DateTime.Now,
            };
            bool result = Repository.EditMelingAnalysis(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}