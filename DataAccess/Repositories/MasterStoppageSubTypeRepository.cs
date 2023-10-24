using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterStoppageSubTypeRepository : IMasterStoppageSubType
    {
        SugarLabEntities db;
        public MasterStoppageSubTypeRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterStoppageSubType(MasterStoppageSubType parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterStoppageSubTypes.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterStoppageSubType(int id)
        {
            MasterStoppageSubType paramCateg = null;

            try
            {
                paramCateg = db.MasterStoppageSubTypes.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterStoppageSubTypes.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterStoppageSubType FindByCode(int id)
        {
            try
            {
                return db.MasterStoppageSubTypes.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterStoppageSubType> GetMasterStoppageSubTypeList()
        {
            try
            {
                return db.MasterStoppageSubTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterStoppageSubType(MasterStoppageSubType parameterCategory)
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

        public List<MasterStoppageSubType> GetMasterStoppageSubListByMasterHead(int id)
        {
            try
            {
                return db.MasterStoppageSubTypes.Where(x => x.MasterStoppageCode == id && x.IsActive == true).OrderBy(x=> x.Name).ToList();
            }
            
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
    }
}
