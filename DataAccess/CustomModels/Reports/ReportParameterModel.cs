using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels.Reports
{
    public class ReportParameterModel
    {
        public int UnitCode { get; set; }
        public int SeasonCode { get; set; }
        public DateTime ReportDate { get; set; }

        public int ReportCode { get; set; }
    }
}
