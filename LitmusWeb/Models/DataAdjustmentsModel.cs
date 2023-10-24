using DataAccess;
using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class DataAdjustmentsModel
    {
        public int id { get; set; }

        [Display(Name = "Unit Code")]
        public Nullable<int> a_unit_code { get; set; }

        [Display(Name = "Season Code")]
        public Nullable<int> a_season_code { get; set; }

        [Required]
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        public System.DateTime a_entry_date { get; set; }

        [Required]
        [Display(Name = "Sulpher")]
        public Nullable<decimal> a_sulpher { get; set; }

        [Required]
        [Display(Name = "Lime")]
        public Nullable<decimal> a_lime { get; set; }

        [Required]
        [Display(Name = "Phosphoric Acid")]
        public Nullable<decimal> a_phosphoric_acid { get; set; }

        [Required]
        [Display(Name = "Color Reducer")]
        public Nullable<decimal> a_color_reducer { get; set; }

        [Required]
        [Display(Name = "Megnafloe")]
        public Nullable<decimal> a_megnafloe { get; set; }

        [Required]
        [Display(Name = "Lub. Oil")]
        public Nullable<decimal> a_lub_oil { get; set; }

        [Required]
        [Display(Name = "Visc. Reducer")]
        public Nullable<decimal> a_viscosity_reducer { get; set; }

        [Required]
        [Display(Name = "Biocide")]
        public Nullable<decimal> a_biocide { get; set; }

        [Required]
        [Display(Name = "Lub. Greace")]
        public Nullable<decimal> a_lub_greace { get; set; }

        [Required]
        [Display(Name = "Boil. Chemical")]
        public Nullable<decimal> a_boiler_chemical { get; set; }

        [Required]
        [Display(Name = "Est. Sugar")]
        public Nullable<decimal> a_estimated_sugar { get; set; }

        [Required]
        [Display(Name = "Est. Molasses")]
        public Nullable<decimal> a_estimated_molasses { get; set; }

        [Required]
        [Display(Name = "Washing Sode")]
        public decimal a_washing_soda { get; set; }

        [Required]
        [Display(Name = "Hydr. Soda")]
        public decimal a_hydrolic_soda { get; set; }

        [Required]
        [Display(Name = "D-Scal. Chem")]
        public decimal a_de_scaling_chemical { get; set; }

        [Required]
        [Display(Name = "Seed Slurry")]
        public decimal a_seed_slurry { get; set; }

        [Required]
        [Display(Name = "A. Foamer")]
        public decimal a_anti_fomer { get; set; }

        [Required]
        [Display(Name = "Chem. BRS Cleaning")]
        public decimal a_chemical_for_brs_cleaning { get; set; }


        [Display(Name = "Active")]
        public bool a_is_active { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime crtd_dt { get; set; }

        [Display(Name = "Updated Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> updt_dt { get; set; }

        [Display(Name = "Created By")]
        public string crtd_by { get; set; }

        [Display(Name = "Updated By")]
        public string updt_by { get; set; }

       
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name ="Est. Cane For Syrup")]
        public Nullable<decimal> EstimatedCaneForSyrupDiversion { get; set; }

        public virtual MasterUnit MasterUnit { get; set; }
        public virtual MasterUnit MasterUnit1 { get; set; }
    }
}