using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class CaneAnalysisModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }

        [Required]
        [Display(Name = "Sample Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime SampleDate { get; set; }

        [Required]
        [Display(Name = "Analysis Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MM-yyyy")]
        public System.DateTime AnalysisDate { get; set; }

        [Required]
        [Display(Name = "Zone Code")]
        public int ZoneCode { get; set; }

        [Required]
        [Display(Name = "Village Code")]
        public int VillageCode { get; set; }

        [Required]
        [Display(Name = "Grower Code")]
        public int GrowerCode { get; set; }

        [Required]
        [Display(Name = "Variety Code")]
        public int VarietyCode { get; set; }

        [Required]
        [Display(Name = "Cane Type")]
        public int CaneType { get; set; }

        [Required]
        [Display(Name = "Land Position")]
        public int LandPosition { get; set; }

        [Required]
        [Display(Name = "Field Condition")]
        public string FieldCondition { get; set; }

        [Required]
        [Display(Name = "Juice Percent")]
        public Nullable<decimal> JuicePercent { get; set; }

        [Required]
        [Display(Name = "Brix")]
        public Nullable<decimal> Brix { get; set; }

        [Required]
        [Display(Name = "Pol")]
        public Nullable<decimal> Pol { get; set; }

        [Required]
        [Display(Name = "Purity")]
        public Nullable<decimal> Purity { get; set; }

        [Required]
        [Display(Name = "Pol in Cane")]
        public Nullable<decimal> PolInCaneToday { get; set; }

        [Required]
        [Display(Name = "Rec. By W.Cap")]
        public Nullable<decimal> RecoveryByWCapToday { get; set; }

        [Required]
        [Display(Name = "Rec. By Mol Pty.")]
        public Nullable<decimal> RecoveryByMolPurityToday { get; set; }

        [Required]
        [Display(Name = "Prv. Harvesting Date")]
        public System.DateTime PreviousSeasonHarvestingDate { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        [Display(Name = "Irrigated Days")]
        public Nullable<int> IrrigatedDays { get; set; }
        [Display(Name = "Season Code")]
        public int SeasonCode { get; set; }

        [Display(Name = "Max. Temp")]
        public Nullable<decimal> MaxTemperature { get; set; }

        [Display(Name = "Min. Temp")]
        public Nullable<decimal> MinTemperature { get; set; }

        [Display(Name = "Humidity")]
        public Nullable<decimal> Humidity { get; set; }
    }
}