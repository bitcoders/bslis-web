using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using System.Data.Entity.Infrastructure;
namespace DataAccess.Repositories
{
    public class MasterUserRepository : IMasterUser

    {
        SugarLabEntities db = null;
        public MasterUserRepository()
        {
            db = new SugarLabEntities();
        }
        public bool addMasterUser(MasterUser masterUser)
        {
            if (masterUser == null)
            {
                return false;
            }
            try
            {
                db.MasterUsers.Add(masterUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool deleteUser(string code)
        {
            try
            {
                MasterUser masterUser = db.MasterUsers.Where(i => i.Code == code).FirstOrDefault();
                if (masterUser != null)
                {
                    db.MasterUsers.Remove(masterUser);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }

        public MasterUser FindByPk(string id)
        {
            MasterUser master = null;
            try
            {
                master = db.MasterUsers.Where(i => i.Code == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return master;
            }
            return master;

        }
        
        public MasterUser FindByUserCode(string userCode)
        {
            MasterUser master = null;
            try
            {
                master = db.MasterUsers.Where(i => i.Code == userCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return master;
            }
            return master;
        }

        public List<MasterUser> GetMasterUserList()
        {
            List<MasterUser> master = null;
            try
            {
                master = db.MasterUsers.ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return master;
            }
            return master;
        }

        public bool updateUser(MasterUser masterUser)
        {
            if (masterUser == null)
            {
                return false;
            }
            try
            {
                //db.Entry(masterUser).State = System.Data.Entity.EntityState.Modified;
                
                MasterUser mu = new MasterUser();
                mu = db.MasterUsers.Where(i => i.Code == masterUser.Code).FirstOrDefault();
                mu.FirstName = masterUser.FirstName;
                mu.LastName = masterUser.LastName;
                mu.IsActive = masterUser.IsActive;
                mu.IsDeleted = masterUser.IsDeleted;
                mu.UnitRights = masterUser.UnitRights;
                mu.DashboardUnits = masterUser.DashboardUnits;
                mu.BaseUnit = masterUser.BaseUnit;
                mu.Role = masterUser.Role;
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool UpdateUserWorkingUnit(MasterUser masterUser)
        {
            if(masterUser == null)
            {
                return false;
            }
            MasterUser user = new MasterUser();
            try
            {
                List<int> UnitAccessRights = new List<int>();
                user = db.MasterUsers.Where(x => x.Code == masterUser.Code).FirstOrDefault();
                UnitAccessRights = user.UnitRights.Split(',').Select(int.Parse).ToList();
                if (UnitAccessRights.Contains(Convert.ToInt16(masterUser.UnitCode)))
                {
                    user.UnitCode = masterUser.UnitCode;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }

        }
        
        public bool AuthorizeUserRole(string UserCode, params string[] Roles)
        {
            List<string> userRightName;
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                MasterUser user = new MasterUser();
                user = FindByUserCode(UserCode);
                if(user != null)
                {
                    userRightName = user.Role.Split(',').ToList();
                    if (userRightName != null)
                    {
                        foreach (var role in Roles)
                        {
                            if (userRightName.Contains(role))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool changePassword (MasterUser mu)
        {
            MasterUser masterUser = new MasterUser();
            try
            {
                masterUser = db.MasterUsers.Where(x => x.Code == mu.Code).FirstOrDefault();
                masterUser.Password = mu.Password;
                masterUser.Salt = mu.Salt;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
    }
}
