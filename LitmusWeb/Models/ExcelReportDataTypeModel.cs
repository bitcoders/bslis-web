
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class ExcelReportDataTypeModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExcelReportDataTypeModel()
        {
            this.ExcelReportTemplates = new HashSet<ExcelReportTemplateModel>();
        }


        [Required(ErrorMessage = "Data Type code can not be blank!")]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExcelReportTemplateModel> ExcelReportTemplates { get; set; }
    }
}