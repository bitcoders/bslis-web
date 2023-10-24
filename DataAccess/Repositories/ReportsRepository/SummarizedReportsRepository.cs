using iText.IO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{
    public class SummarizedReportsRepository
    {

        /// <summary>
        /// summarized report by crop day
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="crop_day"></param>
        /// <returns></returns>
        public proc_summarized_report_by_crop_day_Result SumarizedReportByCropDay(int unit_code, int season_code, int crop_day)
        {
            proc_summarized_report_by_crop_day_Result result = new proc_summarized_report_by_crop_day_Result();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
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

        /// <summary>
        /// proc_summarized Report by report date
        /// data for last year for same date can be achived just setting 'last_year_data' parameter to 'true'
        /// (e.g. you pass date - 05-01-2022 in parameter and set 'last_year_data' to 'true' then you will get data
        /// for 05-01-2021. also, the season_code parameter will be changed/handled in stored procedure named 'proc_summarized_report'.
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="report_date"></param>
        /// <param name="last_year_data"></param>
        /// <returns></returns>
        public proc_summarized_report_Result SummarizedReportResult(int unit_code, int season_code, DateTime report_date, bool last_year_data)
        {
            proc_summarized_report_Result result = new proc_summarized_report_Result();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.proc_summarized_report(unit_code, season_code, report_date.Date, last_year_data).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return result;
            
        }



        /// <summary>
        /// Get summarized report of previous day by just passing the unit code
        /// based on unit code, this function will get current season code and entry date from MasterUnits table
        /// and pass these values to stored procedure named 'proc_summarized_report'
        /// </summary>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public proc_summarized_report_Result SummarizedReportForPreviousDay(int unit_code)
        {
            DateTime report_date;
            bool last_year_data = false;
            proc_summarized_report_Result result = new proc_summarized_report_Result();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    MasterUnit u = new MasterUnit();
                    if(u != null)
                    {
                        u = Db.MasterUnits.Where(x => x.Code == unit_code).FirstOrDefault();
                        if(u.EntryDate > u.CrushingStartDate)
                        {
                            report_date = u.EntryDate.AddDays(-1);
                        }
                        else
                        {
                            report_date = u.EntryDate;
                        }
                        
                        result = Db.proc_summarized_report(unit_code, u.CrushingSeason, report_date.Date, last_year_data).FirstOrDefault();
                    }
                    
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return result;

        }

    }
}
