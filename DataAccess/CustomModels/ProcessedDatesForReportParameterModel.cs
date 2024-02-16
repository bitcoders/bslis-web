using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels
{
    public class ProcessedDatesForReportParameterModel
    {
        public int unit_code { get; set; }
        public int season_code { get; set; }
        public DateTime process_date { get; set; }
        public string processed_by { get; set; }
        public int status_code { get; set; }
        public string status_message { get; set; }
    }
}
