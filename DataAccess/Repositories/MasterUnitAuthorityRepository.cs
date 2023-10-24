using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterUnitAuthorityRepository : IMasterUnitAuthority
    {
        private SugarLabEntities db;
        public MasterUnitAuthorityRepository()
        {
            db = new SugarLabEntities();
        }
        public bool addMasterUnitAuthorities(MasterUnitAuthority masterUnitAuthority)
        {
            if(masterUnitAuthority == null)
            {
                return false;
            }
            try
            {
                if(masterUnitAuthority.valid_from.Date >= masterUnitAuthority.valid_till.Date)
                {
                    return false;
                }
                    db.MasterUnitAuthorities.Add(masterUnitAuthority);
                    db.SaveChanges();
            }
            catch(Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
            return true;
        }

        public bool deleteMasterUnitAuthorities(int id)
        {
            MasterUnitAuthority masterUnitAuthority = db.MasterUnitAuthorities.Where(i => i.Id == id).FirstOrDefault();
            if(masterUnitAuthority == null)
            {
                return false;
            }

            try
            {
                db.MasterUnitAuthorities.Remove(masterUnitAuthority);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }

        public MasterUnitAuthority FindByCode(int id    )
        {
            MasterUnitAuthority masterUnitAuthority = null;
            try
            {
                masterUnitAuthority = db.MasterUnitAuthorities.Where(i => i.Id == id).FirstOrDefault();
                db.MasterUnitAuthorities.Remove(masterUnitAuthority);
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return masterUnitAuthority;

        }

        public List<MasterUnitAuthority> GetMasterUnitAuthoritiesList()
        {
            try
            {
                return db.MasterUnitAuthorities.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updateMasterUnitAuthorities(MasterUnitAuthority masterUnitAuthority)
        {
            if(masterUnitAuthority == null)
            {
                return false;
            }
            try
            {
                db.Entry(masterUnitAuthority).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            
        }
    }
}
