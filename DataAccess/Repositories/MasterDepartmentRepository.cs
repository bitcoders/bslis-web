using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
namespace DataAccess.Repositories
{
   public class MasterDepartmentRepository : IMasterDepartment
    {
        SugarLabEntities db;
        public MasterDepartmentRepository()
        {
            db = new SugarLabEntities();
        }
        public bool addMasterDepartment(MasterDepartment masterDepartment)
        {
            if(masterDepartment != null)
            {
                try
                {
                    db.MasterDepartments.Add(masterDepartment);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public bool deleteDepartment(int id)
        {
            try
            {
                MasterDepartment masterDepartment = new MasterDepartment();
                masterDepartment = db.MasterDepartments.Find(id);
                db.MasterDepartments.Remove(masterDepartment);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterDepartment FindByCode(int id)
        {
            MasterDepartment masterDepartment = null;
            try
            {
                masterDepartment = new MasterDepartment();
                masterDepartment = db.MasterDepartments.Find(id);
                return masterDepartment;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return masterDepartment;
            }
            
        }

        public List<MasterDepartment> GetMasterDepartmentList()
        {
            List<MasterDepartment> masterDepartments = null;
            try
            {
                //masterDepartments = new List<MasterDepartment>();
                masterDepartments = db.MasterDepartments.ToList();
                return masterDepartments;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return masterDepartments;
            }
        }

        public bool updateDepartment(MasterDepartment masterDepartment)
        {
            if(masterDepartment != null)
            {
                try
                {
                    db.Entry(masterDepartment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}
