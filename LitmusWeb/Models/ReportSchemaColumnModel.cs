using System;

namespace LitmusWeb.Models
{
    public class ReportSchemaColumnModel
    {
        public long Code { get; set; }
        public byte SchemaCode { get; set; }
        public string ColumnText { get; set; }
        public string SearchKeyword { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ReportSchemaModel ReportSchemaModel { get; set; }
    }
}