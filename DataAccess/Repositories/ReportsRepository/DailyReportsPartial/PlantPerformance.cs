using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DataAccess.Repositories.AnalysisRepositories;
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.Layout.Properties;

namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial
{
    public class PlantPerformance : DailyReportsRepository
    {
        
        public override string GeneratePdf(int unitCode, int seasonCode, DateTime reportDate, string path)
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss")+".pdf";
            path = path  + fileName;
            FileInfo file = new FileInfo(path);
            file.Directory.Create();
            new PlantPerformance().ManupulatePdf(unitCode, seasonCode, reportDate, path);
            return path;
        }
        private void ManupulatePdf(int unitCode, int CrushingSeason, DateTime ReportDate, string path)
        {
            #region Objects of all Repositories unsed in the class
                LedgerDataRepository ledgerDataRepository = new LedgerDataRepository();
                MasterUnitRepository masterUnitRepository = new MasterUnitRepository();
                ledger_data ledgerData = new ledger_data();
                func_ledger_data_period_summary_Result ledgerDataPeriod = new func_ledger_data_period_summary_Result();
                DailyAnalysisRepository dailyAnalysisRepository = new DailyAnalysisRepository();
                StoppageRepository stoppageRepository = new StoppageRepository();
                MassecuiteSummaryRepository massecuiteSummaryRepository = new MassecuiteSummaryRepository();
                MolassesAnalysisRepository molassesAnalysisRepository = new MolassesAnalysisRepository();
                MeltingAnalysisRepository meltingAnalysisRepository = new MeltingAnalysisRepository();
                HourlyAnalysisRepository hourlyAnalysisRepository = new HourlyAnalysisRepository();
                TwoHourlyAnalysSummaryRepository twoHourlySummaryRepository = new TwoHourlyAnalysSummaryRepository();
            #endregion

            var masterUnitDefaults = masterUnitRepository.FindUnitByPk(unitCode);
            ledgerData = ledgerDataRepository.GetLedgerDataForTheDate(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            ledgerDataPeriod = ledgerDataRepository.GetLedgerDataPeriodSummary(unitCode, CrushingSeason, Convert.ToDateTime(masterUnitDefaults.CrushingStartDate), Convert.ToDateTime(ReportDate));
            //var dailyAnalysesSummary = dailyAnalysisRepository.dailyAnalysesSummaryReport(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var dailyAnalysesSummary = dailyAnalysisRepository.dailyAnalysesSummaryFromCrushingStartDate(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            
            var dailyAnalyses = dailyAnalysisRepository.GetDailyAnalysisDetailsByDate(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var massecuiteSummary = massecuiteSummaryRepository.massecuteToDateSummary(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var molassesSummary = molassesAnalysisRepository.molassesTodateSummary(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var meltingsSummary = meltingAnalysisRepository.GetMeltingsTodateSummary(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var stoppageSummary = stoppageRepository.GetStoppageSummaryForDay(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            
            var hourlyAnalysesSummary = hourlyAnalysisRepository.GetHourlyAnalysisSummaryForDate(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));
            var twoHourlysummary = twoHourlySummaryRepository.GetTwoHourlySummaryForDate(unitCode, CrushingSeason, Convert.ToDateTime(ReportDate));


            FileStream fs = new FileStream(path, FileMode.Create);
            Document pdfDoc = new Document(PageSize.A4.Rotate(), -1, -1, -1, -1);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
            Font HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 8f, Font.BOLD, BaseColor.BLUE);
            Font bodyHeader = new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD, BaseColor.BLACK);
            Font bodyContent = new Font(Font.FontFamily.TIMES_ROMAN, 7f, Font.NORMAL, BaseColor.BLACK);
            
            
            pdfDoc.AddCreationDate();

            pdfDoc.Open();

            PdfPTable table = new PdfPTable(17);
            table.TotalWidth = pdfDoc.PageSize.Width;
            table.LockedWidth = true;
            float[] widths = new float[] { 18f, 100f, 40f, 50f, 60f, 175f, 30f, 50f, 60f, 80f, 45f, 45f, 45f, 40f, 46f, 45f, 40f };
            table.SetWidths(widths);

            
            PdfPCell headerReportDate = new PdfPCell(new Paragraph("Plant Performance @ " + ReportDate.ToString("dd-MMM-yyyy"), HeaderFont));
            PdfPCell HeaderCell = new PdfPCell(new Paragraph(masterUnitDefaults.Name +"-" +
                                                            "("+masterUnitDefaults.Address +")"
                                                                , HeaderFont));
            PdfPCell headerReportPrint = new PdfPCell(new Paragraph("Crop Day @ "+ledgerDataPeriod.days_count , HeaderFont));
            headerReportDate.Border = 0;
            headerReportDate.Colspan = 3;
            headerReportDate.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(headerReportDate);

            HeaderCell.Border = 0;
            HeaderCell.Colspan = 11;
            HeaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(HeaderCell);

            headerReportPrint.Border = 0;
            headerReportPrint.Colspan = 3;
            headerReportPrint.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(headerReportPrint);


            PdfPCell[] docHeader = new PdfPCell[]{
              new PdfPCell(new Phrase("#",bodyHeader)),
              new PdfPCell(new Phrase("Description",bodyHeader)),
              new PdfPCell(new Phrase("Unit",bodyHeader)),
              new PdfPCell(new Phrase("On Date",bodyHeader)),
              new PdfPCell(new Phrase("To Date",bodyHeader)),
              new PdfPCell(new Phrase("Description - Steam",bodyHeader)),
              new PdfPCell(new Phrase("Unit",bodyHeader)),
              new PdfPCell(new Phrase("On Date",bodyHeader)),
              new PdfPCell(new Phrase("To Date",bodyHeader)),
              new PdfPCell(new Phrase("Description ",bodyHeader)),
              new PdfPCell(new Phrase("On Date",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("To Date",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader))
            };

            // adding header using foreach loop to the pdf table
            foreach (PdfPCell header in docHeader)
            {
                header.HorizontalAlignment = Element.ALIGN_LEFT;
                header.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(header);
            }
            
            PdfPCell[] HeaderlineOne = new PdfPCell[]{
                new PdfPCell(new Phrase("1",bodyContent)),
              new PdfPCell(new Phrase("General",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader)),
              new PdfPCell(new Phrase("-Live Steam Generation",bodyHeader)),
              new PdfPCell(new Phrase("Qtls.",bodyHeader)),
              new PdfPCell(new Phrase((dailyAnalyses.live_steam_generation).ToString(),bodyHeader)),
              new PdfPCell(new Phrase((dailyAnalysesSummary.live_steam_generation).ToString(),bodyHeader)),
              new PdfPCell(new Phrase("Analysis ",bodyHeader)),
              new PdfPCell(new Phrase("Brix%",bodyHeader)),
              new PdfPCell(new Phrase("Pol%",bodyHeader)),
              new PdfPCell(new Phrase("Purity",bodyHeader)),
               new PdfPCell(new Phrase("Ph",bodyHeader)),
              new PdfPCell(new Phrase("Brix%",bodyHeader)),
              new PdfPCell(new Phrase("Pol%",bodyHeader)),
              new PdfPCell(new Phrase("Purity",bodyHeader))
            };

            foreach (PdfPCell content in HeaderlineOne)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            // Body Line 1
            PdfPCell[] lineTwo = new PdfPCell[]{
              new PdfPCell(new Phrase("2",bodyContent)),
              new PdfPCell(new Phrase("Cane Crushed",bodyContent)),
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_crushed.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_crushed.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Live Steam Cons.",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase((dailyAnalyses.live_steam_consumption).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.live_steam_consumption.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Primary Juice ",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.combined_pj_brix.ToString(),bodyContent)), // PJ ondate brix
              new PdfPCell(new Phrase(ledgerData.combined_Pj_pol.ToString(),bodyContent)), //PJ on date pol
              new PdfPCell(new Phrase(ledgerData.combined_pj_purity.ToString(),bodyContent)), // on date purity
              new PdfPCell(new Phrase(twoHourlysummary.combined_pj_ph.ToString(),bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.pj_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.pj_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.pj_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwo)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }


            // Body Line 2
            PdfPCell[] lineThree = new PdfPCell[]{
                new PdfPCell(new Phrase("3",bodyContent)),
              new PdfPCell(new Phrase("Avg Crushed",bodyContent)),
              new PdfPCell(new Phrase("qtl/day",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_crushed.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.avg_cane_crushed.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("-Power Turbine",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_turbines.ToString(),bodyContent)),
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_turbines.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Mixed Juice ",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.combined_mj_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.combined_mj_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.combined_mj_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase(twoHourlysummary.combined_mj_ph.ToString(),bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.mj_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.mj_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.mj_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineThree)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #region Body Line4

            PdfPCell[] lineFour = new PdfPCell[]{
                new PdfPCell(new Phrase("4",bodyContent)),
              new PdfPCell(new Phrase("Pol in Cane",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.pol_in_cane_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.pol_in_cane_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Bleeding in proc., 9 ATA",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.bleeding_acf.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.bleeding_acf.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Last M. Juice ",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.combined_lj_brix_avg.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.combined_lj_pol_avg.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.combined_lj_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.lj_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.lj_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.lj_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineFour)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line5

            PdfPCell[] lineFive = new PdfPCell[]{
                new PdfPCell(new Phrase("5",bodyContent)),
                  new PdfPCell(new Phrase("Losses",HeaderFont)),
                  new PdfPCell(new Phrase("%",bodyContent)), //uom
				  new PdfPCell(new Phrase("",bodyContent)), //on date total_loss
				  new PdfPCell(new Phrase("",bodyContent)),// to date
				  new PdfPCell(new Phrase("-De Sup. Heating.",bodyContent)), //description
				  new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
				  new PdfPCell(new Phrase( dailyAnalyses.d_sulpher_heating.ToString().ToString(),bodyContent)), //on date
				  new PdfPCell(new Phrase(dailyAnalysesSummary.d_super_heating.ToString(),bodyContent)), //to date
				  new PdfPCell(new Phrase("Clear Juice ",bodyContent)), // description
				  new PdfPCell(new Phrase(ledgerData.clear_juice_brix.ToString(),bodyContent)), // ondate brix
				  new PdfPCell(new Phrase(ledgerData.clear_juice_pol.ToString(),bodyContent)), // on date pol
				  new PdfPCell(new Phrase(ledgerData.clear_juice_purity.ToString(),bodyContent)), // on date purity
				   new PdfPCell(new Phrase(twoHourlysummary.cj_ph_avg.ToString(),bodyContent)), // on date ph
				  new PdfPCell(new Phrase(ledgerDataPeriod.cj_brix.ToString(),bodyContent)), // to date brix
				  new PdfPCell(new Phrase(ledgerDataPeriod.cj_pol.ToString(),bodyContent)), // to date pol
				  new PdfPCell(new Phrase(ledgerDataPeriod.cj_purity.ToString(),bodyContent)) // todate purity
				};

            foreach (PdfPCell content in lineFive)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line6

            PdfPCell[] lineSix = new PdfPCell[]{
              new PdfPCell(new Phrase("6",bodyContent)),
              new PdfPCell(new Phrase("Bagasse",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.pol_in_bagasse_percentage_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.pol_in_bagasse_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Drain & Pipe Loss",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.drain_pipe_loss.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.drain_pipe_loss.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Unsul. Syrup",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.un_sulphered_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.un_sulphered_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.un_sulphered_purity.ToString(),bodyContent)), // on date purity
              new PdfPCell(new Phrase(twoHourlysummary.un_sulphured_ph_avg.ToString(),bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.unsulphered_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.unsulphered_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.unsulphered_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineSix)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line 7

            PdfPCell[] lineSeven = new PdfPCell[]{
                new PdfPCell(new Phrase("7",bodyContent)),
              new PdfPCell(new Phrase("Filter Cake",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.pol_in_press_cake_percentage_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.pol_in_press_cake_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Exhaust steam generation",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.exhaust_steam_generation.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.exhaust_steam_generation.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Sulpher Syrup",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.sulphered_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.sulphered_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.sulphered_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.sulphered_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.sulphered_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.sulphered_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineSeven)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line8

            PdfPCell[] lineEight = new PdfPCell[]{
              new PdfPCell(new Phrase("8",bodyContent)),
              new PdfPCell(new Phrase("Molasses",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.pol_in_molasses_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_pol_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Exhaust steam consumption",bodyContent)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.exhaust_steam_consumption.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.exhaust_steam_consumption.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Filtrate Juice",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.oliver_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.oliver_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.oliver_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.oliver_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.oliver_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.oliver_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineEight)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line9

            PdfPCell[] lineNine = new PdfPCell[]{
                new PdfPCell(new Phrase("9",bodyContent)),
              new PdfPCell(new Phrase("Underermined",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.unknown_loss_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.unknown_loss_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Steam cons. With Desu. Het. Wat.",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.steam_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.steam_per_ton_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("A- Massecuite",bodyContent)), // description
              new PdfPCell(new Phrase(massecuiteSummary.od_a_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(massecuiteSummary.od_a_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(massecuiteSummary.od_a_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(massecuiteSummary.td_a_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(massecuiteSummary.td_a_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(massecuiteSummary.td_a_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineNine)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line10
            //live_steam_consumption + bleeding_acf + ata3_cogen
           

            PdfPCell[] lineTen = new PdfPCell[]{
              new PdfPCell(new Phrase("10",bodyContent)),
              new PdfPCell(new Phrase("TOTAL",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.total_losses_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.total_loss_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Steam cons. Without Desu.Het. Wat.",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.cane_crushed == 0 ? "0":  Math.Round((((dailyAnalyses.live_steam_consumption+dailyAnalyses.bleeding_acf+dailyAnalyses.ata3_cogen)/dailyAnalyses.cane_crushed))*100,2).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.cane_crushed ==0 ? "0" : Math.Round(((Convert.ToDecimal(dailyAnalysesSummary.steam_consumption_without_d_super_heating)/Convert.ToDecimal(dailyAnalysesSummary.cane_crushed))*100),2).ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("A1-Massecuite",bodyContent)), // description
              new PdfPCell(new Phrase(massecuiteSummary.od_a1_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(massecuiteSummary.od_a1_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(massecuiteSummary.od_a1_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(massecuiteSummary.td_a1_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(massecuiteSummary.td_a1_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(massecuiteSummary.td_a1_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line11

            PdfPCell[] lineEleven = new PdfPCell[]{
                new PdfPCell(new Phrase("11",bodyContent)),
              new PdfPCell(new Phrase("Recovery",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.estimated_sugar_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.estimated_suagar_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power Generation From Co-Gen",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_generation_from_coGen.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_generation_from_congen.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("B-Massecuite",bodyContent)), // description
              new PdfPCell(new Phrase(massecuiteSummary.od_b_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(massecuiteSummary.od_b_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(massecuiteSummary.od_b_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(massecuiteSummary.td_b_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(massecuiteSummary.td_b_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(massecuiteSummary.td_b_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineEleven)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion
            #region Body Line12

            PdfPCell[] lineTwelve = new PdfPCell[]{
                new PdfPCell(new Phrase("12",bodyContent)),
              new PdfPCell(new Phrase("Fiber",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.fiber_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.fiber_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power From Sugar",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_from_sugar.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_from_sugar.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("C-Massecuite",bodyContent)), // description
              new PdfPCell(new Phrase(massecuiteSummary.od_c_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(massecuiteSummary.od_c_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(massecuiteSummary.od_c_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(massecuiteSummary.td_c_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(massecuiteSummary.td_c_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(massecuiteSummary.td_c_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwelve)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line13

            PdfPCell[] lineThirteen = new PdfPCell[]{
                new PdfPCell(new Phrase("13",bodyContent)),
              new PdfPCell(new Phrase("Net. M. Juice",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.net_mixed_juice_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.net_mixed_juice_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power Exported from Co-gen",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_export_grid.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_export_grid.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("C1-Massecuite",bodyContent)), // description
               new PdfPCell(new Phrase(massecuiteSummary.od_c1_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(massecuiteSummary.od_c1_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(massecuiteSummary.od_c1_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(massecuiteSummary.td_c1_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(massecuiteSummary.td_c1_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(massecuiteSummary.td_c1_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineThirteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line14

            PdfPCell[] lineFourteen = new PdfPCell[]{
                new PdfPCell(new Phrase("14",bodyContent)),
              new PdfPCell(new Phrase("Dirt Correction",bodyContent)),
              new PdfPCell(new Phrase("%MJ",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.dirt_correction_perccent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.dirt_correction_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power=>Import(Co-gen)",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_import_cogen.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_import_cogen.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("A-Heavy",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_a_heavy_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_a_heavy_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_a_heavy_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("-",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_a_heavy_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_a_heavy_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_a_heavy_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineFourteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line15

            PdfPCell[] lineFifteen = new PdfPCell[]{
                new PdfPCell(new Phrase("15",bodyContent)),
              new PdfPCell(new Phrase("Cane",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase("",bodyContent)), //on date
              new PdfPCell(new Phrase("",bodyContent)),// to date
              new PdfPCell(new Phrase("D.G. Set",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_dg_set.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_dg_set.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("A1-Heavy",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_a1_heavy_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_a1_heavy_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_a1_heavy_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_a1_heavy_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_a1_heavy_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_a1_heavy_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineFifteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion
            #region Body Line16

            PdfPCell[] lineSixteen = new PdfPCell[]{
                new PdfPCell(new Phrase("16",bodyContent)),
              new PdfPCell(new Phrase("Java Ratio",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.java_ratio.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.java_ratio.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("TOTAL POWER",bodyContent)), //description
              new PdfPCell(new Phrase("KWH",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.total_power.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.total_power.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("B-Heavy",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_b_heavy_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_b_heavy_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_b_heavy_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_b_heavy_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_b_heavy_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_b_heavy_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineSixteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line17

            PdfPCell[] lineSevenTeen = new PdfPCell[]{
              new PdfPCell(new Phrase("17",bodyContent)),
              new PdfPCell(new Phrase("Dry Mill Factor",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.dry_mill_factor_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.dry_mill_factor_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power/Ton Cane",bodyContent)), //description
              new PdfPCell(new Phrase("-",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_per_ton_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_per_ton_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("C1-Heavy",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_c1_heavy_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_c1_heavy_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_c1_purity.ToString(),bodyContent)), // on date purity
              new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_c1_heavy_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_c1_heavy_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_c1_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineSevenTeen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line18

            PdfPCell[] lineEighteen = new PdfPCell[]{
                new PdfPCell(new Phrase("18",bodyContent)),
              new PdfPCell(new Phrase("Early Cane",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_early_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_early_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Power/Ton Sugar",bodyContent)), //description
              new PdfPCell(new Phrase("-",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.power_per_qtl_sugar.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.power_per_ten_ton_sugar.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("A-Light",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_a_light_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_a_light_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_a_ligth_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_a_light_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_a_light_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_a_light_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineEighteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line19

            PdfPCell[] lineNineteen = new PdfPCell[]{
              new PdfPCell(new Phrase("19",bodyContent)),
              new PdfPCell(new Phrase("General Cane",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_general_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_general_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Consumable/Store",bodyContent)), //description
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase("",bodyContent)), //on date
              new PdfPCell(new Phrase("",bodyContent)), //to date
              new PdfPCell(new Phrase("C-Light",bodyContent)), // description
              new PdfPCell(new Phrase(molassesSummary.od_c_light_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(molassesSummary.od_c_light_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(molassesSummary.od_c_light_purity.ToString(),bodyContent)), // on date purity
              new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(molassesSummary.td_c_light_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(molassesSummary.td_c_light_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(molassesSummary.td_c_light_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineNineteen)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line20

            PdfPCell[] lineTwenty = new PdfPCell[]{
                new PdfPCell(new Phrase("20",bodyContent)),
              new PdfPCell(new Phrase("Reject Cane/Burnt",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_reject+"/"+ledgerData.cane_reject,bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_reject_percent+"/"+ledgerDataPeriod.cane_burnt_percent,bodyContent)),// to date
              new PdfPCell(new Phrase("Sulpher",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_sulpher_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_sulpher_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Final Molasses",bodyContent)), // description
              new PdfPCell(new Phrase(ledgerData.final_molasses_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(ledgerData.final_molasses_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(ledgerData.final_molasses_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwenty)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line21

            PdfPCell[] lineTwentyOne = new PdfPCell[]{
                new PdfPCell(new Phrase("21",bodyContent)),
              new PdfPCell(new Phrase("Farm Cane",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_farm_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_farm_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Lime",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_lime_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_lime_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Seed",bodyContent)), // description
              new PdfPCell(new Phrase( meltingsSummary.od_seed_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(meltingsSummary.od_seed_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(meltingsSummary.od_seed_purity.ToString(),bodyContent)), // on date purity
              new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(meltingsSummary.td_seed_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(meltingsSummary.td_seed_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(meltingsSummary.td_seed_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwentyOne)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion
            #region Body Line22

            PdfPCell[] lineTwentyTwo = new PdfPCell[]{
                new PdfPCell(new Phrase("22",bodyContent)),
              new PdfPCell(new Phrase("Gate Cane",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_gate_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_gate_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Phosphoric Acid",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_phosphoric_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_phosphoric_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Melt",bodyContent)), // description
              new PdfPCell(new Phrase(meltingsSummary.od_melt_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(meltingsSummary.od_melt_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(meltingsSummary.od_melt_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(meltingsSummary.td_melt_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(meltingsSummary.td_melt_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(meltingsSummary.td_melt_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwentyTwo)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion


            #region Body Line23

            PdfPCell[] lineTwentyThree = new PdfPCell[]{
                new PdfPCell(new Phrase("23",bodyHeader)),
              new PdfPCell(new Phrase("Centre Cane",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.cane_centre_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.cane_centre_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Viscosity Reducer",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_viscosity_reducer_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_viscosity_reducer_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("C Single Sugar",bodyContent)), // description
              new PdfPCell(new Phrase(meltingsSummary.od_c_single_sugar_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(meltingsSummary.od_c_single_sugar_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(meltingsSummary.od_c_single_sugar_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(meltingsSummary.td_c_single_sugar_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(meltingsSummary.td_c_single_sugar_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(meltingsSummary.td_c_single_sugar_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwentyThree)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line24

            PdfPCell[] lineTwentyFour = new PdfPCell[]{
                new PdfPCell(new Phrase("24",bodyContent)),
              new PdfPCell(new Phrase("MILLS",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase("",bodyContent)), //on date
              new PdfPCell(new Phrase("",bodyContent)),// to date
              new PdfPCell(new Phrase("Biocide",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_biocide_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_biocide_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("C-Double Sugar",bodyContent)), // description
              new PdfPCell(new Phrase(meltingsSummary.od_c_double_sugar_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(meltingsSummary.od_c_double_sugar_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(meltingsSummary.od_c_double_sugar_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(meltingsSummary.td_c_double_sugar_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(meltingsSummary.td_c_double_sugar_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(meltingsSummary.td_c_double_sugar_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwentyFour)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line25

            PdfPCell[] lineTwentyFive = new PdfPCell[]{
                new PdfPCell(new Phrase("25",bodyContent)),
              new PdfPCell(new Phrase("Preparatory Index",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.nm_p_index.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.p_index_avg.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Color Reducer",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_color_reducer_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_color_reducer_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("B Sugar",bodyContent)), // description
              new PdfPCell(new Phrase(meltingsSummary.od_b_sugar_brix.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(meltingsSummary.od_b_sugar_pol.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase(meltingsSummary.od_b_sugar_purity.ToString(),bodyContent)), // on date purity
               new PdfPCell(new Phrase("",bodyContent)), // on date ph
              new PdfPCell(new Phrase(meltingsSummary.td_b_sugar_brix.ToString(),bodyContent)), // to date brix
              new PdfPCell(new Phrase(meltingsSummary.td_b_sugar_pol.ToString(),bodyContent)), // to date pol
              new PdfPCell(new Phrase(meltingsSummary.td_b_sugar_purity.ToString(),bodyContent)) // todate purity
            };

            foreach (PdfPCell content in lineTwentyFive)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line26

            PdfPCell[] lineTwentySix = new PdfPCell[]{
                new PdfPCell(new Phrase("26",bodyContent)),
              new PdfPCell(new Phrase("Maceration",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.water_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.water_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Magnafloe",bodyContent)), //description
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_magnafloe_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_magnafloe_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("ICUMSA Color",bodyHeader)), // description
              new PdfPCell(new Phrase("On Date",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("To Date",bodyContent)) // on date pol
            };

            PdfPCell lineTwentyFivetubeWell = new PdfPCell(new Phrase("Tube Well Turning Status", bodyHeader));
            foreach (PdfPCell content in lineTwentySix)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            lineTwentyFivetubeWell.Colspan = 5;
            table.AddCell(lineTwentyFivetubeWell);
            #endregion

            #region Body Line27

            PdfPCell[] lineTwentySeven = new PdfPCell[]{
                new PdfPCell(new Phrase("27",bodyContent)),
              new PdfPCell(new Phrase("Maceration",bodyContent)),
              new PdfPCell(new Phrase("%Fiber",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.added_water_percent_fiber.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.added_water_percent_fiber.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("-LUBRICANTS",bodyContent)), //description
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase( ledgerData.store_lub_oil_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_lub_oil_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Sugar L-31",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.icumsa_l31.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(dailyAnalysesSummary.icumsa_l31.ToString(),bodyContent)) // on date pol
            };

            PdfPCell lineTwentySevenDuration = new PdfPCell(new Phrase("Duration(In Hrs)", bodyHeader));
            PdfPCell lineTwentySevenCellOne = new PdfPCell(new Phrase(dailyAnalyses.total_operating_tube_well.ToString(), bodyContent));
            PdfPCell lineTwentySevenCellTwo = new PdfPCell(new Phrase(dailyAnalysesSummary.total_operating_tube_well.ToString(), bodyContent));

            foreach (PdfPCell content in lineTwentySeven)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            lineTwentySevenDuration.Colspan = 3;
            table.AddCell(lineTwentySevenDuration);
            table.AddCell(lineTwentySevenCellOne);
            table.AddCell(lineTwentySevenCellTwo);
            #endregion

            #region Body Line28

            PdfPCell[] lineTwentyeight = new PdfPCell[]{
                new PdfPCell(new Phrase("28",bodyContent)),
              new PdfPCell(new Phrase("Bagasse Pol",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.combined_bagasse_average.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.combined_bagasse_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Grease",bodyContent)), //description
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_lub_grease_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_lub_grease_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Sugar M-31",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.icumsa_m31.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(dailyAnalysesSummary.icumsa_m31.ToString(),bodyContent)) // on date pol
            };

            PdfPCell lineTwentyeightETPWaterFlow = new PdfPCell(new Phrase("ETP Water Flow M³/hr", bodyHeader));
            PdfPCell lineTwentyeightCellOne = new PdfPCell(new Phrase(dailyAnalyses.etp_water_flow.ToString(), bodyContent));
            PdfPCell lineTwentyeightCellTwo = new PdfPCell(new Phrase(dailyAnalysesSummary.etp_water_flow.ToString(), bodyContent));

            foreach (PdfPCell content in lineTwentyeight)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            lineTwentyeightETPWaterFlow.Colspan = 3;
            table.AddCell(lineTwentyeightETPWaterFlow);
            table.AddCell(lineTwentyeightCellOne);
            table.AddCell(lineTwentyeightCellTwo);
            #endregion


            #region Body Line29

            PdfPCell[] lineTwentynine = new PdfPCell[]{
                new PdfPCell(new Phrase("29",bodyContent)),
              new PdfPCell(new Phrase("Bagasse Moist",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.combined_bagasse_moist_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.moist_bagasse_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Boiler Chem.",bodyContent)), //description
              new PdfPCell(new Phrase("-",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.store_boiler_chemical_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.store_boiler_chemical_percent_cane.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Sugar S-31",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.icumsa_s31.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(dailyAnalysesSummary.icumsa_s31.ToString(),bodyContent)) // on date pol
            };

            PdfPCell lineTwentynineOctain = new PdfPCell(new Phrase("P2O5 PPM", bodyHeader));
            PdfPCell lineTwentynineCellOne = new PdfPCell(new Phrase("CaO PPM", bodyHeader));
            PdfPCell lineTwentynineCellTwo = new PdfPCell(new Phrase("", bodyContent));

            foreach (PdfPCell content in lineTwentynine)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            lineTwentynineOctain.Colspan = 2;
            lineTwentynineOctain.HorizontalAlignment = Element.ALIGN_CENTER;
            lineTwentynineCellOne.Colspan = 2;
            lineTwentynineCellOne.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(lineTwentynineOctain);
            table.AddCell(lineTwentynineCellOne);
            table.AddCell(lineTwentynineCellTwo);

            #endregion

            #region Body Line30

            PdfPCell[] lineThirty = new PdfPCell[]{
                new PdfPCell(new Phrase("30",bodyContent)),
              new PdfPCell(new Phrase("Bagasse Quantity",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.total_bagasse_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.total_bagasse_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Down Time",bodyContent)), //description
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase("",bodyContent)), //on date
              new PdfPCell(new Phrase("",bodyContent)), //to date
              new PdfPCell(new Phrase("Primary Juice",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.iu_primary_juice.ToString(),bodyContent)), // ondate brix
              new PdfPCell(new Phrase(dailyAnalysesSummary.iu_primary_jiice.ToString(),bodyContent)), // on date pol
              new PdfPCell(new Phrase("On Date",bodyHeader)),
              new PdfPCell(new Phrase("To Date",bodyHeader)),
              new PdfPCell(new Phrase("On Date",bodyHeader)),
              new PdfPCell(new Phrase("To Date",bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader))
            };

            foreach (PdfPCell content in lineThirty)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line31

            PdfPCell[] lineThirtyone = new PdfPCell[]{
                new PdfPCell(new Phrase("31",bodyContent)),
              new PdfPCell(new Phrase("Added Water Ext in M.J",bodyContent)),
              new PdfPCell(new Phrase("%a.w",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.extracted_mj_added_water.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.added_water_extracted_mj_percent_added_water.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Mechanical",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.Mechanical)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_Mechanical)),bodyContent)), //to date
              new PdfPCell(new Phrase("Mixed Juice",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.iu_mixed_juice.ToString() ,bodyContent)), // ondate iu_mixed_juice
              new PdfPCell(new Phrase(dailyAnalysesSummary.iu_mixed_juice.ToString(),bodyContent)), // on date iu_mixed_juice
              new PdfPCell(new Phrase(dailyAnalyses.phosphate_mixed_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalysesSummary.phosphate_mixed_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalyses.calcium_mixed_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalysesSummary.calcium_mixed_jice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader))
            };

            foreach (PdfPCell content in lineThirtyone)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion
            #region Body Line32

            PdfPCell[] lineThirtytwo = new PdfPCell[]{
                new PdfPCell(new Phrase("32",bodyContent)),
              new PdfPCell(new Phrase("Mill Extr.",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.mill_extraction.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.mill_extraction_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Process",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.Process)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_Process)).ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Clear Juice",bodyContent)), // description
              new PdfPCell(new Phrase(dailyAnalyses.iu_clear_juice.ToString(),bodyContent)), // ondate iu_clear juice
              new PdfPCell(new Phrase(dailyAnalysesSummary.iu_clear_juice.ToString(),bodyContent)), // todate  iu_clear juice
              new PdfPCell(new Phrase(dailyAnalyses.phosphate_clear_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalysesSummary.phosphate_clear_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalyses.calcium_clear_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase(dailyAnalysesSummary.calcium_clear_juice.ToString(),bodyHeader)),
              new PdfPCell(new Phrase("",bodyHeader))
            };

            foreach (PdfPCell content in lineThirtytwo)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line33

            PdfPCell[] lineThirtythree = new PdfPCell[]{
                new PdfPCell(new Phrase("32",bodyContent)),
              new PdfPCell(new Phrase("Reduced Mill Extr.",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.reduced_mill_extraction_deer.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.reduced_mill_extraction_deer.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("No Cane & Slow Rate",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.NoCane)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_NoCane)),bodyContent)), //to date
              new PdfPCell(new Phrase("Ret% M Grain",bodyContent)), // description
              new PdfPCell(new Phrase("-",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("-",bodyContent)) // on date pol
            };

            PdfPCell ThirtythreeEtpHeader = new PdfPCell(new Phrase("E.T.P. Treated Water Data", bodyHeader));
            foreach (PdfPCell content in lineThirtythree)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            ThirtythreeEtpHeader.Colspan = 5;
            ThirtythreeEtpHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(ThirtythreeEtpHeader);
            #endregion

            #region Body Line34

            PdfPCell[] lineThirtyfour = new PdfPCell[]{
                new PdfPCell(new Phrase("34",bodyContent)),
              new PdfPCell(new Phrase("ERQV MJ/PJ",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.erq_mj_to_pj.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.erq_mj_to_pj.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Misc",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.Miscellaneous)).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_Miscellaneous)),bodyContent)), //to date
              new PdfPCell(new Phrase("Overtime Wages(Rs)",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)) // on date pol
            };

            PdfPCell[] ThirtyfourEtpHeader = new PdfPCell[]{
                new PdfPCell(new Phrase("Param", bodyHeader)),
                new PdfPCell(new Phrase("Today", bodyHeader)),
                new PdfPCell(new Phrase("To Date", bodyHeader)),
                new PdfPCell(new Phrase("Units", bodyHeader)),
                new PdfPCell(new Phrase("", bodyHeader))
            };
            foreach (PdfPCell content in lineThirtyfour)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            foreach (PdfPCell headerCell in ThirtyfourEtpHeader)
            {
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(headerCell);
            }

            #endregion

            #region Body Line35

            PdfPCell[] lineThirtyfive = new PdfPCell[]{
                new PdfPCell(new Phrase("35",bodyContent)),
              new PdfPCell(new Phrase("ERQV LJ/PJ",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.erq_lj_to_pj.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.erq_lj_to_pj.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Gen. Cleaning",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.GeneralCleaning)).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_GeneralCleaning)),bodyContent)), //to date
              new PdfPCell(new Phrase("Engg.Replacement",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("pH", bodyHeader)),
                new PdfPCell(new Phrase(dailyAnalyses.etp_ph.ToString(), bodyContent)),
                new PdfPCell(new Phrase(dailyAnalysesSummary.etp_ph.ToString(), bodyContent)),
                new PdfPCell(new Phrase("-", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineThirtyfive)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line36

            PdfPCell[] lineThirtysix = new PdfPCell[]{
                new PdfPCell(new Phrase("36",bodyContent)),
              new PdfPCell(new Phrase("Undiluted J. Lost%Fiber",bodyContent)),
              new PdfPCell(new Phrase("%Fiber",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.undiluted_juice_lost_percent_fiber.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.undiluted_juice_lost_percent_fiber.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Festival",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.Festival)).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_Festival)),bodyContent)), //to date
              new PdfPCell(new Phrase("Engg.Extra",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("TSS", bodyHeader)),
                new PdfPCell(new Phrase(dailyAnalyses.etp_tss.ToString(), bodyContent)),
                new PdfPCell(new Phrase(dailyAnalysesSummary.etp_tss.ToString(), bodyContent)),
                new PdfPCell(new Phrase("-", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineThirtysix)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line37

            PdfPCell[] lineThirtyseven = new PdfPCell[]{
                new PdfPCell(new Phrase("37",bodyContent)),
              new PdfPCell(new Phrase("Undiluted Juice",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.undiulted_juice_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.undiluted_juice_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Incl Weather",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.Weather)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_Weather)),bodyContent)), //to date
              new PdfPCell(new Phrase("Mfg Replacement",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("COD", bodyHeader)),
                new PdfPCell(new Phrase(dailyAnalyses.etp_cod.ToString(), bodyContent)),
                new PdfPCell(new Phrase(dailyAnalysesSummary.etp_cod.ToString(), bodyContent)),
                new PdfPCell(new Phrase("mg/liter", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineThirtyseven)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line38

            PdfPCell[] lineUndilutedJuiceExtCane = new PdfPCell[]{
                new PdfPCell(new Phrase("38",bodyContent)),
              new PdfPCell(new Phrase("Undiluted Juice ext % Cane",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.undiluted_juice_extracted.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.undiluted_juice_extrcted_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Co-gen",bodyContent)), //description
              new PdfPCell(new Phrase("h-m%",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.CoGen)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_CoGen)),bodyContent)), //to date
              new PdfPCell(new Phrase("Mfg extra",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("BOD", bodyHeader)),
                new PdfPCell(new Phrase(dailyAnalyses.etp_bod.ToString(), bodyContent)),
                new PdfPCell(new Phrase(dailyAnalysesSummary.etp_bod.ToString(), bodyContent)),
                new PdfPCell(new Phrase("mg/liter", bodyContent)),
                new PdfPCell(new Phrase("S-31", bodyContent))
            };

            foreach (PdfPCell content in lineUndilutedJuiceExtCane)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line39
            PdfPCell lineThirtyEightSerial = new PdfPCell(new Phrase("39", bodyContent));
            PdfPCell lineThirtyEightProcessHeader = new PdfPCell(new Phrase("Process", bodyHeader));
            PdfPCell lineThirtyEightTotalDownTimeHeader = new PdfPCell(new Phrase("Total Down Time", bodyHeader));
            PdfPCell lineThirtyEightTotalDownTimeUnit = new PdfPCell(new Phrase("h-m%", bodyContent));
            PdfPCell lineThirtyEightTotalDownTimeOnDay = new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.TotalDuration)), bodyContent));
            PdfPCell lineThirtyEightTotalDownTimeToDate = new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_TotalDuration)), bodyContent));
            PdfPCell lineThirtyeightChemicalHeader = new PdfPCell(new Phrase("Chem. & Lub. Controlling Parameter", bodyHeader));
            PdfPCell lineThirtyeightChemicalOnDateHeader = new PdfPCell(new Phrase("On Date", bodyHeader));
            PdfPCell lineThirtyeightChemicalToDateHeader = new PdfPCell(new Phrase("To Date", bodyHeader));

            lineThirtyEightProcessHeader.Colspan = 4;
            lineThirtyEightProcessHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineThirtyeightChemicalHeader.Colspan = 3;
            lineThirtyeightChemicalHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineThirtyeightChemicalOnDateHeader.Colspan = 2;
            lineThirtyeightChemicalToDateHeader.Colspan = 3;

            table.AddCell(lineThirtyEightSerial);
            table.AddCell(lineThirtyEightProcessHeader);
            table.AddCell(lineThirtyEightTotalDownTimeHeader);
            table.AddCell(lineThirtyEightTotalDownTimeUnit);
            table.AddCell(lineThirtyEightTotalDownTimeOnDay);
            table.AddCell(lineThirtyEightTotalDownTimeToDate);
            table.AddCell(lineThirtyeightChemicalHeader);
            table.AddCell(lineThirtyeightChemicalOnDateHeader);
            table.AddCell(lineThirtyeightChemicalToDateHeader);
            #endregion

            #region Body Line40

            PdfPCell[] lineThirtynine = new PdfPCell[]{
                new PdfPCell(new Phrase("40",bodyContent)),
              new PdfPCell(new Phrase("Filter Cake Pol",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.press_cake_average.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.pol_in_press_cake_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Plant Availability (crush hrs)",bodyContent)), //description
              new PdfPCell(new Phrase("hh:mm",bodyContent)), //uom
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.actual_minutes_of_crushing)),bodyContent)), //on date
              new PdfPCell(new Phrase(ConvertMinutesToHours(Convert.ToDecimal(stoppageSummary.td_actual_minutes_of_crushing)),bodyContent)), //to date
              new PdfPCell(new Phrase("Particulars",bodyContent)), // description
              new PdfPCell(new Phrase("Qty",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("Amt(Rs.)",bodyContent)), // on date pol
              new PdfPCell(new Phrase("Rs/Bag", bodyHeader)),
                new PdfPCell(new Phrase("Rs/Cane", bodyContent)),
                new PdfPCell(new Phrase("Amount", bodyContent)),
                new PdfPCell(new Phrase("Rs/Bag", bodyContent)),
                new PdfPCell(new Phrase("Rs/Cane", bodyContent))
            };

            foreach (PdfPCell content in lineThirtynine)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line41

            PdfPCell[] lineFourty = new PdfPCell[]{
                new PdfPCell(new Phrase("41",bodyContent)),
              new PdfPCell(new Phrase("Filter Cake Qty",bodyContent)),
              new PdfPCell(new Phrase("Qtl",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.press_cake.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.press_cake_qtl.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Process Indicators",bodyContent)), //description
              new PdfPCell(new Phrase("",bodyContent)), //uom
              new PdfPCell(new Phrase("",bodyContent)), //on date
              new PdfPCell(new Phrase("",bodyContent)), //to date
              new PdfPCell(new Phrase("Process Chemical",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("", bodyHeader)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineFourty)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line42

            PdfPCell[] lineFourtyone = new PdfPCell[]{
                new PdfPCell(new Phrase("42",bodyContent)),
              new PdfPCell(new Phrase("F Mol. estimated",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.estimated_molasses_percent_cane.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.estimated_molasses_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("A Massecuite Volume",bodyContent)), //description
              new PdfPCell(new Phrase("H.L.",bodyContent)), //uom
              new PdfPCell(new Phrase(massecuiteSummary.od_a_hl.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(massecuiteSummary.td_a_hl.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Boiler Chemical",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("", bodyHeader)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineFourtyone)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line43

            PdfPCell[] lineFourtytwo = new PdfPCell[]{
                new PdfPCell(new Phrase("43",bodyContent)),
              new PdfPCell(new Phrase("F Mol. Sent Out",bodyContent)),
              new PdfPCell(new Phrase("%Cane",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.final_molasses_sent_out_percent.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_sent_out_percent.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("B Massecuite",bodyContent)), //description
              new PdfPCell(new Phrase("H.L.",bodyContent)), //uom
              new PdfPCell(new Phrase(massecuiteSummary.od_b_hl.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(massecuiteSummary.td_b_hl.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Oil & Grease",bodyContent)), // description
              new PdfPCell(new Phrase("",bodyContent)), // ondate brix
              new PdfPCell(new Phrase("",bodyContent)), // on date pol
              new PdfPCell(new Phrase("", bodyHeader)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent)),
                new PdfPCell(new Phrase("", bodyContent))
            };

            foreach (PdfPCell content in lineFourtytwo)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line44

            PdfPCell[] lineFourtythree = new PdfPCell[]{
                new PdfPCell(new Phrase("44",bodyContent)),
              new PdfPCell(new Phrase("F Mol. Sent Out",bodyContent)),
              new PdfPCell(new Phrase("Qtl",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.final_molasses_sent_out.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.final_molasses_sent_out.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("C Massecuite",bodyContent)), //description
              new PdfPCell(new Phrase("H.L.",bodyContent)), //uom
              new PdfPCell(new Phrase(massecuiteSummary.od_c_hl.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(massecuiteSummary.td_c_hl.ToString(),bodyContent)), //to date
            };

            PdfPCell lineFourtythreeLastSeasonDataHeader = new PdfPCell(new Phrase("Last Season Data", bodyHeader)); //row span
            PdfPCell lineFourtythreeCropDayHeader = new PdfPCell(new Phrase("Crop Day", bodyHeader));
            PdfPCell lineFourtythreeOnDateHeader = new PdfPCell(new Phrase("On Date", bodyHeader));
            PdfPCell lineFourtythreeDrainWaterAnalysisHeader = new PdfPCell(new Phrase("Drain Water Analysis", bodyHeader));

            foreach (PdfPCell content in lineFourtythree)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            lineFourtythreeLastSeasonDataHeader.Rowspan = 2;
            lineFourtythreeLastSeasonDataHeader.VerticalAlignment = Element.ALIGN_CENTER;
            lineFourtythreeCropDayHeader.Colspan = 2;
            lineFourtythreeCropDayHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineFourtythreeOnDateHeader.Colspan = 2;
            lineFourtythreeOnDateHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineFourtythreeDrainWaterAnalysisHeader.Colspan = 3;
            lineFourtythreeDrainWaterAnalysisHeader.HorizontalAlignment = Element.ALIGN_CENTER;

            table.AddCell(lineFourtythreeLastSeasonDataHeader);
            table.AddCell(lineFourtythreeCropDayHeader);
            table.AddCell(lineFourtythreeOnDateHeader);
            table.AddCell(lineFourtythreeDrainWaterAnalysisHeader);

            #endregion

            #region Body Line45

            PdfPCell[] lineFourfour = new PdfPCell[]{
                new PdfPCell(new Phrase("45",bodyContent)),
              new PdfPCell(new Phrase("TRS in F Mol.",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.b_heavy_final_molasses_trs.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.trs_percentage.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("A massecuite exhaustion",bodyContent)), //description
              new PdfPCell(new Phrase("Unit",bodyContent)), //uom
              new PdfPCell(new Phrase((massecuiteSummary.od_a_purity - molassesSummary.od_a_heavy_purity) < 0 ? "" : (massecuiteSummary.od_a_purity - molassesSummary.od_a_heavy_purity).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase((massecuiteSummary.td_a_purity - molassesSummary.td_a_heavy_purity) < 0 ? "" : (massecuiteSummary.td_a_purity - molassesSummary.td_a_heavy_purity).ToString(),bodyContent)) //to date
              //new PdfPCell(new Phrase("",bodyContent)), //on date
              //new PdfPCell(new Phrase("",bodyContent)) //to date
            };


            PdfPCell lineFourtythreeCropDayToDayHeader = new PdfPCell(new Phrase("Today", bodyHeader));
            PdfPCell lineFourtythreeCropDayOnDateHeader = new PdfPCell(new Phrase("ToDate", bodyHeader));
            PdfPCell lineFourtythreeOnDateTodayHeader = new PdfPCell(new Phrase("Today", bodyHeader));
            PdfPCell lineFourtythreeOnDateTodateHeader = new PdfPCell(new Phrase("ToDate", bodyHeader));
            PdfPCell lineFourtythreeDrainWaterMillHouseDrainWaterHeader = new PdfPCell(new Phrase("Mill House Drain Water", bodyHeader));

            foreach (PdfPCell content in lineFourfour)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }


            lineFourtythreeCropDayToDayHeader.HorizontalAlignment = Element.ALIGN_CENTER;

            lineFourtythreeCropDayOnDateHeader.HorizontalAlignment = Element.ALIGN_CENTER;

            lineFourtythreeOnDateTodayHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineFourtythreeOnDateTodateHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            lineFourtythreeDrainWaterMillHouseDrainWaterHeader.Colspan = 3;
            lineFourtythreeDrainWaterMillHouseDrainWaterHeader.HorizontalAlignment = Element.ALIGN_CENTER;

            table.AddCell(lineFourtythreeCropDayToDayHeader);
            table.AddCell(lineFourtythreeCropDayOnDateHeader);
            table.AddCell(lineFourtythreeOnDateTodayHeader);
            table.AddCell(lineFourtythreeOnDateTodateHeader);
            table.AddCell(lineFourtythreeDrainWaterMillHouseDrainWaterHeader);

            #endregion

            #region Body Line46

            PdfPCell[] lineFourfive = new PdfPCell[]{
                new PdfPCell(new Phrase("46",bodyContent)),
              new PdfPCell(new Phrase("RS in F Mol.",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.b_heavy_final_molasses_rs.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.rs_percentage.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("B massecuite exhaustion",bodyContent)), //description
              new PdfPCell(new Phrase("Unit",bodyContent)), //uom
              new PdfPCell(new Phrase((massecuiteSummary.od_b_purity - molassesSummary.od_b_heavy_purity).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase((massecuiteSummary.td_b_purity - molassesSummary.td_b_heavy_purity).ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Cane Crushed",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("pH",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent))
            };
            foreach (PdfPCell content in lineFourfive)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line47

            PdfPCell[] lineFourSix = new PdfPCell[]{
              new PdfPCell(new Phrase("47",bodyContent)),
              new PdfPCell(new Phrase("Water consumption @ R.V.F",bodyContent)),
              new PdfPCell(new Phrase("%cane",bodyContent)), //uom
              new PdfPCell(new Phrase( dailyAnalyses.cane_crushed == 0? "0" : Math.Round(((dailyAnalyses.filter_water/dailyAnalyses.cane_crushed)*100),2).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.filter_water_percent_cane.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("C massecuite exhaustion",bodyContent)), //description
              new PdfPCell(new Phrase("Unit",bodyContent)), //uom
              new PdfPCell(new Phrase((massecuiteSummary.od_c_purity - ledgerData.final_molasses_purity) < 0 ? "0":(massecuiteSummary.od_c_purity - ledgerData.final_molasses_purity).ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase((massecuiteSummary.td_c_purity - ledgerDataPeriod.final_molasses_purity) < 0 ? "0" : (massecuiteSummary.td_c_purity - ledgerDataPeriod.final_molasses_purity).ToString(),bodyContent)), //to date
              //new PdfPCell(new Phrase("-",bodyContent)), //on date
              //new PdfPCell(new Phrase("-",bodyContent)), //to date
              new PdfPCell(new Phrase("Avg. Crush Day",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("Traces",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent))
            };
            foreach (PdfPCell content in lineFourSix)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line48

            PdfPCell[] lineFourtyseven = new PdfPCell[]{
                new PdfPCell(new Phrase("48",bodyContent)),
              new PdfPCell(new Phrase("House Stock(Sugar in Proc.)",bodyContent)),
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.sugar_in_process_qtl.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.sugar_in_process.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Total Sugar Bagged",bodyHeader)), //description
              new PdfPCell(new Phrase("Qtls.",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.total_sugar_bagged.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.total_sugar_bags.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Recovery",bodyHeader)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("Pol%",bodyHeader)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent))
            };
            foreach (PdfPCell content in lineFourtyseven)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }

            #endregion

            #region Body Line49

            PdfPCell[] lineFourtyeight = new PdfPCell[]{
                new PdfPCell(new Phrase("49",bodyContent)),
              new PdfPCell(new Phrase("Boiling House Recovery",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.boiling_house_recovery.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.boiling_house_recovery.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("L-31",bodyHeader)), //description
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase( hourlyAnalysesSummary.od_l31.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(hourlyAnalysesSummary.td_l31.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Losses",bodyHeader)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
              new PdfPCell(new Phrase("-",bodyContent)),
            };
            foreach (PdfPCell content in lineFourtyeight)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            PdfPCell lineFourtyeightMainDrainWaterHeader = new PdfPCell(new Phrase("Main Drain Water", bodyHeader));
            lineFourtyeightMainDrainWaterHeader.Colspan = 3;
            lineFourtyeightMainDrainWaterHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(lineFourtyeightMainDrainWaterHeader);

            #endregion

            #region Body Line50

            PdfPCell[] lineFourtnine = new PdfPCell[]{
                new PdfPCell(new Phrase("50",bodyContent)),
              new PdfPCell(new Phrase("Reduced B/H Recovery",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.reduced_boiling_house_recovery_deer.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.reduced_boiling_house_recovery_deer.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("M-31",bodyHeader)), //description
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase(hourlyAnalysesSummary.od_m31.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(hourlyAnalysesSummary.td_m31.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Pol",bodyHeader)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("ph",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
            };
            foreach (PdfPCell content in lineFourtnine)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion

            #region Body Line51

            PdfPCell[] lineFifty = new PdfPCell[]{
                new PdfPCell(new Phrase("51",bodyContent)),
              new PdfPCell(new Phrase("Clerification Eff.",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(ledgerData.clerification_efficiency.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(ledgerDataPeriod.clerification_efficiency.ToString().ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("S-31",bodyHeader)), //description
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase(hourlyAnalysesSummary.od_s31.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(hourlyAnalysesSummary.td_s31.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Stoppage(Hr-Min%)",bodyHeader)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("Traces",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
            };
            foreach (PdfPCell content in lineFifty)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }
            #endregion
            #region Body Line52

            PdfPCell[] lineFiftyOne = new PdfPCell[]{
                new PdfPCell(new Phrase("52",bodyContent)),
              new PdfPCell(new Phrase("Condensate Recovery",bodyContent)),
              new PdfPCell(new Phrase("%",bodyContent)), //uom
              new PdfPCell(new Phrase(dailyAnalyses.exhaust_condensate_recovery.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(dailyAnalysesSummary.exhaust_condensate_recovery.ToString(),bodyContent)),// to date
              new PdfPCell(new Phrase("Raw Sugar",bodyHeader)), //description
              new PdfPCell(new Phrase("Qtls",bodyContent)), //uom
              new PdfPCell(new Phrase(hourlyAnalysesSummary.od_raw_sugar.ToString(),bodyContent)), //on date
              new PdfPCell(new Phrase(hourlyAnalysesSummary.td_raw_sugar.ToString(),bodyContent)), //to date
              new PdfPCell(new Phrase("Sugar Production",bodyHeader)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("Pol%",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
              new PdfPCell(new Phrase("",bodyContent)),
            };
            foreach (PdfPCell content in lineFiftyOne)
            {
                content.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(content);
            }


            #endregion
            // at last adding the final table to pdf document
            pdfDoc.Add(table);
            pdfDoc.Close();
        }

     
        private string ConvertMinutesToHours(decimal minutes)
        {
            //int x = Convert.ToDateTime(minutes / 60).Hour;

            decimal hours = minutes/60 ;
            int min = Convert.ToInt32(minutes%60);
            return Math.Truncate(hours).ToString().PadLeft(2,'0') + ":" + min.ToString().PadLeft(2,'0');
        }
    }
}
