using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class MasterVehicleModel
    {
        [Required]
        [Display(Name = "Company Code", Prompt = "Enter Company Code")]
        public Nullable<byte> CompanyCode { get; set; }

        [Required]
        [Display(Name = "Unit Code", Prompt = "Enter Unit Code")]
        public Nullable<int> UnitCode { get; set; }

        [Key]
        [Required]
        [Display(Name = "Vehicle Code", Prompt = "Enter Vehicle Code.")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Vehicle Name", Prompt = "Enter Vehicle Name.")]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 15, maximum: 250, ErrorMessage = "Minimum weight must be between {1} and {2}")]
        [Display(Name = "Minimum Weight", Prompt = "Enter Minimum Weight of Vehicle.")]
        public Nullable<decimal> MinimumWeight { get; set; }

        [Required]
        [Display(Name = "Maximum Weight", Prompt = "Enter Maximum Weight of Vehicle.")]
        [Range(minimum: 15, maximum: 400, ErrorMessage = "Maximum weight must be between {1} and {2}")]
        public Nullable<decimal> MaximumWeight { get; set; }

        [Required]
        [Range(minimum: 15, maximum: 320, ErrorMessage = "Maximum weight must be between {1} and {2}")]
        [Display(Name = "Average Weight", Prompt = "Enter Average Weight of Vehicle.")]
        public Nullable<decimal> AverageWeight { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is vehicle active?")]
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public int id { get; set; }
    }

}