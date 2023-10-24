using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class AndroidMenuesModel
    {
        [Display(Name = "Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Menu Code can not be empty.")]
        [Range(1, 100, ErrorMessage = "Code must be an integer and range must be {1} to {2}.")]
        public int Code { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Menu Name can not be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Menu Name length must be between {1} to {2}.")]
        public string Name { get; set; }
        [Display(Name = "Display Test")]
        [Required(ErrorMessage = "Display Text can not be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Display Text length must be between {1} to {2}.")]
        public string DisplayText { get; set; }

        [Display(Name = "Icon Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Icon Url can not be empty.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Icon Url length must be between {1} to {2}.")]
        public string IconUrl { get; set; }

        [Display(Name = "Controller Name")]

        public string ControllerName { get; set; }

        [Display(Name = "Action Name")]
        public string ActionName { get; set; }


        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> CreatedAt { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "API Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "API Url can not be empty.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "API Url length must be between {1} to {2}")]
        public string ApiUrl { get; set; }

        [Display(Name = "Report Header")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Report Header can not be empty.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Report Header length must be between {1} to {2}")]
        public string ReportHeader { get; set; }
    }
}