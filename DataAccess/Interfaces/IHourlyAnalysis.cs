using System;
using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.CustomModels;

namespace DataAccess.Interfaces
{
    interface IHourlyAnalysis
    {
        bool CreateHourlyAnalysis(HourlyAnalysesViewModel m);
        bool EditHourlyAnalysis(HourlyAnalys hourlyAnalysis);
        (bool success, string message) DeleteHourlyAnalysis(int unit_code, string user_code, int lineId);
        List<HourlyAnalys> GetHourlyAnalysisList(int UnitCode, int SeasonCode, DateTime AnalysisDate);
        List<HourlyAnalys> GetHourlyAnalysisDateWiseList(int UnitCode, int SeasonCode, DateTime FromDate, DateTime ToDate);
        Task<List<HourlyAnalys>> GetHourlyAnalysAllList(int UnitCode, int SeasonCode);
        bool UpdateHourlyAnalysis(HourlyAnalys hourlyAnalysis);
        List<func_hourly_data_for_period_Result> GetHourlyAnalysisSummaryForPeriod(int UnitCode, int SeasonCode);
        func_hourly_data_for_period_Result GetHourlyAnalysisSummaryForDate(int unitCode, int seasonCode, DateTime reportDate);
    }
}
