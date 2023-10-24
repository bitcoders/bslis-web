using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess.Repositories
{
    public class CaneTypeRepository
    {
        public List<CaneType> GetCaneTypes()
        {
            List<CaneType> caneTypes = new List<CaneType>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    caneTypes = Db.CaneTypes.ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return caneTypes;
        }
    }
}
