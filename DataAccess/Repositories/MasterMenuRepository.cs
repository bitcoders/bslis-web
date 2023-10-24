using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;


namespace DataAccess.Repositories
{
    public class MasterMenuRepository : IMasterMenu
    {
        SugarLabEntities db;
        public MasterMenuRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterMenu(MasterMenu masterMenu)
        {
            if(masterMenu == null)
            {
                return false;
            }
            try
            {
                db.MasterMenus.Add(masterMenu);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterMenu(int id)
        {
            MasterMenu menu = null;
            
            try
            {
                menu = db.MasterMenus.Where(t => t.Code == id).FirstOrDefault();
                if(menu == null) {return false;}
                db.MasterMenus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            
            
        }
        /// <summary>
        /// get details of Master Menu by its code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MasterMenu FindByCode(int id)
        {
            try
            {
                return db.MasterMenus.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterMenu> GetMasterMenuList()
        {
            try
            {
                return db.MasterMenus.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterMenu(MasterMenu masterMenu)
        {
            if(masterMenu == null)
            {
                return false;
            }
            try
            {
                db.Entry(masterMenu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
