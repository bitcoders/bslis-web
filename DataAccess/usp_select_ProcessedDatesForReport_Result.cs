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
    
    public partial class usp_select_ProcessedDatesForReport_Result
    {
        public int ID { get; set; }
        public int UnitCode { get; set; }
        public int SeasonCode { get; set; }
        public System.DateTime ProcessDate { get; set; }
        public System.DateTime FirstProcessedAt { get; set; }
        public string FirstProcessedBy { get; set; }
        public System.DateTime RecentProcessedAt { get; set; }
        public string RecentProcessedBy { get; set; }
        public Nullable<int> ProcessCount { get; set; }
        public Nullable<bool> IsFinalizedForReport { get; set; }
        public string DataFinalizedBy { get; set; }
        public Nullable<System.DateTime> DataFinalizedAt { get; set; }
        public Nullable<int> ReportStatusCode { get; set; }
    }
}
