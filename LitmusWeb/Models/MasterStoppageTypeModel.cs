using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterStoppageTypeModel
    {
        [Key]
        [Required]
        [Display(Name = "Code", Description = "A unique code for Stoppage Category.")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name", Description = "A unique name for Stoppage Category.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is stoppage active?", Description = "Is stoppage active?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Description", Description = "Description of Stoppage Type")]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}