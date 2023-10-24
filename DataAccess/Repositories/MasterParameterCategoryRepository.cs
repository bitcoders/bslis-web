using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterParameterCategoryRepository : IMasterParameterCategory
    {
        SugarLabEntities db;
        public MasterParameterCategoryRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterParameterCategory(MasterParameterCategory parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterParameterCategories.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterParameterCategory(int id)
        {
            MasterParameterCategory paramCateg = null;

            try
            {
                paramCateg = db.MasterParameterCategories.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterParameterCategories.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterParameterCategory FindByCode(int id)
        {
            try
            {
                return db.MasterParameterCategories.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterParameterCategory> GetMasterParameterCategoryList()
        {
            try
            {
                return db.MasterParameterCategories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterParameterCategory(MasterParameterCategory parameterCategory)
        {
            if(parameterCategory == null)
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
