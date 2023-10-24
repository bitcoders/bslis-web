using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterUnitModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public byte CompanyCode { get; set; }

        [Display(Name = "Unit Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Unit name must be between {2} and {1} character long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be between {2} and {1} character long")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int CrushingSeason { get; set; }

        //[Required]
        [Display(Name = "Report Start Time")]
        //[RegularExpression(@"^([0-1]\d|2[0-4]):([0-0])\d:([0-0]\d)$",ErrorMessage ="Report Start Time must be between 0 to 24 Hrs.")]
        [Range(0, 23, ErrorMessage = "Hours can be between {1} and {2} only.")]
        public int ReportStartTime { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Crushing Start")]
        public System.DateTime CrushingStartDate { get; set; }

        //[Required]
        [Display(Name = "Crushing End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> CrushingEndDate { get; set; }

        //[Required]
        //[RegularExpression(@"^([0-9]|1[0-9]|2[0-4])$", ErrorMessage ="Hours must be between 0 to 24.")]
        [Range(0, 24, ErrorMessage = "Hours can be between {1} and {2} only.")]
        [Display(Name = "Day Hours")]
        public Nullable<int> DayHours { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Entry Date")]
        public System.DateTime EntryDate { get; set; }

        [Required]
        [Display(Name = "Process Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime ProcessDate { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "New Mill")]
        public Nullable<decimal> NewMillCapacity { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "Old Mill")]
        public Nullable<decimal> OldMillCapacity { get; set; }

        [Required]
        public Nullable<bool> IsActive { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime CreatedDate { get; set; }
        public string Createdy { get; set; }

        [Display(Name = "Crushing Start Time")]
        public Nullable<int> CrushingStartTime { get; set; }

        [Display(Name = "Crushing End Time")]
        public Nullable<int> CrushingEndTime { get; set; }

        [Display(Name = "Editing Allowed Days")]
        [Required]
        [Range(0, 365, ErrorMessage = "Editing days allowed range can be between {1} to {1} only!")]
        public Nullable<int> AllowedModificationDays { get; set; }

        [Required(ErrorMessage = "Crushing Start date & time is required!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Crushing Started At")]
        public Nullable<System.DateTime> CrushingStartDateTime { get; set; }


        [Required(ErrorMessage = "Crushing End date & time (could be expected) is required !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        //[RegularExpression(@"^(\d{4}-\d{2}-\d{2} \d{2}:\d{2})$", ErrorMessage ="Date time format should be YYYY-MM-DD HH:MM")]
        public Nullable<System.DateTime> CrushingEndDateTime { get; set; }

        [Required(ErrorMessage = "Report Start time is required!")]
        [Display(Name = "Report Started At")]
        public Nullable<System.TimeSpan> ReportStartHourMinute { get; set; }
    }


    /// <summary>
    /// MasterUnitModelForUnitAdmin class will be used for Unit based admin.
    /// Unit based admin can only make changes in selected fields
    /// like entry date, crushing start date, process date, working hours only
    /// as these fields changes daily on entry based.
    /// </summary>
    public class MasterUnitModelForUnitAdmin
    {
        [Display(Name = "Unit Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int CrushingSeason { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Crushing Start")]
        public System.DateTime CrushingStartDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Crushing End Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> CrushingEndDate { get; set; }

        [Required]
        //[RegularExpression(@"^([0-9]|1[0-9]|2[0-4])$", ErrorMessage ="Hours must be between 0 to 24.")]
        [Range(0, 24, ErrorMessage = "Hours can be between {1} and {2} only.")]
        [Display(Name = "Day Hours")]
        public Nullable<int> DayHours { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Entry Date")]
        public System.DateTime EntryDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Process Date")]
        public System.DateTime ProcessDate { get; set; }


        [Display(Name = "Crushing Started At")]
        public Nullable<System.DateTime> CrushingStartDateTime { get; set; }

        [Display(Name = "Crushing End At")]
        public Nullable<System.DateTime> CrushingEndDateTime { get; set; }

        [Display(Name = "Report Started At")]
        public Nullable<System.TimeSpan> ReportStartHourMinute { get; set; }

    }


    public class MasterUnitViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public byte CompanyCode { get; set; }

        [Display(Name = "Unit Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Unit name must be between {2} and {1} character long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be between {2} and {1} character long")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int CrushingSeason { get; set; }

        [Required]
        [Display(Name = "Report Start Time")]
        //[RegularExpression(@"^([0-1]\d|2[0-4]):([0-0])\d:([0-0]\d)$",ErrorMessage ="Report Start Time must be between 0 to 24 Hrs.")]
        [Range(0, 23, ErrorMessage = "Hours can be between {1} and {2} only.")]
        public int ReportStartTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Crushing Start")]
        public System.DateTime CrushingStartDate { get; set; }

        [Required]
        [Display(Name = "Crushing End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> CrushingEndDate { get; set; }

        [Required]
        //[RegularExpression(@"^([0-9]|1[0-9]|2[0-4])$", ErrorMessage ="Hours must be between 0 to 24.")]
        [Range(0, 24, ErrorMessage = "Hours can be between {1} and {2} only.")]
        [Display(Name = "Day Hours")]
        public Nullable<int> DayHours { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Entry Date")]
        public System.DateTime EntryDate { get; set; }

        [Required]
        [Display(Name = "Process Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime ProcessDate { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "New Mill")]
        public Nullable<decimal> NewMillCapacity { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "Old Mill")]
        public Nullable<decimal> OldMillCapacity { get; set; }

        [Required]
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Crushing Start Time")]
        public Nullable<int> CrushingStartTime { get; set; }

        [Display(Name = "Crushing End Time")]
        public Nullable<int> CrushingEndTime { get; set; }

        [Display(Name = "Crushing Started At")]
        public Nullable<System.DateTime> CrushingStartDateTime { get; set; }

        [Display(Name = "Crushing End At")]
        public Nullable<System.DateTime> CrushingEndDateTime { get; set; }

        [Display(Name = "Report Started At")]
        public Nullable<System.TimeSpan> ReportStartHourMinute { get; set; }

    }


    public class MasterUnitApiViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public byte CompanyCode { get; set; }

        [Display(Name = "Unit Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Unit name must be between {2} and {1} character long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be between {2} and {1} character long")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int CrushingSeason { get; set; }

        [Required]
        [Display(Name = "Report Start Time")]
        //[RegularExpression(@"^([0-1]\d|2[0-4]):([0-0])\d:([0-0]\d)$",ErrorMessage ="Report Start Time must be between 0 to 24 Hrs.")]
        [Range(0, 23, ErrorMessage = "Hours can be between {1} and {2} only.")]
        public Nullable<System.TimeSpan> ReportStartHourMinute { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Crushing Start")]
        public System.DateTime CrushingStartDate { get; set; }

        [Required]
        [Display(Name = "Crushing End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> CrushingEndDate { get; set; }

        [Required]
        //[RegularExpression(@"^([0-9]|1[0-9]|2[0-4])$", ErrorMessage ="Hours must be between 0 to 24.")]
        [Range(0, 24, ErrorMessage = "Hours can be between {1} and {2} only.")]
        [Display(Name = "Day Hours")]
        public Nullable<int> DayHours { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Entry Date")]
        public System.DateTime EntryDate { get; set; }

        [Required]
        [Display(Name = "Process Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime ProcessDate { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "New Mill")]
        public Nullable<decimal> NewMillCapacity { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity can't be more than 100")]
        [Display(Name = "Old Mill")]
        public Nullable<decimal> OldMillCapacity { get; set; }

        [Required]
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Crushing Start Time")]
        public Nullable<int> CrushingStartTime { get; set; }

        [Display(Name = "Crushing End Time")]
        public Nullable<int> CrushingEndTime { get; set; }

        [Display(Name = "Editing Allowed Days")]
        [Required]
        [Range(0, 365, ErrorMessage = "Editing days allowed range can be between {1} to {1} only!")]
        public Nullable<int> AllowedModificationDays { get; set; }

        public string UpdatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}dd-MM-yyyy")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

    }

    public class MasterUnitApiUnitRights
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Code")]
        public byte CompanyCode { get; set; }

        [Display(Name = "Unit Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Unit name must be between {2} and {1} character long!")]
        public string Name { get; set; }
    }

    public class MasterUnitCustomModel
    {
        public DateTime LatestProcessedDate { get; set; }
    }
}