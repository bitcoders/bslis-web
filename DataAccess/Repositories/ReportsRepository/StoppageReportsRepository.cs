using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{
    public class StoppageReportsRepository
    {
        /// <summary>
        /// Get list of stoppages of selected date
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportDate"></param>
        /// <param name="millCode"></param>
        /// <returns></returns>
        public List<Stoppage> stoppageListForDate(int unitCode, int seasonCode, DateTime reportDate, int millCode)
        {
            List<Stoppage> stoppages = new List<Stoppage>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    stoppages = Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.season_code == seasonCode
                                        && x.s_date == reportDate.Date
                                        && x.s_mill_code == millCode
                                        && x.is_deleted == false).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return stoppages;
        }

        /// <summary>
        /// stoppage list for both (all) mills for a unit
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public List<Stoppage> stoppageListForDate(int unitCode, int seasonCode, DateTime reportDate)
        {
            List<Stoppage> stoppages = new List<Stoppage>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    stoppages = Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.season_code == seasonCode
                                        && x.s_date == reportDate.Date
                                        && x.is_deleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return stoppages;
        }

        /// <summary>
        /// /// get List of stoppages of selected date range
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="todate"></param>
        /// <param name="millCode"></param>
        /// <returns></returns>
        public List<Stoppage> stoppageListForDateRange(int unitCode, int seasonCode, DateTime fromDate, DateTime todate, int millCode)
        {
            List<Stoppage> stoppages = new List<Stoppage>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    stoppages = Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.season_code == seasonCode
                                        && x.s_date >= fromDate.Date
                                        && x.s_date <= todate.Date
                                        && x.s_mill_code == millCode
                                        && x.is_deleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return stoppages;
        }

        public List<string> StoppageOfAllUnitsForDate(int season_code, DateTime stoppage_date)
        {
            List<string> stoppages = new List<string>();
            List<proc_stoppages_all_unit_for_day_Result> result = new List<proc_stoppages_all_unit_for_day_Result>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                result = Db.proc_stoppages_all_unit_for_day(season_code, stoppage_date).ToList();
                if(result != null)
                {
                    foreach(var x in result)
                    {
                        if(x.unit_code == 1)
                        {
                            
                            stoppages.Add(x.Name + "("+x.mill_name+") : " + x.gross_duration.ToString() + " Hr. - " + x.s_comment);
                        }
                        else
                        {
                            stoppages.Add(x.Name + " : " + x.gross_duration.ToString() + " Hr. - " + x.s_comment);
                        }
                        
                    }
                }
                else
                {
                    stoppages.Add ("Applause!! No stoppages for the day!");
                }
            }
            return stoppages;
        }
    }
}
