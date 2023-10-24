using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MasterVehicleRepository
    {
        SugarLabEntities db;
        public MasterVehicleRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddMasterVehicle(MasterVehicle parameterCategory)
        {
            if (parameterCategory == null)
            {
                return false;
            }
            try
            {
                db.MasterVehicles.Add(parameterCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteMasterVehicle(int id)
        {
            MasterVehicle paramCateg = null;

            try
            {
                paramCateg = db.MasterVehicles.Where(t => t.Code == id).FirstOrDefault();
                if (paramCateg == null) { return false; }
                db.MasterVehicles.Remove(paramCateg);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterVehicle FindByCode(int id, int unitCode)
        {
            try
            {
                return db.MasterVehicles.Where(v=>v.id == id && v.UnitCode == unitCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterVehicle> GetMasterVehicleList()
        {
            try
            {
                return db.MasterVehicles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MasterVehicle> GetMasterVehicleByUnitList(int unit_code)
        {
            try
            {
                return db.MasterVehicles.Where(temp=> temp.UnitCode == unit_code).OrderBy(temp => temp.Code).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool UpdateMasterVehicle(MasterVehicle parameterCategory)
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


        
        public bool UpdateMasterVehicleForUnit(MasterVehicle masterVehicle)
        {
            if(masterVehicle == null)
            {
                return false;
            }
            try
            {
                MasterVehicle entity = new MasterVehicle();
                entity = db.MasterVehicles.Where(v=> v.id == masterVehicle.id).FirstOrDefault();
                if(entity == null)
                {
                    return false;
                }
                entity.MinimumWeight = masterVehicle.MinimumWeight;
                entity.MaximumWeight = masterVehicle.MaximumWeight;
                entity.AverageWeight = masterVehicle.AverageWeight;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }
    }
}
