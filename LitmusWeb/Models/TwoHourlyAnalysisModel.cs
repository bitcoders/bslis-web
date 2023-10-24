using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class TwoHourlyAnalysisModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public Nullable<int> Unit_Code { get; set; }

        [Required]
        [Display(Name = "Season")]
        public Nullable<int> season_code { get; set; }

        [Required]
        [Display(Name = "Entry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime Entry_Date { get; set; }

        [Required]
        [Display(Name = "Entry Time")]
        public int entry_Time { get; set; }

        [Required(ErrorMessage = "N.M. P.J. Brix Required")]
        [Display(Name = "Brix")]

        public decimal NM_Primary_Juice_Brix { get; set; }

        [Required(ErrorMessage = "N.M. P.J. Pol Required")]
        [Display(Name = "Pol")]
        public decimal NM_Primary_juice_pol { get; set; }

        [Required(ErrorMessage = "N.M. P.J. Purity Required")]

        [Display(Name = "N.M. P.J Purity")]
        public decimal nm_primary_juice_purity { get; set; }

        [Required(ErrorMessage = "N.M. P.J. pH Required")]
        [Display(Name = "N.M. P.J pH")]
        public decimal nm_primary_juice_ph { get; set; }

        [Display(Name = "Brix")]

        public decimal om_primary_juice_brix { get; set; }

        [Display(Name = "Pol")]
        public decimal om_primary_juice_pol { get; set; }

        [Display(Name = "Purity")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        public decimal om_primary_juice_purity { get; set; }


        [Display(Name = "pH")]
        public decimal om_primary_juice_ph { get; set; }

        [Required(ErrorMessage = "N.M. M.J. Brix Required")]
        [Display(Name = "Brix")]
        public decimal nm_mixed_juice_brix { get; set; }

        [Required(ErrorMessage = "N.M. M.J. Pol Required!")]
        [Display(Name = "Pol")]
        public decimal nm_mixed_juice_pol { get; set; }

        [Required(ErrorMessage = "N.M. M.J. Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal nm_mixed_juice_purity { get; set; }

        [Required(ErrorMessage = "N.M. M.J. pH Required!")]
        [Display(Name = "pH")]
        public decimal nm_mixed_juice_ph { get; set; }

        [Display(Name = "Brix")]
        public decimal om_mixed_juice_brix { get; set; }

        [Display(Name = "Pol")]
        public decimal om_mixed_juice_pol { get; set; }

        [Display(Name = "Purity")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        public decimal om_mixed_juice_purity { get; set; }


        [Display(Name = "pH")]
        public decimal om_mixed_juice_ph { get; set; }

        [Required(ErrorMessage = "N.M. L.J. Brix Required!")]
        [Display(Name = "Brix")]
        public decimal nm_last_juice_brix { get; set; }

        [Required(ErrorMessage = "N.M. L.J. Pol Required!")]
        [Display(Name = "Pol")]
        public decimal nm_last_juice_pol { get; set; }

        [Required(ErrorMessage = "N.M. L.J. Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal nm_last_juice_purity { get; set; }

        [Display(Name = "Brix")]
        public decimal om_last_juice_brix { get; set; }

        [Display(Name = "Pol")]
        public decimal om_last_juice_pol { get; set; }

        [Display(Name = "Purity")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        public decimal om_last_juice_purity { get; set; }

        [Required(ErrorMessage = "Oliver Juice Brix Required!")]
        [Display(Name = "Brix")]
        public decimal oliver_juice_brix { get; set; }

        [Required(ErrorMessage = "Oliver Juice Pol Required!")]
        [Display(Name = "Pol")]
        public decimal oliver_juice_pol { get; set; }

        [Required(ErrorMessage = "Oliver Juice pH Required!")]
        [Display(Name = "pH")]
        public decimal oliver_juice_ph { get; set; }

        [Required(ErrorMessage = "Oliver Juice Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal oliver_juice_purity { get; set; }

        [Required(ErrorMessage = "FCS Juice Brix Required!")]
        [Display(Name = "Brix")]
        public decimal fcs_juice_brix { get; set; }

        [Required(ErrorMessage = "FCS Juice Pol Required!")]
        [Display(Name = "Pol")]
        public decimal fcs_juice_pol { get; set; }

        [Required(ErrorMessage = "FCS Juice pH Required!")]
        [Display(Name = "pH")]
        public decimal fcs_juice_ph { get; set; }

        [Required(ErrorMessage = "FCS Juice Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal fcs_juice_purity { get; set; }


        [Required(ErrorMessage = "Clear Juice Brix Required!")]
        [Display(Name = "Brix")]
        public decimal clear_juice_brix { get; set; }
        [Required(ErrorMessage = "Clear Juice Pol Required!")]
        [Display(Name = "Pol")]
        public decimal clear_juice_pol { get; set; }
        [Required(ErrorMessage = "Clear Juice pH Required!")]
        [Display(Name = "pH")]
        public decimal clear_juice_ph { get; set; }
        [Required(ErrorMessage = "Clear Juice Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal clear_juice_purity { get; set; }

        [Required(ErrorMessage = "Un-sulphured J. Brix Required!")]
        [Display(Name = "Brix")]
        public decimal unsulphured_syrup_brix { get; set; }

        [Required(ErrorMessage = "Un-sulphured J. Pol Required!")]
        [Display(Name = "Pol")]
        public decimal unsulphured_syrup_pol { get; set; }

        [Required(ErrorMessage = "Un-sulphured J. pH Required!")]
        [Display(Name = "pH")]
        public decimal unsulphured_syrup_ph { get; set; }

        [Required(ErrorMessage = "Un-sulphured J. Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal unsulphured_syrup_purity { get; set; }

        [Required(ErrorMessage = "Sulphured J. Brix Required!")]
        [Display(Name = "Brix")]
        public decimal sulphured_syrup_brix { get; set; }

        [Required(ErrorMessage = "Sulphured J. Pol Required!")]
        [Display(Name = "Pol")]
        public decimal sulphured_syrup_pol { get; set; }

        [Required(ErrorMessage = "Sulphured J. pH Required!")]
        [Display(Name = "pH")]
        public decimal sulphured_syrup_ph { get; set; }

        [Required(ErrorMessage = "Sulphured J. Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal sulphured_syrup_purity { get; set; }

        [Required(ErrorMessage = "F.Molasses Brix Required!")]
        [Display(Name = "Brix")]
        public decimal final_molasses_brix { get; set; }

        [Required(ErrorMessage = "F.Molasses Pol Required!")]
        [Display(Name = "Pol")]
        public decimal final_molasses_pol { get; set; }

        [Required(ErrorMessage = "F.Molasses Purity Required!")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Purity be between {1} & {2}.")]
        [Display(Name = "Purity")]
        public decimal final_molasses_purity { get; set; }

        [Required(ErrorMessage = "F.Molasses Temp. Required!")]
        [Display(Name = "Molasses Temp")]
        public decimal final_molasses_temp { get; set; }

        [Required(ErrorMessage = "F.Molasses No. of Tanks Required!")]
        [Display(Name = "Molasses Tanks")]
        public decimal final_molasses_tanks { get; set; }

        [Required(ErrorMessage = "N.M. Bagasse Pol Required!")]
        [Display(Name = "Pol")]
        public decimal nm_bagasse_pol { get; set; }

        [Required(ErrorMessage = "N.M. Bagasse Moisture Required!")]
        [Display(Name = "Bagasse Moisture")]
        public decimal nm_bagasse_moisture { get; set; }

        [Required(ErrorMessage = "O.M. Bagasse Pol Required!")]
        [Display(Name = "Pol")]
        public decimal om_bagasse_pol { get; set; }

        [Required(ErrorMessage = "O.M. Bagasse Moisture Required!")]
        [Display(Name = "Bagasse Moisture")]
        public decimal om_bagasse_moisture { get; set; }

        [Display(Name = "Sample-1")]
        public decimal pol_pressure_cake_sample1 { get; set; }
        [Display(Name = "Sample-2")]
        public decimal pol_pressure_cake_sample2 { get; set; }
        [Display(Name = "Sample-3")]
        public decimal pol_pressure_cake_sample3 { get; set; }
        [Display(Name = "Sample-4")]
        public decimal pol_pressure_cake_sample4 { get; set; }
        [Display(Name = "Sample-5")]
        public decimal pol_pressure_cake_sample5 { get; set; }
        [Display(Name = "Sample-6")]
        public decimal pol_pressure_cake_sample6 { get; set; }
        [Display(Name = "Average")]

        [Required(ErrorMessage = "Press cake Average Required!")]
        public decimal pol_pressure_cake_average { get; set; }

        [Required(ErrorMessage = "Press cake Moisture Required!")]
        [Display(Name = "Moisture")]
        public decimal pol_pressure_cake_moisture { get; set; }

        [Required(ErrorMessage = "Press cake Composite Required!")]
        [Display(Name = "Composite")]
        public decimal pol_pressure_cake_composite { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime crtd_dt { get; set; }

        [Display(Name = "Created By")]
        public string crtd_by { get; set; }
    }
}