using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using DataAccess.Repositories.AnalysisRepositories;


namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class ComparitiveReportExcel
    {
        public async Task<string> ExcelReportFile(int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            ComparitiveReportExcel pe = new ComparitiveReportExcel();
            await Task.FromResult(pe.GenerateExcel(unitCode, seasonCode, ReportDate, filePath));
            return filePath;
        }

        private async Task<bool> GenerateExcel(int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            #region Object Declaration
            LedgerDataRepository ledgerDataRepository = new LedgerDataRepository();
            MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
            func_ledger_data_period_summary_Result ledgerPeriodSummary = new func_ledger_data_period_summary_Result();
            DailyAnalysisRepository dailyAnalysisRepository = new DailyAnalysisRepository();
            DailyAnalys dailyAnalys = new DailyAnalys();
            MolassesAnalysisRepository molassesAnalysisRepository = new MolassesAnalysisRepository();
            StoppageRepository stoppageRepository = new StoppageRepository();

            #endregion
            var MasterUnitDefaults = masterUnitRepository.FindUnitByPk(unitCode);
            ledgerPeriodSummary = await ledgerDataRepository.GetLedgerDataPeriodSummaryAsync(unitCode, seasonCode, Convert.ToDateTime(MasterUnitDefaults.CrushingStartDate), ReportDate);
            var ledgerData = ledgerDataRepository.GetLedgerDataForTheDate(unitCode, seasonCode, ReportDate);
            var dailyAnalysesSummary = dailyAnalysisRepository.dailyAnalysesSummaryReport(unitCode, seasonCode, ReportDate);
            dailyAnalys = dailyAnalysisRepository.GetDailyAnalysisDetailsByDate(unitCode, seasonCode, ReportDate);
            var molassesSummary = molassesAnalysisRepository.molassesTodateSummary(unitCode, seasonCode, ReportDate);
            var stoppageSummary = stoppageRepository.GetStoppageSummaryForDay(unitCode, seasonCode, ReportDate);

            using (XLWorkbook wb = new XLWorkbook())
            {
                try
                {
                    wb.Properties.Author = "Birla Sugar Lab Information System";
                    wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                    wb.Properties.Category = "Reports";
                    

                    var ws = wb.Worksheets.Add("Comparitive");
                    //ws.Protect("Google@1234");

                    
                    ws.Range("A1","I2").Value = MasterUnitDefaults.Name + Environment.NewLine + MasterUnitDefaults.Address;
                    
                    ws.Range("A1","I2").Merge();
                    ws.Cell("A3").Value = "Daily Comparitive Report"; // later get it from databse table
                    ws.Range("A3:I3").Merge();
                    ws.Cell("A4").Value = "Crushing Season "+dailyAnalysesSummary.season_year.ToString();
                    ws.Range("A4:I4").Merge();
                    ws.Cell("B5").Value = "CropDay : " + ledgerPeriodSummary.days_count;
                    ws.Cell("F5").Value = "Date : " + ReportDate.ToLongDateString();
                    ws.Cell("F5").Style.DateFormat.Format = "dd-MMM-yyyy";
                    ws.Range("F5", "I5").Merge();
                    
                    
                    

                    ws.Range("A6", "A7").Value = "S.No.";
                    ws.Range("A6", "A7").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    ws.Range("B6", "B7").Value = "Particular";
                    ws.Range("B6", "B7").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    ws.Range("C6", "C7").Value = "Unit";
                    ws.Range("C6", "C7").Merge().Style.Font.SetBold().Font.FontSize = 11;

                    ws.Range("D6", "E6").Value = "This Year";
                    ws.Range("D6", "E6").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    ws.Cell("D7").Value = "Day";
                    ws.Cell("E7").Value = "To Date";

                    ws.Range("F6", "G6").Value = "Last Year per Crop Day";
                    ws.Range("F6", "G6").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    ws.Cell("F7").Value = "Day";
                    ws.Cell("G7").Value = "To Date";

                    ws.Range("H6", "I6").Value = "Last Year On Date";
                    ws.Range("H6", "I6").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    ws.Cell("H7").Value = "Day";
                    ws.Cell("I7").Value = "To Date";

                    ws.Cell("A8").Value = 1;
                    ws.Cell("B8").Value = "Actual Hrs. of Crushing";
                    ws.Cell("C8").Value = "Hrs-Min";
                    //ws.Cell("D8").Value = ConvertMinutesToHours(1440-Convert.ToInt32(stoppageSummary.TotalDuration));
                    ws.Cell("D8").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.actual_minutes_of_crushing));
                    ws.Cell("D8").Style.DateFormat.Format = "HH:MM";
                    ws.Cell("E8").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_actual_minutes_of_crushing)); ;

                    ws.Cell("A9").Value = 2;
                    ws.Cell("B9").Value = "Cane Crushed(Qtls.)";
                    ws.Cell("C9").Value = "Qtls.";
                    ws.Cell("D9").Value = ledgerData.cane_crushed;
                    ws.Cell("E9").Value = ledgerPeriodSummary.cane_crushed;

                    ws.Cell("A10").Value = 3;
                    ws.Cell("B10").Value = "Recovery % Cane";
                    ws.Cell("C10").Value = "%";
                    ws.Cell("D10").Value = ledgerData.estimated_sugar_percent_cane;
                    ws.Cell("E10").Value = ledgerPeriodSummary.estimated_suagar_percent;


                    ws.Cell("A11").Value = 4;
                    ws.Cell("B11").Value = "Bagasse % Cane";
                    ws.Cell("C11").Value = "%";
                    ws.Cell("D11").Value = ledgerData.total_bagasse_percent_cane;
                    ws.Cell("E11").Value = ledgerPeriodSummary.total_bagasse_percent_cane;

                    ws.Cell("A12").Value = 5;
                    ws.Cell("B12").Value = "Molasses % Cane";
                    ws.Cell("C12").Value = "%";
                    ws.Cell("D12").Value = ledgerData.estimated_molasses_percent_cane;
                    ws.Cell("E12").Value = ledgerPeriodSummary.estimated_molasses_percent_cane;

                    ws.Cell("A13").Value = 6;
                    ws.Cell("B13").Value = "Press Cake % Cane";
                    ws.Cell("C13").Value = "%";
                    ws.Cell("D13").Value = ledgerData.press_cake_percent_cane;
                    ws.Cell("E13").Value = ledgerPeriodSummary.press_cake_percent_cane;

                    ws.Cell("A14").Value = 7;
                    ws.Cell("B14").Value = "Pol % Cane";
                    ws.Cell("C14").Value = "%";
                    ws.Cell("D14").Value = ledgerData.pol_in_cane_percent;
                    ws.Cell("E14").Value = ledgerPeriodSummary.pol_in_cane_percent;



                    ws.Cell("A15").Value = 8;
                    ws.Cell("B15").Value = "Fiber % Cane";
                    ws.Cell("C15").Value = "%";
                    ws.Cell("D15").Value = ledgerData.fiber_percent_cane;
                    ws.Cell("E15").Value = ledgerPeriodSummary.fiber_percent_cane;

                    ws.Cell("A16").Value = 9;
                    ws.Cell("B16").Value = "Sugar Production (Qtls.)";
                    ws.Cell("C16").Value = "Qtls.";
                    ws.Cell("D16").Value = ledgerData.total_sugar_bagged;
                    ws.Cell("E16").Value = ledgerPeriodSummary.total_sugar_bags;


                    ws.Cell("A17").Value = 10;
                    ws.Cell("B17").Value = "Molasses Sent Out";
                    ws.Cell("C17").Value = "Qtls.";
                    ws.Cell("D17").Value = ledgerData.final_molasses_sent_out;
                    ws.Cell("E17").Value = ledgerPeriodSummary.final_molasses_sent_out;


                    ws.Cell("A18").Value = 11;
                    ws.Cell("B18").Value = "Nett Sugar Made";
                    ws.Cell("C18").Value = "Qtls.";
                    ws.Cell("D18").Value = ledgerData.estimated_sugar_qtl;
                    ws.Cell("E18").Value = ledgerPeriodSummary.estimated_sugar_qtl;



                    ws.Cell("A19").Value = 12;
                    ws.Cell("B19").Value = "Nett Molasses Made";
                    ws.Cell("C19").Value = "Qtls.";
                    ws.Cell("D19").Value = ledgerData.estimated_molasses_qtl;
                    ws.Cell("E19").Value = ledgerPeriodSummary.estimated_molasses;



                    ws.Cell("A20").Value = 13;
                    ws.Cell("B20").Value = "Bags(+ -)";
                    ws.Cell("C20").Value = "Qtls.";
                    ws.Cell("D20").Value = ledgerData.estimated_sugar_qtl-ledgerData.total_sugar_bagged;
                    ws.Cell("E20").Value = ledgerPeriodSummary.estimated_sugar_qtl - ledgerPeriodSummary.total_sugar_bags;

                    ws.Cell("A21").Value = 14;
                    ws.Cell("B21").Value = "Molasses(+ -)";
                    ws.Cell("C21").Value = "Qtls.";
                    ws.Cell("D21").Value = ledgerData.estimated_molasses_qtl - ledgerData.final_molasses_sent_out;
                    ws.Cell("E21").Value = ledgerPeriodSummary.estimated_molasses - ledgerPeriodSummary.final_molasses_sent_out;


                    ws.Cell("A22").Value = 15;
                    ws.Cell("B22").Value = "Sugar In Process";
                    ws.Cell("C22").Value = "Qtls.";
                    ws.Cell("D22").Value = ledgerData.sugar_in_process_qtl;
                    ws.Cell("E22").Value = ledgerPeriodSummary.sugar_in_process;


                    ws.Cell("A23").Value = 16;
                    ws.Cell("B23").Value = "Molasses In Process";
                    ws.Cell("C23").Value = "Qtls.";
                    ws.Cell("D23").Value = ledgerData.molasses_in_process_qtl;
                    ws.Cell("E23").Value = ledgerPeriodSummary.molasses_in_process;



                    ws.Cell("A24").Value = 17;
                    ws.Cell("B24").Value = "Jawa Ratio";
                    ws.Cell("C24").Value = "%";
                    ws.Cell("D24").Value = ledgerData.java_ratio;
                    ws.Cell("E24").Value = ledgerPeriodSummary.java_ratio;


                    ws.Cell("A25").Value = 18;
                    ws.Cell("B25").Value = "D.M.F";
                    ws.Cell("C25").Value = "%";
                    ws.Cell("D25").Value = ledgerData.dry_mill_factor_percent_cane;
                    ws.Cell("E25").Value = ledgerPeriodSummary.dry_mill_factor_percent_cane;



                    ws.Cell("A26").Value = 19;
                    ws.Cell("B26").Value = "Pol % Press Cake";
                    ws.Cell("C26").Value = "%";
                    ws.Cell("D26").Value = ledgerData.press_cake_average;
                    ws.Cell("E26").Value = ledgerPeriodSummary.pol_in_press_cake_percent;

                    ws.Cell("A27").Value = 20;
                    ws.Cell("B27").Value = "Pol % Bagasse";
                    ws.Cell("C27").Value = "%";
                    ws.Cell("D27").Value = ledgerData.combined_bagasse_average;
                    ws.Cell("E27").Value = ledgerPeriodSummary.combined_bagasse_percent;


                    ws.Cell("A28").Value = 21;
                    ws.Cell("B28").Value = "Moisture % Bagasse";
                    ws.Cell("C28").Value = "%";
                    ws.Cell("D28").Value = ledgerData.moist_percent_bagasse;
                    ws.Cell("E28").Value = ledgerPeriodSummary.moist_bagasse_percent;

                    ws.Cell("A29").Value = 22;
                    ws.Cell("B29").Value = "Final Molasses Purity";
                    ws.Cell("C29").Value = "%";
                    ws.Cell("D29").Value = ledgerData.final_molasses_purity;
                    ws.Cell("E29").Value = ledgerPeriodSummary.final_molasses_purity;

                    ws.Cell("A30").Value = 23;
                    ws.Cell("B30").Value = "Primary Juice Brix %";
                    ws.Cell("C30").Value = "%";
                    ws.Cell("D30").Value = ledgerData.combined_pj_brix;
                    ws.Cell("E30").Value = ledgerPeriodSummary.pj_brix;


                    ws.Cell("A31").Value = 24;
                    ws.Cell("B31").Value = "Primary Juice Purity";
                    ws.Cell("C31").Value = "%";
                    ws.Cell("D31").Value = ledgerData.combined_pj_purity;
                    ws.Cell("E31").Value = ledgerPeriodSummary.pj_purity;

                    ws.Cell("A32").Value = 25;
                    ws.Cell("B32").Value = "Mixed Juice Brix %";
                    ws.Cell("C32").Value = "%";
                    ws.Cell("D32").Value = ledgerData.combined_mj_brix;
                    ws.Cell("E32").Value = ledgerPeriodSummary.mj_brix;

                    ws.Cell("A33").Value = 26;
                    ws.Cell("B33").Value = "Mixed Juice Purity";
                    ws.Cell("C33").Value = "%";
                    ws.Cell("D33").Value = ledgerData.combined_mj_purity;
                    ws.Cell("E33").Value = ledgerPeriodSummary.mj_purity;


                    ws.Cell("A34").Value = 27;
                    ws.Cell("B34").Value = "PJ - MJ Purity";
                    ws.Cell("C34").Value = "%";
                    ws.Cell("D34").Value = ledgerData.combined_pj_purity - ledgerData.combined_mj_purity;
                    ws.Cell("E34").Value = ledgerPeriodSummary.pj_purity - ledgerPeriodSummary.mj_purity;


                    ws.Cell("A35").Value = 28;
                    ws.Cell("B35").Value = "Mixed Juice % Cane";
                    ws.Cell("C35").Value = "%";
                    ws.Cell("D35").Value = ledgerData.net_mixed_juice_percent_cane;
                    ws.Cell("E35").Value = ledgerPeriodSummary.net_mixed_juice_percent_cane;


                    ws.Cell("A36").Value = 29;
                    ws.Cell("B36").Value = "Added Water % Cane";
                    ws.Cell("C36").Value = "%";
                    ws.Cell("D36").Value = ledgerData.added_water_percent_fiber;
                    ws.Cell("E36").Value = ledgerPeriodSummary.added_water_percent_fiber;


                    ws.Cell("A37").Value = 30;
                    ws.Cell("B37").Value = "Undiluted Jc% Cane)";
                    ws.Cell("C37").Value = "%";
                    ws.Cell("D37").Value = ledgerData.undiulted_juice_percent_cane;
                    ws.Cell("E37").Value = ledgerPeriodSummary.undiluted_juice_percent_cane;

                    ws.Range("A38","I38").Value = "Losses" ;
                    ws.Range("A38", "I38").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    

                    ws.Cell("A39").Value = 31;
                    ws.Cell("B39").Value = "Losses in Bagasse";
                    ws.Cell("C39").Value = "%";
                    ws.Cell("D39").Value = ledgerData.pol_in_bagasse_percentage_cane;
                    ws.Cell("E39").Value = ledgerPeriodSummary.pol_in_bagasse_percent_cane;

                    ws.Cell("A40").Value = 32;
                    ws.Cell("B40").Value = "Loss in Fil.Cake";
                    ws.Cell("C40").Value = "%";
                    ws.Cell("D40").Value = ledgerData.pol_in_press_cake_percentage_cane;
                    ws.Cell("E40").Value = ledgerPeriodSummary.pol_in_press_cake_percent_cane;

                    ws.Cell("A41").Value = 33;
                    ws.Cell("B41").Value = "Loss in Molasses";
                    ws.Cell("C41").Value = "%";
                    ws.Cell("D41").Value = ledgerData.pol_in_molasses_percent_cane;
                    ws.Cell("E41").Value = ledgerPeriodSummary.final_molasses_pol_percent_cane;


                    ws.Cell("A42").Value = 34;
                    ws.Cell("B42").Value = "Unknown Loass";
                    ws.Cell("C42").Value = "%";
                    ws.Cell("D42").Value = ledgerData.unknown_loss_percent_cane;
                    ws.Cell("E42").Value = ledgerPeriodSummary.unknown_loss_percent;

                    ws.Cell("A43").Value = 35;
                    ws.Cell("B43").Value = "Total Loss";
                    ws.Cell("C43").Value = "%";
                    ws.Cell("D43").Value = ledgerData.total_losses_percent;
                    ws.Cell("E43").Value = ledgerPeriodSummary.total_loss_percent;


                    ws.Range("A44", "I44").Value = "Stoppages";
                    ws.Range("A44", "I44").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    

                    ws.Cell("A45").Value = 36;
                    ws.Cell("B45").Value = "New Mill";
                    ws.Cell("C45").Value = "Hrs - Min";
                    ws.Cell("D45").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.nm_gross_duration));
                    //ws.Cell("D45").Style.DateFormat.Format = "HH:MM";
                    ws.Cell("E45").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_nm_gross_duration));
                    //ws.Cell("E45").Style.DateFormat.Format = "HH:MM";

                    ws.Cell("A46").Value = 37;
                    ws.Cell("B46").Value = "Old Mill";
                    ws.Cell("C46").Value = "Hrs - Min";
                    ws.Cell("D46").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.om_gross_duration));
                    //ws.Cell("D46").Style.DateFormat.Format = "HH:MM";
                    ws.Cell("E46").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_om_gross_duration));
                    //ws.Cell("E46").Style.DateFormat.Format = "HH:MM";


                    ws.Cell("A47").Value = 38;
                    ws.Cell("B47").Value = "Total Both Mill";
                    ws.Cell("C47").Value = "Hrs - Min";
                    ws.Cell("D47").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.TotalDuration));
                    //ws.Cell("D47").Style.DateFormat.Format = "HH:MM";
                    ws.Cell("E47").Value = ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_TotalDuration));
                    //ws.Cell("E47").Style.DateFormat.Format = "HH:MM";

                    ws.Cell("A48").Value = 39;
                    ws.Cell("B48").Value = "Crushing speed per day";
                    ws.Cell("C48").Value = "Qtls.";
                    //ws.Cell("D48").Value = Convert.ToDecimal(stoppageSummary.TotalDuration) >= 1440 ? 0 :  Math.Round(Convert.ToDecimal(dailyAnalysesSummary.cane_crushed)/((1440- Convert.ToDecimal(stoppageSummary.actual_gross_minutes_of_crushing))/60));
                    ws.Cell("D48").Value = dailyAnalys.cane_crushed;
                    ws.Cell("E48").Value = Math.Round(Convert.ToDouble(ledgerPeriodSummary.cane_crushed/ledgerPeriodSummary.days_count),2);

                    ws.Range("A49", "I49").Value = "Efficiency Data";
                    ws.Range("A49", "I49").Merge().Style.Font.SetBold().Font.FontSize = 11;
                    

                    ws.Cell("A50").Value = 40;
                    ws.Cell("B50").Value = "Bagasse Baled";
                    ws.Cell("C50").Value = "Qtls.";
                    ws.Cell("D50").Value = dailyAnalys.bagasse_baed;
                    ws.Cell("E50").Value = dailyAnalysesSummary.bagasse_baed;


                    ws.Cell("A51").Value = 41;
                    ws.Cell("B51").Value = "Bagasses Consumed";
                    ws.Cell("C51").Value = "Qtls.";
                    ws.Cell("D51").Value = dailyAnalys.bagasse_consumed_qtl;
                    ws.Cell("E51").Value = dailyAnalysesSummary.bagasse_consumed;


                    ws.Cell("A52").Value = 42;
                    ws.Cell("B52").Value = "Total Bagasse Sold";
                    ws.Cell("C52").Value = "Qtls.";
                    ws.Cell("D52").Value = dailyAnalys.bagasse_sold;
                    ws.Cell("E52").Value = dailyAnalysesSummary.bagasse_sold;


                    ws.Cell("A53").Value = 43;
                    ws.Cell("B53").Value = "Power Generation Co-Gen";
                    ws.Cell("C53").Value = "KWH.";
                    ws.Cell("D53").Value = dailyAnalys.power_generation_from_coGen;
                    ws.Cell("E53").Value = dailyAnalysesSummary.power_generation_from_congen;


                    ws.Cell("A54").Value = 44;
                    ws.Cell("B54").Value = "Power Export from Co-Gen";
                    ws.Cell("C54").Value = "KWH";
                    ws.Cell("D54").Value = dailyAnalys.power_export_grid;
                    ws.Cell("E54").Value = dailyAnalysesSummary.power_export_grid;


                    ws.Cell("A55").Value = 45;
                    ws.Cell("B55").Value = "Steam per 100 Ton Sugar Cane";
                    ws.Cell("C55").Value = "%";
                    ws.Cell("D55").Value = dailyAnalys.steam_per_ton_cane;
                    ws.Cell("E55").Value = dailyAnalysesSummary.steam_per_ton_cane;

                    ws.Cell("A56").Value = 46;
                    ws.Cell("B56").Value = "Steam per 10 ton Sugar";
                    ws.Cell("C56").Value = "%";
                    ws.Cell("D56").Value = dailyAnalys.steam_per_qtl_sugar;
                    ws.Cell("E56").Value = dailyAnalysesSummary.steam_per_ten_ton_sugar;

                    ws.Cell("A57").Value = 47;
                    ws.Cell("B57").Value = "Power per 100 Ton Cane";
                    ws.Cell("C57").Value = "%";
                    ws.Cell("D57").Value = dailyAnalys.power_per_ton_cane;
                    ws.Cell("E57").Value = dailyAnalysesSummary.power_per_ton_cane;

                    ws.Cell("A58").Value = 48;
                    ws.Cell("B58").Value = "Power per 10 Ton Sugar";
                    ws.Cell("C58").Value = "%";
                    ws.Cell("D58").Value = dailyAnalys.power_per_qtl_sugar;
                    ws.Cell("E58").Value = dailyAnalysesSummary.power_per_ten_ton_sugar;

                    //string stoppageRemarks = "No Cane " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.NoCaneGrossDuration)) +Environment.NewLine+
                    //    " Mechanical - " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.MechanicalGrossDuration)) + Environment.NewLine +
                    //    " General Cleaning " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.GeneralCleaningGrossDuration)) + Environment.NewLine +
                    //    " Festival " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.FestivalGrossDuration)) + Environment.NewLine +
                    //    " Weather " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.WeatherGrossDuration)) + Environment.NewLine +
                    //    " Process " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.ProcessGrossDuration)) + Environment.NewLine +
                    //    " Miscellaneous " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.MiscellaneousGrossDuration)) + Environment.NewLine +
                    //    " Unknown " + ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.UnknownGrossDuration));

                    //ws.Range("A59", "I66").Value = stoppageRemarks;
                    ws.Range("A59", "I66").Merge();
                    ws.Range("A59", "I66").Style.Alignment.WrapText = true;

                    #region Cell formats
                    ws.Range("D10", "E15").Style.NumberFormat.NumberFormatId = 2;
                    ws.Range("D17", "E37").Style.NumberFormat.NumberFormatId = 2;
                    ws.Range("D39", "E43").Style.NumberFormat.NumberFormatId = 2;
                    ws.Range("D45", "E47").Style.DateFormat.NumberFormatId = 20;
                    ws.Range("D48", "E48").Style.NumberFormat.NumberFormatId = 1;
                    ws.Range("D50", "E52").Style.NumberFormat.NumberFormatId = 2;
                    ws.Range("D55", "E58").Style.NumberFormat.NumberFormatId = 2;
                    #endregion

                    #region Page Printing Setup
                    ws.PageSetup.Margins.Top = 0.01;
                    ws.PageSetup.Margins.Bottom = 0.01;
                    ws.PageSetup.Margins.Left = 0.01;
                    ws.PageSetup.Margins.Right = 0.01;
                    ws.PageSetup.FitToPages(1, 1);
                    ws.PageSetup.PaperSize = XLPaperSize.A4Paper;
                    
                    #endregion

                    #region Cell Designs
                    ws.Range("A1", "I7").Style.Font.Bold = true;
                    ws.Columns(8, 59).AdjustToContents();
                    //ws.Column(6).Width = 10;
                    //ws.Column(7).Width = 10;
                    //ws.Column(8).Width = 10;
                    //ws.Column(9).Width = 10;
                    ws.Columns(2,2).Width = 25;
                    ws.Range("A2", "I58").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1", "I62").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range("A2", "I58").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range("A2", "I58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;



                    #endregion

                    // setting border for the worksheet


                    // unprotected range and cells
                    ws.Range("F1", "I59").Style.Protection.SetLocked(false);
         
                    // cell width adjust to respective content
                    

                    wb.SaveAs(filePath);
                    return await Task.FromResult(true);
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            };
            
        }

        private string ConvertMinutesToHours(decimal minutes)
        {
            //int x = Convert.ToDateTime(minutes / 60).Hour;

            decimal hours = minutes / 60;
            int min = Convert.ToInt32(minutes % 60);
            return Math.Truncate(hours).ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0');
        }
    }
   
}
