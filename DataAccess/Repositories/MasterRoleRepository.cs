using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess.Repositories
{
    public class MasterRoleRepository
    {
        public List<MasterRole> GetMasterRolesList()
        {
            List<MasterRole> masterRolesList = new List<MasterRole>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    masterRolesList = Db.MasterRoles.Where(x=>x.Is_Visible == true).ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return masterRolesList;
            }       
        }

        /// <summary>
        /// Get role details by its Name
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns>MasterRole</returns>
        public MasterRole GetMasterRoleByName(string RoleName)
        {
            MasterRole masterRole = new MasterRole();
            if (!string.IsNullOrEmpty(RoleName))
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    try
                    {
                        masterRole = Db.MasterRoles.Where(r => r.Name.Equals(RoleName)).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        new Exception(ex.Message);
                    }
                }
            } 
            return masterRole;
        }
    }
}
