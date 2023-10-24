using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class MeltingAnalysModel
    {
        public int id { get; set; }
        public Nullable<int> unit_code { get; set; }
        public Nullable<int> season_code { get; set; }

        [Required]
        [Display(Name = "Melting Type")]
        public int m_code { get; set; }

        [Display(Name = "Melting Name")]
        public string melting_name { get; set; }


        [Required]
        [Display(Name = "Entry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public Nullable<System.DateTime> entry_date { get; set; }

        [Required]
        [Display(Name = "Entry Time")]
        public Nullable<System.TimeSpan> entry_time { get; set; }

        [Required]
        [Display(Name = "Brix")]
        public decimal brix { get; set; }

        [Required]
        [Display(Name = "Pol")]
        public decimal pol { get; set; }

        [Required]
        [Display(Name = "Purity")]
        public decimal purity { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime crtd_dt { get; set; }


        [Display(Name = "Creted By")]
        public string crtd_by { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> updt_dt { get; set; }


        [Display(Name = "Updated By")]
        public string updt_by { get; set; }

        [Required]
        [Display(Name = "Icumsa Unit")]
        public Nullable<decimal> icumsa_unit { get; set; }
        public virtual MasterParameterSubCategoryModel MasterParameterSubCategoryModel { get; set; }
    }
}