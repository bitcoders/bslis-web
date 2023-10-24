using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterUnitAuthorityModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Unit Code")]
        public int UnitCode { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Valid From")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime valid_from { get; set; }

        [Required]
        [Display(Name = "Valid Till")]
        [DataType(DataType.Date)]
        //[CheckDateAttribute(ErrorMessage ="The date can't be less than current date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime valid_till { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}