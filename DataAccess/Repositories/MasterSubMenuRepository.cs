using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterSubMenuRepository : IMasterSubMenu
    {
        SugarLabEntities db;
        public MasterSubMenuRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterSubMenu(MasterSubMenu parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterSubMenus.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterSubMenu(int id)
        {
            MasterSubMenu paramCateg = null;

            try
            {
                paramCateg = db.MasterSubMenus.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterSubMenus.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterSubMenu FindByCode(int id)
        {
            try
            {
                return db.MasterSubMenus.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterSubMenu> GetMasterSubMenuList()
        {
            try
            {
                return db.MasterSubMenus.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterSubMenu(MasterSubMenu parameterCategory)
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
