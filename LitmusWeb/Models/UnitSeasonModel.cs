using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Crushing Start Date")]
        [Required]
        public Nullable<System.DateTime> CrushingStartDateTime { get; set; }

        [Display(Name = "Crushing End Date")]
        [Required]
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

        public virtual MasterSeasonModel MasterSeasonModel { get; set; }
        public virtual MasterUnitModel MasterUnitModel { get; set; }
    }
}