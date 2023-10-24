using System;

namespace LitmusWeb.Models.CustomModels
{
    public class AllRecoveriesViewModel
    {
        public Nullable<decimal> estimated_sugar_on_b_heavy_on_date { get; set; }
        public Nullable<decimal> estimated_sugar_on_c_heavy_on_date { get; set; }
        public Nullable<decimal> estimated_sugar_on_raw_sugar_on_date { get; set; }

        public Nullable<decimal> estimated_sugar_on_syrup_on_date { get; set; }

        public Nullable<decimal> estimated_sugar_on_b_heavy_to_date { get; set; }
        public Nullable<decimal> estimated_sugar_on_c_heavy_to_date { get; set; }
        public Nullable<decimal> estimated_sugar_on_raw_sugar_to_date { get; set; }
        public Nullable<decimal> estimated_sugar_on_syrup_to_date { get; set; }

        public decimal LossInCHeavyPercentCane { get; set; }
        public decimal RawSugarGain { get; set; }
        public decimal LossInBHeavyPercentCane { get; set; }

        public Nullable<decimal> SyrupDiversion { get; set; }

    }
}