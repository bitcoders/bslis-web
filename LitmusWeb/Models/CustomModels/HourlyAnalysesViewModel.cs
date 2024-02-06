using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LitmusWeb.Models;

namespace LitmusWeb.Models.CustomModels
{
    public class HourlyAnalysesViewModel
    {
        public HourlyAnalysisModel hourlyAnalysesModel { get; set; }
        public HourlyAnalysesMillControlDataModel MillControlModel { get; set; }
    }
}