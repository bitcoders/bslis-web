using iText.IO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{


    /// <summary>
    ///  Dynamic report repository is to get summarized data
    ///  1. Get Single date summarized data using 'proc_summarized_report' stored procedure
    ///  2. GetPeriodicalData (proc_summarized_periodical_report_Result)
    ///  3. Get Summarized Data by Crop Day (proc_summarized_report_by_crop_day_Result)
    /// </summary>
    public class DynamicReportRepository
    {
        /// <summary>
        /// Get Single date summarized data using 'proc_summarized_report' stored procedure
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportDate"></param>
        /// <param name="lastSeasonData"></param>
        /// <returns></returns>
        public proc_summarized_report_Result GetLedgerSummary(int unitCode, int seasonCode, DateTime reportDate, bool lastSeasonData = false)
        {
            proc_summarized_report_Result result = new proc_summarized_report_Result();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    result = Db.proc_summarized_report(unitCode, seasonCode, reportDate, lastSeasonData).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return result;
        }

        public proc_summarized_periodical_report_Result GetPeriodicalData(int unitCode, int seasoncode, DateTime fromDate, DateTime toDate, bool lastSeasonData = false)
        {
            proc_summarized_periodical_report_Result result = new proc_summarized_periodical_report_Result();
            try
            {
                if (lastSeasonData == true)
                {
                    seasoncode = seasoncode - 1;
                    dates_for_previous_season(unitCode, seasoncode, fromDate, toDate, out fromDate, out toDate);
                }
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    //result = Db.proc_summarized_periodical_report(unitCode, seasaonCode, fromDate, toDate, lastSeasonData).FirstOrDefault();
                    result = Db.proc_summarized_periodical_report(unitCode, seasoncode, fromDate, toDate, false).FirstOrDefault();
                    if (result == null && lastSeasonData == true)
                    {
                        seasoncode = seasoncode - 1;
                        dates_for_previous_season(unitCode, seasoncode,fromDate, toDate, out fromDate, out toDate);
                        result = Db.proc_summarized_periodical_report(unitCode, seasoncode, fromDate, toDate, false).FirstOrDefault();
                    }
                }
               
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return result;
        }


        public proc_summarized_report_by_crop_day_Result GetCropDaySummary(int unit_code, int season_code, int crop_day)
        {
            proc_summarized_report_by_crop_day_Result result = new proc_summarized_report_by_crop_day_Result();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    result = Db.proc_summarized_report_by_crop_day(unit_code, season_code, crop_day).FirstOrDefault();
                    
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }

            return result;
        }

        // some times one season closes before than current season e.g season 2021 closed on 20-02-2021 
        // and season 2022 is still running after 20-02-2022.
        // Till now the logic to get the last season's data is that I subtract exact one year from the report date.
        // But, as I mentioned earlier, query will not return any data which can throw an exception or wrogn report.
        // ------- Solution
        // (1) Write a function which will get the report or period from_date and to_date
        // (2) If Report is a perioducal report
        //      (a). days_count = If report is a periodical, Get the period days (to_date - from_date).
        //      (b). to_date = Get the last working date (processed_date) of season
        //      (c). from_date = based on days_count calculate from_date 
        //          e.g. last working date of previous season was 20-02-2021 and diff_days is 7 then from_date will be 14-02-2021
        //      Now get the data for calculated using from_date and to_date (calcuated in point b and c)
        //  (3) If report is for a particular date (daily report) then get the last working date of the season only, and based on this report
        //  fetch the data.

        private void dates_for_previous_season(int unit_code, int season_code, DateTime from_date, DateTime to_date, out DateTime report_from_date, out DateTime report_end_date)
        {
            //CommonRepository cRepo = new CommonRepository();
            //report_end_date = cRepo.getMaxProcessedDate(unit_code, season_code);
            //double days_count = (report_end_date - report_end_date).TotalDays;
            //report_from_date = report_end_date.AddDays(-days_count);

            CommonRepository cRepo = new CommonRepository();
            double days_count = to_date.Subtract(from_date).Days;

            report_end_date = cRepo.getMaxProcessedDate(unit_code, season_code);
            if (to_date.AddYears(-1) <= report_end_date)
            {
                report_end_date = to_date.AddYears(-1);
                report_from_date = from_date.AddYears(-1);
            }
            else
            {
                report_from_date = report_end_date.AddDays(-days_count);
            }

        }

    }
}
