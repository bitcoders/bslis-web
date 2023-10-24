using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterStoppageTypeRepository : IMasterStoppageType
    {
        SugarLabEntities db;
        public MasterStoppageTypeRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterStoppageType(MasterStoppageType parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterStoppageTypes.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterStoppageType(int id)
        {
            MasterStoppageType paramCateg = null;

            try
            {
                paramCateg = db.MasterStoppageTypes.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterStoppageTypes.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterStoppageType FindByCode(int id)
        {
            try
            {
                return db.MasterStoppageTypes.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterStoppageType> GetMasterStoppageTypeList()
        {
            try
            {
                return db.MasterStoppageTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterStoppageType(MasterStoppageType parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.Entry(parameterCategory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
