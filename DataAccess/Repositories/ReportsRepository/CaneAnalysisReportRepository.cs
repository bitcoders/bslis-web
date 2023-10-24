using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace DataAccess.Repositories.ReportsRepository
{
    public class CaneAnalysisReportRepository
    {
        public string ExcelReport_CaneAnalysisBySampleDate(int unitCode, int seasonCode, DateTime sampleDate, string filePath)
        {
            string path = "";
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    List<proc_rpt_cane_analysis_by_sample_date_Result> result = new List<proc_rpt_cane_analysis_by_sample_date_Result>();
                    result = Db.proc_rpt_cane_analysis_by_sample_date(unitCode, seasonCode, sampleDate).ToList();
                    if(result.Count > 0)
                    {
                      path =  GenerateExcelReport(sampleDate, filePath, result);
                    }
                }catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return path;
        }
        public string GenerateExcelReport(DateTime sampleDate, string filePath, List<proc_rpt_cane_analysis_by_sample_date_Result> data)
        {

            /// in the end of file creation (before saving the file) 3 columns are deleted.
            /// Initially content in file is starting from cell D1 but after file generation it is visible 
            /// in cell A1 because of those deleted columns.
            /// code accordingly.
            string fileName = "Cane Analysis -" + sampleDate.ToString("yyyy-MMM-dd") + ".xlsx";
            string path = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();

            XLWorkbook wb = new XLWorkbook();
            IXLWorksheet sheet = wb.Worksheets.Add("Report - " + DateTime.Now.ToString("dd-MMM-yyyy"));

            sheet.ShowGridLines = false;

            sheet.Row(6).Height = 105;
            sheet.Row(6).Style.Alignment.WrapText = true;
            sheet.Row(6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            sheet.Range("D1", "V1").Merge();
            sheet.Cell("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Cell("D1").Style.Font.Bold = true;
            sheet.Cell("D2").Value = "Pre - Starting Cane Analysis Report for the Season 2021-22";
            sheet.Cell("D2").Style.Font.Bold = true;
            sheet.Cell("D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Cell("S2").Value = "Date of Sampling - ";
            sheet.Cell("T2").Value = sampleDate.ToString();
            sheet.Cell("T2").Style.NumberFormat.Format = "dd-MMM-yyyy";
            sheet.Cell("S3").Value = "Date of Analysis - ";
            sheet.Cell("T3").Value = sampleDate;
            sheet.Cell("T3").Style.NumberFormat.Format = "dd-MMM-yyyy";
            sheet.Range("D2", "V2").Merge();

            //====================== code for writing header =======================================
            List<string> headers = new List<string>()
            {
                "Sr. No",
                "Grower's Name",
                "Father's Name",
                "Village",
                "Variety",
                "Gate/Center",
                "Land Position",
                "Field Condition",
                "Crop Type",
                "Juice %",
                "Brix",
                "Pol",
                "Purity",
                "Pol in Cane Last Week",
                "Pol in Cane Today",
                "Rec. % Last Week (By W.Crop Formula)",
                "Rec % Today (By W.Carp Formula) ",
                "Rec % Today (By Assuming Mol pty. 30  & Ext. 65 %) ",
                "Prev. Season Cane Crop Harvesting Date"

            };

            char startCell = 'D';
            int currentCellAscii = (int)startCell;
            for (int i= 0; i< headers.Count; i++)
            {
                sheet.Cell((char)currentCellAscii + "6").Value = headers[i].ToString();
                sheet.Cell((char)currentCellAscii + "6").Style.Font.Bold = true;
                sheet.Cell((char)currentCellAscii + "6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                currentCellAscii++;
                
            }

            //======================= Code for writing header of the report ends here ===============

            //===================== code for writing analysis wise data ============================
            int dataStartRowNum = 7;
            int currentRowNum = 7;
            int gridDataLastRow = 7;
            if (data != null)
            {
                sheet.Cell("D1").Value = data.Select(u => u.unitName).First();
                
                sheet.Cell("H4").Value = "Report No -"+ data.Select(d => d.ReportNo).First();
                foreach(var i in data)
                {
                        sheet.Cell("D" + currentRowNum).Value = currentRowNum - 6;
                        sheet.Cell("E"+ currentRowNum.ToString()).Value = i.grower_name.ToString();
                        sheet.Cell("F" + currentRowNum.ToString()).Value = i.father_name.ToString();
                        sheet.Cell("G" + currentRowNum.ToString()).Value = i.village_name.ToString();
                        sheet.Cell("H" + currentRowNum.ToString()).Value = i.variety_name.ToString();
                        sheet.Cell("I" + currentRowNum.ToString()).Value = i.zone_name.ToString();
                        sheet.Cell("J" + currentRowNum.ToString()).Value = i.LandPosition.ToString();
                        sheet.Cell("K" + currentRowNum.ToString()).Value = i.FieldCondition.ToString();
                        sheet.Cell("L" + currentRowNum.ToString()).Value = i.CaneType;
                        sheet.Cell("M" + currentRowNum.ToString()).Value = i.JuicePercent.ToString();
                        sheet.Cell("N" + currentRowNum.ToString()).Value = i.Brix.ToString();
                        sheet.Cell("O" + currentRowNum.ToString()).Value = i.Pol.ToString();
                        sheet.Cell("P" + currentRowNum.ToString()).Value = i.Purity.ToString();
                        sheet.Cell("R" + currentRowNum.ToString()).Value = i.PolInCaneToday.ToString();
                        sheet.Cell("T" + currentRowNum.ToString()).Value = i.RecoveryByWCapToday.ToString();
                        sheet.Cell("U" + currentRowNum.ToString()).Value = i.RecoveryByMolPurityToday.ToString();
                    currentRowNum++;
                }
                gridDataLastRow = currentRowNum-1; // setting a value gridDataLastRow, it will hold value of last row number of data grid which will be used to calculate average and sum using row range eg "E8:E10"
                sheet.Range("E" + currentRowNum + ":L" + currentRowNum).Merge();
                sheet.Cells("E" + currentRowNum).Value = "---Average---";
                sheet.Range("E" + currentRowNum,"V"+currentRowNum).Style.Font.Bold = true;
                sheet.Cells("E" + currentRowNum).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // juice % average
                sheet.Cells("M" + currentRowNum).FormulaA1 = "Average(M" + dataStartRowNum + ":M" + (gridDataLastRow) + ")"; 
                //Brix avg
                sheet.Cells("N" + currentRowNum).FormulaA1 = "Average(N" + dataStartRowNum + ":N" + (gridDataLastRow) + ")";
                //pol avg
                sheet.Cells("O" + currentRowNum).FormulaA1 = "Average(O" + dataStartRowNum + ":O" + (gridDataLastRow) + ")";
                
                // purity avg
                sheet.Cells("P" + currentRowNum).FormulaA1 = "IF(N" + currentRowNum + " > 0,Round(O" + currentRowNum + "/N" + currentRowNum + "*100,2),0)";
                sheet.Cells("R" + currentRowNum).FormulaA1 = "Round(O" + currentRowNum + "*0.72,2)";

                // T Rec % Today (By W.Carp Formula) average
                sheet.Cell("T" + currentRowNum).FormulaA1 = "ROUND((O"+currentRowNum+"-((N" + currentRowNum + "-O" + currentRowNum +")*0.54))*(M" + currentRowNum +"/ 100),2)";
                // U Rec % Today (By Assuming Mol pty. 30  & Ext. 65 %)  average
                sheet.Cell("U" + currentRowNum).FormulaA1 = "ROUND((O11-((N11-O11)*0.43))*0.65,2)";


                sheet.Range("E" + currentRowNum, "U" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("E" + currentRowNum, "U" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("E" + currentRowNum, "U" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("E" + currentRowNum, "U" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                // Adding an empty row after grid
                currentRowNum = currentRowNum + 1;

             
                sheet.Cell("M" + currentRowNum).Value = "Meteorological data on Dt.=";
                sheet.Cell("P" + currentRowNum).Value = sampleDate.ToString("dd-MMM-yyyy");
                sheet.Cell("P" + currentRowNum).Style.NumberFormat.Format = "dd-MMM-yyyy";

                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                currentRowNum = currentRowNum + 1;
                sheet.Cell("H" + currentRowNum).Value = "Particular";
                sheet.Cell("I" + currentRowNum).Value = "Rec %";
                sheet.Cell("J" + currentRowNum).Value = "Brix";
                sheet.Cell("K" + currentRowNum).Value = "Pty";

                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                sheet.Cell("M" + currentRowNum).Value = "Maximum Tempreture";
                sheet.Cell("P" + currentRowNum).Value = data.Select(t => t.MaxTemperature).First();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Merge();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Style.Font.Bold = true;

                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                // Adding an empty row after grid
                currentRowNum = currentRowNum + 1;
                sheet.Cell("H" + currentRowNum).Value = "Minumum";
                sheet.Cell("I" + currentRowNum).FormulaA1 = "MIN(U" + dataStartRowNum + ":U" + gridDataLastRow + ")";
                sheet.Cell("J" + currentRowNum).FormulaA1 = "MIN(N" + dataStartRowNum + ":N" + gridDataLastRow + ")";
                sheet.Cell("K" + currentRowNum).FormulaA1 = "MIN(P" + dataStartRowNum + ":P" + gridDataLastRow + ")";

                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                sheet.Cell("M" + currentRowNum).Value = "Minimum Tempreture";
                sheet.Cell("P" + currentRowNum).Value = data.Select(t=>t.MinTemperature).First();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Merge();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                // Adding an empty row after grid
                currentRowNum = currentRowNum + 1;
                sheet.Cell("H" + currentRowNum).Value = "Maximum";
                sheet.Cell("I" + currentRowNum).FormulaA1 = "Max(U" + dataStartRowNum + ":U" + gridDataLastRow + ")";
                sheet.Cell("J" + currentRowNum).FormulaA1 = "Max(N" + dataStartRowNum + ":N" + gridDataLastRow + ")";
                sheet.Cell("K" + currentRowNum).FormulaA1 = "Max(P" + dataStartRowNum + ":P" + gridDataLastRow + ")";

                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                sheet.Cell("M" + currentRowNum).Value = "RTD value";
                sheet.Cell("P" + currentRowNum).Value = data.Select(t => t.RtdValue).First();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Merge();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                // Adding an empty row after grid
                currentRowNum = currentRowNum + 1;
                sheet.Cell("H" + currentRowNum).Value = "Average";
                sheet.Cell("I" + currentRowNum).FormulaA1 = "=U" + (gridDataLastRow + 1);
                sheet.Cell("J" + currentRowNum).FormulaA1 = "=N" + (gridDataLastRow + 1);
                sheet.Cell("K" + currentRowNum).FormulaA1 = "=P" + (gridDataLastRow + 1);
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("H" + currentRowNum, "K" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                sheet.Cell("M" + currentRowNum).Value = "Humidity";
                sheet.Cell("P" + currentRowNum).Value = data.Select(t => t.Humidity).First();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Merge();
                sheet.Range("M" + currentRowNum + ":O" + currentRowNum).Style.Font.Bold = true;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("M" + currentRowNum, "P" + currentRowNum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                currentRowNum = currentRowNum + 1;
                sheet.Cell("F" + currentRowNum).Value = "Lab Head";
                sheet.Cell("T" + currentRowNum).Value = "Executive President";
            }
            //===================== code for writing analysis wise data ends here ============================


            ///Border styling start here
            
            sheet.Range("D6", "V" + gridDataLastRow).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            sheet.Range("D6", "V" + gridDataLastRow).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            sheet.Range("D6", "V" + gridDataLastRow).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Range("D6", "V" + gridDataLastRow).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


            /// border styling ends here


            sheet.Columns("A", "C").Delete();
            
            wb.SaveAs(path);
            return path;
        }
    }

    
}
