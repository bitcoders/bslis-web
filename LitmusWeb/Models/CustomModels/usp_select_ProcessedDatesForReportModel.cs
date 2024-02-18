using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LitmusWeb.Models.CustomModels
{
    public class usp_select_ProcessedDatesForReportModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Unit Code")]
        public int UnitCode { get; set; }
        [Display(Name="Unit Name")]
        public string Name { get; set; }
        [Display(Name = "Season Code")]
        public int SeasonCode { get; set; }
        [Display(Name = "Process Date")]
        public System.DateTime ProcessDate { get; set; }
        [Display(Name = "First Processed At")]
        public System.DateTime FirstProcessedAt { get; set; }
        [Display(Name = "Processed By")]
        public string FirstProcessedBy { get; set; }

        [Display(Name = "Last Processed At")]
        public System.DateTime RecentProcessedAt { get; set; }
        [Display(Name = "Last Processed By")]
        public string RecentProcessedBy { get; set; }
        [Display(Name = "Process Count(s)")]
        public Nullable<int> ProcessCount { get; set; }
        [Display(Name = "Is Finalized")]
        public bool IsFinalizedForReport { get; set; }
        [Display(Name = "Report Finalized By")]
        public string DataFinalizedBy { get; set; }
        [Display(Name = "Date of Report Finalization")]
        public Nullable<System.DateTime> DataFinalizedAt { get; set; }

        [Display(Name = "Report Status Code")]
        public Nullable<int> ReportStatusCode { get; set; }
        [Display(Name="Report Status")]
        public string Value { get; set; }
        [Display(Name = "Report Status Description")]
        public string Description { get; set; }
    }
}