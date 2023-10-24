using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterDepartmentModel
    {
        [Required]
        [Display(Name = "Department Code")]
        //[RegularExpression("^[0-9]*$",ErrorMessage ="Only Number allowed!")]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "Numbers betwwen {0} to {1} allowed")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Only alphabets are allowed!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}