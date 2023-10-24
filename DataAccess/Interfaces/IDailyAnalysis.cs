using System;
using System.Collections.Generic;
namespace DataAccess.Interfaces
{
    interface IDailyAnalysis
    {
        bool AddDailyAnalysis(DailyAnalys dailyAnalysis);
        bool EditDailyAnalysis(DailyAnalys dailyAnalysis);
        List<DailyAnalys> GetDailyAnalysisByDateRangeList(int unitCode, int seasonCode, DateTime fromDate, DateTime tillDate);
        DailyAnalys GetDailyAnalysisById(int Id, int unitCode);
        DailyAnalys GetDailyAnalysisDetailsByDate(int unitCode, int seasonCode, DateTime entryDate);
        //VU_DailyAnalysesSummary dailyAnalysesSummaryReport(int unitCode, int seasonCode, DateTime entryDate);
        func_DailyAnalysesSummaryForDates_Result dailyAnalysesSummaryReport(int unitCode, int seasonCode, DateTime entryDate);
    }
}
