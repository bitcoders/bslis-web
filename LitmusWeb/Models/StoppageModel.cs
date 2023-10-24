using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{


    public partial class StoppageModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Unit Code")]
        public Nullable<int> unit_code { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Entry Date")]
        public System.DateTime s_date { get; set; }

        [Display(Name = "Start Calendar Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime s_start_calendar_date { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public string s_start_time { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Mill code can be 0 or 1 only (0 ='New Mill' & 1 = 'Old Mill'")]
        [Display(Name = "Mill Code")]
        public int s_mill_code { get; set; }

        [Required]
        [Display(Name = "Stoppage Head")]
        public int s_head_code { get; set; }

        [Display(Name = "Stoppage Head Name")]
        public string s_head_name { get; set; }

        [Required]
        [Display(Name = "Stoppage Sub-head")]
        public int s_sub_head_code { get; set; }

        [Display(Name = "Stoppage Sub-head Name")]
        public string s_sub_head_name { get; set; }

        [Required]
        [Display(Name = "Remarks")]
        //[Range(1,250,ErrorMessage = "length for {0} must be between {1} to {2}")]
        public string s_comment { get; set; }

        [Display(Name = "Created By")]
        public string s_crtd_by { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public Nullable<System.DateTime> s_crtd_dt { get; set; }

        [Display(Name = "Sent SMS Count")]
        public byte sent_sms_count { get; set; }



        [Display(Name = "Season Code")]
        public Nullable<int> season_code { get; set; }
    }

    public class StoppageUpdateModel : StoppageModel
    {

        [Display(Name = "End Calendar Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> s_end_calendar_date { get; set; }

        [Display(Name = "End Time")]
        public string s_end_time { get; set; }

        [Display(Name = "Stoppage Duration (Min.)")]
        public Nullable<int> s_duration { get; set; }

        [Display(Name = "Stoppage Net Duration (Min.)")]
        public Nullable<decimal> s_net_duration { get; set; }

        [Display(Name = "Updated By")]
        public string s_updt_by { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> s_updt_dt { get; set; }

        [Display(Name = "Deleted")]
        public Nullable<bool> is_deleted { get; set; }

    }

    public class StoppageAllChangesModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Entry Date")]
        public System.DateTime s_date { get; set; }

        [Display(Name = "Start Calendar Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime s_start_calendar_date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Start Time is required.")]
        [Display(Name = "Start Time")]
        public string s_start_time { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Mill code can be 0 or 1 only (0 ='New Mill' & 1 = 'Old Mill'")]
        [Display(Name = "Mill Code")]
        public int s_mill_code { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Stoppage Category")]
        [Display(Name = "Stoppage Head")]


        public int s_head_code { get; set; }
        [Required]
        [Display(Name = "Stoppage Head Name")]
        public string s_head_name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Stoppage Reason")]
        [Display(Name = "Stoppage Sub-head")]
        public int s_sub_head_code { get; set; }
        [Required]
        [Display(Name = "Stoppage Sub-head Name")]
        public string s_sub_head_name { get; set; }

        [Required]
        [Display(Name = "Remarks")]
        //[Range(1, 250, ErrorMessage = "length for {0} must be between {1} to {2}")]
        public string s_comment { get; set; }

        [Display(Name = "Created By")]
        public string s_crtd_by { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public Nullable<System.DateTime> s_crtd_dt { get; set; }


        [Display(Name = "End Calendar Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> s_end_calendar_date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "End Time is required")]
        [Display(Name = "End Time")]
        public string s_end_time { get; set; }

        [Display(Name = "Stoppage Duration (Min.)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Duration can not be null or empty!")]
        public Nullable<int> s_duration { get; set; }

        [Display(Name = "Stoppage Net Duration (Min.)")]
        public Nullable<decimal> s_net_duration { get; set; }

        [Display(Name = "Updated By")]
        public string s_updt_by { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> s_updt_dt { get; set; }

        [Display(Name = "Deleted")]
        public Nullable<bool> is_deleted { get; set; }

    }

    public class StoppageHeadWiseSummaryModel
    {
        public Nullable<int> unit_code { get; set; }
        public Nullable<System.DateTime> report_date { get; set; }
        public Nullable<int> season_code { get; set; }
        public Nullable<decimal> NoCane { get; set; }
        public Nullable<decimal> Mechanical { get; set; }
        public Nullable<decimal> CoGen { get; set; }
        public Nullable<decimal> GeneralCleaning { get; set; }
        public Nullable<decimal> Festival { get; set; }
        public Nullable<decimal> Weather { get; set; }
        public Nullable<decimal> Process { get; set; }
        public Nullable<decimal> Miscellaneous { get; set; }
        public Nullable<decimal> Unknown { get; set; }
        public Nullable<decimal> TotalDuration { get; set; }
        public Nullable<decimal> nm_total_duration { get; set; }
        public Nullable<decimal> om_total_duration { get; set; }
        public Nullable<decimal> NoCaneGrossDuration { get; set; }
        public Nullable<decimal> MechanicalGrossDuration { get; set; }
        public Nullable<decimal> CoGenGrossDuration { get; set; }
        public Nullable<decimal> GeneralCleaningGrossDuration { get; set; }
        public Nullable<decimal> FestivalGrossDuration { get; set; }
        public Nullable<decimal> WeatherGrossDuration { get; set; }
        public Nullable<decimal> ProcessGrossDuration { get; set; }
        public Nullable<decimal> MiscellaneousGrossDuration { get; set; }
        public Nullable<decimal> UnknownGrossDuration { get; set; }
        public Nullable<decimal> TotalGrossDuration { get; set; }
        public Nullable<decimal> nm_gross_duration { get; set; }
        public Nullable<decimal> om_gross_duration { get; set; }
        public Nullable<decimal> actual_minutes_of_crushing { get; set; }
        public Nullable<decimal> actual_gross_minutes_of_crushing { get; set; }
        public Nullable<int> td_unit_code { get; set; }
        public Nullable<System.DateTime> td_report_date { get; set; }
        public Nullable<int> td_season_code { get; set; }
        public Nullable<decimal> td_NoCane { get; set; }
        public Nullable<decimal> td_Mechanical { get; set; }
        public Nullable<decimal> td_CoGen { get; set; }
        public Nullable<decimal> td_GeneralCleaning { get; set; }
        public Nullable<decimal> td_Festival { get; set; }
        public Nullable<decimal> td_Weather { get; set; }
        public Nullable<decimal> td_Process { get; set; }
        public Nullable<decimal> td_Miscellaneous { get; set; }
        public Nullable<decimal> td_Unknown { get; set; }
        public Nullable<decimal> td_TotalDuration { get; set; }
        public Nullable<decimal> td_nm_total_duration { get; set; }
        public Nullable<decimal> td_om_total_duration { get; set; }
        public Nullable<decimal> td_NoCaneGrossDuration { get; set; }
        public Nullable<decimal> td_MechanicalGrossDuration { get; set; }
        public Nullable<decimal> td_CoGenGrossDuration { get; set; }
        public Nullable<decimal> td_GeneralCleaningGrossDuration { get; set; }
        public Nullable<decimal> td_FestivalGrossDuration { get; set; }
        public Nullable<decimal> td_WeatherGrossDuration { get; set; }
        public Nullable<decimal> td_ProcessGrossDuration { get; set; }
        public Nullable<decimal> td_MiscellaneousGrossDuration { get; set; }
        public Nullable<decimal> td_UnknownGrossDuration { get; set; }
        public Nullable<decimal> td_TotalGrossDuration { get; set; }
        public Nullable<decimal> td_nm_gross_duration { get; set; }
        public Nullable<decimal> td_om_gross_duration { get; set; }
        public Nullable<decimal> td_actual_minutes_of_crushing { get; set; }
        public Nullable<decimal> td_actual_gross_minutes_of_crushing { get; set; }

    }

}