using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
   public class MasterParameterSubCategoryRepository : IMasterParameterSubCategory
    {
        SugarLabEntities db;
        public MasterParameterSubCategoryRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterParameterSubCategory(MasterParameterSubCategory parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterParameterSubCategories.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterParameterSubCategory(int id)
        {
            MasterParameterSubCategory paramCateg = null;

            try
            {
                paramCateg = db.MasterParameterSubCategories.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterParameterSubCategories.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterParameterSubCategory FindByCode(int id)
        {
            try
            {
                return db.MasterParameterSubCategories.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterParameterSubCategory> GetMasterParameterSubCategoryList()
        {
            try
            {
                return db.MasterParameterSubCategories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterParameterSubCategory(MasterParameterSubCategory parameterCategory)
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


        /// <summary>
        /// Return all active sub parameters belongs to given Master Parameter Category Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<MasterParameterSubCategory></returns>
        public List<MasterParameterSubCategory> GetMasterParameterCategoriesByParameterMasterCode (int id)
        {
            try
            {
                return db.MasterParameterSubCategories
                    .Where(x => x.MasterCategory == id
                        && x.IsActive == true
                    ).ToList();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
            

    }
}
