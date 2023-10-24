using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class ReportDetailsModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportDetailsModel()
        {
            this.ExcelReportTemplates = new HashSet<ExcelReportTemplateModel>();
        }
        [DisplayName("Code")]
        public int Code { get; set; }

        [DisplayName("Report Name")]

        [Required]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Report Format")]
        public string Formats { get; set; }

        [DisplayName("Is-Active")]
        public bool IsActive { get; set; }

        [DisplayName("Report Category")]
        public string ReportCategory { get; set; }

        [DisplayName("Report Schema Code")]
        public Nullable<byte> ReportSchemaCode { get; set; }

        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        [DisplayName("Is Template Based")]
        public Nullable<bool> IsTemplateBased { get; set; }

        [DisplayName("Template Path")]
        public string TemplatePath { get; set; }

        [DisplayName("Template Name")]
        public string TemplateFileName { get; set; }
        public virtual ICollection<ExcelReportTemplateModel> ExcelReportTemplates { get; set; }

        [DisplayName("No of Pages")]
        public Nullable<int> NoOfPages { get; set; }
        public virtual ReportSchemaModel ReportSchemaModel { get; set; }
    }
}