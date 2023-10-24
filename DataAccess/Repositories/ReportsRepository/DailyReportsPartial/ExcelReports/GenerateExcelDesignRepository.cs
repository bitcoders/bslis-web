using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;


namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class GenerateExcelDesignRepository
    {
        StoppageReportsRepository stoppageReportsRepository = new StoppageReportsRepository();
        public bool GenerateExcel(int reportCode, string worksheetName, string filePath, int unitCode, int seasonCode, DateTime reportDate, DateTime? reportToDate = null)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Author = "Birla Sugar Lab Information System";
                wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                wb.Properties.Category = "Reports";

                var ws = wb.Worksheets.Add(worksheetName);
                ws.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ws.PageSetup.CenterHorizontally = true;
                string result;
                // if reportTodate is provied i.e. user is trying to generate data for periodical report
                if (reportToDate == null)
                {
                    result = DrawExcelBasedOnTemplate(ws, reportCode, unitCode, seasonCode, reportDate);
                }
                else
                {
                    result = DrawExcelBasedOnTemplate(ws, reportCode, unitCode, seasonCode, reportDate, reportToDate);
                }
                
                if(result != "success")
                {
                    var errorWs = wb.Worksheets.Add("Error Details").SetTabColor(XLColor.Red).SetTabSelected();
                    errorWs.Cell("A1").Value = result;
                    errorWs.Cell("A1").Style.Font.FontColor = XLColor.Red;
                    errorWs.Range("A1", "H10").Merge();
                }
                wb.SaveAs(filePath);
                return true;
            }
        }
        private string DrawExcelBasedOnTemplate(IXLWorksheet ws, int reportCode,  int unitCode, int seasonCode, DateTime reportFromDate, DateTime ? reportToDate = null)
        {
            try
            {
                ExcelReportsTemplateRepository templateRepository = new ExcelReportsTemplateRepository();
                List<ExcelReportTemplate> template = new List<ExcelReportTemplate>();
                template = templateRepository.GetExcepReportTemplate(reportCode);

                
                ReportDetailsRepository detailsRepository = new ReportDetailsRepository();
                int reportSchemaCode = (int)detailsRepository.GetReportDetails(reportCode).ReportSchemaCode;

                ws.PageSetup.ShowGridlines = false;
                ws.ShowGridLines = false;
                object reportData, earlierSeasonData, seasonDataByCropDay;
                if(reportToDate == null)
                { 
                //ws.Protect("Google@1234");
                reportData = FillDataFromDataset(reportSchemaCode, unitCode, seasonCode, reportFromDate.Date);
                // earlierSeasonData object will hold the value of last season data;
                earlierSeasonData = FillDataFromDataset(reportSchemaCode, unitCode, seasonCode, reportFromDate.Date, true);
                }
                else
                {
                    reportData = FillDataFromDataset(reportSchemaCode, unitCode, seasonCode, reportFromDate.Date, reportToDate.Value.Date);
                    earlierSeasonData = FillDataFromDataset(reportSchemaCode, unitCode, seasonCode, reportFromDate.Date, reportToDate.Value.Date, true);
                }

                if(reportData != null)
                {
                    /// to get data as per crop day of last season
                    int crop_day = Convert.ToInt32(reportData.GetType().GetProperty("crop_day").GetValue(reportData, null));
                    seasonDataByCropDay = FillDataFromDatasetByCropDay(unitCode, seasonCode - 1, crop_day);
                    //seasonDataByCropDay.GetType().GetProperty(x.Value).GetValue(seasonDataByCropDay, null);

                    foreach (var x in template)
                    {
                        switch (x.DataType)
                        {
                            case 1:
                                ws.Cell(x.CellFrom.ToString()).Value = x.Value;
                                ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;
                                break;
                            case 2:
                                ws.Cell(x.CellFrom.ToString()).FormulaA1 = x.Value;
                                break;
                            case 3:
                                ws.Range(x.CellFrom, x.CellTo).Merge();
                                ws.Range(x.CellFrom, x.CellTo).Style.Alignment.Horizontal = GetAlignmentHorzontalValue(x.Value);
                                ws.Range(x.CellFrom, x.CellTo).Style.Alignment.Vertical = GetAlignmentVerticalValue("Center");
                                break;
                            case 4:
                                // number formatting
                                ws.Range(x.CellFrom, x.CellTo == null ? x.CellFrom : x.CellTo).Style.NumberFormat.Format = x.Value.ToString();
                                break;
                            case 5:
                                ws.Range(x.CellFrom, x.CellTo).Style.Border.OutsideBorder = GetBorderStyleValues(x.Value);
                                break;
                            case 6:
                                // Column width
                                ws.Column(x.CellFrom).Width = Convert.ToDouble(x.Value);
                                break;
                            case 7:
                                // row height
                                ws.Rows(x.CellFrom).Height = Convert.ToDouble(x.Value);
                                break;
                            case 8:
                                // text alignment
                                //ws.Cell(x.CellFrom).Style.Alignment.Horizontal = GetAlignmentHorzontalValue(x.Value);
                                ws.Range(x.CellFrom, x.CellTo == "" ? x.CellFrom : x.CellTo).Style.Alignment.Horizontal = GetAlignmentHorzontalValue(x.Value);
                                break;
                            case 9:
                                // vertical alignment
                                //ws.Cell(x.CellFrom).Style.Alignment.Vertical = GetAlignmentVerticalValue(x.Value);
                                ws.Range(x.CellFrom, x.CellTo == "" ? x.CellFrom : x.CellTo).Style.Alignment.Vertical = GetAlignmentVerticalValue(x.Value);
                                break;
                            case 10:
                                ws.Range(x.CellFrom, x.CellTo == "" ? x.CellFrom : x.CellTo).Style.Alignment.WrapText = true;
                                break;
                            case 11:
                                // set border for selected range.
                                AllBorderStyling(ws, x.Value, x.CellFrom, x.CellTo);
                                break;
                            case 12:
                                if (reportData == null)
                                {
                                    ws.Cell("A1").Value = "No data Exist for the selected date/date range.";
                                    ws.Cell("A1").Style.Font.FontColor = XLColor.Red;
                                    ws.Cell("A1").Style.Font.FontSize = 20;

                                    //reportData = FillDataFromDataset(reportCode, unitCode, seasonCode, reportFromDate);
                                }
                                else
                                {
                                    try
                                    {
                                        var propertyInfo = reportData.GetType().GetProperty(x.Value);
                                        var value = propertyInfo.GetValue(reportData, null);
                                        ws.Cell(x.CellFrom).Value = value;
                                        ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;
                                    }
                                    catch (Exception ex)
                                    {
                                        ws.Cell(x.CellFrom).Value = ex.Message;
                                        ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;
                                        ws.Cell(x.CellFrom).Style.Fill.BackgroundColor = XLColor.Pink;
                                        ws.Cell(x.CellFrom).Style.Font.FontColor = XLColor.Red;
                                    }
                                }

                                break;
                            case 13:
                                if (earlierSeasonData == null)
                                {
                                    ws.Cell("B1").Value = "No data Exist for the selected date/date range of previous season.";
                                    ws.Cell("B1").Style.Font.FontColor = XLColor.Red;
                                    ws.Cell("B1").Style.Font.FontSize = 20;
                                    ws.Cell(x.CellFrom).Value = 0;
                                    ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;

                                    //earlierSeasonData = FillDataFromDataset(reportCode, unitCode, seasonCode, reportFromDate, true);
                                }
                                else
                                {
                                    var prvSeasonPropertyInfo = reportData.GetType().GetProperty(x.Value);
                                    try
                                    {
                                        var prvSeasonvalue = prvSeasonPropertyInfo.GetValue(earlierSeasonData, null);
                                        ws.Cell(x.CellFrom).Value = prvSeasonvalue;
                                        ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;
                                    }
                                    catch (Exception ex)
                                    {
                                        ws.Cell(x.CellFrom).Value = ex.Message;
                                        ws.Cell(x.CellFrom).Style.Font.Bold = x.Bold;
                                        ws.Cell(x.CellFrom).Style.Fill.BackgroundColor = XLColor.Pink;
                                        ws.Cell(x.CellFrom).Style.Font.FontColor = XLColor.Red;
                                    }

                                }

                                break;
                            case 14:
                                // stoppage list from column to rows of given mill code

                                List<Stoppage> stoppages = stoppageReportsRepository.stoppageListForDate(unitCode, seasonCode, reportFromDate, Convert.ToInt32(x.Value));
                                ws.Range(x.CellFrom, x.CellTo == null ? x.CellFrom : x.CellTo).Merge();
                                ws.Cell(x.CellFrom).Style.Alignment.WrapText = true;
                                int counter = 1;
                                string millName = "";
                                string stoppageString = "";
                                switch (Convert.ToInt32(x.Value))
                                {
                                    case 0:
                                        millName = "New Mill" + Environment.NewLine;
                                        break;
                                    case 1:
                                        millName = "Old Mill" + Environment.NewLine;
                                        break;
                                    default:
                                        millName = "";
                                        break;
                                }

                                if (stoppages.Count != 0)
                                {
                                    string hrs = "00";
                                    string minutes = "00";
                                    string duration;

                                    foreach (var stoppage in stoppages)
                                    {
                                        hrs = Math.Round(Convert.ToDecimal(stoppage.s_duration / 60), 2).ToString("00");
                                        minutes = Math.Round(Convert.ToDecimal(stoppage.s_duration % 60), 2).ToString("00");
                                        duration = hrs + ": " + minutes;
                                        stoppageString += counter + ") From " + stoppage.s_start_time + " to " + stoppage.s_end_time + " for " + duration + " hours" + " -" + stoppage.s_comment.Trim() + Environment.NewLine;
                                        counter++;
                                    }
                                    ws.Cell(x.CellFrom).Value = millName + stoppageString;
                                    string[] rowNumber = Regex.Split(x.CellFrom, @"\D+");
                                    double currentHeightofRow = ws.Row(Convert.ToInt32(rowNumber[1])).Height;
                                    //if current height of row is less than (15*counter) value than set a new height else let it remain the same
                                    if (currentHeightofRow < (17 * counter) || currentHeightofRow < 17 * (counter + 1))
                                    {
                                        ws.Row(Convert.ToInt32(rowNumber[1])).Height = 17 * (counter + 1);
                                    }
                                }
                                else
                                {
                                    //ws.Cell(x.CellFrom).Value = "Stoppage did not occured1";
                                }
                                break;
                            case 15:
                                // page margin
                                ws.PageSetup.Margins.Top = ws.PageSetup.Margins.Bottom
                                = ws.PageSetup.Margins.Left = ws.PageSetup.Margins.Right
                                = ws.PageSetup.Margins.Header
                                = ws.PageSetup.Margins.Footer
                                = Convert.ToDouble(x.Value);
                                break;
                            default:
                            case 16:
                                // page orientation
                                string orientation = x.Value.ToLower();
                                switch (orientation)
                                {
                                    case "p":
                                        ws.PageSetup.SetPageOrientation(XLPageOrientation.Portrait);

                                        break;
                                    case "l":
                                        ws.PageSetup.SetPageOrientation(XLPageOrientation.Landscape);

                                        break;
                                    default:
                                        ws.PageSetup.SetPageOrientation(XLPageOrientation.Default);
                                        break;
                                }
                                break;
                            case 17:
                                // page setup adjust to the %.
                                ws.PageSetup.AdjustTo(Convert.ToInt32(x.Value));
                                break;
                            case 18:
                                // get summarized report of previous season based on the current crop day.
                                ///****************************************************************
                                ///get crop day from the variable. pass current season code, unit code and crop day to the stored procedure
                                ///in stored procedure based on the crop day get the "Date" of transaction and fetch data.
                                ///****************************************************************
                                if (seasonDataByCropDay == null)
                                {
                                    seasonDataByCropDay = FillDataFromDatasetByCropDay(unitCode, seasonCode - 1, crop_day);
                                }
                                ws.Cell(x.CellFrom).Value = seasonDataByCropDay.GetType().GetProperty(x.Value).GetValue(seasonDataByCropDay, null);

                                break;
                            case 19:
                                // fit to one page
                                // value should be comma saperated eg 1,2 i.e 1 page wide and 2 page tall
                                string[] fitToPages = x.Value.Split(',');
                                ws.PageSetup.FitToPages(Convert.ToInt32(fitToPages[0]), Convert.ToInt32(fitToPages[1]));

                                break;
                            case 20:
                                // set print area
                                // value cell from and cell to be like "A1:L60" by this print command will be sent for the selected range only.
                                string printRange = x.CellFrom + ":" + (x.CellTo == "" ? x.CellFrom : x.CellTo);
                                ws.PageSetup.PrintAreas.Add(printRange);
                                break;
                            case 21:
                                /// insert row below given row number
                                /// comma saperated value. first value defines below which row you want to insert rows.
                                /// second value i.e. after comma define number of rows to insert.
                                /// 
                                string[] rowInsertion = x.Value.Split(',');
                                ws.Row(Convert.ToInt32(rowInsertion[0])).InsertRowsBelow(Convert.ToInt32(rowInsertion[1]));
                                break;
                            case 22:
                                /// insert row above given row number
                                /// comma saperated value. first value defines below which row you want to insert rows.
                                /// second value i.e. after comma define number of rows to insert.
                                /// 
                                string[] rowAboveInsertion = x.Value.Split(',');
                                ws.Row(Convert.ToInt32(rowAboveInsertion[0])).InsertRowsBelow(Convert.ToInt32(rowAboveInsertion[1]));

                                break;
                            case 23:
                                /// insert column at the left of the given column
                                break;
                        }
                    }

                    return "success";
                }
                else
                {
                    return "Message = No data found for selected dates!";
                }
            }
            catch(Exception ex)
            {
                return "Message = "+ ex.Message +Environment.NewLine+ "Data = " +ex.Data + Environment.NewLine + "Source = "+ ex.Source + Environment.NewLine + "Stack Trace = " + ex.StackTrace;
            }
        }
        /// <summary>
        /// Get report data for single date 
        /// </summary>
        /// <param name="reportSchemaCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportDate"></param>
        /// <param name="previousSeasonData"></param>
        /// <returns></returns>
        private object FillDataFromDataset(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportDate, bool previousSeasonData = false)
        {
            ReportSchemaRepository reportSchemaRepository = new ReportSchemaRepository();
            ReportSchema reportSchema = new ReportSchema();
            reportSchema = reportSchemaRepository.GetReportSchemaByCode(reportSchemaCode);
            
            Type dynamicReport = typeof(DynamicReportRepository);
            MethodInfo method = dynamicReport.GetMethod(reportSchema.SysObjectName);
            DynamicReportRepository d = new DynamicReportRepository();
            object x = method.Invoke(d, new object[] { unitCode, seasonCode, reportDate.Date, previousSeasonData });
            
            /// if previous season data is not retuned by above code i.e. the prvious season was closed earlier 
            /// than current seasons date & month. So, we will get the last running date of season 
            /// and return that data
            if(x== null && previousSeasonData == true)
            {
                UnitSeason season = new UnitSeason();
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    int prvSeasonCode = seasonCode - 1;
                    season = Db.UnitSeasons.Where(@y => y.Code == unitCode && y.Season == prvSeasonCode).FirstOrDefault();
                    
                    DateTime lastcurshingdate = Convert.ToDateTime(season.CrushingEndDateTime);
                    
                    x = method.Invoke(d, new object[] { unitCode, prvSeasonCode, lastcurshingdate.Date, false });
                }
            }
            return x;
              
        }

        /// <summary>
        /// get data for given date range (For periodical report)
        /// </summary>
        /// <param name="reportSchemaCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportFromDate"></param>
        /// <param name="reportToDate"></param>
        /// <param name="previousSeasonData"></param>
        /// <returns></returns>
        private object FillDataFromDataset(int reportSchemaCode, int unitCode, int seasonCode, DateTime reportFromDate, DateTime? reportToDate, bool previousSeasonData = false)
        {
            ReportSchemaRepository reportSchemaRepository = new ReportSchemaRepository();


            ReportSchema reportSchema = new ReportSchema();
            reportSchema = reportSchemaRepository.GetReportSchemaByCode(reportSchemaCode);

            Type dynamicReport = typeof(DynamicReportRepository);
            MethodInfo method = dynamicReport.GetMethod(reportSchema.SysObjectName);
            DynamicReportRepository d = new DynamicReportRepository();
            object y = method.Invoke(d, new object[] { unitCode, seasonCode, reportFromDate.Date, reportToDate.Value.Date, previousSeasonData });
            return y;
            
        }
        
        /// <summary>
        /// Get summarized report (single dated) by unit code, season code and crop day
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="crop_day"></param>
        /// <returns></returns>
        private object FillDataFromDatasetByCropDay (int unit_code, int season_code, int crop_day)
        {
            SummarizedReportsRepository summarizedReports = new SummarizedReportsRepository();
       
            object result =   summarizedReports.SumarizedReportByCropDay(unit_code, season_code, crop_day);
            return result;
        }
        
        private void millWiseStoppageList(int unitCode, int seasonCode, DateTime reportDate)
        {

        }
        
        private XLBorderStyleValues GetBorderStyleValues(string code)
        {
            XLBorderStyleValues borderStyle;
            switch (code)
            {
                case "Thick":
                    borderStyle = XLBorderStyleValues.Thick;
                    break;
                case "Thin":
                    borderStyle = XLBorderStyleValues.Thin;
                    break;
                default:
                    borderStyle = XLBorderStyleValues.Thick;
                    break;
            }
            return borderStyle;
        }

        private XLAlignmentHorizontalValues GetAlignmentHorzontalValue(string code)
        {
            XLAlignmentHorizontalValues alignmentStyle;
            switch (code)
            {
                case "Center":
                    alignmentStyle = XLAlignmentHorizontalValues.Center;
                    break;
                case "CenterContinuous":
                    alignmentStyle = XLAlignmentHorizontalValues.CenterContinuous;
                    break;
                case "Distributed":
                    alignmentStyle = XLAlignmentHorizontalValues.Distributed;
                    break;
                case "Fill":
                    alignmentStyle = XLAlignmentHorizontalValues.Fill;
                    break;
                case "General":
                    alignmentStyle = XLAlignmentHorizontalValues.General;
                    break;
                case "Justify":
                    alignmentStyle = XLAlignmentHorizontalValues.Justify;
                    break;
                case "Left":
                    alignmentStyle = XLAlignmentHorizontalValues.Left;
                    break;
                case "Right":
                    alignmentStyle = XLAlignmentHorizontalValues.Right;
                    break;
                default:
                    alignmentStyle = XLAlignmentHorizontalValues.Left;
                    break;
            }
            return alignmentStyle;
        }

        private XLAlignmentVerticalValues GetAlignmentVerticalValue(string code)
        {
            XLAlignmentVerticalValues alignmentStyle;
            switch (code)
            {
                case "Bottom":
                    alignmentStyle = XLAlignmentVerticalValues.Bottom;
                    break;
                case "Center":
                    alignmentStyle = XLAlignmentVerticalValues.Center;
                    break;
                case "Distributed":
                    alignmentStyle = XLAlignmentVerticalValues.Distributed;
                    break;
                case "Justify":
                    alignmentStyle = XLAlignmentVerticalValues.Justify;
                    break;
                case "General":
                    alignmentStyle = XLAlignmentVerticalValues.Top;
                    break;
                default:
                    alignmentStyle = XLAlignmentVerticalValues.Center;
                    break;
            }
            return alignmentStyle;
        }

        private void AllBorderStyling(IXLWorksheet ws, string borderStyle,  string xlRangeFrom, string xlRangeTo)
        {
            char[] rangeFromAlphabet, rangeToAlphabet;
            string rangeFromNumber, rangeToNumber;
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");

            Match rangeFromMatch = re.Match(xlRangeFrom);

            rangeFromAlphabet = rangeFromMatch.Groups[1].Value.ToCharArray();
            rangeFromNumber = rangeFromMatch.Groups[2].Value;

            Match rangeToMatch = re.Match(xlRangeTo);

            rangeToAlphabet = rangeToMatch.Groups[1].Value.ToCharArray();
            rangeToNumber = rangeToMatch.Groups[2].Value;

            
            //foreach (var item in xlRangeFrom)
            //{
                
                //if(item.GetType() == typeof(char))
                //{
                //    rangeFromAlphabet += item;
                //}
                //else if(item.GetType() == typeof(int))
                //{
                //    rangeFromNumber += item;
                //}
            //}
            //foreach (var item in xlRangeTo)
            //{
                
                //if (item.GetType() == typeof(char))
                //{
                //    rangeToAlphabet += item;
                //}
                //else if(item.GetType() == typeof(int))
                //{
                //    rangeToNumber += item;
                //}
            //}
            
            int rangeFromAlphabetAscii = Convert.ToInt32(Char.ToLower(rangeFromAlphabet[0]));
            int rangeToAlphabetAscii = Convert.ToInt32(Char.ToLower(rangeToAlphabet[0]));

            int rangeFromNumberInt = Convert.ToInt32(rangeFromNumber);
            int rangeToNumberInt = Convert.ToInt32(rangeToNumber);
            for (int x = rangeFromAlphabetAscii; x<= rangeToAlphabetAscii; x++)
            {
                for(int y = rangeFromNumberInt; y <= rangeToNumberInt; y++)
                {
                    ws.Cell(Convert.ToChar(x) + y.ToString()).Style.Border.TopBorder = GetBorderStyleValues(borderStyle);
                    ws.Cell(Convert.ToChar(x) + y.ToString()).Style.Border.BottomBorder = GetBorderStyleValues(borderStyle);
                    ws.Cell(Convert.ToChar(x) + y.ToString()).Style.Border.LeftBorder = GetBorderStyleValues(borderStyle);
                    ws.Cell(Convert.ToChar(x) + y.ToString()).Style.Border.RightBorder = GetBorderStyleValues(borderStyle);
                }
            }
        }

        
    }
}
