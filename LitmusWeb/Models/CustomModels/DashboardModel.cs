using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitmusWeb.Models.CustomModels
{
    public class DashboardModel
    {
        public int UnitCode { get; set; }
        public long Id { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int EntryTime { get; set; }
        public string UnitName { get; set; }
        public Nullable<byte> CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> CaneCrushed { get; set; }
        public Nullable<int> SugarBagsTotal { get; set; }
        public Nullable<int> WaterTotal { get; set; }
        public Nullable<int> JuiceTotal { get; set; }
        public decimal CaneDiverted { get; set; }
        public decimal DivertedSyrup { get; set; }
    }

    public class HourlyData
    {
        public string hour { get; set; }
        public string value { get; set; }
    }

    public class UnitWiseHourlDataResult
    {
        public string unitName { get; set;}
        public List<HourlyData> hourlyValues { get; set; } 
    }
}