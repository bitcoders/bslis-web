using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels
{
    public class CaneAnalysisViewModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }
        public DateTime SampleDate { get; set; }
        public System.DateTime AnalysisDate { get; set; }
        public int ZoneCode { get; set; }

        public string ZoneName { get; set; }
        public int VillageCode { get; set; }

        public string VillageName { get; set; }

        public int GrowerCode { get; set; }

        public string GrowerName { get; set; }

        public string RelativeName { get; set; }
        public int VarietyCode { get; set; }
        public string VarietyName { get; set; }
        public string CaneType { get; set; }
        public int LandPosition { get; set; }
        public string FieldCondition { get; set; }
        public Nullable<decimal> JuicePercent { get; set; }
        public Nullable<decimal> Brix { get; set; }
        public Nullable<decimal> Pol { get; set; }
        public Nullable<decimal> Purity { get; set; }
        public Nullable<decimal> PolInCaneToday { get; set; }
        public Nullable<decimal> RecoveryByWCapToday { get; set; }
        public Nullable<decimal> RecoveryByMolPurityToday { get; set; }
        public Nullable<System.DateTime> PreviousSeasonHarvestingDate { get; set; }
        public int SeasonCode { get; set; }
    }
}
