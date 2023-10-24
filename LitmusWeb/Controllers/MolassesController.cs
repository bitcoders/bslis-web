using DataAccess;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace LitmusWeb.Controllers
{
    public class MolassesController : Controller
    {
        // GET: Molasses
        MolassesAnalysisRepository Repository = new MolassesAnalysisRepository();

        [HttpGet]
        [MolassesFilter]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            List<MolassesAnalys> Entity = new List<MolassesAnalys>();
            Entity = Repository.GetMolassesListByDate(
                Convert.ToDateTime(ViewBag.EntryDate),
                Convert.ToInt16(ViewBag.BaseUnit)
                );

            List<MolassesAnalysModel> Model = new List<MolassesAnalysModel>();
            foreach (var i in Entity)
            {
                MolassesAnalysModel Temp = new MolassesAnalysModel()
                {
                    id = i.id,
                    Unit_Code = i.Unit_Code,
                    season_code = i.season_code,
                    mo_entry_date = i.mo_entry_date,
                    mo_entry_time = i.mo_entry_time,
                    mo_code = i.mo_code,
                    molasses_name = i.MasterParameterSubCategory.Name,
                    mo_brix = i.mo_brix,
                    mo_pol = i.mo_pol,
                    mo_purity = i.mo_purity,
                    mo_icumsa_unit = i.mo_icumsa_unit,
                    mo_crtd_date = i.mo_crtd_date,
                    mo_crtd_by = i.mo_crtd_by,
                    mo_updt_dt = i.mo_updt_dt,
                    mo_updt_by = i.mo_updt_by
                };
                Model.Add(Temp);
            }
            return View(Model);
        }

        [HttpGet]
        [MolassesFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Molasses Create")]
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
        [MolassesFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Molasses Create")]
        [ValidationFilter("Create")]
        public ActionResult CreatePost(MolassesAnalysModel molassesAnalysisModel)
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
            MolassesAnalys Entity = new MolassesAnalys()
            {

                Unit_Code = ViewBag.BaseUnit,
                season_code = ViewBag.CrushingSeason,
                mo_entry_date = Convert.ToDateTime(ViewBag.EntryDate),
                mo_entry_time = molassesAnalysisModel.mo_entry_time,
                mo_code = molassesAnalysisModel.mo_code,
                mo_brix = molassesAnalysisModel.mo_brix,
                mo_pol = molassesAnalysisModel.mo_pol,
                mo_purity = molassesAnalysisModel.mo_purity,
                mo_icumsa_unit = molassesAnalysisModel.mo_icumsa_unit,
                mo_crtd_date = Convert.ToDateTime(DateTime.Now),
                mo_crtd_by = Session["UserCode"].ToString()
            };
            bool result = Repository.AddMolasses(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [MolassesFilter]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Molasses Edit")]
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

            var Entity = Repository.GetMolassesDetailsById(id, BaseUnit, CrushingSeason, EntryDate);

            TempData["Id"] = Entity.id;
            TempData["BaseUnit"] = Entity.Unit_Code;
            TempData["CrushingSeason"] = Entity.season_code;
            TempData["EntryDate"] = Entity.mo_entry_date;
            TempData["CreatedBy"] = Entity.mo_crtd_by;
            TempData["CreatedDate"] = Entity.mo_crtd_date;


            MolassesAnalysModel Model = new MolassesAnalysModel()
            {
                id = Entity.id,
                Unit_Code = Entity.Unit_Code,
                season_code = Entity.season_code,
                mo_entry_date = Entity.mo_entry_date,
                mo_entry_time = Entity.mo_entry_time,
                mo_code = Entity.mo_code,
                mo_brix = Entity.mo_brix,
                mo_pol = Entity.mo_pol,
                mo_purity = Entity.mo_purity,
                mo_icumsa_unit = Entity.mo_icumsa_unit,
                mo_crtd_date = Entity.mo_crtd_date,
                mo_crtd_by = Entity.mo_crtd_by,
            };
            return View(Model);
        }

        [HttpPost]
        [MolassesFilter]
        [ActionName(name: "Edit")]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Molasses Edit")]
        [ValidateAntiForgeryToken]
        [ValidationFilter("update")]
        public ActionResult Edit(MolassesAnalysModel Model)
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
            MolassesAnalys Entity = new MolassesAnalys()
            {
                id = Convert.ToInt32(TempData["Id"]),
                Unit_Code = Convert.ToInt16(TempData["BaseUnit"]),
                season_code = Convert.ToInt32(TempData["CrushingSeason"]),
                mo_entry_date = DateTime.Parse(TempData["EntryDate"].ToString()),
                mo_entry_time = Model.mo_entry_time,
                mo_code = Model.mo_code,
                mo_brix = Model.mo_brix,
                mo_pol = Model.mo_pol,
                mo_purity = Model.mo_purity,
                mo_icumsa_unit = Model.mo_icumsa_unit,
                mo_crtd_date = DateTime.Parse(TempData["CreatedDate"].ToString()),
                mo_crtd_by = TempData["CreatedBy"].ToString(),
                mo_updt_by = Session["UserCode"].ToString(),
                mo_updt_dt = DateTime.Now,
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