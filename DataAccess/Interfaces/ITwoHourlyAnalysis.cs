using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace DataAccess.Interfaces
{
    interface ITwoHourlyAnalysis
    {
        bool AddTwoHourlyAnalysis(TwoHourlyAnalys twoHourlyAnalysis);
        bool UpdateTwoHourlyAnalysis(TwoHourlyAnalys twoHourlyAnalysis);
        bool DeleteTwoHourlyAnalysis(int id, int unitCode, int seasonCode);
        List<TwoHourlyAnalys> GetTwoHourlyAnalysis(int unitCode, int seasonCode, DateTime entryDate);
        TwoHourlyAnalys GetTwoHourlyAnalysisDetails(int id, int unitCode, int seasonCode, DateTime EntryDate);
        TwoHourlyAnalys GetTwoHourlyAnalysisDetailsByEntryTime(int unitCode, int seasonCode, DateTime entryDate, int entryTime);
    }
}
