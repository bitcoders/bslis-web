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
    
    public partial class proc_stoppage_headwise_period_summary_Result
    {
        public int master_stoppage_code { get; set; }
        public string master_stoppage_name { get; set; }
        public Nullable<int> sub_stoppage_code { get; set; }
        public string sub_stoppage_name { get; set; }
        public Nullable<int> od_gross_duration_minutes { get; set; }
        public string od_gross_duration { get; set; }
        public Nullable<decimal> od_net_duration_minutes { get; set; }
        public string od_net_duration { get; set; }
        public Nullable<int> td_gross_duration_minutes { get; set; }
        public string td_gross_duration { get; set; }
        public Nullable<decimal> td_net_duration_minutes { get; set; }
        public string td_net_duration { get; set; }
    }
}
