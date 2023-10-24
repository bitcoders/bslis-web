using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Web;
using DataAccess.Repositories.AnalysisRepositories;

namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class ExcelReportGeneratorRepository
    {
        StoppageReportsRepository stoppageRepsiotry = new StoppageReportsRepository();
        HourlyAnalysisRepository hourlyAnalysis = new HourlyAnalysisRepository();       
        public string ExcelReportFile(int reportCode, int unitCode, int seasonCode, DateTime ReportDate, string filePath, string workSheetName = "Sheet 1")
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            //ComparitiveReportAllUnitsExcel groupComparitive = new ComparitiveReportAllUnitsExcel();
            GenerateExcel(unitCode, reportCode, seasonCode, ReportDate, filePath, workSheetName);
            return filePath;
        }

        /// <summary>
        /// Generate Excel for Periodical (given date range)
        /// </summary>
        /// <param name="reportCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="ReportFromDate"></param>
        /// <param name="ReportToDate"></param>
        /// <param name="filePath"></param>
        /// <param name="workSheetName"></param>
        /// <returns></returns>
        public string ExcelReportFile(int reportCode, int unitCode, int seasonCode, DateTime ReportFromDate, DateTime ReportToDate, string filePath, string workSheetName = "Sheet 1")
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();

            GenerateExcel(unitCode, reportCode, seasonCode, ReportFromDate, ReportToDate, filePath, workSheetName);
            return filePath;
        }

        private bool GenerateExcel(int unitcode, int reportCode, int seasonCode, DateTime ReportDate, string filePath, string worksheetName = "Sheet 1")
        {
            //ExcelReportsTemplateRepository templateRepository = new ExcelReportsTemplateRepository();
            //List<ExcelReportTemplate> template = new List<ExcelReportTemplate>();
            //template = templateRepository.GetExcepReportTemplate(reportCode);

            GenerateExcelDesignRepository excelRepository = new GenerateExcelDesignRepository();
            bool result = excelRepository.GenerateExcel(reportCode, worksheetName, filePath, unitcode, seasonCode, ReportDate);
            return result;
        }

        /// <summary>
        /// Generate excel for periodical report (For given date range)
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="reportCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="ReportFromDate"></param>
        /// <param name="ReportToDate"></param>
        /// <param name="filePath"></param>
        /// <param name="worksheetName"></param>
        /// <returns></returns>
        private bool GenerateExcel(int unitcode, int reportCode, int seasonCode, DateTime ReportFromDate, DateTime ReportToDate, string filePath, string worksheetName = "Sheet 1")
        {
            //ExcelReportsTemplateRepository templateRepository = new ExcelReportsTemplateRepository();
            //List<ExcelReportTemplate> template = new List<ExcelReportTemplate>();
            //template = templateRepository.GetExcepReportTemplate(reportCode);

            GenerateExcelDesignRepository excelRepository = new GenerateExcelDesignRepository();
            bool result = excelRepository.GenerateExcel(reportCode, worksheetName, filePath, unitcode, seasonCode, ReportFromDate, ReportToDate);
            return result;
        }


        /// <summary>
        /// Create a excel file based on template. Template details are in 'ReportDetails' table.
        /// </summary>
        /// <param name="formCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="crushingSeason"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="fileName"></param>
        /// <param name="excelTemplateFile"></param>
        /// <param name="outputExcelFilePPath"></param>
        /// <returns></returns>
        public string createCopyFromExcelTemplate(int formCode, int unitCode, int crushingSeason
                                                    , DateTime FromDate, DateTime ToDate
                                                    , string fileName
                                                    , string excelTemplateFile
                                                    , string outputExcelFilePPath)
        {

            // get report details using formCode(report code)
            ReportDetail reportDetail = new ReportDetail();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                reportDetail = Db.ReportDetails.Where(x => x.Code == formCode).FirstOrDefault();
            }
            string outputFilePath = outputExcelFilePPath;
            //fileName += DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            try
            {
                outputFilePath = outputFilePath + fileName;
                // check template file name contains .xlsx extention or not, if not than add the extention to the file name
                if (!excelTemplateFile.Contains(".xlsx"))
                {
                    excelTemplateFile += ".xlsx";
                }
                XLWorkbook templatename = new XLWorkbook(excelTemplateFile);

                //var templateWorksheet = templatename.Worksheet(1);
                var outputWorkbook = new XLWorkbook();

                //outputWorkbook.PageOptions.PagesTall is set to -1 so that it will keep page setup as in template file.
                outputWorkbook.PageOptions.PagesTall = -1;
                outputWorkbook.ShowGridLines = false;
                outputWorkbook.PageOptions.Margins.Top = 0;
                outputWorkbook.PageOptions.Margins.Bottom = 0;
                outputWorkbook.PageOptions.Margins.Left = 0;
                outputWorkbook.PageOptions.Margins.Right = 0;

                var ws = outputWorkbook.AddWorksheet("Data");
                var wsPreviousSeason = outputWorkbook.AddWorksheet("PrvSeason");
                var wsCropDay = outputWorkbook.AddWorksheet("CropDay");
                var wsStoppages = outputWorkbook.AddWorksheet("Stoppages");
                var wsPrviousSeasonStoppages = outputWorkbook.AddWorksheet("prvStoppages");

                // get data from database to fill sheet1(contains raw data)
                proc_summarized_periodical_report_Result data = FillDataFromDataset(formCode, unitCode, crushingSeason, FromDate.Date, ToDate.Date, false);
                proc_summarized_periodical_report_Result prvData = FillDataFromDataset(formCode, unitCode
                        , crushingSeason
                        , FromDate.Date
                        , ToDate.Date, true);
                // Get Stoppage Master Head Wise Summary for the period

                proc_stoppage_headwise_period_summary_Result stoppage = StoppageHeadWisePeriodSummary(unitCode, crushingSeason, FromDate.Date, ToDate.Date);
                proc_stoppage_headwise_period_summary_Result prvStoppage = StoppageHeadWisePeriodSummary(unitCode, crushingSeason, FromDate.Date, ToDate.Date, true);


                #region column names available in proc_summarized Report
                List<string> columnName = new List<string>();
                columnName = typeof(proc_summarized_periodical_report_Result).GetProperties().Select(p => p.Name).ToList();


                /// Add data from database for current season
                ws.Columns(1, 2).Delete();
                string startCell = "A";
                int counter = columnName.Count;
                for (int i = 1; i <= counter; i++)
                {
                    ws.Cell(startCell + i.ToString()).Value = columnName[i - 1].ToString();
                    ws.Cell("B" + i.ToString()).Value = data.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(data, null);
                }


                /// Add data from database for Last Season
                
                wsPreviousSeason.Columns(1, 2).Delete();
                string prvStartCell = "A";
                int prvCounter = columnName.Count;
                for (int i = 1; i <= prvCounter; i++)
                {
                    // writing column names in excel Column A
                    wsPreviousSeason.Cell(prvStartCell + i.ToString()).Value = columnName[i - 1].ToString();
                    // writing values of data in excel cell B using column names
                    if (prvData != null)
                    {
                        wsPreviousSeason.Cell("B" + i.ToString()).Value = prvData.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(prvData, null);
                    }
                }
                #endregion

                #region Column names available in proc_stoppage_headwise_period_summary_Result summary


                List<string> stoppageHeadWiseColumns = new List<string>();
                stoppageHeadWiseColumns = typeof(proc_stoppage_headwise_period_summary_Result).GetProperties().Select(p => p.Name).ToList();
                wsStoppages.Row(1).Delete();
                int stoppageColumnCount = 1;
                foreach (var item in stoppageHeadWiseColumns)
                {
                    wsStoppages.Cell(1, stoppageColumnCount).Value = item;
                    stoppageColumnCount = stoppageColumnCount + 1;
                }
                // bulk insert stoppage data from row 2 column 1
                // wsStoppages.Cell(2, 1).InsertData(stoppage);

                wsPrviousSeasonStoppages.Row(1).Delete();
                stoppageColumnCount = 1;
                foreach (var item in stoppageHeadWiseColumns)
                {
                    wsPrviousSeasonStoppages.Cell(1, stoppageColumnCount).Value = item;
                    stoppageColumnCount = stoppageColumnCount + 1;
                }
                // bulk insert previous season stoppage data from row 2 column 1
                // wsPreviousSeason.Cell(2,1).InsertData(prvStoppage)

                #endregion
                ws.Hide();
                wsPreviousSeason.Hide();
                wsCropDay.Hide();
                wsStoppages.Hide();
                wsPrviousSeasonStoppages.Hide();

                outputWorkbook.SaveAs(outputFilePath);
                //templatename.Worksheet(1).CopyTo(outputWorkbook, templateWorksheet.Name);
                for (int i = 1; i <= reportDetail.NoOfPages; i++)
                {
                    if (templatename.Worksheet(i).Name != "PrvSeason" || templatename.Worksheet(i).Name != "Data" || templatename.Worksheet(i).Name != "CropDay")
                    {
                        templatename.Worksheet(i).CopyTo(outputWorkbook, templatename.Worksheet(i).Name);
                        //outputWorkbook.Worksheet(i).PageSetup.Margins.Top = 0;
                        //outputWorkbook.Worksheet(i).PageSetup.Margins.Bottom = 0;
                        //outputWorkbook.Worksheet(i).PageSetup.Margins.Left = 0;
                        //outputWorkbook.Worksheet(i).PageSetup.Margins.Right = 0;
                        //outputWorkbook.Worksheet(i).PageSetup.FitToPages(1, 1);
                    }
                }
                outputWorkbook.Save();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return outputFilePath;
        }


        public string GenerateSingleDateExcelReportBasedOnExcelTemplate(int formCode, int unitCode, int crushingSeason
            , DateTime reportDate)
        {
            string outputFilePath = "";
            string templatePath = "";
           
            ReportDetail reportDtl = new ReportDetail();
            UnitSeason unitSeason = new UnitSeason();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                reportDtl = Db.ReportDetails.Where(x => x.Code == formCode).FirstOrDefault();
                if(reportDtl == null)
                {
                    return "";
                }
                unitSeason = Db.UnitSeasons.Where(x => x.Code == unitCode && x.Season == crushingSeason).FirstOrDefault();
            }
            string FolderLocation = HttpContext.Current.Server.MapPath(reportDtl.FileGenerationLocation);
            templatePath = HttpContext.Current.Server.MapPath(Path.Combine(reportDtl.TemplatePath, reportDtl.TemplateFileName));
            if (!Directory.Exists(FolderLocation))
            {
                Directory.CreateDirectory(FolderLocation);
            }
            outputFilePath = HttpContext.Current.Server.MapPath(Path.Combine(reportDtl.FileGenerationLocation,reportDtl.TemplateFileName));
            
            if (!outputFilePath.Contains(".xlsx"))
            {
                outputFilePath += ".xlsx";
            }

            XLWorkbook templatename = new XLWorkbook(templatePath);
            var outputWorkbook = new XLWorkbook();
            //outputWorkbook.PageOptions.PagesTall is set to -1 so that it will keep page setup as in template file.
            outputWorkbook.PageOptions.PagesTall = -1;
            outputWorkbook.ShowGridLines = false;
            outputWorkbook.PageOptions.Margins.Top = 0;
            outputWorkbook.PageOptions.Margins.Bottom = 0;
            outputWorkbook.PageOptions.Margins.Left = 0;
            outputWorkbook.PageOptions.Margins.Right = 0;

            var ws = outputWorkbook.AddWorksheet("curr");
            var wsprv = outputWorkbook.AddWorksheet("prv");
            var wsStoppages = outputWorkbook.AddWorksheet("stpg");
            var wsCrpDay = outputWorkbook.AddWorksheet("crop");
            var wsHourly = outputWorkbook.AddWorksheet("hrly");

            proc_summarized_report_Result data = new proc_summarized_report_Result();
            data =  FillDataFromDatasetProcSummarizedReport(formCode, unitCode, crushingSeason, reportDate, false);
            
            proc_summarized_report_Result prvdata = FillDataFromDatasetProcSummarizedReport(formCode, unitCode, crushingSeason, reportDate, true);
            //proc_stoppage_headwise_period_summary_Result stpgData = StoppageHeadWisePeriodSummary(unitCode, crushingSeason, reportDate.Date, reportDate.Date);
            List<Stoppage> stpgData = new List<Stoppage>();
            stpgData = stoppageRepsiotry.stoppageListForDate(unitCode, crushingSeason, reportDate);
            proc_summarized_report_by_crop_day_Result cropDayData = new proc_summarized_report_by_crop_day_Result();
            List<HourlyAnalys> hourlyData = new List<HourlyAnalys>();
            hourlyData = hourlyAnalysis.GetHourlyAnalysisList(unitCode, crushingSeason, reportDate);

            if(data.crop_day!= null)
            {
                cropDayData= SummarizedReportByPrvSeasonCropDay(unitCode, crushingSeason - 1, (int)data.crop_day);
            }

            List<string> columnName = new List<string>();
            
            List<string> stpgColumns = new List<string>();
            List<string> cropDayColumns = new List<string>();
            List<string> hourlyDataColumns = new List<string>();


            columnName = typeof(proc_summarized_report_Result).GetProperties().Select(p => p.Name).ToList();
            //stpgColumns = typeof(proc_stoppage_headwise_period_summary_Result).GetProperties().Select(p => p.Name).ToList();
            stpgColumns = typeof(Stoppage).GetProperties().Select(p => p.Name).ToList();
            cropDayColumns = typeof(proc_summarized_report_by_crop_day_Result).GetProperties().Select(p => p.Name).ToList();
            hourlyDataColumns = typeof(HourlyAnalys).GetProperties().Select(p => p.Name).ToList();

            /// Add data from database for current season
            ws.Columns(1, 2).Delete();
            string startCell = "A";
            int counter = columnName.Count;
            for (int i = 1; i <= counter; i++)
            {
                ws.Cell(startCell + i.ToString()).Value = columnName[i - 1].ToString();
                ws.Cell("B" + i.ToString()).Value = data.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(data, null);
            }

            wsprv.Columns(1, 2).Delete();
            string prvstartCell = "A";
            int prvcounter = columnName.Count;
            if(prvdata != null)
            {
                for (int i = 1; i <= prvcounter; i++)
                {
                    wsprv.Cell(prvstartCell + i.ToString()).Value = columnName[i - 1].ToString();
                    wsprv.Cell("B" + i.ToString()).Value = prvdata.GetType().GetProperty(columnName[i - 1].ToString()).GetValue(prvdata, null);
                }
            }
            

            wsStoppages.Column(20).Delete();
            char stpgstartCell = 'A';
            int startCellAscii = (int)stpgstartCell;
            foreach( var item in stpgColumns)
            {
                string cellName = Convert.ToChar(startCellAscii) + "2";
                wsStoppages.Cell(cellName).Value = item;
                startCellAscii = startCellAscii + 1;
            }
            System.Text.StringBuilder NewMillStoppageSummary = new System.Text.StringBuilder() ;
            System.Text.StringBuilder OldMillStoppageSummary = new System.Text.StringBuilder();
            NewMillStoppageSummary.Append("Mill Stoppages - " + Environment.NewLine);
            OldMillStoppageSummary.Append("Old Mill Stoppages - " + Environment.NewLine);
            foreach (var s in stpgData)
            {
                if(s.s_mill_code == 0)
                {
                    NewMillStoppageSummary.Append("* From " + s.s_start_time + " To" + s.s_end_time + " for "+ s.s_duration/60 +"."+ (Convert.ToInt16(s.s_duration) % 60).ToString("00") +" hrs. - " + s.s_comment + Environment.NewLine);
                }
                else
                {
                    OldMillStoppageSummary.Append(" * From " + s.s_start_time + " To" + s.s_end_time + " for " + s.s_duration / 60 + "." + (Convert.ToInt16(s.s_duration) % 60).ToString("00") + " hrs. Due to " + s.s_comment + Environment.NewLine);
                }
                

            }
            wsStoppages.Cell("A1").Value = NewMillStoppageSummary;
            wsStoppages.Cell("B1").Value = OldMillStoppageSummary;
            wsStoppages.Cell(3, 1).InsertData(stpgData);

            wsCrpDay.Columns(1, 2).Delete();
            string cropStartCell = "A";
            int cropCounter = cropDayColumns.Count;
            if(cropDayData != null)
            {
                for (int i = 1; i <= cropCounter; i++)
                {
                    wsCrpDay.Cell(cropStartCell + i.ToString()).Value = cropDayColumns[i - 1].ToString();
                    wsCrpDay.Cell("B" + i.ToString()).Value = cropDayData.GetType().GetProperty(cropDayColumns[i - 1].ToString()).GetValue(cropDayData, null);
                }

            }

            // data on hourly worksheet
            if(hourlyData.Count >0)
            {
                wsHourly.Rows(1, 25).Delete();
                List<string> hourlyColumns = typeof(HourlyAnalys).GetProperties().Select(c => c.Name).ToList();
                int HourlyColumnCount = 1;
                foreach (var item in hourlyColumns)
                {
                    wsHourly.Cell(1, HourlyColumnCount).Value = item;
                    HourlyColumnCount = HourlyColumnCount + 1;
                }

                
                wsHourly.Cell(2, 1).InsertData(hourlyData);
            }
            else
            {
                wsHourly.Cell(1, 1).Value = "No data found";
            }
            
            ws.Hide();
            wsprv.Hide();
            wsStoppages.Hide();
            wsCrpDay.Hide();
            wsHourly.Hide();
            outputWorkbook.SaveAs(outputFilePath);
            
            //templatename.Worksheet(1).CopyTo(outputWorkbook, templateWorksheet.Name);
            for (int i = 1; i <= reportDtl.NoOfPages; i++)
            {
                if (templatename.Worksheet(i).Name.Trim() != "curr" && templatename.Worksheet(i).Name.Trim() != "prv" && templatename.Worksheet(i).Name.Trim() != "stpg" && templatename.Worksheet(i).Name.Trim() != "hrly")
                {
                    templatename.Worksheet(i).CopyTo(outputWorkbook, templatename.Worksheet(i).Name);
                }
            }
            outputWorkbook.Save();



            return outputFilePath;
        }


        private proc_summarized_report_Result FillDataFromDatasetProcSummarizedReport(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportDate, bool previousSeasonData = false)
        {
            
            if (previousSeasonData == true)
            {
                seasonCode = seasonCode - 1;
                dates_for_previous_season(unitCode, seasonCode, reportDate, reportDate, out reportDate, out reportDate);
                previousSeasonData = false;
            }
            proc_summarized_report_Result result = new proc_summarized_report_Result();
            try
            {
                //proc_summarized_periodical_report_Result result = dynamicReportRepository.GetPeriodicalData(unitCode, seasonCode, reportFromDate, reportToDate, previousSeasonData);

                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    result = Db.proc_summarized_report(unitCode, seasonCode, reportDate, previousSeasonData).FirstOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);

            }
            return result;
        }

        /// <summary>
        /// Get all data for periodical report for the unit, season and date range
        /// </summary>
        /// <param name="reportSchemaCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportFromDate"></param>
        /// <param name="reportToDate"></param>
        /// <param name="previousSeasonData"></param>
        /// <returns>proc_summarized_periodical_report_Result</returns>
        private proc_summarized_periodical_report_Result FillDataFromDataset(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportFromDate, DateTime reportToDate, bool previousSeasonData = false)
        {
            
            proc_summarized_periodical_report_Result result = new proc_summarized_periodical_report_Result();
            try
            {
                if(previousSeasonData == true)
                {
                    seasonCode = seasonCode - 1;
                    dates_for_previous_season(unitCode, seasonCode, reportFromDate, reportToDate, out reportFromDate, out reportToDate);
                    previousSeasonData = false;
                }
                //proc_summarized_periodical_report_Result result = dynamicReportRepository.GetPeriodicalData(unitCode, seasonCode, reportFromDate, reportToDate, previousSeasonData);

                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    result = Db.proc_summarized_periodical_report(unitCode, seasonCode, reportFromDate, reportToDate, previousSeasonData).FirstOrDefault();
                }
                return result;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
               
            }
            return result;
        }

        
        private proc_stoppage_headwise_period_summary_Result StoppageHeadWisePeriodSummary(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate, bool previousSeason = false)
        {
            proc_stoppage_headwise_period_summary_Result stoppageSummary = new proc_stoppage_headwise_period_summary_Result();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                stoppageSummary = Db.proc_stoppage_headwise_period_summary(unitCode, seasonCode, fromDate.Date, toDate.Date, previousSeason).FirstOrDefault();
            }
            return stoppageSummary;

        }


        /// <summary>
        /// Get data of previous season by crop day
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="crop_day"></param>
        /// <returns></returns>
        private proc_summarized_report_by_crop_day_Result SummarizedReportByPrvSeasonCropDay(int unit_code, int season_code, int crop_day)
        {
            proc_summarized_report_by_crop_day_Result data = new proc_summarized_report_by_crop_day_Result();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                data = Db.proc_summarized_report_by_crop_day(unit_code, season_code, crop_day).FirstOrDefault();
            }
            return data;
        }
        /// <summary>
        /// this function will return file path of excel file.
        /// the file have two worksheets containing all periodical data (processed) for current and pervious season.
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="from_date"></param>
        /// <param name="to_date"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ExcelPeriodicalRawData(int unit_code, int season_code, DateTime from_date, DateTime to_date, string filePath)
        {
            
            string fileName = DateTime.Now.ToString("dd-MM-yyy hh-mm") + ".xlsx";
            string path = filePath + fileName;
            var wb = new XLWorkbook();

            var wsCurrentSeason = wb.AddWorksheet("curr");
            var wsPreviousSeason = wb.AddWorksheet("prv");


            DynamicReportRepository dynamicReportRepository = new DynamicReportRepository();
            proc_summarized_periodical_report_Result current_data = dynamicReportRepository.GetPeriodicalData(unit_code, season_code, from_date.Date, to_date.Date, false);
            proc_summarized_periodical_report_Result previous_season_data = dynamicReportRepository.GetPeriodicalData(unit_code, season_code, from_date.Date, to_date.Date, true);

            List<string> columnNames = new List<string>();
            columnNames = typeof(proc_summarized_periodical_report_Result).GetProperties().Select(p => p.Name).ToList();


            int columnCount = columnNames.Count;

            // for current seaon sheet
            for (int i = 1; i <= columnCount; i++)
            {
                wsCurrentSeason.Cell("A" + i.ToString()).Value = columnNames[i - 1].ToString();
                wsCurrentSeason.Cell("B" + i.ToString()).Value = current_data.GetType().GetProperty(columnNames[i - 1].ToString()).GetValue(current_data, null);
            }


            /// for previous season sheet
            for (int i = 1; i <= columnCount; i++)
            {
                wsPreviousSeason.Cell("A" + i.ToString()).Value = columnNames[i - 1].ToString();
                wsPreviousSeason.Cell("B" + i.ToString()).Value = previous_season_data.GetType().GetProperty(columnNames[i - 1].ToString()).GetValue(previous_season_data, null);
            }
            wb.SaveAs(path);
            return path;
        }


        /// <summary>
        /// this function will return file path of excel file.
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="from_date"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public string ExcelReportDataForDate(int unit_code, int season_code, DateTime from_date, string filepath)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".xlsx";
            string path = filepath + fileName;
            try
            {
                int crop_day = 1;
                int previous_season = season_code - 1;
                var wb = new XLWorkbook();
                var current_season_sheet = wb.AddWorksheet("curr");
                var previous_season_sheet = wb.AddWorksheet("prv");
                var crop_day_sheet = wb.AddWorksheet("crop");



                DynamicReportRepository dynRepository = new DynamicReportRepository();
                proc_summarized_report_Result current_data = dynRepository.GetLedgerSummary(unit_code, season_code, from_date, false);
                proc_summarized_report_Result previous_data = dynRepository.GetLedgerSummary(unit_code, season_code, from_date, true);

                if (current_data != null)
                {
                    crop_day = Convert.ToInt32(current_data.crop_day);
                }
                proc_summarized_report_by_crop_day_Result crop_day_data = dynRepository.GetCropDaySummary(unit_code, previous_season, crop_day);

                //dailyReportColumns will contain the list of columns in proc_summarized_report_Result
                List<string> dailyReportColumns = new List<string>();
                //cropDayColumns will contain the list of columns in proc_summarized_report_by_crop_day_Result
                List<string> cropDayColumns = new List<string>();

                dailyReportColumns = typeof(proc_summarized_report_Result).GetProperties().Select(p => p.Name).ToList();
                cropDayColumns = typeof(proc_summarized_report_by_crop_day_Result).GetProperties().Select(p => p.Name).ToList();

                /// current season sheet data
                for (int i = 1; i <= dailyReportColumns.Count; i++)
                {
                    current_season_sheet.Cell("A" + i.ToString()).Value = dailyReportColumns[i - 1].ToString();
                    current_season_sheet.Cell("B" + i.ToString()).Value = current_data.GetType().GetProperty(dailyReportColumns[i - 1].ToString()).GetValue(current_data, null);
                }

                /// previous season sheet data
                for (int i = 1; i <= dailyReportColumns.Count; i++)
                {
                    previous_season_sheet.Cell("A" + i.ToString()).Value = dailyReportColumns[i - 1].ToString();
                    previous_season_sheet.Cell("B" + i.ToString()).Value = previous_data.GetType().GetProperty(dailyReportColumns[i - 1].ToString()).GetValue(previous_data, null);
                }

                // Crop day sheet data
                for (int i = 1; i <= cropDayColumns.Count; i++)
                {
                    crop_day_sheet.Cell("A" + i.ToString()).Value = cropDayColumns[i - 1].ToString();
                    crop_day_sheet.Cell("B" + i.ToString()).Value = crop_day_data.GetType().GetProperty(cropDayColumns[i - 1].ToString()).GetValue(crop_day_data, null);
                }
                wb.SaveAs(path);

            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }

            return path;
        }

        // some times one season closes before than current season e.g season 2021 closed on 20-02-2021 
        // and season 2022 is still running after 20-02-2022.
        // Till now the logic to get the last season's data is that I subtract exact one year from the report date.
        // But, as I mentioned earlier, query will not return any data which can throw an exception or wrogn report.
        // ------- Solution
        // (1) Write a function which will get the report or period from_date and to_date
        // (2) If Report is a perioducal report
        //      (a). days_count = If report is a periodical, Get the period days (to_date - from_date).
        //      (b). to_date = Get the last working date (processed_date) of season
        //      (c). from_date = based on days_count calculate from_date 
        //          e.g. last working date of previous season was 20-02-2021 and diff_days is 7 then from_date will be 14-02-2021
        //      Now get the data for calculated using from_date and to_date (calcuated in point b and c)
        //  (3) If report is for a particular date (daily report) then get the last working date of the season only, and based on this report
        //  fetch the data.

        private void dates_for_previous_season(int unit_code, int season_code, DateTime from_date, DateTime to_date, out DateTime report_from_date, out DateTime report_end_date)
        {
            CommonRepository cRepo = new CommonRepository();
            double days_count = to_date.Subtract(from_date).Days;
            
            report_end_date = cRepo.getMaxProcessedDate(unit_code, season_code);
            if (to_date.AddYears(-1) <= report_end_date)
            {
                report_end_date = to_date.AddYears(-1);
                report_from_date = from_date.AddYears(-1);
            }
            else
            {
                report_from_date = report_end_date.AddDays(-days_count);
            }
                
        }
    }
}
