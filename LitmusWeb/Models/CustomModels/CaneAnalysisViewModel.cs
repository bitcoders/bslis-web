using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models.CustomModels
{
    public class CaneAnalysisViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Unit Code")]
        public int UnitCode { get; set; }

        [Display(Name = "Sample Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime SampleDate { get; set; }

        [Display(Name = "Analysis Code")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime AnalysisDate { get; set; }

        [Display(Name = "Zone Code")]
        public int ZoneCode { get; set; }

        [Display(Name = "Zone Name")]
        public string ZoneName { get; set; }

        [Display(Name = "Village Code")]
        public int VillageCode { get; set; }

        [Display(Name = "Village Name")]
        public string VillageName { get; set; }

        [Display(Name = "Grower Code")]
        public int GrowerCode { get; set; }


        [Display(Name = "Grower Name")]
        public string GrowerName { get; set; }


        [Display(Name = "Relative Name")]
        public string RelativeName { get; set; }

        [Display(Name = "Variety Code")]
        public int VarietyCode { get; set; }

        [Display(Name = "Variety Name")]
        public string VarietyName { get; set; }

        [Display(Name = "Cane Type")]
        public string CaneType { get; set; }

        [Display(Name = "Land Position")]
        public int LandPosition { get; set; }

        [Display(Name = "Field Position")]
        public string FieldCondition { get; set; }

        [Display(Name = "Juice Percent")]
        public Nullable<decimal> JuicePercent { get; set; }

        [Display(Name = "Brix")]
        public Nullable<decimal> Brix { get; set; }

        [Display(Name = "Pol")]
        public Nullable<decimal> Pol { get; set; }

        [Display(Name = "Purity")]
        public Nullable<decimal> Purity { get; set; }

        [Display(Name = "Pol In Cane (Today)")]
        public Nullable<decimal> PolInCaneToday { get; set; }

        [Display(Name = "Rec. By W.Cap")]
        public Nullable<decimal> RecoveryByWCapToday { get; set; }

        [Display(Name = "Rec. by Mol Pty")]
        public Nullable<decimal> RecoveryByMolPurityToday { get; set; }

        [Display(Name = "Prv. Harvesting Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public Nullable<System.DateTime> PreviousSeasonHarvestingDate { get; set; }

        [Display(Name = "Season Code")]
        public int SeasonCode { get; set; }
    }
}