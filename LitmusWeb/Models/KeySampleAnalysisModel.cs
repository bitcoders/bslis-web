using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class KeySampleAnalysisModel
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public Nullable<int> unit_code { get; set; }

        [Required]
        [Display(Name = "Pan Number")]
        [Range(minimum: 0, maximum: 25, ErrorMessage = "Pan Number must be between {1} and {2}")]
        public short pan_number { get; set; }

        [Required]
        [Display(Name = "Entry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public System.DateTime entry_date { get; set; }

        [Required]
        [Display(Name = "Entry Time")]
        public System.TimeSpan entry_time { get; set; }

        [Required]
        [Display(Name = "Sugar Stage")]
        public int sugar_stage { get; set; }

        [Display(Name = "Sugar Stage Name")]
        public string sugar_stage_name { get; set; }

        [Required]
        [Display(Name = "Brix")]
        [Range(minimum: 0, maximum: 500, ErrorMessage = "Brix value must be between {1} and {2}")]
        public decimal brix { get; set; }

        [Required]
        [Display(Name = "Pol")]
        [Range(minimum: 0, maximum: 500, ErrorMessage = "Pol value must be between {1} and {2}")]
        public decimal pol { get; set; }

        [Required]
        [Display(Name = "Purity")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity must be between {1} and {2}")]
        public decimal purity { get; set; }

        [Display(Name = "Created By")]
        public string crtd_by { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime crtd_dt { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int season_code { get; set; }

        //public virtual MasterParameterSubCategory MasterParameterSubCategory { get; set; }
    }
}