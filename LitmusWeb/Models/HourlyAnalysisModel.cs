using Microsoft.Ajax.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class HourlyAnalysisModel
    {
        public long id { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public int unit_code { get; set; }
        [Display(Name = "Season")]
        public int season_code { get; set; }


        [Display(Name = "Entry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime entry_Date { get; set; }
        [Display(Name = "Time")]
        public int entry_time { get; set; }

        [Required]
        [Display(Name = "New Mill Juice")]
        [Range(minimum: 0, maximum: 650, ErrorMessage = "Value must be between{1} & {2}.")]
        public Nullable<int> new_mill_juice { get; set; }

        [Required]
        [Display(Name = "Old Mill Juice")]
        [Range(minimum: 0, maximum: 300, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> old_mill_juice { get; set; }

        [Required]
        [Display(Name = "Total Juice")]
        [Range(minimum: 0, maximum: 950, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> juice_total { get; set; }

        [Required]
        [Display(Name = "New Mill Water")]
        [Range(minimum: 0, maximum: 400, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> new_mill_water { get; set; }

        [Required]
        [Display(Name = "Old Mill Water")]
        [Range(minimum: 0, maximum: 150, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> old_mill_water { get; set; }

        [Required]
        [Display(Name = "Total Water")]
        [Range(minimum: 0, maximum: 550, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> water_total { get; set; }

        [Required]
        [Display(Name = "L-31")]
        [Range(minimum: 0, maximum: 700, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_L31 { get; set; }

        [Required]
        [Display(Name = "L-30")]
        [Range(minimum: 0, maximum: 700, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_L30 { get; set; }

        [Required]
        [Display(Name = "L-Total")]
        [Range(minimum: 0, maximum: 1400, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_L_total { get; set; }

        [Required]
        [Display(Name = "M-31")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_M31 { get; set; }

        [Required]
        [Display(Name = "M-30")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "Value must be between {1} & {2}.")]
        public int? sugar_bags_M30 { get; set; }

        [Required]
        [Display(Name = "M-Total")]
        [Range(minimum: 0, maximum: 2000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_M_total { get; set; }

        [Required]
        [Display(Name = "S-31")]
        [Range(minimum: 0, maximum: 800, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_S31 { get; set; }

        [Required]
        [Display(Name = "S-30")]
        [Range(minimum: 0, maximum: 800, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_S30 { get; set; }

        [Required]
        [Display(Name = "S-Total")]
        [Range(minimum: 0, maximum: 1600, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_S_total { get; set; }

        [Required]
        [Display(Name = "BISS")]
        [Range(minimum: 0, maximum: 500, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_Biss { get; set; }

        [Required]
        [Display(Name = "Raw Sugar")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_raw { get; set; }


        [Required]
        [Display(Name = "Total")]
        [Range(minimum: 0, maximum: 5000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<int> sugar_bags_total { get; set; }

        [Required]
        [Display(Name = "Trace")]
        public string cooling_trace { get; set; }

        [Required]
        [Display(Name = "Pol")]
        public decimal cooling_pol { get; set; }

        [Required]
        [Display(Name = "pH")]
        public decimal cooling_ph { get; set; }


        [Display(Name = "Created Date")]
        public System.DateTime? crtd_dt { get; set; }

        [Display(Name = "Created By")]
        public string crtd_by { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> updt_dt { get; set; }


        [Display(Name = "Updated By")]
        public string updt_by { get; set; }

        [Required]
        [Display(Name = "Trucks")]
        [Range(minimum: 0, maximum: 500, ErrorMessage = "Value must be between {1} & {2}.")]
        public short standing_truck { get; set; }

        [Required]
        [Display(Name = "Trippler")]
        [Range(minimum: 0, maximum: 1500, ErrorMessage = "Value must be between {1} & {2}.")]
        public short standing_trippler { get; set; }

        [Required]
        [Display(Name = "Trolley")]
        [Range(minimum: 0, maximum: 1500, ErrorMessage = "Value must be between {1} & {2}.")]
        public short standing_trolley { get; set; }

        [Required]
        [Display(Name = "Cart")]
        [Range(minimum: 0, maximum: 500, ErrorMessage = "Value must be between {1} & {2}.")]
        public short standing_cart { get; set; }

        [Required]
        [Display(Name = "Un-crushed")]
        [Range(minimum: 0, maximum: 135000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<decimal> un_crushed_cane { get; set; }

        [Required]
        [Display(Name = "Crushed")]
        [Range(minimum: 0, maximum: 135000, ErrorMessage = "Value must be between {1} & {2}.")]
        public Nullable<decimal> crushed_cane { get; set; }


        [Display(Name = "Cane Diverted For Syrup")]
        public decimal cane_diverted_for_syrup { get; set; } = 0;

        [Required]
        [Display(Name = "Diverted Syrup Quanity")]
        public decimal diverted_syrup_quantity { get; set; }

        [Required]
        [Display(Name ="Exported Sugar")]
        public int export_sugar { get; set; }
    }
}