using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repositories
{
    public class FlexRepository
    {

        /// <summary>
        /// get details of flex sub master by its code;
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<FlexSubMaster> GetFlexSubMasterByCode(int MasterFlexCode)
        {
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                List<FlexSubMaster> f = new List<FlexSubMaster>();
                try
                {
                    f = Db.FlexSubMasters.Where(x => x.FlexMasterCode == MasterFlexCode).ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return f;
            }
        }

        /// <summary>
        /// Get active flexsubmaster's list
        /// </summary>
        /// <returns></returns>
        public List<FlexSubMaster> GetActiveFlexMasters()
        {
            using(SugarLabEntities db = new SugarLabEntities())
            {
                List<FlexSubMaster> f = new List<FlexSubMaster>();
                try
                {
                    f = db.FlexSubMasters.Where(x => x.IsActive == true).ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return f;
            }
        }


        /// <summary>
        /// Get all flexsubmaster's list
        /// </summary>
        /// <returns></returns>
        public List<FlexSubMaster> GetFlexMasters()
        {
            using (SugarLabEntities db = new SugarLabEntities())
            {
                List<FlexSubMaster> f = new List<FlexSubMaster>();
                try
                {
                    f = db.FlexSubMasters.ToList();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return f;
            }
        }


        /// <summary>
        /// Get flexMaster by its code
        /// </summary>
        /// <param name="flexSubMasterCode"></param>
        /// <returns></returns>
        public FlexSubMaster FlexSubMasterValueByCode(int flexSubMasterCode)
        {
            FlexSubMaster flexSubMaster = new FlexSubMaster();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    flexSubMaster = Db.FlexSubMasters.Where(x => x.Code == flexSubMasterCode && x.IsActive == true).FirstOrDefault();
                };
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return flexSubMaster;
        }
    }
}
