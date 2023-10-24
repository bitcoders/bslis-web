using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels
{
    public class ProcessCalculationModel
    {
        public int UnitCode { get; set; }
        public int SeasonCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
