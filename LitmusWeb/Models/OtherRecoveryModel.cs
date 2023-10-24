namespace LitmusWeb.Models
{
    public class OtherRecoveryModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }
        public int SeasonCode { get; set; }
        public System.DateTime TransDate { get; set; }
        public decimal NonSugarInCHeavyFinalMolasses { get; set; }
        public decimal CHeavyFinalMolasses { get; set; }
        public decimal LossInCHeavyFinalMolasses { get; set; }
        public decimal LossInCHeavyPercentCane { get; set; }
        public decimal EstimatedSugarPercentOnCHeavy { get; set; }
        public decimal RawSugarGainQtl { get; set; }
        public decimal EstimatedSugarPercentOnRawSugar { get; set; }
        public decimal NonSugarInBHeavyFinalMolasses { get; set; }
        public decimal BHeavyFinalMolasses { get; set; }
        public decimal LossInBHeavyFinalMolasses { get; set; }
        public decimal LossInBHeavyFinalMolassesPercentCane { get; set; }
        public decimal EstimatedSugarPercentOnBHeavy { get; set; }
        public decimal SyrupDiversion { get; set; }
        public decimal EstimatedRecoveryOnSyrup { get; set; }
    }
}