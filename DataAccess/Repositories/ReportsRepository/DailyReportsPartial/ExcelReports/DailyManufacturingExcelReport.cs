using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;

namespace DataAccess.Repositories.ReportsRepository.DailyReportsPartial.ExcelReports
{
    public class DailyManufacturingExcelReport
    {
        public string ExcelReportFile(int reportCode, int unitCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy hh-mm-ss") + ".xlsx";
            filePath = filePath + fileName;
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create();
            //ComparitiveReportAllUnitsExcel groupComparitive = new ComparitiveReportAllUnitsExcel();
            GenerateExcel(unitCode, reportCode, seasonCode, ReportDate, filePath);

            return filePath;
        }

        private bool GenerateExcel(int unitCode, int reportCode, int seasonCode, DateTime ReportDate, string filePath)
        {
            ExcelReportsTemplateRepository templateRepository = new ExcelReportsTemplateRepository();
            List<ExcelReportTemplate> template = new List<ExcelReportTemplate>();
            template = templateRepository.GetExcepReportTemplate(reportCode);

            GenerateExcelDesignRepository generateExcelLayout = new GenerateExcelDesignRepository();
            generateExcelLayout.GenerateExcel(reportCode, "DMR", filePath, unitCode, seasonCode, ReportDate);
            return true;

        }
    }
}
