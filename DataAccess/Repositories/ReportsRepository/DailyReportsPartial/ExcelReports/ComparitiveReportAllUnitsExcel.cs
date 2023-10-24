using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using DataAccess.Repositories.AnalysisRepositories;
using System.Collections;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class ComparitiveReportAllUnitsExcel
    {
        public  string ExcelReportFile(int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            //ComparitiveReportAllUnitsExcel groupComparitive = new ComparitiveReportAllUnitsExcel();
            GenerateExcel(seasonCode, ReportDate, filePath);
            return filePath;
        }
        private bool GenerateExcel( int seasonCode, DateTime ReportDate, string filePath)
        {
            List<proc_report_comparitive_Result> reportData = new List<proc_report_comparitive_Result>();
            MasterUnitRepository unitRepository = new MasterUnitRepository();
            List<MasterUnit> masterUnitList = new List<MasterUnit>();
            masterUnitList = unitRepository.GetActiveMasterUnitList();
            
            
            #region Creating left side item names List
            List<string> LeftItemNames = new List<string>()
            {
                "Actual Hrs. of Crushing",
                "Cane Crushed (Qtls.)",
                "Recovery % Cane",
                "Bagasse % Cane",
                "Molasses % Cane",
                "Press Cake % Cane",
                "Pol % Cane",
                "Fiber % Cane",
                "Sugar Production (Qtls.)",
                "Molasses Sent Out",
                "Nett Sugar made",
                "Nett Molasses made",
                "Bags ( + -)",
                "Molasses ( + -)",
                "Sugar In Process",
                "Molasses In Process",
                "Jawa ratio",
                "D.M.F.",
                "Pol % Press Cake",
                "Pol % Bagasse",
                "Moisture % Bagasse",
                "Final Molasses Purity",
                "Primary Juice Brix %",
                "Primary Juice Purity",
                "Mixed Juice Brix %",
                "Mixed Juice Purity",
                "P.J. - M.J. Purity",
                "Mixed Juice % Cane",
                "Added Water % Fiber",
                "Undiluted jc% cane",
                "Losses",
                "Loss in Bagasse",
                "Loss in Fil.Cake",
                "Loss in Molasses",
                "Unknown Losses",
                "Total",
                "Stoppages",
                "New Mill",
                "Old Mill",
                "Total Both Mill",
                "Crushing Speed per day",
                "Efficacy Data",
                "Bagasse Baled",
                "Bagasse Consumed",
                "Total Bagasse sold ",
                "Power Generation",
                "Power Export",
                "Steam per 100 ton cane",
                "Steam per 10 ton Sugar",
                "Power per 100 ton cane",
                "Power per 10 ton Sugar",
            };
            #endregion

            #region creating list of UOM based on Left Side Item List   
            List<string> uomForGeneral = new List<string>()
            {
                "Hrs - Min",
                    "Qtls.",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "Qtls.",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "%",
                    "Unit",
                    "%",
                    "%",
                    "%"
            };
            List<string> uomForLosses = new List<string>()
            {
                "%",
                "%",
                "%",
                "%",
                "%",

            };
            List<string> uomForStoppages = new List<string>()
            {
                "Hrs - Min %",
                "Hrs - Min %",
                "Hrs - Min %",
                "Qtls.",

            };
            List<string> uomForEfficiencyData = new List<string>()
            {
                "Qtls.",
                "Qtls.",
                "Qtls.",
                "KWH",
                "KWH",
                "%",
                "%",
                "%",
                "%"
            };

            #endregion

            using (SugarLabEntities Db = new SugarLabEntities())
            {
                foreach(var unitCode in masterUnitList)
                {
                    proc_report_comparitive_Result temp = new proc_report_comparitive_Result();
                    try
                    {
                        temp = Db.proc_report_comparitive(Convert.ToInt32(unitCode.Code), Convert.ToInt32(seasonCode), ReportDate).FirstOrDefault();
                        if(temp == null)
                        {
                            proc_report_comparitive_Result emptyTemp = new proc_report_comparitive_Result()
                            {
                                od_crushing_hrs = "0",
                                td_crushing_hrs = "0",
                                trans_date = unitCode.CrushingEndDate,
                                Name = unitCode.Name,
                                od_cane_crushed = 0,
                                td_cane_crushed = 0,
                                od_estimated_sugar_percent_cane = 0,
                                td_estimated_sugar_percent_cane = 0,
                                od_bagasse_percent_cane = 0,
                                td_bagasse_percent_cane = 0,
                                od_estimated_molasses_percent_cane = 0,
                                td_estimated_molasses_percent_cane = 0,
                                od_press_cake_percent_cane = 0,
                                td_press_cake_percent_cane = 0,
                                od_pol_percent_cane = 0,
                                td_pol_percent_cane = 0,
                                od_fiber_percent_cane = 0,
                                td_fiber_percent_cane = 0,
                                od_total_sugar_bags = 0,
                                td_total_sugar_bags = 0,
                                od_final_molasses_sent_out = 0,
                                td_final_molasses_sent_out = 0,
                                od_estimated_sugar_qtl = 0,
                                td_estimated_sugar_qtl = 0,
                                od_estimated_molasses_qtl = 0,
                                td_estimated_molasses_qtl = 0,
                                od_bags_plus_minus = 0,
                                td_bags_plus_minus = 0,
                                od_molasses_plus_minus = 0,
                                td_molasses_plus_minus = 0,
                                od_sugar_in_process_qtl = 0,
                                td_sugar_in_process_qtl = 0,
                                od_molasses_in_process = 0,
                                td_molasses_in_porcess = 0,
                                od_java_ration = 0,
                                td_java_ratio = 0,
                                od_dry_mill_factor_percent_cane = 0,
                                td_dry_mill_factor_percent_cane = 0,
                                od_pol_in_press_percent_cane = 0,
                                td_pol_in_press_percent_cane = 0,
                                od_pol_in_bagasse_percent_cane = 0,
                                td_pol_in_bagasse_percent_cane = 0,
                                od_bagasse_moisture_percent = 0,
                                td_bagasse_moisture_percent = 0,
                                od_final_molasses_purity = 0,
                                td_final_molasses_purity = 0,
                                od_pj_brix = 0,
                                td_pj_brix = 0,
                                od_pj_purity = 0,
                                td_pj_purity = 0,
                                od_mj_brix = 0,
                                td_mj_brix = 0,
                                od_mj_purity = 0,
                                td_mj_purity = 0,
                                od_pj_mj_purity = 0,
                                td_pj_mj_purity = 0,
                                od_net_mixed_juice_percent_cane = 0,
                                td_net_mixed_juice_percent_cane = 0,
                                od_added_water_percent_fiber = 0,
                                td_added_water_percent_fiber = 0,
                                od_undiulted_juice_percent_cane = 0,
                                td_undiulted_juice_percent_cane = 0,
                                od_pol_in_bagasse = 0,
                                td_pol_in_bagasse = 0,
                                od_pol_in_press_cake = 0,
                                td_pol_in_press_cake = 0,
                                od_pol_in_molasses_percent_cane = 0,
                                td_pol_in_molasses_percent_cane = 0,
                                od_unknown_loss_percent_cane = 0,
                                td_unknown_loss_percent_cane = 0,
                                od_total_loss_percent = 0,
                                td_total_loss_percent = 0,
                                od_nm_stoppage = "0",
                                td_nm_stoppage = "0",
                                od_om_stoppage = "0",
                                td_om_stoppage = "0",
                                od_total_stoppage = "0",
                                td_total_stoppage = "0",
                                od_crushing_speed = 0,
                                td_crushing_speed = 0,
                                od_bagasse_baed = 0,
                                td_bagasse_baed = 0,
                                od_bagasse_consumed_qtl = 0,
                                td_bagasse_consumed_qtl = 0,
                                od_bagasse_sold = 0,
                                td_bagasse_sold = 0,
                                od_power_generation_cogen = 0,
                                td_power_generation_from_cogen = 0,
                                od_power_to_grid = 0,
                                td_power_to_grid = 0,
                                od_steam_per_ton_cane = 0,
                                td_steam_per_ton_cane = 0,
                                od_steam_per_ten_ton_sugar = 0,
                                td_steam_per_ten_ton_sugar = 0,
                                od_power_per_hundred_ton_sugar = 0,
                                td_power_per_hundred_ton_sugar = 0,
                                od_power_per_ten_ton_sugar = 0,
                                td_power_per_ten_ton_sugar = 0,
                            };

                            reportData.Add(emptyTemp);
                        }
                        else
                        {
                            reportData.Add(temp);
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        new Exception(ex.Message);
                    }
                    
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Properties.Author = "Birla Sugar Lab Information System";
                    wb.Properties.Company = "Birla Sugar & Industries Ltd.";
                    wb.Properties.Category = "Reports";
                    
                    var ws = wb.Worksheets.Add("Comparitive All Units");
                    //ws.Protect("Google@1234");
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.PaperSize = XLPaperSize.A4Paper;

                    
                    // page margin
                    ws.PageSetup.Margins.Top = ws.PageSetup.Margins.Bottom
                    = ws.PageSetup.Margins.Left = ws.PageSetup.Margins.Right
                    = ws.PageSetup.Margins.Header
                    = ws.PageSetup.Margins.Footer
                    = Convert.ToDouble(.10);

                    // page adjustment
                    ws.PageSetup.AdjustTo(61);
                    ws.PageSetup.CenterHorizontally = true;

                    ws.PageSetup.Margins.SetTop(0);
                    ws.PageSetup.Margins.SetRight(0);
                    ws.PageSetup.Margins.SetLeft(0);
                    ws.PageSetup.Margins.SetBottom(0);

                    ws.Range("A1", "Q2").Value = "Avadh & Magadh Sugar & Energy Ltd.";

                    ws.Range("A1", "Q2").Merge();
                    ws.Cell("A3").Value = "Daily Comparitive Report (All Sugar Units)"; // later get it from databse table
                    ws.Range("A3:Q3").Merge();
                    
                    ws.Range("A4:M4").Merge();
                    ws.Cell("N4").Value = "Report Compilation Date : " + ReportDate.ToLongDateString();
                    //ws.Cell("Q5").Style.DateFormat.Format = "dd-MMM-yyyy";
                    
                    //ws.Range("F5", "O5").Merge();


                    #region Cell Merging
                    ws.Range("A5", "A7").Merge();
                    ws.Range("B5", "B7").Merge();
                    ws.Range("C5", "C7").Merge();
                    ws.Range("D5", "E5").Merge();
                    ws.Range("F5", "G5").Merge();
                    ws.Range("H5", "I5").Merge();
                    ws.Range("J5", "K5").Merge();
                    ws.Range("L5", "M5").Merge();
                    ws.Range("N4", "Q4").Merge();
                    ws.Range("N5", "O5").Merge();
                    ws.Range("P5", "Q5").Merge();

                    ws.Range("B38", "Q38").Merge();
                    ws.Range("B44", "Q44").Merge();
                    ws.Range("B49", "Q49").Merge();
                    ws.Range("A59", "Q62").Merge();

                    #endregion

                    #region Font Styling

                    
                    ws.Range("A1", "Q7").Style.Font.Bold = true;
                    
                    ws.Range("B38", "Q38").Style.Font.Bold = true;
                    ws.Range("B44", "Q44").Style.Font.Bold = true;
                    
                    ws.Range("B49", "Q49").Style.Font.Bold = true;
                    ws.Range("A59", "Q62").Style.Font.Bold = true;
                    ws.Range("D8", "Q8").Style.DateFormat.Format = "HH:MM";
                    ws.Range("D10", "Q43").Style.NumberFormat.Format = "00.00";
                    ws.Range("D45", "Q47").Style.DateFormat.Format = "HH:MM";
                    
                    ws.Range("D50", "Q52").Style.NumberFormat.Format = "00.00";
                    ws.Range("D53", "Q54").Style.NumberFormat.Format = "00";
                    ws.Range("D55", "Q58").Style.NumberFormat.Format = "00.00";

                    #endregion

                    #region Width & Alignments
                    ws.Column(2).Width = 20;
                    ws.Column(3).Width = 9.44;
                    ws.Columns(4, 17).Width = 10.22;
                    ws.Row(5).Height = 27;
                    ws.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("A5", "Q5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Range("A8", "A58").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    ws.Range("A5", "Q7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("D8", "Q52").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("D53", "Q58").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Cell("N4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    
                    #endregion

                    #region Borders
                    ws.Range("A5", "Q62").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range("A5", "Q62").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range("A5", "Q62").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range("A5", "Q62").Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    ws.Range("A1", "Q4").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    ws.Range("A5", "Q7").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("A5", "A58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B5", "B58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("C5", "C58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("D5", "E58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("F5", "G58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("H5", "I58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("J5", "K58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("L5", "M58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("N5", "O58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("P5", "Q58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    //ws.Range("A1", "Q7").Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                    //ws.Range("A1", "Q7").Style.Border.RightBorder = XLBorderStyleValues.Thick;
                    //ws.Range("A1", "Q7").Style.Border.TopBorder = XLBorderStyleValues.Thick;
                    //ws.Range("A1", "Q7").Style.Border.BottomBorder = XLBorderStyleValues.Thick;

                    ws.Range("D8", "Q58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B38", "Q38").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B44", "Q44").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B44", "Q58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    ws.Range("B49", "Q49").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    ws.Range("B5","B58").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    ws.Range("A1", "Q62").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;


                    
                    //ws.Range("A20", "Q21").Style.Fill.BackgroundColor = XLColor.Red;
                    //ws.Range("A34", "Q34").Style.Fill.BackgroundColor = XLColor.Red;

                    #endregion
                    ws.Cell("A4").Value = "Crushing Season " + reportData.Where(x => x.UnitCode == 1).Select(x => x.season_year).FirstOrDefault();
                    /// using loop writing the item names in left side in excel sheet.
                    int leftPanelCellNum = 8;
                    int count = 1;
                    foreach(var item in LeftItemNames)
                    {
                        ws.Cell("A" + leftPanelCellNum.ToString()).Value = count;
                        ws.Cell("B" + leftPanelCellNum.ToString()).Value = item;
                        
                        leftPanelCellNum++;
                        count++;
                    }
                    leftPanelCellNum = 8;
                    foreach(var item in uomForGeneral)
                    {
                        ws.Cell("C" + leftPanelCellNum.ToString()).Value = item;
                        leftPanelCellNum++;
                    }
                    leftPanelCellNum = 39;
                    foreach (var item in uomForLosses)
                    {
                        ws.Cell("C" + leftPanelCellNum.ToString()).Value = item;
                        leftPanelCellNum++;
                    }

                    leftPanelCellNum = 45;
                    foreach (var item in uomForStoppages)
                    {
                        ws.Cell("C" + leftPanelCellNum.ToString()).Value = item;
                        leftPanelCellNum++;
                    }
                    leftPanelCellNum = 50;
                    foreach (var item in uomForEfficiencyData)
                    {
                        ws.Cell("C" + leftPanelCellNum.ToString()).Value = item;
                        leftPanelCellNum++;
                    }


                    var unitNames = reportData.Select(x => x.Name).ToList();
                    var cropDays = reportData.Select(x => x.days_count).ToList();
                    var trans_date = reportData.Select(x => x.trans_date.Value.ToString("dd-MM-yyyy")).ToList();
                    var od_crushing_hrs = reportData.Select(x => new { x.od_crushing_hrs, x.td_crushing_hrs }).ToList();
                    var crushing = reportData.Select(x => new { x.od_cane_crushed, x.td_cane_crushed }).ToList();
                    var estimated_sugar = reportData.Select(x => new { x.od_estimated_sugar_percent_cane, x.td_estimated_sugar_percent_cane }).ToList();
                    var bagasse_percent_cane = reportData.Select(x => new { x.od_bagasse_percent_cane, x.td_bagasse_percent_cane }).ToList();

                    var estimated_molasses_percent_cane = reportData.Select(x => new { x.od_estimated_molasses_percent_cane, x.td_estimated_molasses_percent_cane }).ToList();
                    var press_cake_percent_cane = reportData.Select(x => new { x.od_press_cake_percent_cane, x.td_press_cake_percent_cane }).ToList();
                    var pol_percent_cane = reportData.Select(x => new { x.od_pol_percent_cane, x.td_pol_percent_cane }).ToList();
                    var fiber_percent_cane = reportData.Select(x => new { x.od_fiber_percent_cane, x.td_fiber_percent_cane }).ToList();
                    var total_sugar_bags = reportData.Select(x => new { x.od_total_sugar_bags, x.td_total_sugar_bags }).ToList();
                    var final_molasses_sent_out = reportData.Select(x => new { x.od_final_molasses_sent_out, x.td_final_molasses_sent_out }).ToList();
                    var estimated_sugar_qtl = reportData.Select(x => new { x.od_estimated_sugar_qtl, x.td_estimated_sugar_qtl }).ToList();
                    var estimated_molasses_qtl = reportData.Select(x => new { x.od_estimated_molasses_qtl, x.td_estimated_molasses_qtl }).ToList();
                    var bags_plus_minus = reportData.Select(x => new { x.od_bags_plus_minus, x.td_bags_plus_minus }).ToList();
                    var molasses_plus_minus = reportData.Select(x => new { x.od_molasses_plus_minus, x.td_molasses_plus_minus }).ToList();
                    var sugar_in_process_qtl = reportData.Select(x => new { x.od_sugar_in_process_qtl, x.td_sugar_in_process_qtl }).ToList();
                    var molasses_in_process = reportData.Select(x => new { x.od_molasses_in_process, x.td_molasses_in_porcess }).ToList();
                    var java_ration = reportData.Select(x => new { x.od_java_ration, x.td_java_ratio }).ToList();
                    var dry_mill_factor_percent_cane = reportData.Select(x => new { x.od_dry_mill_factor_percent_cane, x.td_dry_mill_factor_percent_cane }).ToList();
                    var pol_in_press_percent_cane = reportData.Select(x => new { x.od_pol_in_press_percent_cane, x.td_pol_in_press_percent_cane }).ToList();
                    var pol_in_bagasse_percent_cane = reportData.Select(x => new { x.od_pol_in_bagasse_percent_cane, x.td_pol_in_bagasse_percent_cane }).ToList();
                    var bagasse_moisture_percent = reportData.Select(x => new { x.od_bagasse_moisture_percent, x.td_bagasse_moisture_percent }).ToList();
                    var final_molasses_purity = reportData.Select(x => new { x.od_final_molasses_purity, x.td_final_molasses_purity }).ToList();
                    var pj_brix = reportData.Select(x => new { x.od_pj_brix, x.td_pj_brix }).ToList();
                    var pj_purity = reportData.Select(x => new { x.od_pj_purity, x.td_pj_purity }).ToList();
                    var mj_brix = reportData.Select(x => new { x.od_mj_brix, x.td_mj_brix }).ToList();
                    var mj_purity = reportData.Select(x => new { x.od_mj_purity, x.td_mj_purity }).ToList();
                    var pj_mj_purity = reportData.Select(x => new { x.od_pj_mj_purity, x.td_pj_mj_purity }).ToList();
                    var net_mixed_juice_percent_cane = reportData.Select(x => new { x.od_net_mixed_juice_percent_cane, x.td_net_mixed_juice_percent_cane }).ToList();
                    var added_water_percent_fiber = reportData.Select(x => new { x.od_added_water_percent_fiber, x.td_added_water_percent_fiber }).ToList();
                    var undiaulted_juice_percent_cane = reportData.Select(x => new { x.od_undiulted_juice_percent_cane, x.td_undiulted_juice_percent_cane }).ToList();
                    var pol_in_bagasse = reportData.Select(x => new { x.od_pol_in_bagasse, x.td_pol_in_bagasse }).ToList();
                    var pol_in_press_cake = reportData.Select(x => new { x.od_pol_in_press_cake, x.td_pol_in_press_cake }).ToList();
                    var pol_in_molasses_percent_cane = reportData.Select(x => new { x.od_pol_in_molasses_percent_cane, x.td_pol_in_molasses_percent_cane }).ToList();
                    var unknown_loss_percent_cane = reportData.Select(x => new { x.od_unknown_loss_percent_cane, x.td_unknown_loss_percent_cane }).ToList();
                    var total_loss_percent = reportData.Select(x => new { x.od_total_loss_percent, x.td_total_loss_percent }).ToList();
                    var nm_stoppage = reportData.Select(x => new { x.od_nm_stoppage, x.td_nm_stoppage }).ToList();
                    var om_stoppage = reportData.Select(x => new { x.od_om_stoppage, x.td_om_stoppage }).ToList();
                    var total_stoppage = reportData.Select(x => new { x.od_total_stoppage, x.td_total_stoppage }).ToList();
                    var crushing_speed = reportData.Select(x => new { x.od_crushing_speed, x.td_crushing_speed }).ToList();
                    var bagasse_baed = reportData.Select(x => new { x.od_bagasse_baed, x.td_bagasse_baed }).ToList();
                    var bagasse_consumed_qtl = reportData.Select(x => new { x.od_bagasse_consumed_qtl, x.td_bagasse_consumed_qtl }).ToList();
                    var bagasse_sold = reportData.Select(x => new { x.od_bagasse_sold, x.td_bagasse_sold }).ToList();
                    var power_generation_cogen = reportData.Select(x => new { x.od_power_generation_cogen, x.td_power_generation_from_cogen }).ToList();
                    var power_to_grid = reportData.Select(x => new { x.od_power_to_grid, x.td_power_to_grid }).ToList();
                    var steam_per_ton_cane = reportData.Select(x => new { x.od_steam_per_ton_cane, x.td_steam_per_ton_cane }).ToList();
                    var steam_per_ten_ton_sugar = reportData.Select(x => new { x.od_steam_per_ten_ton_sugar, x.td_steam_per_ten_ton_sugar }).ToList();
                    var power_per_hundred_ton_sugar = reportData.Select(x => new { x.od_power_per_hundred_ton_sugar, x.td_power_per_hundred_ton_sugar }).ToList();
                    var power_per_ten_ton_sugar = reportData.Select(x => new { x.od_power_per_ten_ton_sugar, x.td_power_per_ten_ton_sugar }).ToList();


                    ws.Cell("A5").Value= "Sr.#";
                    ws.Cell("B5").Value= "Particulars";
                    ws.Cell("C5").Value = "Unit";


                    char cellAlphabets = 'D';
                    char cellAlphabetsToDate = 'E';
                    int cellAddress = 5;
                    int Ascii;
                    int NextAscii;

                    foreach(var x in unitNames)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;
                    }
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    cellAddress = 5;
                    
                    foreach (var x in trans_date)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = ws.Cell(cellAlphabets + cellAddress.ToString()).Value.ToString() +Environment.NewLine+ "("+x.ToString()+")";
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Style.Alignment.WrapText = true;
                    }
                    cellAddress = 6;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';

                    foreach (var x in cropDays)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = "Crop Day";
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 7;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';

                    foreach (var x in cropDays)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = "Day";
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = "To-Date";
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }


                    cellAddress = 8;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';

                    foreach (var x in od_crushing_hrs)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_crushing_hrs;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_crushing_hrs;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 9;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in crushing)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_cane_crushed;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_cane_crushed;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    cellAddress = 10;
                   
                    foreach (var x in estimated_sugar)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_estimated_sugar_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_estimated_sugar_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 11;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bagasse_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bagasse_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bagasse_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 12;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in estimated_molasses_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_estimated_molasses_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_estimated_molasses_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 12;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in estimated_molasses_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_estimated_molasses_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_estimated_molasses_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 13;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in press_cake_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_press_cake_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_press_cake_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 14;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 15;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in fiber_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_fiber_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_fiber_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 16;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in total_sugar_bags)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_total_sugar_bags;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_total_sugar_bags;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 17;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in final_molasses_sent_out)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_final_molasses_sent_out;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_final_molasses_sent_out;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 18;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in estimated_sugar_qtl)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_estimated_sugar_qtl;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_estimated_sugar_qtl;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 19;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in estimated_molasses_qtl)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_estimated_molasses_qtl;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_estimated_molasses_qtl;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 20;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bags_plus_minus)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bags_plus_minus;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bags_plus_minus;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 21;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in molasses_plus_minus)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_molasses_plus_minus;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_molasses_plus_minus;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 22;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in sugar_in_process_qtl)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_sugar_in_process_qtl;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_sugar_in_process_qtl;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 23;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in molasses_in_process)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_molasses_in_process;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_molasses_in_porcess;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 24;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in java_ration)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_java_ration;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_java_ratio;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 25;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in dry_mill_factor_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_dry_mill_factor_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_dry_mill_factor_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 26;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_in_press_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_in_press_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_in_press_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 27;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_in_bagasse_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_in_bagasse_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_in_bagasse_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 28;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bagasse_moisture_percent)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bagasse_moisture_percent;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bagasse_moisture_percent;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 29;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in final_molasses_purity)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_final_molasses_purity;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_final_molasses_purity;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 30;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pj_brix)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pj_brix;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pj_brix;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 31;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pj_purity)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pj_purity;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pj_purity;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 32;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in mj_brix)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_mj_brix;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_mj_brix;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }


                    cellAddress = 33;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in mj_purity)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_mj_purity;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_mj_purity;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 34;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pj_mj_purity)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pj_mj_purity;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pj_mj_purity;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 35;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in net_mixed_juice_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_net_mixed_juice_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_net_mixed_juice_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 36;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in added_water_percent_fiber)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_added_water_percent_fiber;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_added_water_percent_fiber;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 37;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in undiaulted_juice_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_undiulted_juice_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_undiulted_juice_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 39;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_in_bagasse)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_in_bagasse;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_in_bagasse;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }


                    cellAddress = 40;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_in_press_cake)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_in_press_cake;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_in_press_cake;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 41;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in pol_in_molasses_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_pol_in_molasses_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_pol_in_molasses_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 42;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in unknown_loss_percent_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_unknown_loss_percent_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_unknown_loss_percent_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 43;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in total_loss_percent)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_total_loss_percent;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_total_loss_percent;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 45;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in nm_stoppage)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_nm_stoppage;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_nm_stoppage;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 46;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in om_stoppage)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_om_stoppage;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_om_stoppage;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 47;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in total_stoppage)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_total_stoppage;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_total_stoppage;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 48;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in crushing_speed)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_crushing_speed;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_crushing_speed;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 50;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bagasse_baed)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bagasse_baed;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bagasse_baed;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 51;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bagasse_consumed_qtl)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bagasse_consumed_qtl;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bagasse_consumed_qtl;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 52;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in bagasse_sold)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_bagasse_sold;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_bagasse_sold;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 53;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in power_generation_cogen)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_power_generation_cogen;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_power_generation_from_cogen;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 54;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in power_to_grid)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_power_to_grid;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_power_to_grid;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 55;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in steam_per_ton_cane)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_steam_per_ton_cane;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_steam_per_ton_cane;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 56;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in steam_per_ten_ton_sugar)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_steam_per_ten_ton_sugar;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_steam_per_ten_ton_sugar;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    cellAddress = 57;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in power_per_hundred_ton_sugar)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_power_per_hundred_ton_sugar;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_power_per_hundred_ton_sugar;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }
                    cellAddress = 58;
                    cellAlphabets = 'D';
                    cellAlphabetsToDate = 'E';
                    foreach (var x in power_per_ten_ton_sugar)
                    {
                        ws.Cell(cellAlphabets + cellAddress.ToString()).Value = x.od_power_per_ten_ton_sugar;
                        Ascii = (int)cellAlphabets;
                        NextAscii = Ascii + 2;
                        cellAlphabets = (char)NextAscii;

                        ws.Cell(cellAlphabetsToDate + cellAddress.ToString()).Value = x.td_power_per_ten_ton_sugar;
                        Ascii = (int)cellAlphabetsToDate;
                        NextAscii = Ascii + 2;
                        cellAlphabetsToDate = (char)NextAscii;
                    }

                    ws.Cell("A59").Value = "* The report contains data of the last processed/finalized date of each unit only.";
                    ws.Cell("A64").Value = "Lab Head (ASEL Seohara)";
                    wb.SaveAs(filePath);
                    return true;
                }
            }
            

            
        }
    }
}
