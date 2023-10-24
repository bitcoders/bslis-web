using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.AnalysisRepositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class StoppageController : Controller
    {
        StoppageRepository Repository = new StoppageRepository();
        // GET: Stoppage
        int stoppageId;
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Read", "CHEMIST")]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<Stoppage> stoppageEntity = new List<Stoppage>();
            List<StoppageUpdateModel> stoppageModels = new List<StoppageUpdateModel>();
            SetUnitDefaultValues();
            stoppageEntity = Repository.GetStoppagesList(Convert.ToInt32(Session["BaseUnitCode"]), ViewBag.CrushingSeason, ViewBag.EntryDate);
            foreach (var item in stoppageEntity)
            {
                stoppageModels.Add(new StoppageUpdateModel
                {
                    id = item.id,
                    unit_code = item.unit_code,
                    s_date = item.s_date,
                    season_code = item.season_code,
                    s_start_calendar_date = item.s_start_calendar_date,
                    s_start_time = item.s_start_time,
                    s_end_calendar_date = item.s_end_calendar_date,
                    s_end_time = item.s_end_time,
                    s_duration = item.s_duration,
                    s_net_duration = item.s_net_duration,
                    s_mill_code = item.s_mill_code,
                    s_head_code = item.s_head_code,
                    s_head_name = item.s_head_name,
                    s_sub_head_code = item.s_sub_head_code,
                    s_sub_head_name = item.s_sub_head_name,
                    s_comment = item.s_comment,
                    s_crtd_by = item.s_crtd_by
                });
            }
            return View(stoppageModels);
        }
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Create")]
        [ValidationFilter("Create")]
        public ActionResult Create()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            SetUnitDefaultValues();
            StoppageModel model = new StoppageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Create")]
        [ValidationFilter("Create")]
        public ActionResult Create(StoppageModel stoppageModel)
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
            SetUnitDefaultValues();
            Stoppage Entity = new Stoppage()
            {
                unit_code = Convert.ToInt16(Session["BaseUnitCode"]),
                s_date = DateTime.Parse(ViewBag.EntryDate.ToString()),
                season_code = ViewBag.CrushingSeason,
                s_start_calendar_date = DateTime.Now,
                s_start_time = stoppageModel.s_start_time,
                s_mill_code = stoppageModel.s_mill_code,
                s_head_code = stoppageModel.s_head_code,
                s_head_name = stoppageModel.s_head_name,
                s_sub_head_code = stoppageModel.s_sub_head_code,
                s_sub_head_name = stoppageModel.s_sub_head_name,
                s_comment = stoppageModel.s_comment,
                s_crtd_by = Session["UserCode"].ToString(),
                is_deleted = false,

            };
            bool result = Repository.AddStoppage(Entity);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Stoppage edit is used to set end time of the stoppage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Edit", "Stoppage Create")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            SetUnitDefaultValues(); // load default values from MasterUnits
            Stoppage Entity = new Stoppage();

            Entity = Repository.GetStoppageDetailsById(Convert.ToInt16(Session["BaseUnitCode"])
                , Convert.ToInt16(ViewBag.CrushingSeason), id);
            stoppageId = id;
            StoppageUpdateModel UpdateModel = new StoppageUpdateModel()
            {
                id = Entity.id,
                unit_code = Entity.unit_code,
                s_date = Entity.s_date,
                season_code = Entity.season_code,
                s_start_calendar_date = Entity.s_start_calendar_date,
                s_start_time = Entity.s_start_time,
                s_end_calendar_date = DateTime.Now,
                s_end_time = DateTime.Now.TimeOfDay.ToString(),
                s_mill_code = Entity.s_mill_code,
                s_head_code = Entity.s_head_code,
                s_head_name = Entity.s_head_name,
                s_sub_head_code = Entity.s_sub_head_code,
                s_sub_head_name = Entity.s_sub_head_name,
                s_comment = Entity.s_comment
            };
            return View(UpdateModel);
        }
        [HttpPost]
        [ActionName(name: "Edit")]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Edit", "Stoppage Create")]
        [ValidationFilter("update")]
        public ActionResult EditPost(StoppageUpdateModel stoppageModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            SetUnitDefaultValues();
            decimal netStoppageDuration = 0M;
            decimal duration = 0;
            switch (stoppageModel.s_mill_code)
            {
                case 0:
                    duration = (stoppageModel.s_duration * Convert.ToDecimal(ViewBag.NewMillCapacity)) / 100;
                    //netStoppageDuration = Math.Round(((stoppageModel.s_duration * Convert.ToDecimal(ViewBag.NewMillCapacity)) / 100),0);
                    netStoppageDuration = (decimal)Repository.CustomRound(duration, 0.50M);
                    break;
                case 1:
                    duration = (stoppageModel.s_duration * Convert.ToDecimal(ViewBag.OldMillCapacity)) / 100;
                    netStoppageDuration = (decimal)Repository.CustomRound(duration, 0.50M);
                    //netStoppageDuration = Math.Round(((stoppageModel.s_duration * Convert.ToDecimal(ViewBag.OldMillCapacity)) / 100),0);
                    break;
                default:
                    netStoppageDuration = 0;
                    break;
            }

            Stoppage Entity = new Stoppage()
            {
                id = stoppageModel.id,
                unit_code = stoppageModel.unit_code,
                season_code = ViewBag.CrushingSeason,
                s_start_calendar_date = stoppageModel.s_start_calendar_date,
                s_end_calendar_date = DateTime.Now.Date,
                s_end_time = stoppageModel.s_end_time,
                s_duration = stoppageModel.s_duration,
                s_net_duration = netStoppageDuration,
                s_date = stoppageModel.s_date,
                s_start_time = stoppageModel.s_start_time,
                s_mill_code = stoppageModel.s_mill_code,
                s_head_code = stoppageModel.s_head_code,
                s_head_name = stoppageModel.s_head_name,
                s_sub_head_code = stoppageModel.s_sub_head_code,
                s_sub_head_name = stoppageModel.s_sub_head_name,
                s_comment = stoppageModel.s_comment,
                s_updt_by = Session["UserCode"].ToString(),
                s_updt_dt = DateTime.Now,
                is_deleted = false
            };
            bool result = Repository.UpdateStoppage(Entity);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index", "Stoppage");
        }


        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Reason Edit")]
        [ValidationFilter("update")]
        public ActionResult ChangeReason(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            SetUnitDefaultValues();
            Stoppage entity = new Stoppage();
            entity = Repository.GetStoppageDetailsById(Convert.ToInt16(TempData["BaseUnitCode"]), ViewBag.CrushingSeason, id);
            if (entity.s_end_time == null || entity.s_end_time.Trim().Length == 0)
            {
                return View("Index", "Stoppages");
            }
            StoppageUpdateModel model = new StoppageUpdateModel()
            {
                id = entity.id,
                unit_code = entity.unit_code,
                s_date = entity.s_date,
                s_start_calendar_date = entity.s_start_calendar_date,
                s_start_time = entity.s_start_time,
                s_mill_code = entity.s_mill_code,
                s_head_code = entity.s_head_code,
                s_head_name = entity.s_head_name,
                s_sub_head_code = entity.s_sub_head_code,
                s_sub_head_name = entity.s_sub_head_name,
                s_comment = entity.s_comment,
                season_code = entity.season_code,
                s_end_calendar_date = entity.s_end_calendar_date,
                s_end_time = entity.s_end_time,
                s_duration = entity.s_duration,
                s_net_duration = entity.s_net_duration,
                is_deleted = entity.is_deleted,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Stoppage Reason Edit")]
        [ActionName(name: "ChangeReason")]
        [ValidationFilter("update")]
        public ActionResult PostChangeReason(StoppageAllChangesModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (model.s_end_time == null || model.s_end_time.Trim().Length == 0)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            SetUnitDefaultValues();
            Stoppage entity = new Stoppage()
            {
                id = model.id,
                unit_code = (int)Session["BaseUnitCode"],
                season_code = ViewBag.CrushingSeason,
                s_date = model.s_date,
                s_start_calendar_date = model.s_start_calendar_date,
                s_start_time = model.s_start_time,
                s_mill_code = model.s_mill_code,
                s_head_code = model.s_head_code,
                s_head_name = model.s_head_name,
                s_sub_head_code = model.s_sub_head_code,
                s_sub_head_name = model.s_sub_head_name,
                s_comment = model.s_comment,
                s_end_calendar_date = model.s_end_calendar_date,
                s_end_time = model.s_end_time,
                s_duration = model.s_duration,
                s_net_duration = model.s_net_duration,
                is_deleted = model.is_deleted
            };
            bool result = Repository.UpdateStoppageReason(entity);
            if (result != true)
            {
                return View("Error");
            }
            return RedirectToAction("Index");


        }


        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "stoppage time change")]
        [ValidationFilter("update")]
        public ActionResult ChangeStoppageTime(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Stoppage stoppage = new Stoppage();
            SetUnitDefaultValues();
            stoppage = Repository.GetStoppageDetailsById(Convert.ToInt32(Session["BaseUnitCode"]), ViewBag.CrushingSeason, id);
            if (stoppage == null)
            {
                return RedirectToAction("Index", "Stoppage");
            }
            StoppageAllChangesModel model = new StoppageAllChangesModel()
            {
                id = stoppage.id,
                s_start_time = stoppage.s_start_time,
                s_end_time = stoppage.s_end_time,
                s_duration = stoppage.s_duration,
                s_net_duration = stoppage.s_net_duration,
                s_mill_code = stoppage.s_mill_code,
                s_head_code = stoppage.s_head_code,
                s_head_name = stoppage.s_head_name,
                s_sub_head_code = stoppage.s_sub_head_code,
                s_sub_head_name = stoppage.s_sub_head_name,
                s_comment = stoppage.s_comment,
                s_crtd_by = stoppage.s_crtd_by,
                s_crtd_dt = stoppage.s_crtd_dt,
                is_deleted = stoppage.is_deleted,
            };
            return View(model);
        }

        [HttpPost]
        [CustomAuthorizationFilter("Super Admin", "stoppage time change")]
        [ValidateAntiForgeryToken]
        [ActionName(name: "ChangeStoppageTime")]
        [ValidationFilter("update")]
        public ActionResult PostChangeStoppageTime(StoppageAllChangesModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            if (model.s_head_code == 0 || model.s_sub_head_code == 0)
            {
                ModelState.AddModelError("s_head_code", "Stoppage Category & Reason is required.");
            }
            if (!ModelState.IsValid)
            {
                SetUnitDefaultValues();
                TempData["ErrorTitle"] = "Invalid Input while modifying Stoppage!";

                return View("Error");
            }
            SetUnitDefaultValues();
            Stoppage stoppage = new Stoppage()
            {
                id = model.id,
                s_date = model.s_date,
                s_start_calendar_date = model.s_start_calendar_date,
                s_start_time = model.s_start_time,
                s_mill_code = model.s_mill_code,
                s_head_code = model.s_head_code,
                s_head_name = model.s_head_name,
                s_sub_head_code = model.s_sub_head_code,
                s_sub_head_name = model.s_sub_head_name,
                s_comment = model.s_comment,
                s_end_calendar_date = model.s_end_calendar_date,
                s_end_time = model.s_end_time,
                s_duration = model.s_duration,
                s_net_duration = calculateNetDuration(model.s_mill_code, model.s_duration),
                is_deleted = model.is_deleted,
                s_crtd_by = model.s_crtd_by,
                s_crtd_dt = model.s_crtd_dt,
                s_updt_by = Session["UserCode"].ToString(),
                s_updt_dt = DateTime.Now,

            };
            bool status = Repository.UpdateStoppageduration(Convert.ToInt32(Session["BaseUnitcode"]), ViewBag.CrushingSeason, stoppage);
            if (!status)
            {
                SetUnitDefaultValues();
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorTitle = "Errior while Modifying Stoppage Data";
                foreach (var x in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList())
                {
                   // errorViewModel.ErrorMessage.Add(x.ToString());
                }
                ViewData["ErrorMessage"] = errorViewModel;
                return View("Error");
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// show the details of stoppage to which user is going to delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Developer", "Stoppage Delete")]
        [ValidationFilter("view")]
        public ActionResult DeleteStoppage(int id)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Stoppage stoppage = new Stoppage();
            SetUnitDefaultValues();
            stoppage = Repository.GetStoppageDetailsById(Convert.ToInt32(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), id);
            if (stoppage != null)
            {
                StoppageUpdateModel model = new StoppageUpdateModel()
                {
                    id = stoppage.id,
                    unit_code = stoppage.unit_code,
                    s_date = stoppage.s_date,
                    season_code = stoppage.season_code,
                    s_start_time = stoppage.s_start_time,
                    s_end_time = stoppage.s_end_time,
                    s_duration = stoppage.s_duration,
                    s_head_name = stoppage.s_head_name,
                    s_sub_head_name = stoppage.s_sub_head_name,
                    s_comment = stoppage.s_comment,
                    s_crtd_by = stoppage.s_crtd_by
                };

                return View(model);
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost]
        [CustomAuthorizationFilter("Super Admin", "Developer", "Stoppage Delete")]
        [ActionName(name: "DeleteStoppage")]
        [ValidationFilter("update")]
        public ActionResult PostDeleteStoppage(StoppageModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            SetUnitDefaultValues();


            bool status = Repository.DeleteStoppage(Convert.ToInt32(Session["BaseUnitCode"]), model.id, Session["UserCode"].ToString());
            if (status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }

        }

        /// <summary>
        /// An Method which will return a list of Stoppages in json format, by this 
        /// we will fetch the list of stoppages based on its master code using Ajax
        /// and can render on a web page accrodingly.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxGetSubStoppage(int id)
        {
            MasterStoppageSubStoppageModel model = new MasterStoppageSubStoppageModel();
            model.StoppageSubTypes = populateDropDown(id);
            return Json(model);
        }

        /// <summary>
        /// Date: 01-09-2019 01:30 AM
        /// Author => Ravi B.
        /// This function will get default values of current user's base unit
        /// and set some values like entry date, unit name etc to viewbag so that
        /// we can use these values in ActionResults
        /// </summary>
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
            ViewBag.EntryDate = UnitDefaultValues.EntryDate;
            ViewBag.CropDay = cropDays;
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
            ViewBag.MasterStoppageTypes = MasterStoppageTypes;
            ViewBag.OldMillCapacity = UnitDefaultValues.OldMillCapacity;
            ViewBag.NewMillCapacity = UnitDefaultValues.NewMillCapacity;
            if (UnitDefaultValues.OldMillCapacity > 0)
            {
                ViewBag.MillCount = 2;
            }
            else
            {
                ViewBag.MillCount = 1;
            }
        }

        /// <summary>
        /// Get the list of sub stoppage types based on master stoppage code
        /// Date: 01-09-2019 02:09 AM
        /// Ravi B.
        /// </summary>
        /// <param name="stoppageHead"></param>
        /// <returns></returns>
        private List<SelectListItem> populateDropDown(int stoppageHead)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                RedirectToAction("Index", "Home");
            }
            List<SelectListItem> items = new List<SelectListItem>();
            MasterStoppageSubTypeRepository repository = new MasterStoppageSubTypeRepository();
            var stoppageList = repository.GetMasterStoppageSubListByMasterHead(stoppageHead);
            if (stoppageList != null)
            {
                foreach (var x in stoppageList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code.ToString()
                    });
                }
                return items;
            }
            else
            {
                return null;
            }

        }

        private decimal calculateNetDuration(int millCode, int? grossDuration)
        {
            decimal netStoppageDuration = 0M;
            decimal duration = 0;

            decimal x = Decimal.TryParse(grossDuration.ToString(), out x) ? x : 0M;

            SetUnitDefaultValues();
            switch (millCode)
            {
                case 0:
                    duration = (grossDuration * Convert.ToDecimal(ViewBag.NewMillCapacity)) / 100;
                    //netStoppageDuration = Math.Round(((stoppageModel.s_duration * Convert.ToDecimal(ViewBag.NewMillCapacity)) / 100),0);
                    netStoppageDuration = (decimal)Repository.CustomRound(duration, 0.50M);
                    break;
                case 1:
                    duration = (grossDuration * Convert.ToDecimal(ViewBag.OldMillCapacity)) / 100;
                    netStoppageDuration = (decimal)Repository.CustomRound(duration, 0.50M);
                    //netStoppageDuration = Math.Round(((stoppageModel.s_duration * Convert.ToDecimal(ViewBag.OldMillCapacity)) / 100),0);
                    break;
                default:
                    netStoppageDuration = 0;
                    break;
            }
            return netStoppageDuration;
        }
    }
}