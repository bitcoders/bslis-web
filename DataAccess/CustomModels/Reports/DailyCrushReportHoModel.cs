using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels.Reports
{
    public class DailyCrushReportHoModel
    {
        public string ReportName { get; set; }

        public DateTime ReportDate { get; set; }
        public int UnitCode { get; set; }
        public string UnitName { get; set; }
        public string NewMillHourWorked { get; set; }

        public string OldMillHourWorked { get; set; }

        public Nullable<decimal> TotalCaneCrushedOnDate { get; set; }
        public Nullable<decimal> TotalCaneCrushedToDate { get; set; }
        public Nullable<decimal> CaneDivertedForEthonol { get; set; }

        public Nullable<decimal> ActualRecoveryOnDate { get; set; }
        public Nullable<decimal> ActualRecoveryToDate { get; set; }

        public Nullable<decimal> RecoveryOnDatePreviousYear { get; set; }
        public Nullable<decimal> RecoveryOnSyrup { get; set; }
        public Nullable<decimal> RecoveryOnBHeavy { get; set; }
        public Nullable<decimal> RecoveryOnCHeavy { get; set; }

        public Nullable<decimal> MolassesPurity { get; set; }

        public Nullable<decimal> NewMillBagassePol { get; set; }
        public Nullable<decimal> OldMillBagassePol { get; set; }
        public Nullable<decimal> TotalLoss { get; set; }
        public Nullable<decimal> MolassesPercentCane { get; set; }
        public Nullable<decimal> PrimaryJuiceBrix { get; set; }
        public Nullable<decimal> PrimaryJuicePurity { get; set; }
        public Nullable<decimal> PolInCane { get; set; }
        public Nullable<decimal> FiberPercentCane { get; set; }
        public Nullable<decimal> WhiteSugarProducedOnDate { get; set; }
        public Nullable<decimal> WhiteSugarProducedToDate { get; set; }
        public Nullable<decimal> RawSugarProducedOnDate { get; set; }
        public Nullable<decimal> RawSugarProducedToDate { get; set; }

        public Nullable<decimal> CaneEarlyOnDate { get; set; }
        public Nullable<decimal> CaneEarlyToDate { get; set; }

        public Nullable<decimal> CaneRejectedOnDate { get; set; }
        public Nullable<decimal> CaneRejectedToDate { get; set; }

        public Nullable<decimal> CaneBadOnDate { get; set; }
        public Nullable<decimal> CaneBadToDate { get; set; }

        /// Distillery Data
        public Nullable<decimal> RectifiedSpirit { get; set; }
        public Nullable<decimal> AbsoluteAlcohol { get; set; }
        public Nullable<decimal> AbsoluteSyrup { get; set; }
        public Nullable<decimal> ENA { get; set; }
        public Nullable<decimal> TotalProductionOnDate { get; set; }
        public Nullable<decimal> TotalProductionToDate { get; set; }
        public Nullable<decimal> TotalProductionForMonth { get; set; }
        public Nullable<decimal> RectifiedSpiritRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholRecovery { get; set; }
        public Nullable<decimal> RainFallInch { get; set; }

        public int IcumsaL31 { get; set; }
        public int IcumsaL30 { get; set; }
        public int IcumsaM31 { get; set; }
        public int IcumsaM30 { get; set; }
        public int IcumsaS31 { get; set; }
        public int IcumsaS30 { get; set; }
        public int IcumsaBiss { get; set; }

        public Nullable<decimal> PowerExportSugarOnDate { get; set; }
        public Nullable<decimal> PowerExportDistilleryOnDate { get; set; }

        public Nullable<decimal> PowerExportMonth { get; set; }
        

        public Nullable<decimal> PowerExportYear { get; set; }
        


    }
}
