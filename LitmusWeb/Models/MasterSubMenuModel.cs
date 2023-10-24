using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class MasterSubMenuModel
    {
        [Key]
        [Required]
        [Display(Name = "Sub-menu Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Master-menu Code")]
        public Nullable<int> MasterMenuCode { get; set; }

        [Required]
        [Display(Name = "Sub-menu Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Text")]
        public string DisplayText { get; set; }

        [Required]
        [Display(Name = "Display Sequence")]
        public byte DisplaySequence { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is active?", Description = "Is active?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

    }
}