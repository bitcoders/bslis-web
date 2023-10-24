using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterStoppageSubTypeModel
    {
        [Key]
        [Required]
        [Display(Name = "Code", Description = "A unique code for Stoppage Type.")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Master Stoppage Code", Description = "Code of Master Stoppage under which category current stoppage type falls.")]
        public Nullable<int> MasterStoppageCode { get; set; }

        [Required]
        [Display(Name = "Name", Description = "A unique name for Stoppage Category.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is stoppage active?", Description = "Is stoppage active?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Description", Description = "Description of Stoppage category")]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Group with Sub-Stoppage")]
        public int SubStoppageGroup { get; set; }

        [Display(Name = "Stoppage Type")]
        [Range(minimum: 1, maximum: 2, ErrorMessage = "Stoppage Type Code must be {1} or {2}")]
        public int StoppageType { get; set; }
    }
}