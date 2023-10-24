using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;



namespace LitmusWeb.Controllers
{
    public class VehicleController : Controller
    {

        MasterVehicleRepository VehicleRepository = new MasterVehicleRepository();
        // GET: Vehicle
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("view")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterVehicle> VehicleEntity = VehicleRepository.GetMasterVehicleList();

            List<MasterVehicleModel> vehicleModel = new List<MasterVehicleModel>();
            foreach (var item in VehicleEntity)
            {
                MasterVehicleModel temp = new MasterVehicleModel()
                {
                    CompanyCode = item.CompanyCode,
                    UnitCode = item.UnitCode,
                    Code = item.Code,
                    Name = item.Name,
                    MinimumWeight = item.MinimumWeight,
                    MaximumWeight = item.MaximumWeight,
                    AverageWeight = item.AverageWeight,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                vehicleModel.Add(temp);
            }
            return View(vehicleModel);
        }

        // GET: Vehicle/Details/5
        [HttpGet]
        [ValidationFilter("view")]
        public ActionResult Details(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterVehicleModel vehicleModel = GetSubMenuModel(id);
            return View(vehicleModel);
        }

        // GET: Vehicle/Create
        [CustomAuthorizationFilter("Super Admin")]
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

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("Create")]
        public ActionResult Create(MasterVehicleModel vehicleModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            // if model state is not valid, return to the same view i.e. create view
            //08-08-2019
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // TODO: Add insert logic here
                MasterVehicle VehicleEntity = new MasterVehicle()
                {
                    CompanyCode = vehicleModel.CompanyCode,
                    UnitCode = vehicleModel.UnitCode,
                    Code = vehicleModel.Code,
                    Name = vehicleModel.Name,
                    MinimumWeight = vehicleModel.MinimumWeight,
                    MaximumWeight = vehicleModel.MaximumWeight,
                    AverageWeight = vehicleModel.AverageWeight,
                    IsActive = vehicleModel.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Session["UserCode"].ToString()
                };
                VehicleRepository.AddMasterVehicle(VehicleEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Edit/5
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("update")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterVehicleModel vehicleModel = GetSubMenuModel(id);
            return View(vehicleModel);
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("update")]
        public ActionResult Edit(MasterVehicleModel vehicleModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            // return to the same view if Model state is invalid. 08-08-2019
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // TODO: Add update logic here
                MasterVehicle VehicleEntity = new MasterVehicle()
                {
                    CompanyCode = vehicleModel.CompanyCode,
                    UnitCode = vehicleModel.UnitCode,
                    Code = vehicleModel.Code,
                    Name = vehicleModel.Name,
                    MinimumWeight = vehicleModel.MinimumWeight,
                    MaximumWeight = vehicleModel.MaximumWeight,
                    AverageWeight = vehicleModel.AverageWeight,
                    CreatedDate = vehicleModel.CreatedDate,
                    IsActive = vehicleModel.IsActive,
                };
                bool result = VehicleRepository.UpdateMasterVehicle(VehicleEntity);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Delete/5
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("update")]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterVehicleModel vehicleModel = GetSubMenuModel(id);
            return View(vehicleModel);
        }

        // POST: Vehicle/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidationFilter("update")]
        public ActionResult DeletePost(int id)
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
            try
            {
                // TODO: Add delete logic here
                bool result = VehicleRepository.DeleteMasterVehicle(id);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [NonAction]
        public MasterVehicleModel GetSubMenuModel(int id)
        {
            MasterVehicle VehicleEntity = new MasterVehicle();
            VehicleEntity = VehicleRepository.FindByCode(id, Convert.ToInt32(Session["BaseUnitCode"]));
            MasterVehicleModel vehicleModel = new MasterVehicleModel()
            {
                CompanyCode = VehicleEntity.CompanyCode,
                UnitCode = VehicleEntity.UnitCode,
                Code = VehicleEntity.Code,
                Name = VehicleEntity.Name,
                MinimumWeight = VehicleEntity.MinimumWeight,
                MaximumWeight = VehicleEntity.MaximumWeight,
                AverageWeight = VehicleEntity.AverageWeight,
                IsActive = VehicleEntity.IsActive,
                CreatedDate = VehicleEntity.CreatedDate,
                CreatedBy = VehicleEntity.CreatedBy
            };
            return vehicleModel;
        }




        /// At unit require to provide limited acess that is why creating saperate methods 
        #region VehicleControl for Unit Admins
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        public ActionResult UnitVehicleMaster()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            int unitCode = Convert.ToInt16(Session["BaseUnitCode"]);
            var vehicleDetails = VehicleRepository.GetMasterVehicleByUnitList(unitCode);
            if (vehicleDetails.Count <= 0)
            {
                return View("Error");
            }
            List<MasterVehicleModel> masterVehicleModels = new List<MasterVehicleModel>();
            foreach (var item in vehicleDetails)
            {
                MasterVehicleModel temp = new MasterVehicleModel()
                {
                    id = item.id,
                    CompanyCode = item.CompanyCode,
                    UnitCode = item.UnitCode,
                    Code = item.Code,
                    Name = item.Name,
                    MinimumWeight = item.MinimumWeight,
                    MaximumWeight = item.MaximumWeight,
                    AverageWeight = item.AverageWeight,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy
                };
                masterVehicleModels.Add(temp);
            }

            return View(masterVehicleModels);
        }
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        public ActionResult UnitVehicleMasterEdit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterVehicle masterVehicle = new MasterVehicle();

            masterVehicle = VehicleRepository.FindByCode(id, Convert.ToInt32(Session["BaseUnitCode"]));
            if (masterVehicle == null)
            {
                return View("Error");
            }
            /// if user's unit code and vehicle's unit code are not same then show an error message (because user may tried to access another unit's vehicle forcefully)
            if (masterVehicle.UnitCode != Convert.ToInt16(Session["BaseUnitCode"]))
            {
                return View("Error");
            }
            MasterVehicleModel model = new MasterVehicleModel()
            {
                UnitCode = masterVehicle.UnitCode,
                Code = masterVehicle.Code,
                Name = masterVehicle.Name,
                MinimumWeight = masterVehicle.MinimumWeight,
                MaximumWeight = masterVehicle.MaximumWeight,
                AverageWeight = masterVehicle.AverageWeight,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        [ActionName(name: "UnitVehicleMasterEdit")]
        public ActionResult UnitVehicleMasterEditPost(MasterVehicleModel masterVehicleModel)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterVehicle Entity = new MasterVehicle()
            {
                id = masterVehicleModel.id,
                MinimumWeight = masterVehicleModel.MinimumWeight,
                MaximumWeight = masterVehicleModel.MaximumWeight,
                AverageWeight = masterVehicleModel.AverageWeight,
            };
            bool result = VehicleRepository.UpdateMasterVehicleForUnit(Entity);
            if (result == false)
            {
                return View("Error");
            }
            return RedirectToAction("UnitVehicleMaster");
        }
        #endregion
    }
}
