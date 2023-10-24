using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ZoneRepository
    {
        public List<Zone> GetZoness(int unitcode)
        {
            List<Zone> zones = new List<Zone>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    zones = Db.Zones.Where(x => x.UnitCode == unitcode).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return zones;
        }

        public Zone GetGrowerByCode(int unitCode, int zonesCode)
        {
            Zone zones = new Zone();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    zones = Db.Zones.Where(x => x.UnitCode == unitCode && x.Code == zonesCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return zones;
        }
    }
}
