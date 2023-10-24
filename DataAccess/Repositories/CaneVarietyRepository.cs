using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CaneVarietyRepository
    {
        public List<CaneVariety> GetCaneVarieties ()
        {
            List<CaneVariety> caneVarieties = new List<CaneVariety>();
            try
            {
                using(SugarLabEntities db = new SugarLabEntities())
                {
                    caneVarieties = db.CaneVarieties.ToList();
                }
            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
            }
            return caneVarieties;
        }

        public CaneVariety GetCaneVarietyByCode(int code)
        {
            CaneVariety caneVariety = new CaneVariety();
            try
            {
                using (SugarLabEntities db = new SugarLabEntities())
                {
                    caneVariety = db.CaneVarieties.Where(x=>x.Code == code).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
            }
            return caneVariety;
        }


        /// <summary>
        /// Get list of varities matching to given name
        /// </summary>
        /// <param name="varietyName"></param>
        /// <returns></returns>
        public List<CaneVariety> SearchCaneVariety(string varietyName)
        {
            List<CaneVariety> caneVarieties = new List<CaneVariety>();
            try
            {
                using (SugarLabEntities db = new SugarLabEntities())
                {
                    caneVarieties = db.CaneVarieties.Where(x=>x.Name.Contains(varietyName)).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return caneVarieties;
        }
    }
}
