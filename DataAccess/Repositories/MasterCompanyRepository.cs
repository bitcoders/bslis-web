using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess
{
    public class MasterCompanyRepository
    {
        private SugarLabEntities db;
        public MasterCompanyRepository()
        {
            db = new SugarLabEntities();
        }
        #region CompanyMaster Methods
        public bool AddMasterCompanyMaster(MasterCompany masterCompany)
        {
            if (masterCompany == null)
            {
                return false;
            }
            try
            {
                db.MasterCompanies.Add(masterCompany);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateMasterCompanyMaster(MasterCompany masterCompany)
        {
            if (masterCompany == null)
            {
                return false;
            }
            try
            {
                db.Entry(masterCompany).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteMasterCompany(int code)
        {
            try
            {
                MasterCompany masterCompany = db.MasterCompanies.Where(i => i.Code == code).FirstOrDefault();
                if (masterCompany != null)
                {
                    db.MasterCompanies.Remove(masterCompany);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
            return true;
        }
        public List<MasterCompany> GetMasterCompaniesList()
        {
            try
            {
                return db.MasterCompanies.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public MasterCompany FindCompanyByPK(int code)
        {
            MasterCompany master = null;
            try
            {
                master = db.MasterCompanies.Where(i => i.Code == code).FirstOrDefault();
            }
            catch
            {
                master = null;
            }
            return master;
        }
        #endregion
    }
}
