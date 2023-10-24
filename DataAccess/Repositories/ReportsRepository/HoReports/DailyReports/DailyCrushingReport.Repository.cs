using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CustomModels.Reports;
using DataAccess.Repositories.ReportsRepository.HoReports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.IO;

namespace DataAccess.Repositories.ReportsRepository.HoReports.DailyReports
{
    public class DailyCrushingReport
    {
        public DailyCrushReportHoModel DailyCrushReport(ReportParameterModel param)
        {
            DailyCrushReportHoModel data = new DailyCrushReportHoModel();

            if (param == null)
            {
                return data;
            }
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                
                proc_summarized_report_Result d = new proc_summarized_report_Result();
                d = Db.proc_summarized_report(param.UnitCode, param.SeasonCode, param.ReportDate, false).First();
                
                if(d != null)
                {
                    DailyCrushReportHoModel temp = new DailyCrushReportHoModel()
                    {
                        UnitCode = param.UnitCode,
                        ReportDate = param.ReportDate,
                        NewMillHourWorked = d.od_nm_gross_working_duration,
                        OldMillHourWorked = d.od_om_gross_working_duration,
                        TotalCaneCrushedOnDate = d.od_cane_crushed,
                        TotalCaneCrushedToDate = d.td_cane_crushed,
                        CaneDivertedForEthonol = d.od_cane_used_for_syrup,
                        ActualRecoveryOnDate = d.od_estimated_sugar_percent_cane,
                        ActualRecoveryToDate = d.td_estimated_sugar_percent_cane,
                        RecoveryOnSyrup = d.od_diverted_syrup_percent_cane,
                        RecoveryOnBHeavy = d.od_estimated_sugar_percent_cane,
                        RecoveryOnCHeavy = d.od_estimated_sugar_percent_on_c_heavy,
                        MolassesPurity = d.od_final_molasses_purity,
                        NewMillBagassePol = d.od_nm_bagasse_pol_avg,
                        OldMillBagassePol = d.od_om_bagasse_pol_avg,
                        TotalLoss = d.od_total_loss,
                        MolassesPercentCane = d.od_molasses_percent_cane,
                        PrimaryJuiceBrix = d.od_primary_juice_brix,
                        PrimaryJuicePurity = d.od_primary_juice_purity,
                        PolInCane = d.od_pol_in_cane_percent,
                        FiberPercentCane = d.od_fiber_percent_cane,
                        WhiteSugarProducedOnDate = d.od_sugar_bagged_without_raw_sugar,
                        WhiteSugarProducedToDate = d.td_sugar_bagged_without_raw_sugar,
                        RawSugarProducedOnDate = d.od_raw_sugar,
                        RawSugarProducedToDate = d.td_raw_sugar,
                        RecoveryOnDatePreviousYear = 0,
                        IcumsaL31 = d.od_icumsa_l31,
                        IcumsaL30 = d.od_icumsa_l30,
                        IcumsaM31 = d.od_icumsa_M31,
                        IcumsaM30 = d.od_icumsa_M30,
                        IcumsaS31 = d.od_icumsa_S31,
                        IcumsaS30 = d.od_icumsa_S30,
                        IcumsaBiss = 0,
                        PowerExportSugarOnDate = d.od_power_from_sugar,
                        PowerExportDistilleryOnDate = 0,
                        PowerExportMonth = 0,
                        PowerExportYear = 0,
                        CaneEarlyOnDate = d.od_cane_early,
                        CaneEarlyToDate = d.td_cane_early,
                        CaneRejectedOnDate = d.od_cane_reject,
                        CaneRejectedToDate = d.td_cane_reject,
                        CaneBadOnDate = 0,
                        CaneBadToDate = 0

                    };
                    return temp;
                }
            }
            return data;
        }

        public string GeneratePdf(string path, ReportParameterModel parameterModel)
        {
            string fileName = "CrushingReport -"+DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".pdf";
            path = path + fileName;
            FileInfo file = new FileInfo(path);
            file.Directory.Create();
            
            List<DailyCrushReportHoModel> data = new List<DailyCrushReportHoModel>();
            for(int i = 1; i<= 7; i++)
            {
                parameterModel.UnitCode = i;
                var x = DailyCrushReport(parameterModel);
                data.Add(x);
            }
            
            var seohara_analysis = data.Where(x => x.UnitCode == 1).First();
            var rosa_analysis = data.Where(x => x.UnitCode == 2).First();
            var har_analysis = data.Where(x => x.UnitCode == 3).First();
            var hata_analysis = data.Where(x => x.UnitCode == 4).First();
            var nar_analysis = data.Where(x => x.UnitCode == 5).First();
            var sid_analysis = data.Where(x => x.UnitCode == 6).First();
            var has_analysis = data.Where(x => x.UnitCode == 7).First();




            FileStream fs = new FileStream(path, FileMode.Create);
            Document doc = new Document(PageSize.A4.Rotate(),-2,-2,-2,-2);
            PdfWriter wr = PdfWriter.GetInstance(doc, fs);

            Font HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 11f, Font.NORMAL, BaseColor.BLUE);
            Font bodyHeader = new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD, BaseColor.BLACK);
            Font bodyContent = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.NORMAL, BaseColor.BLACK);

            doc.AddCreationDate();
            doc.Open();

            PdfPTable table = new PdfPTable(9);
            table.TotalWidth = 825f;
            table.DefaultCell.Border = 0;
            table.LockedWidth = true;
            float[] widths = new float[] { 120f, 65f, 75, 75, 75, 75, 75, 75, 75 };
            table.SetWidths(widths);
            
            PdfPCell headerOneReport = new PdfPCell(new Paragraph("Crushing Report For - " + seohara_analysis.ReportDate.ToShortDateString(), HeaderFont));
            headerOneReport.Colspan = 9;
            table.AddCell(headerOneReport);
            PdfPCell headerTwoBlank = new PdfPCell(new Paragraph(""));
            headerTwoBlank.Colspan = 2;
            table.AddCell(headerTwoBlank);
            PdfPCell[] headerUnits = new PdfPCell[]
            {
                new PdfPCell(new Phrase("SEOHARA",bodyContent)),
                new PdfPCell(new Phrase("HARGAON",bodyContent)),
                new PdfPCell(new Phrase("ROSA",bodyContent)),
                new PdfPCell(new Phrase("HATA",bodyContent)),
                new PdfPCell(new Phrase("NARKATIAGANJ",bodyContent)),
                new PdfPCell(new Phrase("SIDHWALIA",bodyContent)),
                new PdfPCell(new Phrase("HASANPUR",bodyContent)),
            };
            
            foreach(PdfPCell d in headerUnits)
            {
                d.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(d);
            }

            PdfPCell[] lineOne = new PdfPCell[]
            {
                new PdfPCell(new Phrase("Hour Worked",bodyContent)),
                new PdfPCell(new Phrase("New Mill",bodyContent)),
                new PdfPCell(new Phrase(seohara_analysis.NewMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(har_analysis.NewMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(rosa_analysis.NewMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(hata_analysis.NewMillHourWorked,bodyContent)),

                new PdfPCell(new Phrase(nar_analysis.NewMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(sid_analysis.NewMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(has_analysis.NewMillHourWorked,bodyContent))
            };

            foreach(PdfPCell d in lineOne)
            {
                d.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(d);
            }

            PdfPCell[] lineTwo = new PdfPCell[]
            {
                new PdfPCell(new Phrase("Hour Worked",bodyContent)),
                new PdfPCell(new Phrase("Old Mill",bodyContent)),
                new PdfPCell(new Phrase(seohara_analysis.OldMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(har_analysis.OldMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(rosa_analysis.OldMillHourWorked,bodyContent)),
                
                new PdfPCell(new Phrase(hata_analysis.OldMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(nar_analysis.OldMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(sid_analysis.OldMillHourWorked,bodyContent)),
                new PdfPCell(new Phrase(has_analysis.OldMillHourWorked,bodyContent))
            };
            foreach (PdfPCell d in lineTwo)
            {
                d.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(d);
            }

            PdfPCell[] lineThree = new PdfPCell[]
            {
                new PdfPCell(new Phrase("Total Cane Crushed On-Date",bodyContent)),
                new PdfPCell(new Phrase("",bodyContent)),
                new PdfPCell(new Phrase(seohara_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                new PdfPCell(new Phrase(har_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                new PdfPCell(new Phrase(rosa_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                
                new PdfPCell(new Phrase(hata_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                new PdfPCell(new Phrase(nar_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                new PdfPCell(new Phrase(sid_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent)),
                new PdfPCell(new Phrase(has_analysis.TotalCaneCrushedOnDate.ToString(),bodyContent))
            };
            foreach (PdfPCell d in lineThree)
            {
                d.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(d);
            }

            PdfPCell[] lineFour = new PdfPCell[]
            {
                new PdfPCell(new Phrase("Cane Diverted for Ethanol",bodyContent)),
                new PdfPCell(new Phrase("",bodyContent)),
                new PdfPCell(new Phrase(seohara_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                new PdfPCell(new Phrase(har_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                new PdfPCell(new Phrase(rosa_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                
                new PdfPCell(new Phrase(hata_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                new PdfPCell(new Phrase(nar_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                new PdfPCell(new Phrase(sid_analysis.CaneDivertedForEthonol.ToString(),bodyContent)),
                new PdfPCell(new Phrase(has_analysis.CaneDivertedForEthonol.ToString(),bodyContent))
            };
            foreach (PdfPCell d in lineFour)
            {
                d.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(d);
            }

            doc.Add(table);
            doc.Close();
            return path;
        }

        public string GenerateExcel(ReportParameterModel p)
        {
            CommonRepository commRepo = new CommonRepository();
            string fileName = commRepo.CreateExcelFileWithDataUsingTemplate(p.ReportCode, p.SeasonCode, p.ReportDate);
            return fileName;
        }
    }
}