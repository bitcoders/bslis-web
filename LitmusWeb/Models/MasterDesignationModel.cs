using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterDesignationModel
    {
        [Required]
        [Display(Name = "Designation Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Department Code")]
        public int DepartmentCode { get; set; }

        [Required]
        [Display(Name = "Designation Name")]
        [RegularExpression(@"^[a-z A-Z ]*$", ErrorMessage = "Only alphabets are allowed!")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Desination name can contain {2} to {1} characters only")]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}