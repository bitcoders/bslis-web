using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataAccess.CustomModels.Reports
{
    public class DashboardProcessedData
    {
        public int id { get; set; }
        public DateTime entry_date { get; set; }
        public int unit_code { get; set; }
        public string unit_name { get; set; }
        public int company_code { get; set; }
        public string  company_name { get; set; }
        public decimal cane_crushed { get; set; }
        public decimal estimated_sugar_percent_cane { get; set; }
        public decimal estimated_recovery_on_syrup { get; set; }
        public decimal estimated_sugar_percent_on_b_heavy { get; set; }
        public decimal estimated_sugar_percent_on_c_heavy { get; set; }
        public decimal estimated_sugar_percent_on_raw_sugar { get; set; }
        public decimal total_losses_percent_cane { get; set; }
        public decimal unknwon_loss_percent_cane { get; set; }
        public decimal steam_percent_cane { get; set; }
        public decimal total_bagasse_percent_cane { get; set; }
        public decimal total_sugar_bagged { get; set; }
        public decimal estimated_molasses_percent_cane { get; set; }
        public decimal fiber_percent_cane { get; set; }
        public decimal pol_in_cane { get; set; }
                                 
    }

    public class Result
    {
        public DateTime entry_date { get; set; }
        public string value { get; set; }

        public decimal cane_crushed { get; set; }
        public decimal estimated_sugar_percent_cane { get; set; }
        public decimal esimated_recovery_on_syrup { get; set; }
        public decimal estimated_sugar_percent_on_b_heavy { get; set; }

        public decimal total_losses_percent { get; set; }

        public decimal unknwown_loss { get; set; }
        public decimal steam_percent_cane { get; set; }
        public decimal total_bagasse_percent_cane { get; set; }
        public decimal total_sugar_bagged { get; set; }
        public decimal estimated_molasses_percent_cane { get; set; }
        public decimal fiber_percent_cane { get; set; }
        public decimal pol_in_cane { get; set; }
    }

    public class UnitWiseResult
    {
        public string unit_name { get; set; }
        public List<Result> results { get; set; }
    }
}
