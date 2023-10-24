using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class WeatherTypesController : Controller
    {
        // GET: WeatherTypes

        WeatherTypeRepository wtRepo;
        public WeatherTypesController()
        {
            wtRepo = new WeatherTypeRepository();
        }


        [CustomAuthorizationFilter("Super Admin", "Developer")]
        [HttpGet]
        public ActionResult Index()
        {
            //ViewBag.Title = "Weather Type!";
            List<WeatherTypeModel> m = new List<WeatherTypeModel>();
            List<WeatherType> e = new List<WeatherType>();
            e = wtRepo.GetWeatherTypes();
            if (e.Count > 0)
            {
                foreach (var i in e)
                {
                    WeatherTypeModel temp = new WeatherTypeModel()
                    {
                        id = i.id,
                        Code = i.Code,
                        Text = i.Text,
                        Description = i.Description,
                        IsActive = i.IsActive

                    };
                    m.Add(temp);
                }
                return View(m);
            }
            return View();
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        public ActionResult Create()
        {
            ViewBag.Title = "Define New Weather Type!";
            return View();
        }

        [HttpPost]
        [CustomAuthorizationFilter("Super Admin", "Developer")]
        [ActionName(name: "Create")]
        public ActionResult CreatePost(WeatherTypeModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }

            WeatherType wt = new WeatherType()
            {
                Code = model.Code,
                Text = model.Text,
                Description = model.Description,
                IsActive = model.IsActive
            };
            if (wtRepo.AddWeatherType(wt) == true)
            {
                return RedirectToAction("index", "WeatherTypes");
            }
            return View();
        }
    }
}