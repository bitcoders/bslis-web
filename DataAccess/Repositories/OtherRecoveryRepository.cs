using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OtherRecoveryRepository
    {
        public OtherRecovery GetOtherRecoveries(int unit_code, int season_code, DateTime date)
        {
            OtherRecovery oRecovery = new OtherRecovery();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    oRecovery = Db.OtherRecoveries.Where(x => x.UnitCode == unit_code
                    && x.SeasonCode == season_code
                    && x.TransDate == date
                    ).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return oRecovery;
        }

        public OtherRecovery GetOtherRecoveriesByUnitCode(int unit_code)
        {
            OtherRecovery oRecovery = new OtherRecovery();
            MasterUnit units = new MasterUnit();
            
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    /// first get default season and entry date of unit, and use these values to fetch
                    /// records of other recoveries
                    units = Db.MasterUnits.Where(x => x.Code == unit_code).FirstOrDefault();
                    if(units != null)
                    {
                        oRecovery = Db.OtherRecoveries.Where(x => x.UnitCode == unit_code
                        && x.SeasonCode == units.CrushingSeason
                        && x.TransDate == units.EntryDate
                        ).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return oRecovery;
        }

        /// <summary>
        /// Get other recoveries of previous day (one day ealier of current entry date) by its unit code
        /// </summary>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public OtherRecovery GetOtherRecoveriesPreviousDayByUnitCode(int unit_code)
        {
            OtherRecovery oRecovery = new OtherRecovery();
            MasterUnit units = new MasterUnit();

            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    /// first get default season and entry date of unit, and use these values to fetch
                    /// records of other recoveries
                    units = Db.MasterUnits.Where(x => x.Code == unit_code).FirstOrDefault();
                    DateTime prviousDate = units.EntryDate.AddDays(-1);
                    if (units != null)
                    {
                        oRecovery = Db.OtherRecoveries.Where(x => x.UnitCode == unit_code
                        && x.SeasonCode == units.CrushingSeason
                        && x.TransDate == prviousDate
                        ).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return oRecovery;
        }
    }
}
