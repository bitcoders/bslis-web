using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MolassesAnalysModel
    {
        public int id { get; set; }

        [Display(Name = "Unit")]
        public Nullable<int> Unit_Code { get; set; }

        [Display(Name = "Season")]
        public Nullable<int> season_code { get; set; }

        [Required]
        [Display(Name = "Date")]
        public System.DateTime mo_entry_date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public string mo_entry_time { get; set; }

        [Required]
        [Display(Name = "Molasses Type")]
        public int mo_code { get; set; }

        [Display(Name = "Molasses Name")]
        public string molasses_name { get; set; }
        [Required]
        [Display(Name = "Birx")]
        public decimal mo_brix { get; set; }

        [Required]
        [Display(Name = "Pol")]
        public decimal mo_pol { get; set; }

        [Required]
        [Display(Name = "Purity")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity must be between {1} and {2}")]
        public decimal mo_purity { get; set; }


        [Display(Name = "Created Date")]
        public System.DateTime mo_crtd_date { get; set; }


        [Display(Name = "Created By")]
        public string mo_crtd_by { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> mo_updt_dt { get; set; }

        [Display(Name = "Updated By")]
        public string mo_updt_by { get; set; }

        [Required]
        [Display(Name = "ICUMSA Unit")]
        public Nullable<decimal> mo_icumsa_unit { get; set; }

        public virtual MasterParameterSubCategoryModel MasterParameterSubCategoryModel { get; set; }
    }
}