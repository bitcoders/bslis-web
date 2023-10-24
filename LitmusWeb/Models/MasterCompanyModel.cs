using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterCompanyModel
    {
        public int? Id { get; set; }
        public byte Code { get; set; }
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Company name must be between {2} and {1} character long!")]
        public string Name { get; set; }
        public string Icon { get; set; }
        [StringLength(250, MinimumLength = 4, ErrorMessage = "Registered Address must be between {2} and {1} character long!")]
        [Display(Name = "Registered Address")]
        public string RegisteredAddress { get; set; }
        [StringLength(12, MinimumLength = 8, ErrorMessage = "PAN No must be between {2} and {1} character long!")]
        [Display(Name = "Pan Number")]
        public string PANNO { get; set; }
        [Display(Name = "Tan Number")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "TAN No. must be between {2} and {1} character long!")]
        public string TANNO { get; set; }
        [Display(Name = "Active")]
        public Nullable<bool> IsActive { get; set; }
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}