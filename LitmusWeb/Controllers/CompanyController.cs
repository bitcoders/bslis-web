using DataAccess;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class CompanyController : Controller
    {

        MasterCompanyRepository companyRepository = new MasterCompanyRepository();
        // GET: Company
        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterCompanyModel> companies = null;
            List<MasterCompany> mc = companyRepository.GetMasterCompaniesList();
            companies = new List<Models.MasterCompanyModel>();
            foreach (var m in mc)
            {
                Models.MasterCompanyModel temp = new MasterCompanyModel
                {
                    Code = m.Code,
                    Name = m.Name,
                    Icon = m.Icon,
                    RegisteredAddress = m.RegisteredAddress,
                    PANNO = m.PANNO,
                    TANNO = m.TANNO,
                    IsActive = m.IsActive,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy
                };
                companies.Add(temp);
            }
            return View(companies);
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Create(Models.MasterCompanyModel mcm)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                DataAccess.MasterCompany masterCompany = new MasterCompany()
                {
                    Code = mcm.Code,
                    Name = mcm.Name,
                    Icon = mcm.Icon,
                    RegisteredAddress = mcm.RegisteredAddress,
                    PANNO = mcm.PANNO,
                    TANNO = mcm.TANNO,
                    CreatedBy = mcm.CreatedBy,
                    CreatedDate = mcm.CreatedDate,
                    IsActive = mcm.IsActive
                };
                bool result = companyRepository.AddMasterCompanyMaster(masterCompany);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            var businessEntity = companyRepository.FindCompanyByPK(id);
            MasterCompanyModel masterCompanyModel = new MasterCompanyModel()
            {
                Code = businessEntity.Code,
                Name = businessEntity.Name,
                Icon = businessEntity.Icon,
                RegisteredAddress = businessEntity.RegisteredAddress,
                PANNO = businessEntity.PANNO,
                TANNO = businessEntity.TANNO,
                IsActive = businessEntity.IsActive,
                CreatedDate = businessEntity.CreatedDate,
                CreatedBy = businessEntity.CreatedBy
            };
            return View(masterCompanyModel);
        }
        [HttpPost]
        [CustomAuthorizationFilter("Super Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.MasterCompanyModel mcm)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            var company = companyRepository.FindCompanyByPK(mcm.Code);
            if (ModelState.IsValid)
            {
                company.Code = mcm.Code;
                company.Name = mcm.Name;
                company.Icon = mcm.Icon;
                company.RegisteredAddress = mcm.RegisteredAddress;
                company.PANNO = mcm.PANNO;
                company.TANNO = mcm.TANNO;
                company.IsActive = mcm.IsActive;
                company.CreatedDate = mcm.CreatedDate;

                bool result = companyRepository.UpdateMasterCompanyMaster(company);
                if (!result)
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            var businessEntity = companyRepository.FindCompanyByPK(id);
            MasterCompanyModel masterCompanyModel = new MasterCompanyModel()
            {
                Code = businessEntity.Code,
                Name = businessEntity.Name,
                Icon = businessEntity.Icon,
                RegisteredAddress = businessEntity.RegisteredAddress,
                PANNO = businessEntity.PANNO,
                TANNO = businessEntity.TANNO,
                IsActive = businessEntity.IsActive,
                CreatedDate = businessEntity.CreatedDate,
                CreatedBy = businessEntity.CreatedBy
            };
            return View(masterCompanyModel);
        }

        [HttpPost]
        [CustomAuthorizationFilter("Super Admin")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Super Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            bool result = companyRepository.DeleteMasterCompany(id);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }

}