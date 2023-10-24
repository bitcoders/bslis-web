using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using DataAccess.Repositories.AnalysisRepositories;
using ExcelNumberFormat;

namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class DailyCrushReportExcel
    {
        private string ConvertMinutesToHours(decimal minutes)
        {
            //int x = Convert.ToDateTime(minutes / 60).Hour;

            decimal hours = minutes / 60;
            int min = Convert.ToInt32(minutes % 60);
            return Math.Truncate(hours).ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0');
        }
        public async Task<string> ExcelReportFile(int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            DailyCrushReportExcel pe = new DailyCrushReportExcel();
            await Task.FromResult(pe.GenerateExcel(unitCode, seasonCode, ReportDate, filePath));
            return filePath;
        }
        private async Task<bool> GenerateExcel(int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            #region Object Declaration
            SummarizedReportsRepository summarizedReportsRepository = new SummarizedReportsRepository();

            LedgerDataRepository ledgerDataRepository = new LedgerDataRepository();
            MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
            func_ledger_data_period_summary_Result ledgerPeriodSummary = new func_ledger_data_period_summary_Result();
            DailyAnalysisRepository dailyAnalysisRepository = new DailyAnalysisRepository();
            DailyAnalys dailyAnalys = new DailyAnalys();
            MolassesAnalysisRepository molassesAnalysisRepository = new MolassesAnalysisRepository();
            MassecuiteSummaryRepository massecuiteSummaryRepository = new MassecuiteSummaryRepository();
            MeltingAnalysisRepository meltingAnalysisRepository = new MeltingAnalysisRepository();
            HourlyAnalysisRepository hourlyAnalysRepository = new HourlyAnalysisRepository();
            TwoHourlyAnalysSummaryRepository twoHourlyAnalysSummaryRepository = new TwoHourlyAnalysSummaryRepository();
            StoppageRepository stoppageRepository = new StoppageRepository();


            proc_summarized_report_Result summarized_report_for_current_season = new proc_summarized_report_Result();
            proc_summarized_report_Result summarized_Report_last_season = new proc_summarized_report_Result();
            proc_summarized_report_by_crop_day_Result summarized_report_last_season_crop_day = new proc_summarized_report_by_crop_day_Result();


            #endregion

            var MasterUnitDefaults = masterUnitRepository.FindUnitByPk(unitCode);
            ledgerPeriodSummary = await ledgerDataRepository.GetLedgerDataPeriodSummaryAsync(unitCode, seasonCode, Convert.ToDateTime(MasterUnitDefaults.CrushingStartDate), ReportDate);
            var ledgerData = ledgerDataRepository.GetLedgerDataForTheDate(unitCode, seasonCode, ReportDate);
            //var dailyAnalysesSummary = dailyAnalysisRepository.dailyAnalysesSummaryReport(unitCode, seasonCode, ReportDate);
            var dailyAnalysesSummary = dailyAnalysisRepository.dailyAnalysesSummaryFromCrushingStartDate(unitCode, seasonCode, ReportDate);
            dailyAnalys = dailyAnalysisRepository.GetDailyAnalysisDetailsByDate(unitCode, seasonCode, ReportDate);
            var molassesSummary = molassesAnalysisRepository.molassesTodateSummary(unitCode, seasonCode, ReportDate);
            var stoppageSummary = stoppageRepository.GetStoppageSummaryForDay(unitCode, seasonCode, ReportDate);
            var stoppageDetails = stoppageRepository.GetStoppagesList(unitCode, seasonCode, ReportDate);
            var massecuteSummary = massecuiteSummaryRepository.massecuteToDateSummary(unitCode, seasonCode, ReportDate);
            var meltingSummary = meltingAnalysisRepository.GetMeltingsTodateSummary(unitCode, seasonCode, ReportDate);
            var hourlyAnalyses = hourlyAnalysRepository.GetHourlyAnalysisSummaryForDate(unitCode, seasonCode, ReportDate);
            var twoHourlySummary = twoHourlyAnalysSummaryRepository.GetTwoHourlySummaryForDate(unitCode, seasonCode, ReportDate);
            List<HourlyAnalys> hourlyAnalysList = hourlyAnalysRepository.GetHourlyAnalysisList(unitCode, seasonCode, ReportDate);


            summarized_report_for_current_season = summarizedReportsRepository.SummarizedReportResult(unitCode, seasonCode, ReportDate,false);
            /// fetching last season data;
            summarized_Report_last_season = summarizedReportsRepository.SummarizedReportResult(unitCode, seasonCode, ReportDate, true);
            int crop_day = (int)dailyAnalysesSummary.days_count;
            summarized_report_last_season_crop_day = summarizedReportsRepository.SumarizedReportByCropDay(unitCode, seasonCode-1, crop_day);
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                try
                {
                    wb.Properties.Author = "Birla Sugar Lab Information System";
                    wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                    wb.Properties.Category = "Reports";

                    var ws = wb.Worksheets.Add("DCR");
                    // page margin
                    ws.PageSetup.Margins.Top = ws.PageSetup.Margins.Bottom
                    = ws.PageSetup.Margins.Left = ws.PageSetup.Margins.Right
                    = ws.PageSetup.Margins.Header
                    = ws.PageSetup.Margins.Footer
                    = Convert.ToDouble(.10);

                    // page adjustment
                    ws.PageSetup.AdjustTo(74);
                    if(hourlyAnalyses == null)
                    {
                        ws.Cell("A1").Value = "No data Exist for the selected date/date range.";
                        ws.Cell("A1").Style.Font.FontColor = XLColor.Red;
                        ws.Cell("A1").Style.Font.FontSize = 20;
                        wb.SaveAs(filePath);
                        return await Task.FromResult(true);
                    }
                    #region Merged Cells
                    ws.Range("A1", "L1").Merge();
                        ws.Range("A2", "L2").Merge();
                    ws.Range("F3", "H3").Merge();
                    ws.Range("J3", "L3").Merge();
                    ws.Range("B4", "D4").Merge();
                        ws.Range("B5", "D5").Merge();
                        ws.Range("B6", "D6").Merge();
                        ws.Range("B7", "D7").Merge();
                        ws.Range("B8", "D8").Merge();
                        ws.Range("B9", "D9").Merge();
                    ws.Range("B10", "D10").Merge();
                    ws.Range("B11", "D11").Merge();
                    ws.Range("B17", "C17").Merge();
                    ws.Range("B18", "C18").Merge();
                    ws.Range("B19", "C19").Merge();
                    ws.Range("B3", "D3").Merge();
                    ws.Range("B20", "E20").Merge();
                    ws.Range("B21", "E21").Merge();
                    ws.Range("B22", "E22").Merge();
                    ws.Range("B23", "E23").Merge();
                    ws.Range("B24", "E24").Merge();
                    ws.Range("B25", "E25").Merge();
                    ws.Range("B26", "E26").Merge();
                    ws.Range("B27", "E27").Merge();
                    ws.Range("B28", "E28").Merge();
                    ws.Range("B29", "E29").Merge();
                    ws.Range("B30", "E30").Merge();
                    ws.Range("B31", "E31").Merge();
                    ws.Range("B32", "E32").Merge();
                    ws.Range("B33", "E33").Merge();
                    ws.Range("B34", "E34").Merge();
                    ws.Range("B35", "E35").Merge();
                    ws.Range("B36", "E36").Merge();
                    ws.Range("B37", "E37").Merge();
                    ws.Range("B38", "E38").Merge();
                    ws.Range("B39", "E39").Merge();
                    ws.Range("B40", "E40").Merge();
                    ws.Range("B41", "E41").Merge();
                    ws.Range("B42", "E42").Merge();
                    ws.Range("B43", "E43").Merge();

                    ws.Range("B44", "L44").Merge();
                    ws.Range("B49", "F49").Merge();
                    ws.Range("C50", "D50").Merge();
                    ws.Range("E50", "F50").Merge();
                    
                    ws.Range("B59", "L59").Merge();
                    ws.Range("B61", "C61").Merge();
                    ws.Range("E61", "F61").Merge();

                    ws.Range("B62", "j62").Merge();
                    ws.Range("K62", "L62").Merge();
                    ws.Range("B65", "L65").Merge();

                    ws.Range("G9", "H9").Merge();
                    ws.Range("G10", "H10").Merge();
                    ws.Range("G11", "H11").Merge();
                    ws.Range("G12", "H12").Merge();

                    ws.Range("G13", "H13").Merge();
                    ws.Range("G14", "H14").Merge();
                    ws.Range("G15", "H15").Merge();
                    ws.Range("G16", "H16").Merge();
                    ws.Range("G17", "H17").Merge();
                    ws.Range("G18", "H18").Merge();
                    ws.Range("G19", "H19").Merge();
                    ws.Range("G20", "H20").Merge();
                    ws.Range("G21", "H21").Merge();
                    ws.Range("G22", "H22").Merge();
                    ws.Range("G23", "H23").Merge();
                    ws.Range("G24", "H24").Merge();
                    ws.Range("G25", "H25").Merge();
                    ws.Range("G26", "H26").Merge();
                    

                    ws.Range("G29", "H29").Merge();
                    ws.Range("G30", "H30").Merge();
                    ws.Range("G31", "H31").Merge();

                    ws.Range("j29", "k29").Merge();
                    ws.Range("j30", "k30").Merge();
                    ws.Range("j31", "k31").Merge();

                    ws.Range("G32", "J32").Merge();
                    ws.Range("G33", "J33").Merge();
                    ws.Range("G34", "J34").Merge();
                    ws.Range("G35", "J35").Merge();
                    ws.Range("G36", "J36").Merge();
                    ws.Range("G37", "J37").Merge();
                    ws.Range("G38", "J38").Merge();
                    ws.Range("G39", "J39").Merge();
                    ws.Range("G40", "J40").Merge();
                    ws.Range("G41", "J41").Merge();
                    ws.Range("G42", "J42").Merge();
                    ws.Range("G43", "J43").Merge();

                    
                    ws.Range("H49", "L49").Merge();
                    ws.Range("G55", "J55").Merge();
                    ws.Range("G56", "J56").Merge();
                    ws.Range("G57", "J57").Merge();
                    ws.Range("G58", "J58").Merge();

                    ws.Range("K4", "L4").Merge();
                    ws.Range("K9", "L9").Merge();
                    ws.Range("K10", "L10").Merge();
                    ws.Range("K11", "L11").Merge();
                    ws.Range("K12", "L12").Merge();
                    ws.Range("K13", "L13").Merge();
                    ws.Range("K14", "L14").Merge();
                    ws.Range("K15", "L15").Merge();

                    ws.Range("K16", "L16").Merge();
                    ws.Range("K17", "L17").Merge();
                    ws.Range("K18", "L18").Merge();
                    ws.Range("K19", "L19").Merge();
                    ws.Range("K20", "L20").Merge();
                    ws.Range("K21", "L21").Merge();
                    ws.Range("K22", "L22").Merge();
                    ws.Range("K23", "L23").Merge();
                    ws.Range("K24", "L24").Merge();
                    ws.Range("K25", "L25").Merge();
                    ws.Range("K26", "L26").Merge();

                    ws.Range("K32", "L32").Merge();
                    ws.Range("K33", "L33").Merge();
                    ws.Range("K34", "L34").Merge();
                    ws.Range("K35", "L35").Merge();
                    ws.Range("K36", "L36").Merge();
                    ws.Range("K37", "L37").Merge();
                    ws.Range("K38", "L38").Merge();
                    ws.Range("K39", "L39").Merge();

                    ws.Range("K40", "L40").Merge();
                    ws.Range("K41", "L41").Merge();
                    ws.Range("K42", "L42").Merge();
                    ws.Range("K43", "L43").Merge();

                    ws.Range("K45", "L45").Merge();
                    ws.Range("K46", "L46").Merge();
                    ws.Range("K47", "L47").Merge();
                    ws.Range("K48", "L48").Merge();

                    ws.Range("K50", "L50").Merge();
                    ws.Range("K51", "L51").Merge();
                    ws.Range("K52", "L52").Merge();
                    ws.Range("K53", "L53").Merge();
                    ws.Range("K54", "L54").Merge();

                    ws.Range("K55", "L55").Merge();
                    ws.Range("K56", "L56").Merge();
                    ws.Range("K57", "L57").Merge();
                    ws.Range("K58", "L58").Merge();

                    ws.Range("K60", "L60").Merge();
                    ws.Range("K61", "L61").Merge();
                    
                    ws.Range("B63", "B64").Merge();
                    ws.Range("E63", "E64").Merge();
                    ws.Range("H63", "H64").Merge();

                    ws.Range("B66", "L67").Merge();
                    #endregion

                    #region Excel Formatting

                    ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("A1", "L4").Style.Font.Bold = true;
                    
                    ws.Range("G9", "L9").Style.Font.Bold = true;
                    ws.Range("G16", "L16").Style.Font.Bold = true;
                    ws.Range("A12", "F12").Style.Font.Bold = true;
                    ws.Range("A17", "F17").Style.Font.Bold = true;
                    //ws.Range("G27", "L28").Style.Font.Bold = true;
                    ws.Range("G32", "L32").Style.Font.Bold = true;
                    ws.Range("G38", "L38").Style.Font.Bold = true;
                    ws.Range("A44", "L44").Style.Font.Bold = true;
                    ws.Range("D45", "L45").Style.Font.Bold = true;
                    ws.Range("A49", "F51").Style.Font.Bold = true;
                    ws.Range("H50", "L50").Style.Font.Bold = true;
                    ws.Cell("G49").Style.Font.Bold = true;
                    ws.Cell("G51").Style.Font.Bold = true;
                    ws.Cell("G53").Style.Font.Bold = true;
                    ws.Cell("G55").Style.Font.Bold = true;
                    
                    
                    ws.Range("A59", "L59").Style.Font.Bold = true;
                    ws.Range("A62", "L62").Style.Font.Bold = true;
                    ws.Cell("I63").Style.Alignment.WrapText = true;
                    ws.Cell("G49").Style.Alignment.WrapText = true;
                    ws.Cell("H63").Style.Alignment.WrapText = true;


                    ws.Range("D45", "L48").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A63", "L64").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("G50", "G54").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("J51", "L54").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    string[] cellsToAlignHorizontallyInCenter = { "C60", "E60", "G60", "I60", "K60"
                                                                    , "D61","G61", "K61"};
                    foreach(string cell in cellsToAlignHorizontallyInCenter)
                    {
                        ws.Cell(cell).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    }
                    

                    ws.Range("A63", "L64").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    //ws.Columns(1, 12).Width = 15;
                    ws.Row(1).Height = 29;
                    ws.Column(1).Width = 1.89;
                    ws.Column(2).Width = 14.56;
                    ws.Column(3).Width = 10.56;
                    ws.Column(4).Width = 12.11;
                    ws.Column(5).Width = 13.67;
                    ws.Column(6).Width = 14.56;
                    ws.Column(7).Width = 12.11;
                    ws.Column(8).Width = 9.56;
                    ws.Column(9).Width = 9.56;
                    ws.Column(10).Width = 9.56;
                    ws.Column(11).Width = 6;
                    ws.Column(12).Width = 6;

                    ws.Cell("G49").Style.Font.FontSize = 8;
                    
                    ws.Range("F20", "F35").Style.NumberFormat.Format = "00.00";
                    ws.Range("F36", "F37").Style.NumberFormat.Format = "00";
                    ws.Range("F38", "F43").Style.NumberFormat.Format = "00.00";
                    ws.Range("D46", "L48").Style.NumberFormat.Format = "00";
                    ws.Range("I17", "L26").Style.NumberFormat.Format = "00.00";
                    ws.Range("E6","F11").Style.NumberFormat.Format = "00.00";
                    ws.Range("K30", "K37").Style.NumberFormat.Format = "hh:mm";
                    ws.Range("K39", "K43").Style.NumberFormat.Format = "hh:mm";

                   

                    #endregion

                    #region Border Styles

                    ws.Range("A1", "L64").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1", "L64").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1", "L64").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1", "L64").Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    ws.Range("A1", "A68").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("A4", "L68").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("A1", "L4").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B5", "F11").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    
                    
                    ws.Range("G9", "L14").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B12", "F12").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B13", "F16").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G15", "L15").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G16", "L26").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B17", "F17").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B18", "F37").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    
                    ws.Range("G16", "L26").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B38", "F43").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B44", "L44").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B45", "L48").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B49", "F49").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B50", "F58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B59", "L59").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B60", "L61").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B63", "L64").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B62", "L62").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B65", "L65").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    ws.Range("G5", "L8").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G27", "L27").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G28", "L31").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G32", "L37").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G38", "L43").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("H49", "L49").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("H50", "L54").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("G55", "L58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    
                    #endregion



                    #region Cell Background Colors
                    //ws.Range("G44", "G46").Style.Fill.BackgroundColor = XLColor.LightApricot;
                    //ws.Range("G48", "L50").Style.Fill.BackgroundColor = XLColor.LightApricot;

                    #endregion

                    ws.Cell("A1").Value = MasterUnitDefaults.Name+Environment.NewLine+MasterUnitDefaults.Address;
                    ws.Cell("A2").Value = "Daily Crushing Report";
                    ws.Cell("B3").Value = "Season " + dailyAnalysesSummary.season_year ;
                    ws.Cell("E3").Value = "Crop Day " + dailyAnalysesSummary.days_count;
                    ws.Cell("I3").Value = "Date";
                    ws.Cell("J3").Value = ReportDate;

                    ws.Cell("A4").Value = "A.";
                    ws.Cell("B4").Value = "Operational";
                    ws.Cell("E4").Value = "Today";
                    ws.Cell("F4").Value = "To Date";
                    ws.Cell("G4").Value = "Distillery";
                    ws.Cell("H4").Value = "Today";
                    ws.Cell("I4").Value = "Month";
                    ws.Cell("J4").Value = "To Date";
                    ws.Cell("K4").Value = "Recovery %";


                    ws.Cell("B5").Value = "Cane Crushed";
                    ws.Cell("E5").Value = ledgerData.cane_crushed;
                    ws.Cell("F5").Value = summarized_report_for_current_season.td_cane_crushed;
                    ws.Cell("G5").Value = "R.S (C Heavy)";
                    ws.Cell("H5").Value = dailyAnalys.RectifiedSpirit;
                    ws.Cell("I5").Value = summarized_report_for_current_season.month_rectified_spirit;
                    ws.Cell("J5").Value = summarized_report_for_current_season.td_rectified_sprit;
                    ws.Cell("K5").Value = dailyAnalys.RectifiedSpiritDayRecover;
                    ws.Cell("L5").Value = dailyAnalys.RectifiedSpiritToDateRecovery;

                    ws.Cell("B6").Value = "Recovery";
                    ws.Cell("E6").Value = summarized_report_for_current_season.od_estimated_sugar_percent_cane;
                    ws.Cell("F6").Value = summarized_report_for_current_season.td_estimated_sugar_percent_cane;
                    ws.Cell("G6").Value = "A.A.(B. Heavy)";
                    ws.Cell("H6").Value = dailyAnalys.AbsoluteAlcohol;
                    ws.Cell("I6").Value = summarized_report_for_current_season.month_absolute_alcohol;
                    ws.Cell("J6").Value = ws.Cell("J5").Value = summarized_report_for_current_season.td_absolute_alcohol;
                    ws.Cell("K6").Value = dailyAnalys.AbsoluteAlcoholDayRecovery;
                    ws.Cell("L6").Value = dailyAnalys.AbsoluteAlcoholToDateRecovery;

                    ws.Cell("B7").Value = "Recovery on syrup";
                    ws.Cell("E7").Value = summarized_report_for_current_season.od_estimated_sugar_percent_on_syrup;
                    ws.Cell("F7").Value = summarized_report_for_current_season.td_estimated_sugar_percent_on_syrup;

                    ws.Cell("G7").Value = "ENA";
                    ws.Cell("H7").Value = dailyAnalys.Ethanol;
                    ws.Cell("I7").Value = summarized_report_for_current_season.month_ethanol;
                    ws.Cell("J7").Value = summarized_report_for_current_season.td_ethanol;
                    ws.Cell("K7").Value = dailyAnalys.EthanolDayRecovery;
                    ws.Cell("L7").Value = dailyAnalys.EthanolToDateRecovery;

                    ws.Cell("B8").Value = "Recovery on B-Heavy";
                    ws.Cell("E8").Value = summarized_report_for_current_season.od_estimated_sugar_percent_on_b_heavy;
                    ws.Cell("F8").Value = summarized_report_for_current_season.td_estimated_sugar_percent_on_b_heavy;
                    ws.Cell("G8").Value = "Total";
                    ws.Cell("H8").Value = ws.Evaluate("SUM(H5:H7)");
                    ws.Cell("I8").Value = ws.Evaluate("SUM(I5:I7)");
                    ws.Cell("J8").Value = ws.Evaluate("SUM(J5:J7)");

                    ws.Cell("B9").Value = "Recovery on C-Heavy";
                    ws.Cell("E9").Value = summarized_report_for_current_season.od_estimated_sugar_percent_on_c_heavy;
                    ws.Cell("F9").Value = summarized_report_for_current_season.td_estimated_sugar_percent_on_c_heavy;
                    ws.Cell("G9").Value = "Power Details";
                    ws.Cell("I9").Value = "Day";
                    ws.Cell("J9").Value = "Month";
                    ws.Cell("K9").Value = "Todate";

                    ws.Cell("B10").Value = "Recovery on raw Sugar";
                    ws.Cell("E10").Value = summarized_report_for_current_season.od_estimated_sugar_percent_on_raw_sugar;
                    ws.Cell("F10").Value = summarized_report_for_current_season.td_estimated_sugar_percent_on_raw_sugar;

                    ws.Cell("G10").Value = "Power Generation";
                    ws.Cell("I10").Value = summarized_report_for_current_season.od_power_generation_cogen.ToString();
                    ws.Cell("K10").Value = summarized_report_for_current_season.td_power_generation_cogen.ToString();
                    ws.Cell("J10").Value = summarized_report_for_current_season.month_power_generation_co_gen;
                    ws.Cell("B11").Value = "Recovery (Last year as per crop day)";
                    if (summarized_report_last_season_crop_day != null)
                    {
                        ws.Cell("E11").Value = summarized_report_last_season_crop_day.od_estimated_sugar_percent_cane;
                        ws.Cell("F11").Value = summarized_report_last_season_crop_day.td_estimated_sugar_percent_cane;
                    }
                    else
                    {
                        ws.Cell("E11").Value = 0;
                        ws.Cell("F11").Value = 0;
                    }

                    ws.Cell("G11").Value = "Power Export from co-gen";
                    ws.Cell("H11").Value = dailyAnalys.power_export_grid;
                    ws.Cell("I11").Value = dailyAnalys.PowerExportFromCogen;
                    ws.Cell("j11").Value = summarized_report_for_current_season.month_power_export_co_gen;
                    //ws.Cell("k11").Value = dailyAnalysesSummary.power_export_grid;
                    ws.Cell("K11").Value = summarized_report_for_current_season.td_power_export_from_co_gen;


                    ws.Cell("B12").Value = "Sugar Production";
                    ws.Cell("C12").Value = "On Date %";
                    ws.Cell("D12").Value = "I.C.U.S.A.";
                    ws.Cell("E12").Value = ledgerData.total_sugar_bagged;
                    ws.Cell("F12").Value = ledgerPeriodSummary.total_sugar_bags;

                    ws.Cell("G12").Value = "Power Export From Disti.";
                    ws.Cell("I12").Value = dailyAnalys.PowerExportFromDistillery;
                    ws.Cell("J12").Value = summarized_report_for_current_season.month_power_export_distillery;
                    ws.Cell("K12").Value = summarized_report_for_current_season.td_power_export_from_distillery;


                    ws.Cell("B13").Value = "L-31";
                    ws.Cell("C13").Value = Math.Round((Convert.ToDecimal(hourlyAnalyses.od_l31) * 100)/ (ledgerData.total_sugar_bagged > 0  ? Convert.ToDecimal(ledgerData.total_sugar_bagged) : 1 ),2);
                    ws.Cell("D13").Value = dailyAnalys.icumsa_l31;
                    ws.Cell("E13").Value = hourlyAnalyses.od_l31;
                    ws.Cell("F13").Value = hourlyAnalyses.td_l31;


                    ws.Cell("G13").Value = "Power To Distillery";
                    ws.Cell("I13").Value = dailyAnalys.PowerToDitillery;
                    ws.Cell("J13").Value = summarized_report_for_current_season.month_power_to_distillery;
                    ws.Cell("k13").Value = dailyAnalysesSummary.power_to_distillery;


                    

                    ws.Cell("B14").Value = "M-31";
                    ws.Cell("C14").Value = Math.Round((Convert.ToDecimal(hourlyAnalyses.od_m31) * 100) / (ledgerData.total_sugar_bagged > 0 ? Convert.ToDecimal(ledgerData.total_sugar_bagged) : 1), 2);
                    ws.Cell("D14").Value = dailyAnalys.icumsa_m31;
                    ws.Cell("E14").Value = hourlyAnalyses.od_m31;
                    ws.Cell("F14").Value = hourlyAnalyses.td_m31;
                    ws.Cell("G14").Value = "Total";
                    ws.Cell("I14").Value = ws.Evaluate("SUM(I11:I13)");
                    ws.Cell("J14").Value = ws.Evaluate("SUM(J11:J13)");
                    ws.Cell("K14").Value = ws.Evaluate("SUM(K11:K13)");


                    ws.Cell("B15").Value = "S-31";
                    ws.Cell("C15").Value = Math.Round((Convert.ToDecimal(hourlyAnalyses.od_s31) * 100) / (ledgerData.total_sugar_bagged > 0 ? Convert.ToDecimal(ledgerData.total_sugar_bagged) : 1), 2);
                    ws.Cell("D15").Value = dailyAnalys.icumsa_s31;
                    ws.Cell("E15").Value = hourlyAnalyses.od_s31;
                    ws.Cell("F15").Value = hourlyAnalyses.td_s31;
                    ws.Cell("G15").Value = "Bagasse Sold";
                    ws.Cell("I15").Value = summarized_report_for_current_season.od_bagasse_sold;
                    ws.Cell("K15").Value = summarized_report_for_current_season.td_bagasse_sold;


                    ws.Cell("B16").Value = "Raw Sugar";
                    ws.Cell("C16").Value = summarized_report_for_current_season.od_raw_percent.ToString();
                    ws.Cell("D16").Value = summarized_report_for_current_season.od_icumsa_raw.ToString();
                    ws.Cell("E16").Value = summarized_report_for_current_season.od_raw_sugar.ToString();
                    ws.Cell("F16").Value = summarized_report_for_current_season.td_raw_sugar.ToString();
                    ws.Cell("G16").Value = "Particulars";
                    ws.Cell("I16").Value = "Brix";
                    ws.Cell("J16").Value = "Pol";
                    ws.Cell("K16").Value = "Purity";


                    ws.Cell("A17").Value = "B.";
                    ws.Cell("B17").Value = "Efficiency";
                    ws.Cell("G17").Value = "Primary Juice";
                    ws.Cell("I17").Value = ledgerData.combined_pj_brix;
                    ws.Cell("J17").Value = ledgerData.combined_Pj_pol;
                    ws.Cell("K17").Value = ledgerData.combined_pj_purity;


                    ws.Cell("B18").Value = "Hours worked";
                    ws.Cell("D17").Value = "New Mill";
                    ws.Cell("E17").Value = "Old Mill";
                    ws.Cell("F17").Value = "Combine";
                    ws.Cell("G18").Value = "Mixed Juice";
                    ws.Cell("I18").Value = ledgerData.combined_mj_brix;
                    ws.Cell("J18").Value = ledgerData.combined_mj_pol;
                    ws.Cell("K18").Value = ledgerData.combined_mj_purity;

                    

                    ws.Cell("D18").Value = summarized_report_for_current_season.od_nm_gross_working_duration;
                    if(ledgerData.unit_code == 1 )
                    {
                        //ws.Cell("E14").Value = ConvertMinutesToHours(Convert.ToDecimal(1440 - stoppageSummary.om_gross_duration));
                        ws.Cell("E18").Value = summarized_report_for_current_season.od_om_gross_working_duration;
                    }
                    else
                    {
                        ws.Cell("E18").Value = "Not applicable";
                    }

                    //ws.Cell("F14").Value = ConvertMinutesToHours(Convert.ToDecimal(1440-stoppageSummary.TotalDuration));
                    ws.Cell("F18").Value = summarized_report_for_current_season.od_total_working_net_duration;

                    ws.Cell("B19").Value = "Hours Lost";
                    ws.Cell("D19").Value = summarized_report_for_current_season.od_nm_gross_stoppage_duration;
                    if (ledgerData.unit_code == 1)
                    {
                        //ws.Cell("E14").Value = ConvertMinutesToHours(Convert.ToDecimal(1440 - stoppageSummary.om_gross_duration));
                        ws.Cell("E19").Value = summarized_report_for_current_season.od_om_gross_stoppage_duration;
                    }
                    else
                    {
                        ws.Cell("E19").Value = "Not applicable";
                    }
                    ws.Cell("F19").Value = summarized_report_for_current_season.od_total_stoppages;
                    ws.Cell("G19").Value = "Un Sulphured Syrup";
                    ws.Cell("I19").Value = summarized_report_for_current_season.od_unsulphured_syrup_brix;
                    ws.Cell("J19").Value = summarized_report_for_current_season.od_unsulphured_syrup_pol;
                    ws.Cell("K19").Value = summarized_report_for_current_season.od_unsulphured_syrup_purity;
                    
                    

                    ws.Cell("B20").Value = "Total Loss";
                    ws.Cell("F20").Value = ledgerData.total_losses_percent;
                    ws.Cell("G20").Value = "C Massecuite";
                    ws.Cell("I20").Value = massecuteSummary.od_c_brix;
                    ws.Cell("J20").Value = massecuteSummary.od_c_pol;
                    ws.Cell("K20").Value = massecuteSummary.od_c_purity;


                    ws.Cell("B21").Value = "Pol % Cane";
                    ws.Cell("F21").Value = ledgerData.pol_in_cane_percent;
                    ws.Cell("G21").Value = "A-Heavy Molasses";
                    ws.Cell("I21").Value = molassesSummary == null ? 0 : molassesSummary.od_a_heavy_brix;
                    ws.Cell("J21").Value = molassesSummary == null ? 0 : molassesSummary.od_a_heavy_pol;
                    ws.Cell("K21").Value = molassesSummary == null ? 0 : molassesSummary.od_a_heavy_purity;


                    ws.Cell("B22").Value = "Molasses % Cane";
                    ws.Cell("F22").Value = ledgerData.estimated_molasses_percent_cane;
                    
                    ws.Cell("G22").Value = "B-Heavy Molasses";
                    ws.Cell("I22").Value = molassesSummary.od_b_heavy_brix == null ? 0 : molassesSummary.od_b_heavy_brix;
                    ws.Cell("J22").Value = molassesSummary.od_b_heavy_pol == null ? 0 : molassesSummary.od_b_heavy_pol;
                    //ws.Cell("K22").Value = ws.Evaluate("Round(((J18*100)/I18),2)");
                    ws.Cell("K22").Value = molassesSummary.od_b_heavy_purity == null ? 0 : molassesSummary.od_b_heavy_purity;

                    ws.Cell("B23").Value = "Molasses % Cane (Before Syrup Diversion)";
                    ws.Cell("F23").Value = summarized_report_for_current_season.od_molasses_by_cane_used_for_sugar_production;
                    ws.Cell("G23").Value = "C-Single Cured";
                    ws.Cell("I23").Value = meltingSummary == null ? 0 : meltingSummary.od_c_single_sugar_brix;
                    ws.Cell("J23").Value = meltingSummary == null ? 0 : meltingSummary.od_c_single_sugar_pol; ;
                    ws.Cell("K23").Value = meltingSummary == null ? 0 : meltingSummary.od_c_single_sugar_purity; ;

                    ws.Cell("B24").Value = "Pol % Bagasse";
                    ws.Cell("F24").Value = ledgerData.combined_bagasse_average;
                    ws.Cell("G24").Value = "C-Double Cured";
                    ws.Cell("I24").Value = meltingSummary == null ? 0 : meltingSummary.od_c_double_sugar_brix;
                    ws.Cell("J24").Value = meltingSummary == null ? 0 : meltingSummary.od_c_double_sugar_pol;
                    ws.Cell("K24").Value = meltingSummary == null ? 0 : meltingSummary.od_c_double_sugar_purity;

                    ws.Cell("B25").Value = "Moisture % Bagasse";
                    ws.Cell("F25").Value = ledgerData.moist_percent_bagasse;
                    ws.Cell("G25").Value = "B-Single Cured";
                    ws.Cell("I25").Value = meltingSummary == null ? 0 : meltingSummary.od_b_sugar_brix;
                    ws.Cell("J25").Value = meltingSummary == null ? 0 : meltingSummary.od_b_sugar_pol;
                    ws.Cell("K25").Value = meltingSummary == null ? 0 : meltingSummary.od_b_sugar_purity;

                    ws.Cell("B26").Value = "Bagasse % Cane";
                    ws.Cell("F26").Value = ledgerData.total_bagasse_percent_cane;
                    ws.Cell("G26").Value = "Final Molasses";
                    ws.Cell("I26").Value = ledgerData.final_molasses_brix;
                    ws.Cell("J26").Value = ledgerData.final_molasses_pol;
                    ws.Cell("K26").Value = ledgerData.final_molasses_purity;

                    ws.Cell("B27").Value = "Fiber % Cane";
                    ws.Cell("F27").Value = ledgerData.fiber_percent_cane;
                    ws.Cell("G27").Value = "Cane quantity diverted for Ethonol (Qtls.)";

                    ws.Cell("G28").Value = "Syrup quantity  diverted For Ethonol (Qtls.)";
                    ws.Cell("L28").Value = summarized_report_for_current_season.od_syrup_diversion;
                    ws.Cell("G29").Value = "Loss in Bagasse %";

                    ws.Cell("I29").Value = summarized_report_for_current_season.od_pol_in_bagasse_percent;
                    ws.Cell("L27").Value = summarized_report_for_current_season.od_cane_used_for_syrup;
                    ws.Cell("J29").Value = "Loss in Molasses %";
                    ws.Cell("L29").Value = summarized_report_for_current_season.od_pol_in_molasses_percent_cane;

                    ws.Cell("G30").Value = "Loss in press cake %";
                    ws.Cell("I30").Value = summarized_report_for_current_season.od_pol_in_press_cake;
                    ws.Cell("J30").Value = "Unknown Loss %";
                    ws.Cell("L30").Value = summarized_report_for_current_season.od_unknown_losses_calculated_percent;
                    ws.Cell("G31").Value = "Loss in syrup %";
                    ws.Cell("I31").Value = summarized_report_for_current_season.od_pol_in_syup_percent_cane;
                    
                    ws.Cell("J31").Value = "Sugar In Sugar %";
                    
                    ws.Cell("L31").Value = summarized_report_for_current_season.od_sugar_in_sugar_percent;
                    ws.Cell("B28").Value = "Maceration % Cane";
                    ws.Cell("F28").Value = ledgerData.water_percent_cane;
                    

                    ws.Cell("B29").Value = "Maceration % Fiber";
                    ws.Cell("F29").Value = ledgerData.added_water_percent_fiber;
                    


                    ws.Cell("B30").Value = "Mixed Juice % Cane";
                    ws.Cell("F30").Value = ledgerData.net_mixed_juice_percent_cane;
                    
                    ws.Cell("G32").Value = "Stoppage on New Mill";
                    ws.Cell("K32").Value = "HH:MM";
                    int nm_start_cell_number = 33;
                    int om_start_cell_number = 39;
                    ws.Range("K33", "K37").Style.NumberFormat.Format = "HH:MM";
                    ws.Range("K39", "K43").Style.NumberFormat.Format = "HH:MM";
                    foreach (var item in stoppageDetails)
                    {
                        switch(item.s_mill_code)
                        {
                            case 0:
                                ws.Cell("G" + nm_start_cell_number.ToString()).Value = item.s_comment.Trim();
                                ws.Cell("K" + nm_start_cell_number.ToString()).Value = ConvertMinutesToHours((decimal)item.s_duration);
                                ws.Range("G" + nm_start_cell_number.ToString(), "J" + nm_start_cell_number.ToString()).Merge();
                                 nm_start_cell_number++;
                                break;
                            case 1:
                                ws.Cell("G" + om_start_cell_number.ToString()).Value = item.s_comment.Trim();
                                ws.Cell("K" + om_start_cell_number.ToString()).Value = ConvertMinutesToHours((decimal)item.s_duration);
                                ws.Range("G" + om_start_cell_number.ToString(), "J" + om_start_cell_number.ToString()).Merge();
                                om_start_cell_number++;
                                break;
                               
                            default:
                                break;
                        }
                    }
                    //ws.Cell("G23").Value = s;
                    

                    ws.Cell("B31").Value = "Press Cake Pol %";
                    ws.Cell("F31").Value = ledgerData.press_cake_average;
                    

                    ws.Cell("B32").Value = "Steam Consumption % Cane With D.S.H. water";
                    ws.Cell("F32").Value = dailyAnalys.steam_percent_cane;

                    ws.Cell("B33").Value = "Steam Consumption % Cane Without D.S.H. water";
                    ws.Cell("F33").Value = summarized_report_for_current_season.od_steam_consumption_without_d_superheating_percent_cane;



                    //ws.Cell("B24").Value = "Primary Extraction (PE)";
                    //ws.Cell("F24").Value = dailyAnalys.nm_pry_ext;

                    ws.Cell("B34").Value = "Primary Extraction (PE)";
                    ws.Cell("F34").Value = dailyAnalys.nm_pry_ext;

                    ws.Cell("B35").Value = "Preparation Index (PI)";
                    ws.Cell("F35").Value = dailyAnalys.nm_p_index;
                   
                    
                    ws.Cell("B36").Value = "Cane Crushed Rate Incl. Stpg.";
                    int totalDuration = Convert.ToInt16(stoppageSummary.TotalDuration);
                    ws.Cell("F36").Value = dailyAnalys.cane_crushed;
                    
                    

                    ws.Cell("B37").Value = "Cane Crushed Rate Excl. Stpg.";
                    //ws.Cell("F29").Value = totalDuration <= 0 ? dailyAnalys.cane_crushed : ((dailyAnalys.cane_crushed * 1440) / (1440 - totalDuration));
                    ws.Cell("F37").Value = summarized_report_for_current_season.od_average_crushe_excluding_stoppage;


                    ws.Cell("B38").Value = "pH of Primary Juice";
                    ws.Cell("F38").Value = twoHourlySummary.combined_pj_ph;
                    ws.Cell("G38").Value = "Stoppages on Old Mill";
                    ws.Cell("K38").Value = "HH:MM";

                    
                    

                    ws.Cell("B39").Value = "pH of Mixed Juice";
                    ws.Cell("F39").Value = twoHourlySummary.combined_mj_ph;
                    

                    ws.Cell("B40").Value = "pH of Clear Juice";
                    ws.Cell("F40").Value = twoHourlySummary.clear_juice_ph_avg;
                    

                    ws.Cell("B41").Value = "pH of Sulphur Syrup";
                    ws.Cell("F41").Value = twoHourlySummary.sulphered_ph_avg;
                    

                    ws.Cell("B42").Value = "pH of Cooling Tower";
                    ws.Cell("F42").Value = hourlyAnalyses.od_cooling_tower_ph;
                    

                    ws.Cell("B43").Value = "pH of Boiler Water";
                    ws.Cell("F43").Value = dailyAnalys.boiler_water;
                   

                    ws.Cell("A44").Value = "C";
                    ws.Cell("B44").Value = "Juice Flow M³";

                    ws.Cell("B45").Value = "Hours";
                    ws.Cell("D45").Value = "1st";
                    ws.Cell("E45").Value = "2nd";
                    ws.Cell("F45").Value = "3rd";
                    ws.Cell("G45").Value = "4th";
                    ws.Cell("H45").Value = "5th";
                    ws.Cell("I45").Value = "6th";
                    ws.Cell("J45").Value = "7th";
                    ws.Cell("K45").Value = "8th";

                    ws.Cell("B46").Value = "Shift 8-4";
                    ws.Cell("B47").Value = "Shift 4-12";
                    ws.Cell("B48").Value = "Shift 12-8";
                    
                    int startTime = 9;
                    int cellAddress = 46;
                    for (int i = 1; i<=3; i++)
                    {
                        char cellAlphabet = 'D';
                        for(int hour = 1; hour<= 8; hour ++)
                        {
                            if(startTime >= 25)
                            {
                                startTime = 1;
                            }
                           int juiceFlow = Convert.ToInt32(hourlyAnalysList.Where(x => x.entry_time == startTime).Select(x => x.juice_total).FirstOrDefault());
                            ws.Cell(cellAlphabet + cellAddress.ToString()).Value = juiceFlow;
                            int Ascii = (int)cellAlphabet;
                            int nextAscii = Ascii + 1;
                            cellAlphabet = (char)nextAscii;
                            startTime = startTime+1;
                        }
                        cellAddress = cellAddress + 1;
                    }

                    ws.Cell("A49").Value= "D";
                    ws.Cell("B49").Value = "Cane Purchase";

                    ws.Cell("G49").Value = "Yard Balance (Qtls.)";
                    
                    ws.Cell("G50").Value = hourlyAnalyses.od_uncrushed;
                    ws.Cell("H49").Value = "E. Weather";

                    ws.Cell("C50").Value = "On Date";
                    ws.Cell("E50").Value = "To-Date";
                    ws.Cell("H50").Value = "Temp.";
                    ws.Cell("J50").Value = "Max.";
                    ws.Cell("K50").Value = "Min.";

                    ws.Cell("C51").Value = "%";
                    ws.Cell("D51").Value = "Qtl.";
                    ws.Cell("E51").Value = "%";
                    ws.Cell("F51").Value = "Qtl.";

                    ws.Cell("G51").Value = "DMF";
                    ws.Cell("H51").Value = "Day Condition";
                    ws.Cell("J51").Value = dailyAnalys.temp_max;
                    ws.Cell("K51").Value = dailyAnalys.temp_min;

                    ws.Cell("B52").Value = "Gate + Farm";
                    ws.Cell("C52").Value = ledgerData.cane_gate_percent;
                    ws.Cell("D52").Value = ledgerData.cane_gate;
                    ws.Cell("E52").Value = ledgerPeriodSummary.cane_gate_percent;
                    ws.Cell("F52").Value = ledgerPeriodSummary.cane_gate;
                    ws.Cell("G52").Value = ledgerData.dry_mill_factor_percent_cane;
                    ws.Cell("J52").Value = "On-Date";
                    ws.Cell("K52").Value = "To-Date";

                    ws.Cell("B53").Value = "Centre";
                    ws.Cell("C53").Value = ledgerData.cane_centre_percent;
                    ws.Cell("D53").Value = ledgerData.cane_centre;
                    ws.Cell("E53").Value = ledgerPeriodSummary.cane_centre_percent;
                    ws.Cell("F53").Value = ledgerPeriodSummary.cane_centre;
                    ws.Cell("G53").Value = "JAVA RATIO";
                   
                    ws.Cell("H53").Value = "Rainfall(inch)";
                    ws.Cell("J53").Value = dailyAnalys.rain_fall;
                    ws.Cell("K53").Value = dailyAnalysesSummary.rain_fall;

                    ws.Cell("B54").Value = "Total";
                    ws.Cell("C54").Value = ws.Evaluate("sum(C52:C53)");
                    ws.Cell("D54").Value = ws.Evaluate("sum(D52:D53)");
                    ws.Cell("E54").Value = ws.Evaluate("sum(E52:E53)");
                    ws.Cell("F54").Value = ws.Evaluate("sum(F52:F53)");
                    ws.Cell("G54").Value = summarized_report_for_current_season.od_java_ratio;
                    ws.Cell("H54").Value = "Humidity%";
                    ws.Cell("J54").Value = dailyAnalys.humidity;
                    ws.Cell("K54").Value = "";

                    ws.Cell("B55").Value = "Early";
                    ws.Cell("C55").Value = ledgerData.cane_early_percent;
                    ws.Cell("D55").Value =  ledgerData.cane_early ;
                    ws.Cell("E55").Value = ledgerPeriodSummary.cane_early_percent;
                    ws.Cell("F55").Value = ledgerPeriodSummary.cane_early;
                    ws.Cell("G55").Value = "Distillery Stoppage";

                    ws.Cell("B56").Value = "General";
                    ws.Cell("C56").Value = ledgerData.cane_general_percent;
                    ws.Cell("D56").Value = ledgerData.cane_general;
                    ws.Cell("E56").Value = ledgerPeriodSummary.cane_general_percent;
                    ws.Cell("F56").Value = ledgerPeriodSummary.cane_general;


                    ws.Cell("B57").Value = "Reject & Burnt";
                    ws.Cell("C57").Value = ledgerData.cane_reject_percent+ledgerData.cane_burnt_percent;
                    ws.Cell("D57").Value = ledgerData.cane_reject+ledgerData.cane_burnt;
                    ws.Cell("E57").Value = ledgerPeriodSummary.cane_reject_percent+ledgerPeriodSummary.cane_burnt_percent;
                    ws.Cell("F57").Value = ledgerPeriodSummary.cane_reject+ledgerPeriodSummary.cane_burnt;

                    ws.Cell("B58").Value = "Total";
                    ws.Cell("C58").Value = ws.Evaluate("SUM(C55:C57)");
                    ws.Cell("D58").Value = ws.Evaluate("SUM(D55:D57)");
                    ws.Cell("E58").Value = ws.Evaluate("SUM(E55:E57)");
                    ws.Cell("F58").Value = ws.Evaluate("SUM(F55:F57)");

                    ws.Cell("A59").Value = "F";
                    ws.Cell("B59").Value = "ETP Parameters";

                    ws.Cell("B60").Value = "BOD";
                    ws.Cell("C60").Value = dailyAnalys.etp_bod;
                    ws.Cell("D60").Value = "COD";
                    ws.Cell("E60").Value = dailyAnalys.etp_cod;
                    ws.Cell("F60").Value = "TSS";
                    ws.Cell("G60").Value = dailyAnalys.etp_tss;
                    ws.Cell("H60").Value = "pH";
                    ws.Cell("I60").Value = dailyAnalys.etp_ph;
                    ws.Cell("J60").Value = "Flow M3";
                    ws.Cell("K60").Value = dailyAnalys.etp_water_flow;

                    ws.Cell("B61").Value = "Exhaust Condensate Rec%";
                    ws.Cell("D61").Value = dailyAnalys.exhaust_condensate_recovery;
                    

                    ws.Cell("E61").Value = "Tube Well Running Hours";
                    ws.Cell("G61").Value = dailyAnalys.total_operating_tube_well;
                    ws.Cell("H61").Value = "No of big C-M/C pan Dtopped";
                    ws.Cell("K61").Value = dailyAnalys.T_c_massecuite_pan;
                    //ws.Cell("K53").Value = dailyAnalysesSummary.total_operating_tube_well;

                    ws.Cell("A62").Value = "G";
                    ws.Cell("B62").Value = "Water Consumption at Pans";
                    ws.Cell("K62").Value = "At OC Filter";
                    ws.Cell("B63").Value = "A-Pan Station";
                    ws.Cell("C63").Value = "M³";
                    ws.Cell("D63").Value = "% On Cane";
                    ws.Cell("E63").Value = "B-Pan Station";
                    ws.Cell("F63").Value = "M³";
                    ws.Cell("G63").Value = "% On Cane";
                    ws.Cell("H63").Value = "C-Pan Station";
                    ws.Cell("I63").Value = "M³";
                    ws.Cell("J63").Value = "% On Cane";
                    ws.Cell("K63").Value = "M³";
                    ws.Cell("L63").Value = "%";

                    ws.Cell("C64").Value = dailyAnalys.water_pan_a/10;
                    ws.Cell("D64").Value = ((dailyAnalys.water_pan_a * 100) / (dailyAnalys.cane_crushed == 0 ? 1 : dailyAnalys.cane_crushed));
                    ws.Cell("D64").Style.NumberFormat.Format ="00.00";
                    
                    ws.Cell("F64").Value = dailyAnalys.water_pan_b/10;
                    ws.Cell("G64").Value = Math.Round((Convert.ToDecimal(dailyAnalys.water_pan_b) * 100) / (dailyAnalys.cane_crushed == 0 ? 1 : dailyAnalys.cane_crushed),2);
                    ws.Cell("H64").Style.NumberFormat.Format = "00.00";

                    ws.Cell("I64").Value = dailyAnalys.water_pan_c/10;
                    ws.Cell("J64").Value = (dailyAnalys.water_pan_c * 100) / (dailyAnalys.cane_crushed == 0 ? 1 : dailyAnalys.cane_crushed);
                    ws.Cell("J64").Style.NumberFormat.Format = "00.00";
                    ws.Cell("K64").Value = summarized_report_for_current_season.od_filter_water/10;
                    ws.Cell("L64").Value = summarized_report_for_current_season.od_filter_water_percent_cane;
                    ws.Cell("B65").Value = "Remarks";

                    ws.Cell("B68").Value = "Lab Head";
                    ws.Cell("K68").Value = "Unit Head";

                    
                    

                    wb.SaveAs(filePath);

                    return await Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            }
        }
    }
}
