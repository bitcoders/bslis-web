using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UnitSeasonsRepository
    {
        // Get list of UnitSeason
        /// <summary>
        /// Get list of UnitSeasons of all units and all seasons
        /// </summary>
        /// <returns>List<UnitSeasons></returns>
        public List<UnitSeason> unitSeasonsList()
        {
            List<UnitSeason> unitSeasons = new List<UnitSeason>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    unitSeasons = Db.UnitSeasons.OrderBy(x=>x.Code).ThenBy(x=>x.Season).ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return unitSeasons;
        }
        // Get List of UnitSeason for single unit
        /// <summary>
        /// Get list of UnitSeasons of selected units and all seasons
        /// </summary>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public List<UnitSeason> UnitSeasons (int unit_code)
        {
            List<UnitSeason> unitSeasons = new List<UnitSeason>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    unitSeasons = Db.UnitSeasons.Where(x=>x.Code == unit_code).OrderBy(x => x.Code).ThenBy(x => x.Season).ToList();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return unitSeasons;

        }


        /// <summary>
        /// get season details by unit code and season code
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <returns></returns>
        public UnitSeason UnitSeasonByUnitCodeAndSeasonCode (int unitCode, int seasonCode)
        {
            UnitSeason season = new UnitSeason();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    season = Db.UnitSeasons.Where(x => x.Code == unitCode && x.Season == seasonCode).First();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return season;
        }

        public UnitSeason UnitSeason (Guid id)
        {
            UnitSeason unitSeason = new UnitSeason();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    unitSeason = Db.UnitSeasons.Where(x => x.id == id).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return unitSeason;
        }
        // create UnitSeason

        public bool CreateUnitSeason(UnitSeason data)
        {
            UnitSeason unitseason = new UnitSeason();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    Db.UnitSeasons.Add(data);
                    if(Db.SaveChanges() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }

        // Edit UnitSeason
        public bool EditUnitSeason(UnitSeason data)
        {
            if (data == null)
            {
                return false;
            }
            #region Old Code
            //AuditRepository auditRepo = new AuditRepository();
            //UnitSeason unitSeason = new UnitSeason();
            //int resultCount = 0;
            //var oldData = new object();
            //var newData = new object();
            //using (SugarLabEntities Db = new SugarLabEntities())
            //{
            //    try
            //    {
            //        Db.Configuration.ProxyCreationEnabled = false;
            //        oldData = Db.UnitSeasons.Where(x => x.id == data.id).FirstOrDefault();
            //    }
            //    catch (Exception ex)
            //    {
            //        new Exception(ex.Message);
            //    }

            //}
            //using (SugarLabEntities Db = new SugarLabEntities())
            //{
            //    try
            //    {
            //        Db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            //        resultCount = Db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        new Exception(ex.Message);
            //    }
            //}
            //    if (resultCount == 1)
            //    {
            //        using (SugarLabEntities Db = new SugarLabEntities())
            //        {
            //            Db.Configuration.ProxyCreationEnabled = false;
            //            newData = Db.UnitSeasons.Where(x => x.id == data.id).FirstOrDefault();
            //            auditRepo.CreateAuditTrail(AuditActionType.Update, data.id.ToString(), oldData, newData);
            //            return true;
            //        }
            //    }
            #endregion

            try
            {
                using(SugarLabEntities _db = new SugarLabEntities())
                {
                   var parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("id", data.id));
                    parameters.Add(new SqlParameter("unit_code", data.Code));
                    parameters.Add(new SqlParameter("season_code",data.Season));
                    parameters.Add(new SqlParameter("crushing_start_datetime",data.CrushingStartDateTime));
                    parameters.Add(new SqlParameter("crushing_end_datetime", data.CrushingEndDateTime));
                    parameters.Add(new SqlParameter("new_mill_capacity",data.NewMillCapacity));
                    parameters.Add(new SqlParameter("old_mill_capacity",data.OldMillCapacity));
                    parameters.Add(new SqlParameter("report_start_hourMinuete", data.ReportStartHourMinute));
                    parameters.Add(new SqlParameter("disableDailyProcess",data.DisableDailyProcess));
                    parameters.Add(new SqlParameter("disableAdd", data.DisableAdd));
                    parameters.Add(new SqlParameter("disableUpdate", data.DisableUpdate));
                    parameters.Add(new SqlParameter("autoReportStartDate", data.AutoReportStartDate));
                    parameters.Add(new SqlParameter("autoReportEndDate", data.AutoReportEndDate));

                    _db.Database.ExecuteSqlCommand("EXEC usp_update_unitSeasons @id, @unit_code, @season_code, @crushing_start_datetime, @crushing_end_datetime" +
                        ", @new_mill_capacity, @old_mill_capacity, @report_start_hourMinuete, @disableDailyProcess, @disableAdd, @disableUpdate, @autoReportStartDate, @autoReportEndDate", parameters.ToArray());
                    return true;
                }
            }
            catch(Exception ex)
            {
              new Exception(ex.Message);
            }
            return false;
        }

        //Delete Unit Season

        public bool DeleteUnitSeason(UnitSeason data)
        {
            return false;
        }
    }
}
