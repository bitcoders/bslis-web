﻿using System;
using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccess.Interfaces
{
    interface IHourlyAnalysis
    {
        bool CreateHourlyAnalysis(HourlyAnalys hourlyAnalysis);
        bool EditHourlyAnalysis(HourlyAnalys hourlyAnalysis);
        bool DeleteHourlyAnalysis(int UnitCode, int SeasonCode, string AnalysisDate, int AnalysisHour);
        List<HourlyAnalys> GetHourlyAnalysisList(int UnitCode, int SeasonCode, DateTime AnalysisDate);
        List<HourlyAnalys> GetHourlyAnalysisDateWiseList(int UnitCode, int SeasonCode, DateTime FromDate, DateTime ToDate);
        Task<List<HourlyAnalys>> GetHourlyAnalysAllList(int UnitCode, int SeasonCode);
        bool UpdateHourlyAnalysis(HourlyAnalys hourlyAnalysis);
        List<func_hourly_data_for_period_Result> GetHourlyAnalysisSummaryForPeriod(int UnitCode, int SeasonCode);
        func_hourly_data_for_period_Result GetHourlyAnalysisSummaryForDate(int unitCode, int seasonCode, DateTime reportDate);
    }
}
