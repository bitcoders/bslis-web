using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models.CustomModels
{
    public class ApiParameterModel
    {
        public int unit_code { get; set; }
        public int season_code { get; set; }
    }
    public class ApiParamUnitCode
    {
        public int unit_code { get; set; }
    }
    public class ApiSingleStringParam
    {
        public string StringParam { get; set; }
    }

    public class ApiDoubleStringParam
    {
        public string FirstString { get; set; }
        public string SecondString { get; set; }
    }
    public class ApiSingleIntegerParam
    {
        public int IntegerParam { get; set; }
    }


    public class ApiExcelTemplateParameters
    {
        public int ReportSchemaCode { get; set; }
        public int DatatypeCode { get; set; }
    }

    public class ApiExcelTemplateRowDetailsById
    {
        public int Id { get; set; }
    }

    public class ApiReportDetails
    {
        public int workingCode { get; set; }
    }

    public class RawDataDownloadParameters
    {
        [Required]
        [Display(Name = "Data Type")]
        public int analysisDataCode { get; set; }
        //public int unitCode { get; set; }
        //public int seasonCode { get; set; } 

        [Required]
        [Display(Name = "From Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}dd-MMM-yyyy")]
        public DateTime fromDate { get; set; }

        [Required]
        [Display(Name = "To Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}dd-MMM-yyyy")]
        public DateTime toDate { get; set; }

        [Display(Name = "Include Deleted")]
        public bool includeDelted { get; set; }
    }

    public class ValidationBeforeProcessParameter
    {
        [Required]
        public int unit_code { get; set; }

        [Required]
        public int season_code { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string process_date { get; set; }
    }

    public class GrowerApiParams
    {
        public int unitCode { get; set; }
        public int villageCode { get; set; }
        public int growerCode { get; set; }
    }

    public class VillageApiParams
    {
        public int unitCode { get; set; }
        public int villageCode { get; set; }
    }

    public class ReportParamUnitSeasonDate
    {
        public int unitCode { get; set; }
        public int seasonCode { get; set; }
        public DateTime reportate { get; set; }
    }

    public class DashboardApiParam
    {
        public string user_code { get; set; }
        public int company_code { get; set; }
        public int unit_code { get; set; }
        public int season_code { get; set; }
        public DateTime entry_date { get; set; }
    }

    public class DashboardProcessedDataApiParam
    {
        public string user_code { get; set; }
        public int company_code { get; set; }
        public int season_code { get; set; }
        public int history_days { get; set; }
    }

    public class ApiParamaApproveReport
    {
        public int id { get; set; }
        public string user_code { get; set; }
    }
}