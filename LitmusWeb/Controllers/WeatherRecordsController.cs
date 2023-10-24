using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class WeatherRecordsController : Controller
    {
        // GET: WeatherRecords
        WeatherRecordsRepository wrepo;
        public WeatherRecordsController()
        {
            wrepo = new WeatherRecordsRepository();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Weather Records";
            GetAllWeatherTypes();
            return View();
        }

        [HttpGet]
        [UnitBasedValuesFilter]
        public ActionResult Add()
        {

            ViewBag.Title = "Add New Records";
            GetAllWeatherTypes();
            WeatherRecordsModel model = new WeatherRecordsModel();
            return View(model);
        }

        [HttpPost]
        [UnitBasedValuesFilter]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public ActionResult AddPost(WeatherRecordsModel model)
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (model == null || !ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }


            WeatherRecord wr = new WeatherRecord()
            {
                UnitCode = Convert.ToInt32(Session["BaseUnitCode"]),
                SeasonCode = model.SeasonCode,
                RecordDate = model.RecordDate,
                TemperatureMax = model.TemperatureMax,
                TemperatureMin = model.TemperatureMin,
                Humidity = model.Humidity,
                WindSpeed = model.WindSpeed,
                RainFallMm = model.RainFallMm,
                UvIndex = model.UvIndex,
                WeatherType = model.WeatherType,
                AllWeatherConditions = model.AllWeatherConditions
            };
            wrepo.AddWeatherRecord(wr);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MonthlyRainFallReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(name: "MonthlyRainFallReport")]
        public ActionResult MonthlyRainFallReportPost(FormCollection dates)
        {
            if (!ModelState.IsValid || dates == null)
            {
                return View();
            }
            if (Session.IsNewSession || Session["UserCode"] == null)
            {
                return RedirectToAction("Home", "login");
            }

            List<WeatherRecordsModel> model = new List<WeatherRecordsModel>();
            List<WeatherRecord> e = new List<WeatherRecord>();
            e = wrepo.MonthWiseYearlyRainFall(Convert.ToInt32(dates["txtReportFromDate"]), Convert.ToInt32(dates["txtReportToDate"]));
            if (e != null || e.Count > 0)
            {
                foreach (var x in e)
                {
                    WeatherRecordsModel temp = new WeatherRecordsModel()
                    {
                        Id = x.Id,
                        UnitCode = x.UnitCode,
                        SeasonCode = x.SeasonCode,
                        RecordDate = x.RecordDate,
                        TemperatureMin = x.TemperatureMin,
                        TemperatureMax = x.TemperatureMax,
                        Humidity = x.Humidity,
                        WindSpeed = x.WindSpeed,
                        RainFallMm = x.RainFallMm,
                        UvIndex = x.UvIndex
                    };
                    model.Add(temp);
                }
            }

            return View(model);

        }

        [NonAction]
        public void GetAllWeatherTypes()
        {
            WeatherRecordsRepository wr = new WeatherRecordsRepository();
            MasterSeasonRepository seasonRepo = new MasterSeasonRepository();
            List<MasterSeason> us = new List<MasterSeason>();

            List<WeatherType> wt = wr.GetAllWeatherTypes();
            us = seasonRepo.GetMasterSeasonList();

            List<WeatherTypeModel> wm = new List<WeatherTypeModel>();
            List<MasterSeasonModel> seasons = new List<MasterSeasonModel>();
            foreach (var i in wt)
            {
                WeatherTypeModel temp = new WeatherTypeModel()
                {
                    id = i.id,
                    Code = i.Code,
                    Text = i.Text,
                    Description = i.Description
                };
                wm.Add(temp);
            }

            foreach (var s in us)
            {
                MasterSeasonModel temp = new MasterSeasonModel()
                {
                    id = s.id,
                    SeasonCode = s.SeasonCode,
                    SeasonYear = s.SeasonYear
                };
                seasons.Add(temp);
            }
            ViewBag.Seasons = seasons;
            ViewBag.WeatherTypes = wm;
        }


    }
}