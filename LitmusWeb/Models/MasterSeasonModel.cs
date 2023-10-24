using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class MasterSeasonModel
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Season Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numeric Value is allowed!")]
        public int SeasonCode { get; set; }

        [Required]
        [Display(Name = "Season Year")]
        public string SeasonYear { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }


        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}