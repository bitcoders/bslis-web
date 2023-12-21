using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels
{
    public class DashboardModel
    {
        public int UnitCode { get; set; }
        public string UnitName { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> CaneCrushed { get; set; }
        public Nullable<int> SugarBagsTotal { get; set; }
        public Nullable<int> WaterTotal { get; set; }
        public Nullable<int> JuiceTotal { get; set; }
        public decimal CaneDiverted { get; set; }
        public decimal DivertedSyrup { get; set; }
    }
}
