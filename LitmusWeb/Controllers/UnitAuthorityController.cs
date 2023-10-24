using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class UnitAuthorityController : Controller
    {
        MasterUnitAuthorityRepository mar = new MasterUnitAuthorityRepository();
        // GET: UnitAuthority
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            List<MasterUnitAuthority> mua = mar.GetMasterUnitAuthoritiesList();
            List<MasterUnitAuthorityModel> muaModel = new List<MasterUnitAuthorityModel>();
            if (muaModel != null)
            {
                foreach (var item in mua)
                {
                    Models.MasterUnitAuthorityModel temp = new MasterUnitAuthorityModel
                    {
                        Id = item.Id,
                        UnitCode = item.UnitCode,
                        Name = item.Name,
                        Designation = item.Designation,
                        valid_from = item.valid_from,
                        valid_till = item.valid_till,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                    };
                    muaModel.Add(temp);
                }
            }
            return View(muaModel);
        }

        [HttpGet]
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
        public ActionResult Create(Models.MasterUnitAuthorityModel masterUnitAuthorityModel)
        {
            bool result;
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    MasterUnitAuthority mua = new MasterUnitAuthority()
                    {
                        UnitCode = masterUnitAuthorityModel.UnitCode,
                        Name = masterUnitAuthorityModel.Name,
                        Designation = masterUnitAuthorityModel.Designation,
                        valid_from = masterUnitAuthorityModel.valid_from,
                        valid_till = masterUnitAuthorityModel.valid_till,
                        CreatedDate = DateTime.Now,
                        CreatedBy = Session["UserCode"].ToString()
                    };
                    result = mar.addMasterUnitAuthorities(mua);
                    if (!result)
                    {
                        return View("Error");
                    }
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            MasterUnitAuthority masterUnitAuthority = new MasterUnitAuthority();
            masterUnitAuthority = mar.FindByCode(id);
            MasterUnitAuthorityModel m = new MasterUnitAuthorityModel
            {
                Id = masterUnitAuthority.Id,
                UnitCode = masterUnitAuthority.UnitCode,
                Name = masterUnitAuthority.Name,
                Designation = masterUnitAuthority.Designation,
                valid_from = masterUnitAuthority.valid_from,
                valid_till = masterUnitAuthority.valid_till
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.MasterUnitAuthorityModel m)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            var authority = mar.FindByCode(m.Id);
            if (ModelState.IsValid)
            {
                authority.Name = m.Name;
                authority.Designation = m.Designation;
                authority.valid_from = m.valid_from;
                authority.valid_till = m.valid_till;

                bool result = mar.updateMasterUnitAuthorities(authority);
                if (!result)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", "UnitAuthority");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            var authority = mar.FindByCode(id);
            Models.MasterUnitAuthorityModel m = new MasterUnitAuthorityModel
            {
                Id = authority.Id,
                Name = authority.Name,
                UnitCode = authority.UnitCode,
                Designation = authority.Designation,
                valid_from = authority.valid_from,
                valid_till = authority.valid_till,
                CreatedDate = authority.CreatedDate,
                CreatedBy = authority.CreatedBy,
            };

            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Delete")]
        public ActionResult DeleteAuthority(int id)
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }

            var result = mar.deleteMasterUnitAuthorities(id);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index", "UnitAuthority");

        }
    }
}