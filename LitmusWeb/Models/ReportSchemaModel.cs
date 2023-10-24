using System;
using System.Collections.Generic;


namespace LitmusWeb.Models
{
    public class ReportSchemaModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportSchemaModel()
        {
            this.ReportSchemaColumnsModel = new HashSet<ReportSchemaColumnModel>();
        }

        public byte Code { get; set; }
        public string SysObjectName { get; set; }
        public string SysObjectDescripton { get; set; }
        public string SearchKeywords { get; set; }

        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string SchemaType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportSchemaColumnModel> ReportSchemaColumnsModel { get; set; }
    }
}