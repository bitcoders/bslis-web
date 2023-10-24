using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitmusWeb.Models
{
    public class usp_cane_diversion_factor_model
    {
        public Nullable<int> season_code { get; set; }
        public Nullable<System.DateTime> factor_date { get; set; }
        public Nullable<decimal> syrup_diverted { get; set; }
        public Nullable<decimal> cane_diverted { get; set; }
        public Nullable<decimal> factor_value { get; set; }
    }
}