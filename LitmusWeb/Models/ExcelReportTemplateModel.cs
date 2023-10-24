namespace LitmusWeb.Models
{
    public class ExcelReportTemplateModel
    {
        public int Id { get; set; }
        public int ReportCode { get; set; }
        public int DataType { get; set; }
        public string CellFrom { get; set; }
        public string CellTo { get; set; }
        public string Value { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public string FontColor { get; set; }
        public string BackGroundColor { get; set; }
        public string NumerFormat { get; set; }
        public string HelpText { get; set; }

        public string DataTypeName { get; set; }
        public string ReportName { get; set; }

        public int ReportDetails_ReportCode { get; set; }
        public virtual ExcelReportDataTypeModel ExcelReportDataType { get; set; }
        public virtual ReportDetailsModel ReportDetails { get; set; }
    }
}

