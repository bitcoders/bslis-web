using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitmusWeb.Models.CustomModels
{
    public class HourlyAnalysesViewModel
    {
        public HourlyAnalys hourlyAnalysesModel { get; set; }
        public HourlyAnalysesMillControlData MillControlModel { get; set; }
    }
}