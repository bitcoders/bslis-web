using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;

namespace DataAccess.Repositories.ReportsRepository
{

    /// <summary>
    /// A repository which is used to download raw data in excel format at user end.
    /// </summary>
    public class AnalysisRawDataExcelRepository
    {
        public void FetchRawData(int analysisTypeCode, int unitCode,int seasonCode, DateTime fromDate, DateTime toDate)
        {

        }

        public string FetchRawData(int analysisTypeCode, int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string filePath, bool incluedDeleted = false)
        {
            
            string fileName = analysisTypeCode + "_" + unitCode + "_" + seasonCode + "_" + DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            switch(analysisTypeCode)
            {
                case 1:
                    GenerateExcelForHourlyData(unitCode, seasonCode, fromDate, toDate, "Hourly Data", filePath);
                    break;
                case 2:
                    GenerateExcelForTwoHourlyData(unitCode, seasonCode, fromDate, toDate, "Two Hourly Data", filePath);
                    break;
                case 3:
                    //Massecuite
                    break;
                case 4:
                    //Molasses
                    break;
                case 5:
                    //Meltings 
                    break;
                case 6:
                    //Key Sample
                    break;
                case 7:
                    // Stoppage
                      GenerateExcelForStoppageData(unitCode, seasonCode, fromDate, toDate,"Stoppages", filePath, incluedDeleted);
                    break;
                case 8:
                    GenerateExcelForDailyAnalysesyData(unitCode, seasonCode, fromDate, toDate, "Daily Analyses", filePath);
                    break;
                case 9:
                    //Data Adjustments
                    GenerateExcelForDataAdjustment(unitCode, seasonCode, fromDate, toDate, "Data Adjustment", filePath);
                    break;
                case 10:
                    GenerateExcelForLedgerData(unitCode, seasonCode, fromDate, toDate, "Processed Data", filePath);
                    break;
                default:
                    break;
            }
            return filePath;

        }


        private void GenerateExcelForStoppageData(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath, bool incluedDeleted = false)
        {
            List<Stoppage> stoppages = new List<Stoppage>();
            List<String> columnNames = new List<string>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                stoppages = Db.Stoppages.Where(s => s.unit_code == unitCode
                    && s.season_code == seasonCode
                    && s.s_date >= fromDate
                    && s.s_date <= toDate
                    && s.is_deleted == incluedDeleted
                ).ToList();
            }
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Raw Data";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if(stoppages != null)
                {
                    columnNames = typeof(Stoppage).GetProperties().Select(p => p.Name).ToList();
                    char startCell = 'A';
                    int AsciiCode = Convert.ToInt32(startCell);
                    foreach (var item in columnNames)
                    {
                        string cellName = Convert.ToChar(AsciiCode) + "1";
                        ws.Cell(cellName).Value = item;
                        AsciiCode = AsciiCode + 1;
                    }
                    
                    ws.Cell(2, 1).InsertData(stoppages);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);

            }
        }

        /// <summary>
        /// Get list of Hourly Analyses Data for the season
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="worksheetName"></param>
        /// <param name="filePath"></param>
        private void GenerateExcelForHourlyData(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath)
        {
            List<HourlyAnalys> hourlyAnalys = new List<HourlyAnalys>();
            List<string> columnNames = new List<string>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                hourlyAnalys = Db.HourlyAnalyses.Where(@h => h.unit_code == unitCode && h.season_code == seasonCode
                && h.entry_Date >= fromDate && h.entry_Date <= toDate
                ).ToList();
            }
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Hourly Analyses";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if(hourlyAnalys.Count > 0)
                {
                    columnNames = typeof(HourlyAnalys).GetProperties().Select(@c => c.Name).ToList();
                    //char startCell = 'A';
                    //int AsciiCode = Convert.ToInt32(startCell);
                    //foreach (var item in columnNames)
                    //{
                    //    string cellName = Convert.ToChar(AsciiCode) + "1";
                    //    ws.Cell(cellName).Value = item;
                    //    AsciiCode = AsciiCode + 1;
                    //}

                    int count = 1;
                    foreach(var item in columnNames)
                    {
                        ws.Cell(1, count).Value = item;
                        count = count+1;
                    }
                    ws.Cell(2, 1).InsertData(hourlyAnalys);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);
            }
        }


        /// <summary>
        /// Get Two Hourly Entry Data for given date range
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="worksheetName"></param>
        /// <param name="filePath"></param>
        private void GenerateExcelForTwoHourlyData(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath)
        {
            List<TwoHourlyAnalys> twoHourlyAnalyses = new List<TwoHourlyAnalys>();
            List<string> columnNames = new List<string>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                twoHourlyAnalyses = Db.TwoHourlyAnalyses.Where(@h => h.Unit_Code == unitCode && h.season_code == seasonCode
                && h.Entry_Date >= fromDate && h.Entry_Date <= toDate
                ).ToList();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Two hourly analyses data";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if (twoHourlyAnalyses.Count > 0)
                {
                    columnNames = typeof(TwoHourlyAnalys).GetProperties().Select(@c => c.Name).ToList();
                    //char startCell = 'A';
                    //int AsciiCode = Convert.ToInt32(startCell);
                    //foreach (var item in columnNames)
                    //{
                    //    string cellName = Convert.ToChar(AsciiCode) + "1";
                    //    ws.Cell(cellName).Value = item;
                    //    AsciiCode = AsciiCode + 1;
                    //}
                    int count = 1;
                    foreach (var item in columnNames)
                    {
                        ws.Cell(1, count).Value = item;
                        count = count + 1;
                    }
                    ws.Cell(2, 1).InsertData(twoHourlyAnalyses);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);
            }
        }

        /// <summary>
        /// Daily Analyses Entry Data for given date range
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="worksheetName"></param>
        /// <param name="filePath"></param>
        private void GenerateExcelForDailyAnalysesyData(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath)
        {
            List<DailyAnalys> dailyAnalys = new List<DailyAnalys>();
            List<string> columnNames = new List<string>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                Db.Configuration.ProxyCreationEnabled = false;
                dailyAnalys = Db.DailyAnalyses.Where(@h => h.unit_code == unitCode && h.season_code == seasonCode
                && h.entry_date >= fromDate && h.entry_date <= toDate
                ).ToList();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Daily analyses data";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if (dailyAnalys.Count > 0)
                {
                    columnNames = typeof(DailyAnalys).GetProperties().Select(@c => c.Name).ToList();
                    //char startCell = 'A';
                    //int AsciiCode = Convert.ToInt32(startCell);
                    //foreach (var item in columnNames)
                    //{
                    //    string cellName = Convert.ToChar(AsciiCode) + "1";
                    //    ws.Cell(cellName).Value = item;
                    //    AsciiCode = AsciiCode + 1;
                    //}

                    int count = 1;
                    foreach (var item in columnNames)
                    {
                        ws.Cell(1, count).Value = item;
                        count = count + 1;
                    }
                    ws.Cell(2, 1).InsertData(dailyAnalys);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);
            }
        }

        private void GenerateExcelForLedgerData(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath)
        {
            List<ledger_data> ledgerdata = new List<ledger_data>();
            List<string> columnNames = new List<string>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                ledgerdata = Db.ledger_data.Where(@h => h.unit_code == unitCode && h.season_code == seasonCode
                && h.trans_date >= fromDate && h.trans_date <= toDate
                ).ToList();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Ledger data ";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if (ledgerdata.Count > 0)
                {
                    columnNames = typeof(ledger_data).GetProperties().Select(@c => c.Name).ToList();
                    //char startCell = 'A';
                    //int AsciiCode = Convert.ToInt32(startCell);
                    //foreach (var item in columnNames)
                    //{
                    //    string cellName = Convert.ToChar(AsciiCode) + "1";
                    //    ws.Cell(cellName).Value = item;
                    //    AsciiCode = AsciiCode + 1;
                    //}
                    int count = 1;
                    foreach (var item in columnNames)
                    {
                        ws.Cell(1, count).Value = item;
                        count = count + 1;
                    }
                    ws.Cell(2, 1).InsertData(ledgerdata);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);
            }
        }

        private void GenerateExcelForDataAdjustment(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, string worksheetName, string filePath)
        {
            List<DataAdjustment> dataAdjustments = new List<DataAdjustment>();
            List<string> columnNames = new List<string>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                Db.Configuration.ProxyCreationEnabled = false;
                dataAdjustments = Db.DataAdjustments.Where(@h => h.a_unit_code == unitCode && h.a_season_code == seasonCode
                && h.a_entry_date >= fromDate && h.a_entry_date <= toDate
                ).ToList();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Data Adjustment data ";
                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = true;
                if (dataAdjustments.Count > 0)
                {
                    columnNames = typeof(DataAdjustment).GetProperties().Select(@c => c.Name).ToList();
                    //char startCell = 'A';
                    //int AsciiCode = Convert.ToInt32(startCell);
                    //foreach (var item in columnNames)
                    //{
                    //    string cellName = Convert.ToChar(AsciiCode) + "1";
                    //    ws.Cell(cellName).Value = item;
                    //    AsciiCode = AsciiCode + 1;
                    //}
                    int count = 1;
                    foreach (var item in columnNames)
                    {
                        ws.Cell(1, count).Value = item;
                        count = count + 1;
                    }
                    ws.Cell(2, 1).InsertData(dataAdjustments);
                }
                else
                {
                    ws.Cell(1, 1).Value = "No data exists!";
                }
                wb.SaveAs(filePath);
            }
        }


    }
}
