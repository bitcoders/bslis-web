using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Repositories
{
    public class GrowerRepository
    {
        public List<Grower> Growers(int unitcode)
        {
            List<Grower> grower = new List<Grower>();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    grower = Db.Growers.Where(x => x.UnitCode == unitcode).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return grower;
        }

        public Grower GetGrowerByCode(int unitCode, int villageCode, int growerCode)
        {
            Grower grower = new Grower();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    grower = Db.Growers.Where(x => x.UnitCode == unitCode && x.VillageCode == villageCode && x.Code == growerCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return grower;
        }
    }
}
