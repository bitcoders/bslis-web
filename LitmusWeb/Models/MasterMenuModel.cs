using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterMenuModel
    {
        [Key]
        [Required]
        [Display(Name = "Menu Code", Description = "A unique code for Master Menu.")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Menu Name", Description = "A unique name for Master Menu.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Text", Description = "A text which will be displayed in menu list.")]
        public string DisplayText { get; set; }

        [Required]
        [Display(Name = "Display Order", Description = "Sequence of menu item.")]
        public byte DisplaySequence { get; set; }

        [Required]
        [Display(Name = "Description", Description = "Description of the master menu item.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is vehicle active?", Description = "Is vehicle active?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Controller Name", Description = "This field is for Web Developer to identify the controller name in source code.")]
        public string ControllerName { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}