using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Web;
namespace DataAccess.Repositories.ReportsRepository.HoReports
{
    public class CommonRepository
    {
        ReportDetailsRepository reportDetailRepo;
        StoppageReportsRepository stoppageReportRepo;
        public CommonRepository()
        {
            reportDetailRepo = new ReportDetailsRepository();
            stoppageReportRepo = new StoppageReportsRepository();
        }
        public string CreateExcelFileWithDataUsingTemplate(int reportCode, int seasonCode, DateTime reportDate)
        {
            ReportDetail details = new ReportDetail();
            details = reportDetailRepo.GetReportDetails(reportCode);
            string fileName = details.Name + DateTime.Now.ToString("dd.MMM.yyy hh-mm-ss") + ".xlsx";
            string filePath = HttpContext.Current.Server.MapPath(details.FileGenerationLocation +fileName);
            string templateFilePath = HttpContext.Current.Server.MapPath(details.TemplatePath +"/"+ details.TemplateFileName);
            XLWorkbook templateName = new XLWorkbook(templateFilePath);

            var wb = new XLWorkbook();
            //outputWorkbook.PageOptions.PagesTall is set to -1 so that it will keep page setup as in template file.
            wb.PageOptions.PagesTall = -1;
            wb.ShowGridLines = false;
            wb.PageOptions.Margins.Top = 0;
            wb.PageOptions.Margins.Bottom = 0;
            wb.PageOptions.Margins.Left = 0;
            wb.PageOptions.Margins.Right = 0;

            var ws = wb.AddWorksheet("data");
            var wsPrv = wb.AddWorksheet("prv");
            var wsStoppages = wb.AddWorksheet("stpg");
            //var rosa = wb.AddWorksheet("rosa");
            //var har = wb.AddWorksheet("har");
            //var hata = wb.AddWorksheet("hata");
            //var nar = wb.AddWorksheet("nar");
            //var has = wb.AddWorksheet("has");
            //var sid = wb.AddWorksheet("sid");
            List<proc_summarized_report_Result> allUnitData = new List<proc_summarized_report_Result>();
            List<proc_summarized_report_Result> allUnitPrvData = new List<proc_summarized_report_Result>();
            List<string> stoppageOfAllUnits = new List<string>();
            
            int unitCode;
            for (int i = 1; i <= 7; i++)
            {
                unitCode = i;
                proc_summarized_report_Result data;
                data = GetSummarizedData(reportCode, unitCode, seasonCode, reportDate, false);
                if(data == null)
                {
                   DateTime getDataFor =  LastDataProcessedDate(unitCode, seasonCode);
                   data = GetSummarizedData(reportCode, unitCode, seasonCode, getDataFor, false);
                }
                proc_summarized_report_Result prvdata;
                    prvdata = GetSummarizedData(reportCode, unitCode, seasonCode, reportDate, true);
                if(prvdata == null)
                {
                    DateTime getDataFor = LastDataProcessedDate(unitCode, (seasonCode-1));
                    prvdata = GetSummarizedData(reportCode, unitCode, (seasonCode-1), getDataFor, false);
                }
                allUnitData.Add(data);
                allUnitPrvData.Add(prvdata);
            }
            stoppageOfAllUnits = stoppageReportRepo.StoppageOfAllUnitsForDate(seasonCode, reportDate);
            #region column names available in proc_summarized Report
            List<string> columnName = new List<string>();
            columnName = typeof(proc_summarized_report_Result).GetProperties().Select(p => p.Name).ToList();


            /// Add data from database for current season
            ws.Columns(1, 2).Delete();
            string startCell = "A";
            char dataCell = 'B';
            int counter = columnName.Count;
            foreach (var data in allUnitData)
            {
                if(data != null)
                {

                
                for (int i = 1; i <= counter; i++)
                    {
                        ws.Cell(startCell + i.ToString()).Value = columnName[i - 1].ToString();
                        ws.Cell(dataCell + i.ToString()).Value = data.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(data, null);
                    }
                }
                int ascii = (int)dataCell;
                int nextCellAscii = ascii + 1;
                dataCell = (char)nextCellAscii;
            }

            // All data from database for previous season
            wsPrv.Columns(1, 2).Delete();
            string prvstartCell = "A";
            char prvdataCell = 'B';
            int prvcounter = columnName.Count;
            foreach (var data in allUnitPrvData)
            {
                if (data != null)
                {


                    for (int i = 1; i <= prvcounter; i++)
                    {
                        wsPrv.Cell(prvstartCell + i.ToString()).Value = columnName[i - 1].ToString();
                        wsPrv.Cell(prvdataCell + i.ToString()).Value = data.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(data, null);
                    }
                }
                int ascii = (int)prvdataCell;
                int nextCellAscii = ascii + 1;
                prvdataCell = (char)nextCellAscii;
            }

            //stoppage worksheet
            wsStoppages.Columns(1, 2).Delete();
            int stpgStartCell = 2;
            foreach (string s in stoppageOfAllUnits)
            {
                wsStoppages.Cell("A"+stpgStartCell).Value = s;
                stpgStartCell++;
            }

            // stoppage worksheet end

            #endregion
            wb.SaveAs(filePath);

            for(int i = 1; i<= details.NoOfPages; i++)
            {
                if(templateName.Worksheet(i).Name != "data" && templateName.Worksheet(i).Name != "prv" && templateName.Worksheet(i).Name != "stpg")
                {
                    templateName.Worksheet(i).CopyTo(wb, templateName.Worksheet(i).Name);
                    
                }
            }
            ws.Protect("Nimda#6266");
            wsPrv.Protect("Nimda#6266");
            ws.Hide(); // hiding the sheet containing data
            wsPrv.Hide();
            wsStoppages.Hide();
            wb.Save();
            return filePath;
        }

        private proc_summarized_report_Result GetSummarizedData(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportFromDate, DateTime reportToDate, bool previousSeasonData = false)
        {
            DynamicReportRepository dynamicReportRepository = new DynamicReportRepository();
            proc_summarized_report_Result result = dynamicReportRepository.GetLedgerSummary(unitCode,seasonCode,reportToDate,previousSeasonData);
            return result;

        }

        /// <summary>
        /// Summarized report for single date
        /// </summary>
        /// <param name="reportSchemaCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportFromDate"></param>
        /// <param name="previousSeasonData"></param>
        /// <returns></returns>
        private proc_summarized_report_Result GetSummarizedData(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportFromDate, bool previousSeasonData = false)
        {
            DynamicReportRepository dynamicReportRepository = new DynamicReportRepository();
            proc_summarized_report_Result result = dynamicReportRepository.GetLedgerSummary(unitCode, seasonCode, reportFromDate, previousSeasonData);
            return result;

        }

        private DateTime LastDataProcessedDate(int unit_code, int season_code)
        {
            DateTime lastProcessedDate;
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                lastProcessedDate = Convert.ToDateTime(Db.ledger_data.Where(@x => x.unit_code == unit_code && x.season_code == season_code).Max(@x => x.trans_date).GetValueOrDefault());
                return lastProcessedDate;
            }

        }
    }
}
