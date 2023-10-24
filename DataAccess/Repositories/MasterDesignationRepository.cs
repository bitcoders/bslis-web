using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterDesignationRepository : IMasterDesignation
    {
        SugarLabEntities db = null;
        public MasterDesignationRepository()
        {
            db = new SugarLabEntities();
        }
        public bool addMasterDesignation(MasterDesignation masterDesignation)
        {
            if(masterDesignation == null)
            {
                return false;
            }
            try
            {
                db.MasterDesignations.Add(masterDesignation);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            
            return true;
        }

        public bool deleteDesignation(int id)
        {
            try
            {
                MasterDesignation masterDesignation = db.MasterDesignations.Find(id);
                if(masterDesignation != null)
                {
                    db.MasterDesignations.Remove(masterDesignation);
                    db.SaveChanges();

                }

            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }

        public MasterDesignation FindByPk(int id)
        {
            MasterDesignation master = null;
            try
            {
                master = db.MasterDesignations.Find(id);
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return master;
            }
            return master;

        }

        public List<MasterDesignation> GetMasterDesignationList()
        {
            List<MasterDesignation> master = null;
            try
            {
                master = db.MasterDesignations.ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return master;
            }
            return master;
        }

        public bool updateDesignation(MasterDesignation masterDesignation)
        {
            if(masterDesignation == null )
            {
                return false;
            }
            try
            {
                db.Entry(masterDesignation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
    }
}
