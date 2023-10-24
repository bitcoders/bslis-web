using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Repositories.ReportsRepository
{
    public class TwoHourlyAnalysSummaryRepository
    {
        readonly SugarLabEntities Db;
        public TwoHourlyAnalysSummaryRepository()
        {
            Db = new SugarLabEntities();
        }

        public List<func_two_hourly_transaction_summary_Result> GetTwoHourlySummaryForDateList(int unitCode, int seasonCode, DateTime entryDate)
        {
            List<func_two_hourly_transaction_summary_Result> result = new List<func_two_hourly_transaction_summary_Result>();
            try
            {
                result = Db.func_two_hourly_transaction_summary(unitCode, seasonCode, entryDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="entryDate"></param>
        /// <returns></returns>
        public  func_two_hourly_transaction_summary_Result GetTwoHourlySummaryForDate(int unitCode, int seasonCode, DateTime entryDate)
        {
            func_two_hourly_transaction_summary_Result result = new func_two_hourly_transaction_summary_Result();
            try
            {
                result = Db.func_two_hourly_transaction_summary(unitCode, seasonCode, entryDate).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return result;
            }
        }
        public func_two_hourly_transaction_summary_for_hours_Result GetTwo_Hourly_Transaction_Summary_For_Hours_Results(int unitCode, int seasonCode, DateTime EntryDate, int fromHour, int EndHour)
        {
            func_two_hourly_transaction_summary_for_hours_Result results = new func_two_hourly_transaction_summary_for_hours_Result();
            try
            {
                results = Db.func_two_hourly_transaction_summary_for_hours(unitCode, seasonCode, EntryDate, fromHour, EndHour).FirstOrDefault();
                return results;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return results;
            }
        }
    }
}
