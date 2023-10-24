using System.Collections.Generic;
namespace LitmusWeb.Models.CustomModels
{
    public class DailyReportViewModel
    {
        public List<ReportDetailsModel> reportDetailsModel { get; set; }
        public List<MasterSeasonModel> masterSeasonsModel { get; set; }
    }
}