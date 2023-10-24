using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Filters;
using LitmusWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class PeriodicalStockController : Controller
    {

        PeriodicalStockRepository periodicalStockRepository = new PeriodicalStockRepository();
        // GET: PeriodicalStock
        [HttpGet]
        [ValidationFilter("View")]
        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            SetUnitDefaultValues();
            List<PeriodicalStockModel> model = new List<PeriodicalStockModel>();
            List<PeriodicalStock> entityList = new List<PeriodicalStock>();
            entityList = periodicalStockRepository.getPeriodicalStockList(Convert.ToInt32(Session["BaseUnitCode"]), ViewBag.CrushingSeason);
            if (entityList != null)
            {
                foreach (var x in entityList)
                {
                    PeriodicalStockModel temp = new PeriodicalStockModel()
                    {
                        Id = x.Id,
                        UnitCode = x.UnitCode,
                        SeasonCode = x.SeasonCode,
                        EntryDate = x.EntryDate,
                        MixedJuiceJuiceHl = x.MixedJuiceJuiceHl,
                        MixedJuiceJuiceBrix = x.MixedJuiceJuiceBrix,
                        MixedJuiceJuicePol = x.MixedJuiceJuicePol,
                        MixedJuiceJuicePurity = x.MixedJuiceJuicePurity,
                        MixedJuiceJuiceAvailableSugar = x.MixedJuiceJuiceAvailableSugar,
                        MixedJuiceJuiceAvailableMolasses = x.MixedJuiceJuiceAvailableMolasses,
                        ClearJuiceHl = x.ClearJuiceHl,
                        ClearJuiceBrix = x.ClearJuiceBrix,
                        ClearJuicePol = x.ClearJuicePol,
                        ClearJuicePurity = x.ClearJuicePurity,
                        ClearJuiceAvailableSugar = x.ClearJuiceAvailableSugar,
                        ClearJuiceAvailableMolasses = x.ClearJuiceAvailableMolasses,
                        SyrupJuiceHl = x.SyrupJuiceHl,
                        SyrupJuiceBrix = x.SyrupJuiceBrix,
                        SyrupJuicePol = x.SyrupJuicePol,
                        SyrupJuicePurity = x.SyrupJuicePurity,
                        SyrupJuiceAvailableSugar = x.SyrupJuiceAvailableSugar,
                        SyrupJuiceAvailableMolasses = x.SyrupJuiceAvailableMolasses,
                        SeedJuiceHl = x.SeedJuiceHl,
                        SeedJuiceBrix = x.SeedJuiceBrix,
                        SeedJuicePol = x.SeedJuicePol,
                        SeedJuicePurity = x.SeedJuicePurity,
                        SeedJuiceAvailableSugar = x.SeedJuiceAvailableSugar,
                        SeedJuiceAvailableMolasses = x.SeedJuiceAvailableMolasses,
                        MassecuiteAJuiceHl = x.MassecuiteAJuiceHl,
                        MassecuiteAJuiceBrix = x.MassecuiteAJuiceBrix,
                        MassecuiteAJuicePol = x.MassecuiteAJuicePol,
                        MassecuiteAJuicePurity = x.MassecuiteAJuicePurity,
                        MassecuiteAJuiceAvailableSugar = x.MassecuiteAJuiceAvailableSugar,
                        MassecuiteAJuiceAvailableMolasses = x.MassecuiteAJuiceAvailableMolasses,
                        MassecuiteCJuiceHl = x.MassecuiteCJuiceHl,
                        MassecuiteCJuiceBrix = x.MassecuiteCJuiceBrix,
                        MassecuiteCJuicePol = x.MassecuiteCJuicePol,
                        MassecuiteCJuicePurity = x.MassecuiteCJuicePurity,
                        MassecuiteCJuiceAvailableSugar = x.MassecuiteCJuiceAvailableSugar,
                        MassecuiteCJuiceAvailableMolasses = x.MassecuiteCJuiceAvailableMolasses,
                        MassecuiteCOneJuiceHl = x.MassecuiteCOneJuiceHl,
                        MassecuiteCOneJuiceBrix = x.MassecuiteCOneJuiceBrix,
                        MassecuiteCOneJuicePol = x.MassecuiteCOneJuicePol,
                        MassecuiteCOneJuicePurity = x.MassecuiteCOneJuicePurity,
                        MassecuiteCOneJuiceAvailableSugar = x.MassecuiteCOneJuiceAvailableSugar,
                        MassecuiteCOneJuiceAvailableMolasses = x.MassecuiteCOneJuiceAvailableMolasses,
                        MassecuiteROneJuiceHl = x.MassecuiteROneJuiceHl,
                        MassecuiteROneJuiceBrix = x.MassecuiteROneJuiceBrix,
                        MassecuiteROneJuicePol = x.MassecuiteROneJuicePol,
                        MassecuiteROneJuicePurity = x.MassecuiteROneJuicePurity,
                        MassecuiteROneJuiceAvailableSugar = x.MassecuiteROneJuiceAvailableSugar,
                        MassecuiteROneJuiceAvailableMolasses = x.MassecuiteROneJuiceAvailableMolasses,
                        MassecuiteBJuiceHl = x.MassecuiteBJuiceHl,
                        MassecuiteBJuiceBrix = x.MassecuiteBJuiceBrix,
                        MassecuiteBJuicePol = x.MassecuiteBJuicePol,
                        MassecuiteBJuicePurity = x.MassecuiteBJuicePurity,
                        MassecuiteBJuiceAvailableSugar = x.MassecuiteBJuiceAvailableSugar,
                        MassecuiteBJuiceAvailableMolasses = x.MassecuiteBJuiceAvailableMolasses,
                        MassecuiteRTwoJuiceHl = x.MassecuiteRTwoJuiceHl,
                        MassecuiteRTwoJuiceBrix = x.MassecuiteRTwoJuiceBrix,
                        MassecuiteRTwoJuicePol = x.MassecuiteRTwoJuicePol,
                        MassecuiteRTwoJuicePurity = x.MassecuiteRTwoJuicePurity,
                        MassecuiteRTwoJuiceAvailableSugar = x.MassecuiteRTwoJuiceAvailableSugar,
                        MassecuiteRTwoJuiceAvailableMolasses = x.MassecuiteRTwoJuiceAvailableMolasses,
                        MassecuiteRThreeJuiceHl = x.MassecuiteRThreeJuiceHl,
                        MassecuiteRThreeJuiceBrix = x.MassecuiteRThreeJuiceBrix,
                        MassecuiteRThreeJuicePol = x.MassecuiteRThreeJuicePol,
                        MassecuiteRThreeJuicePurity = x.MassecuiteRThreeJuicePurity,
                        MassecuiteRThreeJuiceAvailableSugar = x.MassecuiteRThreeJuiceAvailableSugar,
                        MassecuiteRThreeJuiceAvailableMolasses = x.MassecuiteRThreeJuiceAvailableMolasses,
                        MolassesAHeavyJuiceHl = x.MolassesAHeavyJuiceHl,
                        MolassesAHeavyJuiceBrix = x.MolassesAHeavyJuiceBrix,
                        MolassesAHeavyJuicePol = x.MolassesAHeavyJuicePol,
                        MolassesAHeavyJuicePurity = x.MolassesAHeavyJuicePurity,
                        MolassesAHeavyJuiceAvailableSugar = x.MolassesAHeavyJuiceAvailableSugar,
                        MolassesAHeavyJuiceAvailableMolasses = x.MolassesAHeavyJuiceAvailableMolasses,
                        MolassesALightJuiceHl = x.MolassesALightJuiceHl,
                        MolassesALightJuiceBrix = x.MolassesALightJuiceBrix,
                        MolassesALightJuicePol = x.MolassesALightJuicePol,
                        MolassesALightJuicePurity = x.MolassesALightJuicePurity,
                        MolassesALightJuiceAvailableSugar = x.MolassesALightJuiceAvailableSugar,
                        MolassesALightJuiceAvailableMolasses = x.MolassesALightJuiceAvailableMolasses,
                        MolassesBHeavyJuiceHl = x.MolassesBHeavyJuiceHl,
                        MolassesBHeavyJuiceBrix = x.MolassesBHeavyJuiceBrix,
                        MolassesBHeavyJuicePol = x.MolassesBHeavyJuicePol,
                        MolassesBHeavyJuicePurity = x.MolassesBHeavyJuicePurity,
                        MolassesBHeavyJuiceAvailableSugar = x.MolassesBHeavyJuiceAvailableSugar,
                        MolassesBHeavyJuiceAvailableMolasses = x.MolassesBHeavyJuiceAvailableMolasses,
                        MolassesCLightJuiceHl = x.MolassesCLightJuiceHl,
                        MolassesCLightJuiceBrix = x.MolassesCLightJuiceBrix,
                        MolassesCLightJuicePol = x.MolassesCLightJuicePol,
                        MolassesCLightJuicePurity = x.MolassesCLightJuicePurity,
                        MolassesCLightJuiceAvailableSugar = x.MolassesCLightJuiceAvailableSugar,
                        MolassesCLightJuiceAvailableMolasses = x.MolassesCLightJuiceAvailableMolasses,
                        MolassesCOneJuiceHl = x.MolassesCOneJuiceHl,
                        MolassesCOneJuiceBrix = x.MolassesCOneJuiceBrix,
                        MolassesCOneJuicePol = x.MolassesCOneJuicePol,
                        MolassesCOneJuicePurity = x.MolassesCOneJuicePurity,
                        MolassesCOneJuiceAvailableSugar = x.MolassesCOneJuiceAvailableSugar,
                        MolassesCOneJuiceAvailableMolasses = x.MolassesCOneJuiceAvailableMolasses,
                        MolassesROneHeavyJuiceHl = x.MolassesROneHeavyJuiceHl,
                        MolassesROneHeavyJuiceBrix = x.MolassesROneHeavyJuiceBrix,
                        MolassesROneHeavyJuicePol = x.MolassesROneHeavyJuicePol,
                        MolassesROneHeavyJuicePurity = x.MolassesROneHeavyJuicePurity,
                        MolassesROneHeavyJuiceAvailableSugar = x.MolassesROneHeavyJuiceAvailableSugar,
                        MolassesROneHeavyJuiceAvailableMolasses = x.MolassesROneHeavyJuiceAvailableMolasses,
                        MolassesRTwoJuiceHl = x.MolassesRTwoJuiceHl,
                        MolassesRTwoJuiceBrix = x.MolassesRTwoJuiceBrix,
                        MolassesRTwoJuicePol = x.MolassesRTwoJuicePol,
                        MolassesRTwoJuicePurity = x.MolassesRTwoJuicePurity,
                        MolassesRTwoJuiceAvailableSugar = x.MolassesRTwoJuiceAvailableSugar,
                        MolassesRTwoJuiceAvailableMolasses = x.MolassesRTwoJuiceAvailableMolasses,
                        MolassesRThreeHeavyJuiceHl = x.MolassesRThreeHeavyJuiceHl,
                        MolassesRThreeHeavyJuiceBrix = x.MolassesRThreeHeavyJuiceBrix,
                        MolassesRThreeHeavyJuicePol = x.MolassesRThreeHeavyJuicePol,
                        MolassesRThreeHeavyJuicePurity = x.MolassesRThreeHeavyJuicePurity,
                        MolassesRThreeHeavyJuiceAvailableSugar = x.MolassesRThreeHeavyJuiceAvailableSugar,
                        MolassesRThreeHeavyJuiceAvailableMolasses = x.MolassesRThreeHeavyJuiceAvailableMolasses,
                        SugarUnweightedJuiceHl = x.SugarUnweightedJuiceHl,
                        SugarUnweightedJuiceBrix = x.SugarUnweightedJuiceBrix,
                        SugarUnweightedJuicePol = x.SugarUnweightedJuicePol,
                        SugarUnweightedJuicePurity = x.SugarUnweightedJuicePurity,
                        SugarUnweightedJuiceAvailableSugar = x.SugarUnweightedJuiceAvailableSugar,
                        SugarUnweightedJuiceAvailableMolasses = x.SugarUnweightedJuiceAvailableMolasses,
                        FineLiqorJuiceHl = x.FineLiqorJuiceHl,
                        FineLiqorJuiceBrix = x.FineLiqorJuiceBrix,
                        FineLiqorJuicePol = x.FineLiqorJuicePol,
                        FineLiqorJuicePurity = x.FineLiqorJuicePurity,
                        FineLiqorJuiceAvailableSugar = x.FineLiqorJuiceAvailableSugar,
                        FineLiqorJuiceAvailableMolasses = x.FineLiqorJuiceAvailableMolasses,
                        TotalJuiceHl = x.TotalJuiceHl,
                        TotalJuiceBrix = x.TotalJuiceBrix,
                        TotalJuicePol = x.TotalJuicePol,
                        TotalJuicePurity = x.TotalJuicePurity,
                        TotalJuiceAvailableSugar = x.TotalJuiceAvailableSugar,
                        TotalJuiceAvailableMolasses = x.TotalJuiceAvailableMolasses,
                        BagasseSaved = x.BagasseSaved,
                        BagasseToDistillery = x.BagasseToDistillery,
                        BagassePurchased = x.BagassePurchased

                    };

                    model.Add(temp);
                }
            }
            return View(model);
        }

        [HttpGet]
        [CustomAuthorizationFilter("Super Admin", "Unit Admin", "Developer")]
        [ValidationFilter("Create")]
        public ActionResult AddPeriodicalStock()
        {
            if (Session["UserCode"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            SetUnitDefaultValues();
            PeriodicalStockModel model = new PeriodicalStockModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationFilter("Create")]
        [ActionName("AddPeriodicalStock")]
        public ActionResult AddPeriodicalStockPost(PeriodicalStockModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }

            SetUnitDefaultValues();
            PeriodicalStock entity = new PeriodicalStock()
            {
                Id = Guid.NewGuid(),
                UnitCode = Convert.ToInt32(Session["BaseUnitCode"]),
                SeasonCode = ViewBag.CrushingSeason,
                EntryDate = model.EntryDate,
                MixedJuiceJuiceHl = model.MixedJuiceJuiceHl,
                MixedJuiceJuiceBrix = model.MixedJuiceJuiceBrix,
                MixedJuiceJuicePol = model.MixedJuiceJuicePol,
                MixedJuiceJuicePurity = model.MixedJuiceJuicePurity,
                MixedJuiceJuiceAvailableSugar = model.MixedJuiceJuiceAvailableSugar,
                MixedJuiceJuiceAvailableMolasses = model.MixedJuiceJuiceAvailableMolasses,
                ClearJuiceHl = model.ClearJuiceHl,
                ClearJuiceBrix = model.ClearJuiceBrix,
                ClearJuicePol = model.ClearJuicePol,
                ClearJuicePurity = model.ClearJuicePurity,
                ClearJuiceAvailableSugar = model.ClearJuiceAvailableSugar,
                ClearJuiceAvailableMolasses = model.ClearJuiceAvailableMolasses,
                SyrupJuiceHl = model.SyrupJuiceHl,
                SyrupJuiceBrix = model.SyrupJuiceBrix,
                SyrupJuicePol = model.SyrupJuicePol,
                SyrupJuicePurity = model.SyrupJuicePurity,
                SyrupJuiceAvailableSugar = model.SyrupJuiceAvailableSugar,
                SyrupJuiceAvailableMolasses = model.SyrupJuiceAvailableMolasses,
                SeedJuiceHl = model.SeedJuiceHl,
                SeedJuiceBrix = model.SeedJuiceBrix,
                SeedJuicePol = model.SeedJuicePol,
                SeedJuicePurity = model.SeedJuicePurity,
                SeedJuiceAvailableSugar = model.SeedJuiceAvailableSugar,
                SeedJuiceAvailableMolasses = model.SeedJuiceAvailableMolasses,
                MassecuiteAJuiceHl = model.MassecuiteAJuiceHl,
                MassecuiteAJuiceBrix = model.MassecuiteAJuiceBrix,
                MassecuiteAJuicePol = model.MassecuiteAJuicePol,
                MassecuiteAJuicePurity = model.MassecuiteAJuicePurity,
                MassecuiteAJuiceAvailableSugar = model.MassecuiteAJuiceAvailableSugar,
                MassecuiteAJuiceAvailableMolasses = model.MassecuiteAJuiceAvailableMolasses,
                MassecuiteCJuiceHl = model.MassecuiteCJuiceHl,
                MassecuiteCJuiceBrix = model.MassecuiteCJuiceBrix,
                MassecuiteCJuicePol = model.MassecuiteCJuicePol,
                MassecuiteCJuicePurity = model.MassecuiteCJuicePurity,
                MassecuiteCJuiceAvailableSugar = model.MassecuiteCJuiceAvailableSugar,
                MassecuiteCJuiceAvailableMolasses = model.MassecuiteCJuiceAvailableMolasses,
                MassecuiteCOneJuiceHl = model.MassecuiteCOneJuiceHl,
                MassecuiteCOneJuiceBrix = model.MassecuiteCOneJuiceBrix,
                MassecuiteCOneJuicePol = model.MassecuiteCOneJuicePol,
                MassecuiteCOneJuicePurity = model.MassecuiteCOneJuicePurity,
                MassecuiteCOneJuiceAvailableSugar = model.MassecuiteCOneJuiceAvailableSugar,
                MassecuiteCOneJuiceAvailableMolasses = model.MassecuiteCOneJuiceAvailableMolasses,
                MassecuiteROneJuiceHl = model.MassecuiteROneJuiceHl,
                MassecuiteROneJuiceBrix = model.MassecuiteROneJuiceBrix,
                MassecuiteROneJuicePol = model.MassecuiteROneJuicePol,
                MassecuiteROneJuicePurity = model.MassecuiteROneJuicePurity,
                MassecuiteROneJuiceAvailableSugar = model.MassecuiteROneJuiceAvailableSugar,
                MassecuiteROneJuiceAvailableMolasses = model.MassecuiteROneJuiceAvailableMolasses,
                MassecuiteBJuiceHl = model.MassecuiteBJuiceHl,
                MassecuiteBJuiceBrix = model.MassecuiteBJuiceBrix,
                MassecuiteBJuicePol = model.MassecuiteBJuicePol,
                MassecuiteBJuicePurity = model.MassecuiteBJuicePurity,
                MassecuiteBJuiceAvailableSugar = model.MassecuiteBJuiceAvailableSugar,
                MassecuiteBJuiceAvailableMolasses = model.MassecuiteBJuiceAvailableMolasses,
                MassecuiteRTwoJuiceHl = model.MassecuiteRTwoJuiceHl,
                MassecuiteRTwoJuiceBrix = model.MassecuiteRTwoJuiceBrix,
                MassecuiteRTwoJuicePol = model.MassecuiteRTwoJuicePol,
                MassecuiteRTwoJuicePurity = model.MassecuiteRTwoJuicePurity,
                MassecuiteRTwoJuiceAvailableSugar = model.MassecuiteRTwoJuiceAvailableSugar,
                MassecuiteRTwoJuiceAvailableMolasses = model.MassecuiteRTwoJuiceAvailableMolasses,
                MassecuiteRThreeJuiceHl = model.MassecuiteRThreeJuiceHl,
                MassecuiteRThreeJuiceBrix = model.MassecuiteRThreeJuiceBrix,
                MassecuiteRThreeJuicePol = model.MassecuiteRThreeJuicePol,
                MassecuiteRThreeJuicePurity = model.MassecuiteRThreeJuicePurity,
                MassecuiteRThreeJuiceAvailableSugar = model.MassecuiteRThreeJuiceAvailableSugar,
                MassecuiteRThreeJuiceAvailableMolasses = model.MassecuiteRThreeJuiceAvailableMolasses,
                MolassesAHeavyJuiceHl = model.MolassesAHeavyJuiceHl,
                MolassesAHeavyJuiceBrix = model.MolassesAHeavyJuiceBrix,
                MolassesAHeavyJuicePol = model.MolassesAHeavyJuicePol,
                MolassesAHeavyJuicePurity = model.MolassesAHeavyJuicePurity,
                MolassesAHeavyJuiceAvailableSugar = model.MolassesAHeavyJuiceAvailableSugar,
                MolassesAHeavyJuiceAvailableMolasses = model.MolassesAHeavyJuiceAvailableMolasses,
                MolassesALightJuiceHl = model.MolassesALightJuiceHl,
                MolassesALightJuiceBrix = model.MolassesALightJuiceBrix,
                MolassesALightJuicePol = model.MolassesALightJuicePol,
                MolassesALightJuicePurity = model.MolassesALightJuicePurity,
                MolassesALightJuiceAvailableSugar = model.MolassesALightJuiceAvailableSugar,
                MolassesALightJuiceAvailableMolasses = model.MolassesALightJuiceAvailableMolasses,
                MolassesBHeavyJuiceHl = model.MolassesBHeavyJuiceHl,
                MolassesBHeavyJuiceBrix = model.MolassesBHeavyJuiceBrix,
                MolassesBHeavyJuicePol = model.MolassesBHeavyJuicePol,
                MolassesBHeavyJuicePurity = model.MolassesBHeavyJuicePurity,
                MolassesBHeavyJuiceAvailableSugar = model.MolassesBHeavyJuiceAvailableSugar,
                MolassesBHeavyJuiceAvailableMolasses = model.MolassesBHeavyJuiceAvailableMolasses,
                MolassesCLightJuiceHl = model.MolassesCLightJuiceHl,
                MolassesCLightJuiceBrix = model.MolassesCLightJuiceBrix,
                MolassesCLightJuicePol = model.MolassesCLightJuicePol,
                MolassesCLightJuicePurity = model.MolassesCLightJuicePurity,
                MolassesCLightJuiceAvailableSugar = model.MolassesCLightJuiceAvailableSugar,
                MolassesCLightJuiceAvailableMolasses = model.MolassesCLightJuiceAvailableMolasses,
                MolassesCOneJuiceHl = model.MolassesCOneJuiceHl,
                MolassesCOneJuiceBrix = model.MolassesCOneJuiceBrix,
                MolassesCOneJuicePol = model.MolassesCOneJuicePol,
                MolassesCOneJuicePurity = model.MolassesCOneJuicePurity,
                MolassesCOneJuiceAvailableSugar = model.MolassesCOneJuiceAvailableSugar,
                MolassesCOneJuiceAvailableMolasses = model.MolassesCOneJuiceAvailableMolasses,
                MolassesROneHeavyJuiceHl = model.MolassesROneHeavyJuiceHl,
                MolassesROneHeavyJuiceBrix = model.MolassesROneHeavyJuiceBrix,
                MolassesROneHeavyJuicePol = model.MolassesROneHeavyJuicePol,
                MolassesROneHeavyJuicePurity = model.MolassesROneHeavyJuicePurity,
                MolassesROneHeavyJuiceAvailableSugar = model.MolassesROneHeavyJuiceAvailableSugar,
                MolassesROneHeavyJuiceAvailableMolasses = model.MolassesROneHeavyJuiceAvailableMolasses,
                MolassesRTwoJuiceHl = model.MolassesRTwoJuiceHl,
                MolassesRTwoJuiceBrix = model.MolassesRTwoJuiceBrix,
                MolassesRTwoJuicePol = model.MolassesRTwoJuicePol,
                MolassesRTwoJuicePurity = model.MolassesRTwoJuicePurity,
                MolassesRTwoJuiceAvailableSugar = model.MolassesRTwoJuiceAvailableSugar,
                MolassesRTwoJuiceAvailableMolasses = model.MolassesRTwoJuiceAvailableMolasses,
                MolassesRThreeHeavyJuiceHl = model.MolassesRThreeHeavyJuiceHl,
                MolassesRThreeHeavyJuiceBrix = model.MolassesRThreeHeavyJuiceBrix,
                MolassesRThreeHeavyJuicePol = model.MolassesRThreeHeavyJuicePol,
                MolassesRThreeHeavyJuicePurity = model.MolassesRThreeHeavyJuicePurity,
                MolassesRThreeHeavyJuiceAvailableSugar = model.MolassesRThreeHeavyJuiceAvailableSugar,
                MolassesRThreeHeavyJuiceAvailableMolasses = model.MolassesRThreeHeavyJuiceAvailableMolasses,
                SugarUnweightedJuiceHl = model.SugarUnweightedJuiceHl,
                SugarUnweightedJuiceBrix = model.SugarUnweightedJuiceBrix,
                SugarUnweightedJuicePol = model.SugarUnweightedJuicePol,
                SugarUnweightedJuicePurity = model.SugarUnweightedJuicePurity,
                SugarUnweightedJuiceAvailableSugar = model.SugarUnweightedJuiceAvailableSugar,
                SugarUnweightedJuiceAvailableMolasses = model.SugarUnweightedJuiceAvailableMolasses,
                FineLiqorJuiceHl = model.FineLiqorJuiceHl,
                FineLiqorJuiceBrix = model.FineLiqorJuiceBrix,
                FineLiqorJuicePol = model.FineLiqorJuicePol,
                FineLiqorJuicePurity = model.FineLiqorJuicePurity,
                FineLiqorJuiceAvailableSugar = model.FineLiqorJuiceAvailableSugar,
                FineLiqorJuiceAvailableMolasses = model.FineLiqorJuiceAvailableMolasses,
                TotalJuiceHl = model.TotalJuiceHl,
                TotalJuiceBrix = model.TotalJuiceBrix,
                TotalJuicePol = model.TotalJuicePol,
                TotalJuicePurity = model.TotalJuicePurity,
                TotalJuiceAvailableSugar = model.TotalJuiceAvailableSugar,
                TotalJuiceAvailableMolasses = model.TotalJuiceAvailableMolasses,
                BagasseSaved = model.BagasseSaved,
                BagasseToDistillery = model.BagasseToDistillery,
                BagassePurchased = model.BagassePurchased,
                IsDeleted = false,
                CreatedBy = Session["UserCode"].ToString(),
                CreatedAt = DateTime.Now
            };
            bool result = periodicalStockRepository.AddPerioducalStock(entity);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        [ValidationFilter("Edit")]
        public ActionResult Edit(Guid id)
        {
            PeriodicalStockModel m = new PeriodicalStockModel();
            try
            {
                PeriodicalStock p = new PeriodicalStock();
                p = periodicalStockRepository.GetPeriodicalStock(id);
                if (p != null)
                {
                    PeriodicalStockModel model = new PeriodicalStockModel()
                    {
                        UnitCode = p.UnitCode,
                        SeasonCode = p.SeasonCode,
                        EntryDate = p.EntryDate,
                        MixedJuiceJuiceHl = p.MixedJuiceJuiceHl,
                        MixedJuiceJuiceBrix = p.MixedJuiceJuiceBrix,
                        MixedJuiceJuicePol = p.MixedJuiceJuicePol,
                        MixedJuiceJuicePurity = p.MixedJuiceJuicePurity,
                        MixedJuiceJuiceAvailableSugar = p.MixedJuiceJuiceAvailableSugar,
                        MixedJuiceJuiceAvailableMolasses = p.MixedJuiceJuiceAvailableMolasses,
                        ClearJuiceHl = p.ClearJuiceHl,
                        ClearJuiceBrix = p.ClearJuiceBrix,
                        ClearJuicePol = p.ClearJuicePol,
                        ClearJuicePurity = p.ClearJuicePurity,
                        ClearJuiceAvailableSugar = p.ClearJuiceAvailableSugar,
                        ClearJuiceAvailableMolasses = p.ClearJuiceAvailableMolasses,
                        SyrupJuiceHl = p.SyrupJuiceHl,
                        SyrupJuiceBrix = p.SyrupJuiceBrix,
                        SyrupJuicePol = p.SyrupJuicePol,
                        SyrupJuicePurity = p.SyrupJuicePurity,
                        SyrupJuiceAvailableSugar = p.SyrupJuiceAvailableSugar,
                        SyrupJuiceAvailableMolasses = p.SyrupJuiceAvailableMolasses,
                        SeedJuiceHl = p.SeedJuiceHl,
                        SeedJuiceBrix = p.SeedJuiceBrix,
                        SeedJuicePol = p.SeedJuicePol,
                        SeedJuicePurity = p.SeedJuicePurity,
                        SeedJuiceAvailableSugar = p.SeedJuiceAvailableSugar,
                        SeedJuiceAvailableMolasses = p.SeedJuiceAvailableMolasses,
                        MassecuiteAJuiceHl = p.MassecuiteAJuiceHl,
                        MassecuiteAJuiceBrix = p.MassecuiteAJuiceBrix,
                        MassecuiteAJuicePol = p.MassecuiteAJuicePol,
                        MassecuiteAJuicePurity = p.MassecuiteAJuicePurity,
                        MassecuiteAJuiceAvailableSugar = p.MassecuiteAJuiceAvailableSugar,
                        MassecuiteAJuiceAvailableMolasses = p.MassecuiteAJuiceAvailableMolasses,
                        MassecuiteCJuiceHl = p.MassecuiteCJuiceHl,
                        MassecuiteCJuiceBrix = p.MassecuiteCJuiceBrix,
                        MassecuiteCJuicePol = p.MassecuiteCJuicePol,
                        MassecuiteCJuicePurity = p.MassecuiteCJuicePurity,
                        MassecuiteCJuiceAvailableSugar = p.MassecuiteCJuiceAvailableSugar,
                        MassecuiteCJuiceAvailableMolasses = p.MassecuiteCJuiceAvailableMolasses,
                        MassecuiteCOneJuiceHl = p.MassecuiteCOneJuiceHl,
                        MassecuiteCOneJuiceBrix = p.MassecuiteCOneJuiceBrix,
                        MassecuiteCOneJuicePol = p.MassecuiteCOneJuicePol,
                        MassecuiteCOneJuicePurity = p.MassecuiteCOneJuicePurity,
                        MassecuiteCOneJuiceAvailableSugar = p.MassecuiteCOneJuiceAvailableSugar,
                        MassecuiteCOneJuiceAvailableMolasses = p.MassecuiteCOneJuiceAvailableMolasses,
                        MassecuiteROneJuiceHl = p.MassecuiteROneJuiceHl,
                        MassecuiteROneJuiceBrix = p.MassecuiteROneJuiceBrix,
                        MassecuiteROneJuicePol = p.MassecuiteROneJuicePol,
                        MassecuiteROneJuicePurity = p.MassecuiteROneJuicePurity,
                        MassecuiteROneJuiceAvailableSugar = p.MassecuiteROneJuiceAvailableSugar,
                        MassecuiteROneJuiceAvailableMolasses = p.MassecuiteROneJuiceAvailableMolasses,
                        MassecuiteBJuiceHl = p.MassecuiteBJuiceHl,
                        MassecuiteBJuiceBrix = p.MassecuiteBJuiceBrix,
                        MassecuiteBJuicePol = p.MassecuiteBJuicePol,
                        MassecuiteBJuicePurity = p.MassecuiteBJuicePurity,
                        MassecuiteBJuiceAvailableSugar = p.MassecuiteBJuiceAvailableSugar,
                        MassecuiteBJuiceAvailableMolasses = p.MassecuiteBJuiceAvailableMolasses,
                        MassecuiteRTwoJuiceHl = p.MassecuiteRTwoJuiceHl,
                        MassecuiteRTwoJuiceBrix = p.MassecuiteRTwoJuiceBrix,
                        MassecuiteRTwoJuicePol = p.MassecuiteRTwoJuicePol,
                        MassecuiteRTwoJuicePurity = p.MassecuiteRTwoJuicePurity,
                        MassecuiteRTwoJuiceAvailableSugar = p.MassecuiteRTwoJuiceAvailableSugar,
                        MassecuiteRTwoJuiceAvailableMolasses = p.MassecuiteRTwoJuiceAvailableMolasses,
                        MassecuiteRThreeJuiceHl = p.MassecuiteRThreeJuiceHl,
                        MassecuiteRThreeJuiceBrix = p.MassecuiteRThreeJuiceBrix,
                        MassecuiteRThreeJuicePol = p.MassecuiteRThreeJuicePol,
                        MassecuiteRThreeJuicePurity = p.MassecuiteRThreeJuicePurity,
                        MassecuiteRThreeJuiceAvailableSugar = p.MassecuiteRThreeJuiceAvailableSugar,
                        MassecuiteRThreeJuiceAvailableMolasses = p.MassecuiteRThreeJuiceAvailableMolasses,
                        MolassesAHeavyJuiceHl = p.MolassesAHeavyJuiceHl,
                        MolassesAHeavyJuiceBrix = p.MolassesAHeavyJuiceBrix,
                        MolassesAHeavyJuicePol = p.MolassesAHeavyJuicePol,
                        MolassesAHeavyJuicePurity = p.MolassesAHeavyJuicePurity,
                        MolassesAHeavyJuiceAvailableSugar = p.MolassesAHeavyJuiceAvailableSugar,
                        MolassesAHeavyJuiceAvailableMolasses = p.MolassesAHeavyJuiceAvailableMolasses,
                        MolassesALightJuiceHl = p.MolassesALightJuiceHl,
                        MolassesALightJuiceBrix = p.MolassesALightJuiceBrix,
                        MolassesALightJuicePol = p.MolassesALightJuicePol,
                        MolassesALightJuicePurity = p.MolassesALightJuicePurity,
                        MolassesALightJuiceAvailableSugar = p.MolassesALightJuiceAvailableSugar,
                        MolassesALightJuiceAvailableMolasses = p.MolassesALightJuiceAvailableMolasses,
                        MolassesBHeavyJuiceHl = p.MolassesBHeavyJuiceHl,
                        MolassesBHeavyJuiceBrix = p.MolassesBHeavyJuiceBrix,
                        MolassesBHeavyJuicePol = p.MolassesBHeavyJuicePol,
                        MolassesBHeavyJuicePurity = p.MolassesBHeavyJuicePurity,
                        MolassesBHeavyJuiceAvailableSugar = p.MolassesBHeavyJuiceAvailableSugar,
                        MolassesBHeavyJuiceAvailableMolasses = p.MolassesBHeavyJuiceAvailableMolasses,
                        MolassesCLightJuiceHl = p.MolassesCLightJuiceHl,
                        MolassesCLightJuiceBrix = p.MolassesCLightJuiceBrix,
                        MolassesCLightJuicePol = p.MolassesCLightJuicePol,
                        MolassesCLightJuicePurity = p.MolassesCLightJuicePurity,
                        MolassesCLightJuiceAvailableSugar = p.MolassesCLightJuiceAvailableSugar,
                        MolassesCLightJuiceAvailableMolasses = p.MolassesCLightJuiceAvailableMolasses,
                        MolassesCOneJuiceHl = p.MolassesCOneJuiceHl,
                        MolassesCOneJuiceBrix = p.MolassesCOneJuiceBrix,
                        MolassesCOneJuicePol = p.MolassesCOneJuicePol,
                        MolassesCOneJuicePurity = p.MolassesCOneJuicePurity,
                        MolassesCOneJuiceAvailableSugar = p.MolassesCOneJuiceAvailableSugar,
                        MolassesCOneJuiceAvailableMolasses = p.MolassesCOneJuiceAvailableMolasses,
                        MolassesROneHeavyJuiceHl = p.MolassesROneHeavyJuiceHl,
                        MolassesROneHeavyJuiceBrix = p.MolassesROneHeavyJuiceBrix,
                        MolassesROneHeavyJuicePol = p.MolassesROneHeavyJuicePol,
                        MolassesROneHeavyJuicePurity = p.MolassesROneHeavyJuicePurity,
                        MolassesROneHeavyJuiceAvailableSugar = p.MolassesROneHeavyJuiceAvailableSugar,
                        MolassesROneHeavyJuiceAvailableMolasses = p.MolassesROneHeavyJuiceAvailableMolasses,
                        MolassesRTwoJuiceHl = p.MolassesRTwoJuiceHl,
                        MolassesRTwoJuiceBrix = p.MolassesRTwoJuiceBrix,
                        MolassesRTwoJuicePol = p.MolassesRTwoJuicePol,
                        MolassesRTwoJuicePurity = p.MolassesRTwoJuicePurity,
                        MolassesRTwoJuiceAvailableSugar = p.MolassesRTwoJuiceAvailableSugar,
                        MolassesRTwoJuiceAvailableMolasses = p.MolassesRTwoJuiceAvailableMolasses,
                        MolassesRThreeHeavyJuiceHl = p.MolassesRThreeHeavyJuiceHl,
                        MolassesRThreeHeavyJuiceBrix = p.MolassesRThreeHeavyJuiceBrix,
                        MolassesRThreeHeavyJuicePol = p.MolassesRThreeHeavyJuicePol,
                        MolassesRThreeHeavyJuicePurity = p.MolassesRThreeHeavyJuicePurity,
                        MolassesRThreeHeavyJuiceAvailableSugar = p.MolassesRThreeHeavyJuiceAvailableSugar,
                        MolassesRThreeHeavyJuiceAvailableMolasses = p.MolassesRThreeHeavyJuiceAvailableMolasses,
                        SugarUnweightedJuiceHl = p.SugarUnweightedJuiceHl,
                        SugarUnweightedJuiceBrix = p.SugarUnweightedJuiceBrix,
                        SugarUnweightedJuicePol = p.SugarUnweightedJuicePol,
                        SugarUnweightedJuicePurity = p.SugarUnweightedJuicePurity,
                        SugarUnweightedJuiceAvailableSugar = p.SugarUnweightedJuiceAvailableSugar,
                        SugarUnweightedJuiceAvailableMolasses = p.SugarUnweightedJuiceAvailableMolasses,
                        FineLiqorJuiceHl = p.FineLiqorJuiceHl,
                        FineLiqorJuiceBrix = p.FineLiqorJuiceBrix,
                        FineLiqorJuicePol = p.FineLiqorJuicePol,
                        FineLiqorJuicePurity = p.FineLiqorJuicePurity,
                        FineLiqorJuiceAvailableSugar = p.FineLiqorJuiceAvailableSugar,
                        FineLiqorJuiceAvailableMolasses = p.FineLiqorJuiceAvailableMolasses,
                        TotalJuiceHl = p.TotalJuiceHl,
                        TotalJuiceBrix = p.TotalJuiceBrix,
                        TotalJuicePol = p.TotalJuicePol,
                        TotalJuicePurity = p.TotalJuicePurity,
                        TotalJuiceAvailableSugar = p.TotalJuiceAvailableSugar,
                        TotalJuiceAvailableMolasses = p.TotalJuiceAvailableMolasses,
                        BagasseSaved = p.BagasseSaved,
                        BagasseToDistillery = p.BagasseToDistillery,
                        BagassePurchased = p.BagassePurchased,
                        IsDeleted = p.IsDeleted,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt

                    };
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return View(m);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationFilter("Edit")]
        [ActionName("Edit")]
        public ActionResult EditPost(PeriodicalStockModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View();
            }
            PeriodicalStock entity = new PeriodicalStock()
            {
                Id = model.Id,
                UnitCode = model.UnitCode,
                SeasonCode = model.SeasonCode,
                EntryDate = model.EntryDate,
                MixedJuiceJuiceHl = model.MixedJuiceJuiceHl,
                MixedJuiceJuiceBrix = model.MixedJuiceJuiceBrix,
                MixedJuiceJuicePol = model.MixedJuiceJuicePol,
                MixedJuiceJuicePurity = model.MixedJuiceJuicePurity,
                MixedJuiceJuiceAvailableSugar = model.MixedJuiceJuiceAvailableSugar,
                MixedJuiceJuiceAvailableMolasses = model.MixedJuiceJuiceAvailableMolasses,
                ClearJuiceHl = model.ClearJuiceHl,
                ClearJuiceBrix = model.ClearJuiceBrix,
                ClearJuicePol = model.ClearJuicePol,
                ClearJuicePurity = model.ClearJuicePurity,
                ClearJuiceAvailableSugar = model.ClearJuiceAvailableSugar,
                ClearJuiceAvailableMolasses = model.ClearJuiceAvailableMolasses,
                SyrupJuiceHl = model.SyrupJuiceHl,
                SyrupJuiceBrix = model.SyrupJuiceBrix,
                SyrupJuicePol = model.SyrupJuicePol,
                SyrupJuicePurity = model.SyrupJuicePurity,
                SyrupJuiceAvailableSugar = model.SyrupJuiceAvailableSugar,
                SyrupJuiceAvailableMolasses = model.SyrupJuiceAvailableMolasses,
                SeedJuiceHl = model.SeedJuiceHl,
                SeedJuiceBrix = model.SeedJuiceBrix,
                SeedJuicePol = model.SeedJuicePol,
                SeedJuicePurity = model.SeedJuicePurity,
                SeedJuiceAvailableSugar = model.SeedJuiceAvailableSugar,
                SeedJuiceAvailableMolasses = model.SeedJuiceAvailableMolasses,
                MassecuiteAJuiceHl = model.MassecuiteAJuiceHl,
                MassecuiteAJuiceBrix = model.MassecuiteAJuiceBrix,
                MassecuiteAJuicePol = model.MassecuiteAJuicePol,
                MassecuiteAJuicePurity = model.MassecuiteAJuicePurity,
                MassecuiteAJuiceAvailableSugar = model.MassecuiteAJuiceAvailableSugar,
                MassecuiteAJuiceAvailableMolasses = model.MassecuiteAJuiceAvailableMolasses,
                MassecuiteCJuiceHl = model.MassecuiteCJuiceHl,
                MassecuiteCJuiceBrix = model.MassecuiteCJuiceBrix,
                MassecuiteCJuicePol = model.MassecuiteCJuicePol,
                MassecuiteCJuicePurity = model.MassecuiteCJuicePurity,
                MassecuiteCJuiceAvailableSugar = model.MassecuiteCJuiceAvailableSugar,
                MassecuiteCJuiceAvailableMolasses = model.MassecuiteCJuiceAvailableMolasses,
                MassecuiteCOneJuiceHl = model.MassecuiteCOneJuiceHl,
                MassecuiteCOneJuiceBrix = model.MassecuiteCOneJuiceBrix,
                MassecuiteCOneJuicePol = model.MassecuiteCOneJuicePol,
                MassecuiteCOneJuicePurity = model.MassecuiteCOneJuicePurity,
                MassecuiteCOneJuiceAvailableSugar = model.MassecuiteCOneJuiceAvailableSugar,
                MassecuiteCOneJuiceAvailableMolasses = model.MassecuiteCOneJuiceAvailableMolasses,
                MassecuiteROneJuiceHl = model.MassecuiteROneJuiceHl,
                MassecuiteROneJuiceBrix = model.MassecuiteROneJuiceBrix,
                MassecuiteROneJuicePol = model.MassecuiteROneJuicePol,
                MassecuiteROneJuicePurity = model.MassecuiteROneJuicePurity,
                MassecuiteROneJuiceAvailableSugar = model.MassecuiteROneJuiceAvailableSugar,
                MassecuiteROneJuiceAvailableMolasses = model.MassecuiteROneJuiceAvailableMolasses,
                MassecuiteBJuiceHl = model.MassecuiteBJuiceHl,
                MassecuiteBJuiceBrix = model.MassecuiteBJuiceBrix,
                MassecuiteBJuicePol = model.MassecuiteBJuicePol,
                MassecuiteBJuicePurity = model.MassecuiteBJuicePurity,
                MassecuiteBJuiceAvailableSugar = model.MassecuiteBJuiceAvailableSugar,
                MassecuiteBJuiceAvailableMolasses = model.MassecuiteBJuiceAvailableMolasses,
                MassecuiteRTwoJuiceHl = model.MassecuiteRTwoJuiceHl,
                MassecuiteRTwoJuiceBrix = model.MassecuiteRTwoJuiceBrix,
                MassecuiteRTwoJuicePol = model.MassecuiteRTwoJuicePol,
                MassecuiteRTwoJuicePurity = model.MassecuiteRTwoJuicePurity,
                MassecuiteRTwoJuiceAvailableSugar = model.MassecuiteRTwoJuiceAvailableSugar,
                MassecuiteRTwoJuiceAvailableMolasses = model.MassecuiteRTwoJuiceAvailableMolasses,
                MassecuiteRThreeJuiceHl = model.MassecuiteRThreeJuiceHl,
                MassecuiteRThreeJuiceBrix = model.MassecuiteRThreeJuiceBrix,
                MassecuiteRThreeJuicePol = model.MassecuiteRThreeJuicePol,
                MassecuiteRThreeJuicePurity = model.MassecuiteRThreeJuicePurity,
                MassecuiteRThreeJuiceAvailableSugar = model.MassecuiteRThreeJuiceAvailableSugar,
                MassecuiteRThreeJuiceAvailableMolasses = model.MassecuiteRThreeJuiceAvailableMolasses,
                MolassesAHeavyJuiceHl = model.MolassesAHeavyJuiceHl,
                MolassesAHeavyJuiceBrix = model.MolassesAHeavyJuiceBrix,
                MolassesAHeavyJuicePol = model.MolassesAHeavyJuicePol,
                MolassesAHeavyJuicePurity = model.MolassesAHeavyJuicePurity,
                MolassesAHeavyJuiceAvailableSugar = model.MolassesAHeavyJuiceAvailableSugar,
                MolassesAHeavyJuiceAvailableMolasses = model.MolassesAHeavyJuiceAvailableMolasses,
                MolassesALightJuiceHl = model.MolassesALightJuiceHl,
                MolassesALightJuiceBrix = model.MolassesALightJuiceBrix,
                MolassesALightJuicePol = model.MolassesALightJuicePol,
                MolassesALightJuicePurity = model.MolassesALightJuicePurity,
                MolassesALightJuiceAvailableSugar = model.MolassesALightJuiceAvailableSugar,
                MolassesALightJuiceAvailableMolasses = model.MolassesALightJuiceAvailableMolasses,
                MolassesBHeavyJuiceHl = model.MolassesBHeavyJuiceHl,
                MolassesBHeavyJuiceBrix = model.MolassesBHeavyJuiceBrix,
                MolassesBHeavyJuicePol = model.MolassesBHeavyJuicePol,
                MolassesBHeavyJuicePurity = model.MolassesBHeavyJuicePurity,
                MolassesBHeavyJuiceAvailableSugar = model.MolassesBHeavyJuiceAvailableSugar,
                MolassesBHeavyJuiceAvailableMolasses = model.MolassesBHeavyJuiceAvailableMolasses,
                MolassesCLightJuiceHl = model.MolassesCLightJuiceHl,
                MolassesCLightJuiceBrix = model.MolassesCLightJuiceBrix,
                MolassesCLightJuicePol = model.MolassesCLightJuicePol,
                MolassesCLightJuicePurity = model.MolassesCLightJuicePurity,
                MolassesCLightJuiceAvailableSugar = model.MolassesCLightJuiceAvailableSugar,
                MolassesCLightJuiceAvailableMolasses = model.MolassesCLightJuiceAvailableMolasses,
                MolassesCOneJuiceHl = model.MolassesCOneJuiceHl,
                MolassesCOneJuiceBrix = model.MolassesCOneJuiceBrix,
                MolassesCOneJuicePol = model.MolassesCOneJuicePol,
                MolassesCOneJuicePurity = model.MolassesCOneJuicePurity,
                MolassesCOneJuiceAvailableSugar = model.MolassesCOneJuiceAvailableSugar,
                MolassesCOneJuiceAvailableMolasses = model.MolassesCOneJuiceAvailableMolasses,
                MolassesROneHeavyJuiceHl = model.MolassesROneHeavyJuiceHl,
                MolassesROneHeavyJuiceBrix = model.MolassesROneHeavyJuiceBrix,
                MolassesROneHeavyJuicePol = model.MolassesROneHeavyJuicePol,
                MolassesROneHeavyJuicePurity = model.MolassesROneHeavyJuicePurity,
                MolassesROneHeavyJuiceAvailableSugar = model.MolassesROneHeavyJuiceAvailableSugar,
                MolassesROneHeavyJuiceAvailableMolasses = model.MolassesROneHeavyJuiceAvailableMolasses,
                MolassesRTwoJuiceHl = model.MolassesRTwoJuiceHl,
                MolassesRTwoJuiceBrix = model.MolassesRTwoJuiceBrix,
                MolassesRTwoJuicePol = model.MolassesRTwoJuicePol,
                MolassesRTwoJuicePurity = model.MolassesRTwoJuicePurity,
                MolassesRTwoJuiceAvailableSugar = model.MolassesRTwoJuiceAvailableSugar,
                MolassesRTwoJuiceAvailableMolasses = model.MolassesRTwoJuiceAvailableMolasses,
                MolassesRThreeHeavyJuiceHl = model.MolassesRThreeHeavyJuiceHl,
                MolassesRThreeHeavyJuiceBrix = model.MolassesRThreeHeavyJuiceBrix,
                MolassesRThreeHeavyJuicePol = model.MolassesRThreeHeavyJuicePol,
                MolassesRThreeHeavyJuicePurity = model.MolassesRThreeHeavyJuicePurity,
                MolassesRThreeHeavyJuiceAvailableSugar = model.MolassesRThreeHeavyJuiceAvailableSugar,
                MolassesRThreeHeavyJuiceAvailableMolasses = model.MolassesRThreeHeavyJuiceAvailableMolasses,
                SugarUnweightedJuiceHl = model.SugarUnweightedJuiceHl,
                SugarUnweightedJuiceBrix = model.SugarUnweightedJuiceBrix,
                SugarUnweightedJuicePol = model.SugarUnweightedJuicePol,
                SugarUnweightedJuicePurity = model.SugarUnweightedJuicePurity,
                SugarUnweightedJuiceAvailableSugar = model.SugarUnweightedJuiceAvailableSugar,
                SugarUnweightedJuiceAvailableMolasses = model.SugarUnweightedJuiceAvailableMolasses,
                FineLiqorJuiceHl = model.FineLiqorJuiceHl,
                FineLiqorJuiceBrix = model.FineLiqorJuiceBrix,
                FineLiqorJuicePol = model.FineLiqorJuicePol,
                FineLiqorJuicePurity = model.FineLiqorJuicePurity,
                FineLiqorJuiceAvailableSugar = model.FineLiqorJuiceAvailableSugar,
                FineLiqorJuiceAvailableMolasses = model.FineLiqorJuiceAvailableMolasses,
                TotalJuiceHl = model.TotalJuiceHl,
                TotalJuiceBrix = model.TotalJuiceBrix,
                TotalJuicePol = model.TotalJuicePol,
                TotalJuicePurity = model.TotalJuicePurity,
                TotalJuiceAvailableSugar = model.TotalJuiceAvailableSugar,
                TotalJuiceAvailableMolasses = model.TotalJuiceAvailableMolasses,
                BagasseSaved = model.BagasseSaved,
                BagasseToDistillery = model.BagasseToDistillery,
                BagassePurchased = model.BagassePurchased,
                IsDeleted = model.IsDeleted,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
            if (periodicalStockRepository.EditPerioducalStock(entity))
            {
                return RedirectToAction("Index");
            }
            return View();


        }


        /// <summary>
        /// A function wich set some default values in viewbag so that we can use them 
        /// in other Action methods
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

            TempData["BaseUnitCode"] = Session["BaseUnitCode"];
            ViewBag.UnitName = UnitDefaultValues.Name;
            ViewBag.EntryDate = UnitDefaultValues.EntryDate.ToShortDateString();
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now;
        }



    }
}