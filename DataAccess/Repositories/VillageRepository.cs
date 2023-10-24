using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class VillageRepository
    {
        public List<Village> Villages(int unitcode)
        {
            List<Village> villages = new List<Village>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    villages = Db.Villages.Where(x => x.UnitCode == unitcode).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return villages;
        }

        public Village GetVillageByCode(int unitCode, int villageCode)
        {
            Village v = new Village();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    v = Db.Villages.Where(x => x.UnitCode == unitCode && x.Code == villageCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return v;
        }
    }
}
