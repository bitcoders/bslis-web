using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{

    public class DataAdjustmentController : Controller
    {

        DataAdjustmentRepository Repository = new DataAdjustmentRepository();
        // GET: DataAdjustment

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("create")]
        public ActionResult Add()
        {
            SetUnitDefaultValues();
            DataAdjustmentsModel model = new DataAdjustmentsModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ActionName("Add")]
        [ValidationFilter("Create")]
        public ActionResult AddPost(DataAdjustmentsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            SetUnitDefaultValues();
            DataAdjustment Entity = new DataAdjustment()
            {
                a_unit_code = Convert.ToInt16(TempData["BaseUnitCode"]),
                a_season_code = ViewBag.CrushingSeason,
                a_entry_date = model.a_entry_date,
                a_sulpher = model.a_sulpher,
                a_lime = model.a_lime,
                a_phosphoric_acid = model.a_phosphoric_acid,
                a_color_reducer = model.a_color_reducer,
                a_megnafloe = model.a_megnafloe,
                a_lub_oil = model.a_lub_oil,
                a_viscosity_reducer = model.a_viscosity_reducer,
                a_biocide = model.a_biocide,
                a_lub_greace = model.a_lub_greace,
                a_boiler_chemical = model.a_boiler_chemical,
                a_estimated_sugar = model.a_estimated_sugar,
                a_estimated_molasses = model.a_estimated_molasses,
                a_washing_soda = model.a_washing_soda,
                a_hydrolic_soda = model.a_hydrolic_soda,
                a_de_scaling_chemical = model.a_de_scaling_chemical,
                a_seed_slurry = model.a_seed_slurry,
                a_anti_fomer = model.a_anti_fomer,
                a_chemical_for_brs_cleaning = model.a_chemical_for_brs_cleaning,
                a_is_active = true,
                crtd_dt = DateTime.Now,
                crtd_by = Session["UserCode"].ToString(),
                EstimatedCaneForSyrupDiversion = model.EstimatedCaneForSyrupDiversion
            };
            bool result = Repository.CreateDataAdjustment(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                return View("Index", "Home");
            }
            SetUnitDefaultValues();
            List<DataAdjustment> DjList = new List<DataAdjustment>();
            DjList = Repository.GetDataAdjustmentByUnit(Convert.ToInt32(Session["BaseUnitCode"]), ViewBag.CrushingSeason);
            if (DjList == null)
            {
                return View("Index");
            }
            List<DataAdjustmentsModel> DjModelList = new List<DataAdjustmentsModel>();
            foreach (var item in DjList)
            {
                DataAdjustmentsModel temp = new DataAdjustmentsModel()
                {
                    id = item.id,
                    a_unit_code = item.a_unit_code,
                    a_season_code = item.a_season_code,
                    a_entry_date = item.a_entry_date,
                    a_sulpher = item.a_sulpher,
                    a_lime = item.a_lime,
                    a_phosphoric_acid = item.a_phosphoric_acid,
                    a_color_reducer = item.a_color_reducer,
                    a_megnafloe = item.a_megnafloe,
                    a_lub_oil = item.a_lub_oil,
                    a_viscosity_reducer = item.a_viscosity_reducer,
                    a_biocide = item.a_biocide,
                    a_lub_greace = item.a_lub_greace,
                    a_boiler_chemical = item.a_boiler_chemical,
                    a_estimated_sugar = item.a_estimated_sugar,
                    a_estimated_molasses = item.a_estimated_molasses,
                    a_washing_soda = item.a_washing_soda,
                    a_hydrolic_soda = item.a_hydrolic_soda,
                    a_de_scaling_chemical = item.a_de_scaling_chemical,
                    a_seed_slurry = item.a_seed_slurry,
                    a_anti_fomer = item.a_anti_fomer,
                    a_chemical_for_brs_cleaning = item.a_chemical_for_brs_cleaning,
                    a_is_active = true,
                    crtd_dt = DateTime.Now,
                    crtd_by = item.crtd_by,
                    EstimatedCaneForSyrupDiversion = item.EstimatedCaneForSyrupDiversion
                };
                DjModelList.Add(temp);
            }
            return View(DjModelList);
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            SetUnitDefaultValues();
            int unitCode = Convert.ToInt32(Session["BaseUnitCode"]);
            int seasonCode = Convert.ToInt32(ViewBag.CrushingSeason);
            DataAdjustment djEntity = new DataAdjustment();
            djEntity = Repository.GetDetailsById(unitCode, seasonCode, id);

            try
            {
                DataAdjustmentsModel djModel = new DataAdjustmentsModel()
                {
                    id = djEntity.id,
                    a_unit_code = djEntity.a_unit_code,
                    a_season_code = djEntity.a_season_code,
                    a_entry_date = djEntity.a_entry_date,
                    a_sulpher = djEntity.a_sulpher,
                    a_lime = djEntity.a_lime,
                    a_phosphoric_acid = djEntity.a_phosphoric_acid,
                    a_color_reducer = djEntity.a_color_reducer,
                    a_megnafloe = djEntity.a_megnafloe,
                    a_lub_oil = djEntity.a_lub_oil,
                    a_viscosity_reducer = djEntity.a_viscosity_reducer,
                    a_biocide = djEntity.a_biocide,
                    a_lub_greace = djEntity.a_lub_greace,
                    a_boiler_chemical = djEntity.a_boiler_chemical,
                    a_estimated_sugar = djEntity.a_estimated_sugar,
                    a_estimated_molasses = djEntity.a_estimated_molasses,
                    a_washing_soda = djEntity.a_washing_soda,
                    a_hydrolic_soda = djEntity.a_hydrolic_soda,
                    a_de_scaling_chemical = djEntity.a_de_scaling_chemical,
                    a_seed_slurry = djEntity.a_seed_slurry,
                    a_anti_fomer = djEntity.a_anti_fomer,
                    a_chemical_for_brs_cleaning = djEntity.a_chemical_for_brs_cleaning,
                    a_is_active = true,
                    crtd_dt = DateTime.Now,
                    crtd_by = djEntity.crtd_by,
                    EstimatedCaneForSyrupDiversion = djEntity.EstimatedCaneForSyrupDiversion
                };
                return View(djModel);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ActionName(name: "Edit")]
        [ValidationFilter("update")]
        public ActionResult EditPost(DataAdjustmentsModel m)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                string errorMessage = "";
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var x in allErrors)
                {
                    errorMessage = errorMessage + " " + x.ErrorMessage + Environment.NewLine;
                }
                TempData["ErrorTitle"] = errorMessage;
                return View("Error");

            }
            SetUnitDefaultValues();
            try
            {
                DataAdjustment dataAdjustment = new DataAdjustment()
                {
                    id = m.id,
                    a_unit_code = Convert.ToInt32(Session["BaseUnitCode"]),
                    a_season_code = Convert.ToInt32(ViewBag.CrushingSeason),
                    a_entry_date = m.a_entry_date,
                    a_sulpher = m.a_sulpher,
                    a_lime = m.a_lime,
                    a_phosphoric_acid = m.a_phosphoric_acid,
                    a_color_reducer = m.a_color_reducer,
                    a_megnafloe = m.a_megnafloe,
                    a_lub_oil = m.a_lub_oil,
                    a_viscosity_reducer = m.a_viscosity_reducer,
                    a_biocide = m.a_biocide,
                    a_lub_greace = m.a_lub_greace,
                    a_boiler_chemical = m.a_boiler_chemical,
                    a_estimated_sugar = m.a_estimated_sugar,
                    a_estimated_molasses = m.a_estimated_molasses,
                    a_washing_soda = m.a_washing_soda,
                    a_hydrolic_soda = m.a_hydrolic_soda,
                    a_de_scaling_chemical = m.a_de_scaling_chemical,
                    a_seed_slurry = m.a_seed_slurry,
                    a_anti_fomer = m.a_anti_fomer,
                    a_chemical_for_brs_cleaning = m.a_chemical_for_brs_cleaning,
                    a_is_active = true,
                    crtd_by = m.crtd_by,
                    crtd_dt = m.crtd_dt,
                    updt_by = Session["UserCode"].ToString(),
                    updt_dt = DateTime.Now,
                    EstimatedCaneForSyrupDiversion = m.EstimatedCaneForSyrupDiversion
                };
                bool result = Repository.EditDataAdjustment(dataAdjustment);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", "DataAdjustment", new { dataAdjustment.id });
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return View("Error");
            }



        }

        [NonAction]
        private void SetUnitDefaultValues()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Index", "Home");
            }
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            MasterStoppageTypeRepository stoppageRepository = new MasterStoppageTypeRepository();

            var UnitDefaultValues = UnitRepository.FindUnitByPk(Convert.ToInt16(Session["BaseUnitCode"]));
            var MasterStoppageTypes = stoppageRepository.GetMasterStoppageTypeList();

            int cropDays = Convert.ToInt32(UnitDefaultValues.EntryDate.Subtract(UnitDefaultValues.CrushingStartDate).TotalDays);

            TempData["BaseUnitCode"] = Session["BaseUnitCode"];
            ViewBag.UnitName = UnitDefaultValues.Name;
            ViewBag.EntryDate = UnitDefaultValues.EntryDate.ToShortDateString();
            ViewBag.CropDay = cropDays;
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
            ViewBag.MasterStoppageTypes = MasterStoppageTypes;

            if (UnitDefaultValues.OldMillCapacity > 0)
            {
                ViewBag.MillCount = 2;
            }
            else
            {
                ViewBag.MillCount = 1;
            }
        }
    }
}