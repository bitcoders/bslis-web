using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CustomModels;
namespace DataAccess.Repositories
{
    public class WeatherRecordsRepository
    {

        public List<WeatherRecord> weatherRecordsForDateRange(WeatherRecordsViewModel wm)
        {
            List<WeatherRecord> wr = new List<WeatherRecord>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    wr = Db.WeatherRecords.Where(x => x.UnitCode == wm.UnitCode && x.RecordDate >= wm.FromDate && x.RecordDate <= wm.ToDate).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return wr;
        }
        // Add a record

        public bool AddWeatherRecord(WeatherRecord wr)
        {
            if(wr == null)
            {
                return false;
            }

            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    Db.WeatherRecords.Add(wr);
                    Db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }

        // Edit a record

        // Delete a record


        public List<WeatherType> GetAllWeatherTypes()
        {
            List<WeatherType> wt = new List<WeatherType>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    wt = Db.WeatherTypes.ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            
            return wt;
        }

        /// <summary>
        /// Return weather records between given years.
        /// </summary>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <returns>List<WeatherRecords></returns>
        public List<WeatherRecord> MonthWiseYearlyRainFall(int yearFrom, int yearTo)
        {
            List<WeatherRecord> weatherRecords = new List<WeatherRecord>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    weatherRecords = Db.WeatherRecords.Where(@x => x.RecordDate.Year >= yearFrom && x.RecordDate.Year <= yearTo).ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return weatherRecords;

            }
        }
    }
}
