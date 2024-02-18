//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    
    public partial class usp_select_hourlyAnalyses_Result
    {
        public long id { get; set; }
        public int unit_code { get; set; }
        public int season_code { get; set; }
        public System.DateTime entry_Date { get; set; }
        public int entry_time { get; set; }
        public Nullable<int> new_mill_juice { get; set; }
        public Nullable<int> old_mill_juice { get; set; }
        public Nullable<int> juice_total { get; set; }
        public Nullable<int> new_mill_water { get; set; }
        public Nullable<int> old_mill_water { get; set; }
        public Nullable<int> water_total { get; set; }
        public Nullable<int> sugar_bags_L31 { get; set; }
        public Nullable<int> sugar_bags_L30 { get; set; }
        public Nullable<int> sugar_bags_L_total { get; set; }
        public Nullable<int> sugar_bags_M31 { get; set; }
        public Nullable<int> sugar_bags_M30 { get; set; }
        public Nullable<int> sugar_bags_M_total { get; set; }
        public Nullable<int> sugar_bags_S31 { get; set; }
        public Nullable<int> sugar_bags_S30 { get; set; }
        public Nullable<int> sugar_bags_S_total { get; set; }
        public Nullable<int> sugar_Biss { get; set; }
        public Nullable<int> sugar_raw { get; set; }
        public Nullable<int> sugar_bags_total { get; set; }
        public string cooling_trace { get; set; }
        public decimal cooling_pol { get; set; }
        public decimal cooling_ph { get; set; }
        public short standing_truck { get; set; }
        public short standing_trippler { get; set; }
        public short standing_trolley { get; set; }
        public short standing_cart { get; set; }
        public Nullable<decimal> un_crushed_cane { get; set; }
        public Nullable<decimal> crushed_cane { get; set; }
        public decimal cane_diverted_for_syrup { get; set; }
        public decimal diverted_syrup_quantity { get; set; }
        public int export_sugar { get; set; }
        public int MillDataID { get; set; }
        public long HourlyAnalysesNo { get; set; }
        public System.DateTime mill_data_entry_date { get; set; }
        public int mill_data_entry_time { get; set; }
        public decimal imbibition_water_temp { get; set; }
        public decimal exhaust_steam_temp { get; set; }
        public bool mill_biocide_dosing { get; set; }
        public bool mill_washing { get; set; }
        public bool mill_steaming { get; set; }
        public decimal sugar_bags_temp { get; set; }
        public decimal molasses_inlet_temp { get; set; }
        public decimal molasses_outlet_temp { get; set; }
        public decimal mill_hydraulic_pressure_one { get; set; }
        public decimal mill_hydraulic_pressure_two { get; set; }
        public decimal mill_hydraulic_pressure_three { get; set; }
        public decimal mill_hydraulic_pressure_four { get; set; }
        public decimal mill_hydraulic_pressure_five { get; set; }
        public decimal mill_load_one { get; set; }
        public decimal mill_load_two { get; set; }
        public decimal mill_load_three { get; set; }
        public decimal mill_load_four { get; set; }
        public decimal mill_load_five { get; set; }
        public decimal mill_rpm_one { get; set; }
        public decimal mill_rpm_two { get; set; }
        public decimal mill_rpm_three { get; set; }
        public decimal mill_rpm_four { get; set; }
        public decimal mill_rpm_five { get; set; }
    }
}
