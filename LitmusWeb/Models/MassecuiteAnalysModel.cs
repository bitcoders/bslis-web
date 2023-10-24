using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MassecuiteAnalysModel
    {
        public long id { get; set; }
        [Display(Name = "Unit")]
        public Nullable<int> unit_code { get; set; }

        [Display(Name = "Season Code")]
        public Nullable<int> season_code { get; set; }

        [Required]
        [Display(Name = "Massecuite Type")]
        public int m_param_type_code { get; set; }

        [Display(Name = "Master Parameter Name", Description = "Provide Name of Master Parameter , of which subset is the current parameter")]
        public string MasterCategoryName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> m_entry_date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public System.TimeSpan m_entry_time { get; set; }

        [Required]
        [Display(Name = "H.L")]
        public decimal m_hl { get; set; }

        [Required]
        [Display(Name = "Pan No.")]
        public decimal m_pan_no { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public System.TimeSpan m_start_time { get; set; }

        [Required]
        [Display(Name = "Drop Time")]
        public System.TimeSpan m_drop_time { get; set; }

        [Required]
        [Display(Name = "Crystal NO")]
        public int m_crystal_no { get; set; }

        [Required]
        [Display(Name = "Brix")]
        public decimal m_brix { get; set; }

        [Required]
        [Display(Name = "Pol")]
        public decimal m_pol { get; set; }

        [Required]
        [Display(Name = "Purity")]
        public decimal m_purity { get; set; }


        [Display(Name = "Created Date")]
        public System.DateTime crtd_dt { get; set; }


        [Display(Name = "Created By")]
        public string crtd_by { get; set; }

        public Nullable<System.DateTime> updt_dt { get; set; }

        public string updt_by { get; set; }

        [Required]
        [Display(Name = "Icumsa Unit")]
        public Nullable<decimal> m_icumsa_unit { get; set; }

        public virtual MasterParameterSubCategoryModel MasterParameterSubCategory { get; set; }

    }
}