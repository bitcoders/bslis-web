using System;
using System.ComponentModel.DataAnnotations;
/*
    Ticket ID       Owner           Date            Remarks
    [BSLIS-41]      Ravi Bhushan    12-02-2024      Added two columns 'AutoReportStartDate', 'AutoReportEndDate'
 */
namespace LitmusWeb.Models
{
    public class UnitSeasonModel
    {
        [Display(Name = "ID")]
        public System.Guid id { get; set; }

        [Display(Name = "Unit Code")]
        [Required]
        public int Code { get; set; }

        [Display(Name = "Season Code")]
        [Required]
        public int Season { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Crushing Start Date")]
        [Required]
        public Nullable<System.DateTime> CrushingStartDateTime { get; set; }

        [Display(Name = "Crushing End Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public Nullable<System.DateTime> CrushingEndDateTime { get; set; }

        [Display(Name = "New Mill Capacity")]
        [Required]
        public Nullable<decimal> NewMillCapacity { get; set; }

        [Display(Name = "Old Mill Capacity")]
        [Required]
        public Nullable<decimal> OldMillCapacity { get; set; }

        [Display(Name = "Day Start Time")]
        [Required]
        public Nullable<System.TimeSpan> ReportStartHourMinute { get; set; }

        [Display(Name = "Created At")]

        public Nullable<System.DateTime> CreatedAt { get; set; }

        [Display(Name = "Created By")]

        public string CreatedBy { get; set; }

        [Required]
        [Display(Name = "Disable Daily Process ")]

        public bool DisableDailyProcess { get; set; }

        [Required]
        [Display(Name = "Disable Add Record ")]
        public bool DisableAdd { get; set; }

        [Required]
        [Display(Name = "Disable Edit Record ")]
        public bool DisableUpdate { get; set; }
        //[BSLIS-41]
        [Display(Name ="Auto Report Start Date")]
        public Nullable<System.DateTime> AutoReportStartDate { get; set; }

        [Display(Name = "Auto Report End Date")]
        public Nullable<System.DateTime> AutoReportEndDate { get; set; }
        //[BSLIS-41]
        public virtual MasterSeasonModel MasterSeasonModel { get; set; }
        public virtual MasterUnitModel MasterUnitModel { get; set; }
    }
}