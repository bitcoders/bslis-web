using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace LitmusWeb.Controllers.Reports
{
    public class LedgerPrintingController : Controller
    {
        // GET: Ledger
        LedgerPrintingRepository Repository = new LedgerPrintingRepository();

        public ActionResult Index()
        {
            if (Session["UserCode"] == null)
            {
                TempData["PreviousUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer;
                return RedirectToAction("Index", "Home");
            }
            SetUnitDefaultValues();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult GetFile(FormCollection ledgerForm)
        {
            SetUnitDefaultValues();

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            string reportdate = ledgerForm["reportDate"].ToString();
            foreach (var data in LedgerData(reportdate))
            {
                textWriter.WriteLine(data.LedgerText);
            }
            textWriter.Flush();
            var length = memoryStream.Length;
            textWriter.Close();
            var toWrite = new byte[length];

            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);
            return File(memoryStream.GetBuffer(), "text/plain", "ledger.txt");
        }

        /// <summary>
        /// a function which will return the complete data of ledger 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private List<LedgerPrintingModel> LedgerData(string reportDate)
        {
            HttpContext.Server.ScriptTimeout = 500;
            SetUnitDefaultValues();
            List<LedgerPrintingModel> ledgerPrintingModel = new List<LedgerPrintingModel>();

            List<proc_ledger_page_one_Result> resultPageOne = new List<proc_ledger_page_one_Result>();
            List<proc_ledger_page_two_Result> resultPageTwo = new List<proc_ledger_page_two_Result>();
            List<proc_ledger_page_three_Result> resultPageThree = new List<proc_ledger_page_three_Result>();
            List<proc_ledger_page_four_Result> resultPageFour = new List<proc_ledger_page_four_Result>();
            List<proc_ledger_page_five_Result> resultPageFive = new List<proc_ledger_page_five_Result>();
            List<proc_ledger_page_six_Result> resultPageSix = new List<proc_ledger_page_six_Result>();
            List<proc_ledger_page_seven_Result> resultPageSeven = new List<proc_ledger_page_seven_Result>();
            List<proc_ledger_page_eight_Result> resultPageEight = new List<proc_ledger_page_eight_Result>();
            List<proc_ledger_page_nine_Result> resultPageNine = new List<proc_ledger_page_nine_Result>();
            List<proc_ledger_page_ten_Result> resultPageTen = new List<proc_ledger_page_ten_Result>();
            List<proc_ledger_page_eleven_Result> resultPageEleven = new List<proc_ledger_page_eleven_Result>();
            List<proc_ledger_page_twelve_Result> resultPageTwelve = new List<proc_ledger_page_twelve_Result>();
            List<proc_ledger_page_thirteen_Result> resultPageThirteen = new List<proc_ledger_page_thirteen_Result>();

            //resultPageOne = Repository.PrintLedgerPageOne(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageTwo = Repository.PrintLedgerPageTwo(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageThree = Repository.PrintLedgerPageThree(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageFour = Repository.PrintLedgerPageFour(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageFive = Repository.PrintLedgerPageFive(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageSix = Repository.PrintLedgerPageSix(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageSeven = Repository.PrintLedgerPageSeven(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageEight = Repository.PrintLedgerPageEight(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageNine = Repository.PrintLedgerPageNine(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageTen = Repository.PrintLedgerPageTen(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageEleven = Repository.PrintLedgerPageEleven(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageTwelve = Repository.PrintLedgerPageTwelve(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));
            //resultPageThirteen = Repository.PrintLedgerPageThirteen(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(ViewBag.ProcessDate));


            resultPageOne = Repository.PrintLedgerPageOne(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageTwo = Repository.PrintLedgerPageTwo(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageThree = Repository.PrintLedgerPageThree(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageFour = Repository.PrintLedgerPageFour(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageFive = Repository.PrintLedgerPageFive(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageSix = Repository.PrintLedgerPageSix(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageSeven = Repository.PrintLedgerPageSeven(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageEight = Repository.PrintLedgerPageEight(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageNine = Repository.PrintLedgerPageNine(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageTen = Repository.PrintLedgerPageTen(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageEleven = Repository.PrintLedgerPageEleven(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageTwelve = Repository.PrintLedgerPageTwelve(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));
            resultPageThirteen = Repository.PrintLedgerPageThirteen(Convert.ToInt16(Session["BaseUnitCode"]), Convert.ToInt16(ViewBag.CrushingSeason), Convert.ToDateTime(reportDate));

            foreach (var a in resultPageOne)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = a.text
                };
                ledgerPrintingModel.Add(tempModel);
            }

            foreach (var b in resultPageTwo)
            {

                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }

            foreach (var b in resultPageThree)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }

            if (resultPageFour != null)
            {
                foreach (var b in resultPageFour)
                {
                    LedgerPrintingModel tempModel = new LedgerPrintingModel()
                    {
                        LedgerText = b.text
                    };
                    ledgerPrintingModel.Add(tempModel);
                }
            }

            if (resultPageFive != null)
            {
                foreach (var b in resultPageFive)
                {
                    LedgerPrintingModel tempModel = new LedgerPrintingModel()
                    {
                        LedgerText = b.text
                    };
                    ledgerPrintingModel.Add(tempModel);
                }
            }

            foreach (var b in resultPageSix)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageSeven)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }

            foreach (var b in resultPageEight)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageNine)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageTen)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageEleven)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageTwelve)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }
            foreach (var b in resultPageThirteen)
            {
                LedgerPrintingModel tempModel = new LedgerPrintingModel()
                {
                    LedgerText = b.text
                };
                ledgerPrintingModel.Add(tempModel);
            }

            return ledgerPrintingModel;
        }

        /// <summary>
        /// set default values for the unit based on current session data
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
            ViewBag.EntryDate = UnitDefaultValues.EntryDate;
            ViewBag.ProcessDate = UnitDefaultValues.ProcessDate;
            ViewBag.CrushingSeason = UnitDefaultValues.CrushingSeason;
            ViewBag.EntryTime = DateTime.Now.ToShortTimeString();
        }
    }
}