using System;
using System.Linq;
using DataAccess.Repositories;

namespace DataAccess
{
    public class LitmusRepository
    {
        CryptographyRepository cryptoRepository;
        private SugarLabEntities db;
        
        public LitmusRepository()
        {
            cryptoRepository = new CryptographyRepository();
            db = new SugarLabEntities();
        }
        #region MasterUsers Method
        public bool AddMasterUser(MasterUser userInfo)
        {
            return true;
        }
        public bool UpdateMasterUser(MasterUser userInfo)
        {
            return true;
        }
        public bool DeleteMasterUser(MasterUser userInfo)
        {
            return true;
        }
        public MasterUser ValidateUser(string code, string password)
        {
            MasterUser masterUser = null;
            try
            {
                string salt = db.MasterUsers.Where(x => x.Code == code).Select(temp => temp.Salt).FirstOrDefault();
                string cryptoPassword = cryptoRepository.GenerateHashedString(password, salt);
                masterUser = db.MasterUsers.Where(v => v.Code == code && v.Password == cryptoPassword && v.IsDeleted == false).FirstOrDefault();
                return masterUser;
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
                new Exception(ex.Message);
                return masterUser;
            }
        }
        public string GetServerDetails()
        {
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                string DatabaseName = "";
                try
                {
                    DatabaseName = db.Database.SqlQuery<string>("select @@SERVERNAME").FirstOrDefault();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                
                return DatabaseName;
            };
        }
        /// <summary>
        /// Get user details by Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MasterUser GetMasterUser(int id)
        {
            return db.MasterUsers.Where(i => i.Id == id).FirstOrDefault();
        }

        public MasterUnit GetUnitSettings(int unit_code)
        {
            MasterUnit masterUnit = new MasterUnit();
            try
            {
                masterUnit = db.MasterUnits.Where(temp => temp.Code == unit_code).FirstOrDefault();
            }
            catch(Exception ex)
            {
                  SaveExceptionLogs(ex);
            }
            return masterUnit;
        }
        #endregion

        private void SaveExceptionLogs(Exception ex)
        {
            //    ExceptionRepository expRepository = new ExceptionRepository();
            //    if (ex != null)
            //    {
            //        ExceptionLog exceptionLog = new ExceptionLog()
            //        {
            //            Code = "501",
            //            FileName = "LitmusRepository",
            //            StackTrace = ex.StackTrace,
            //            ErrorCode = "501",
            //            InnerException = ex.InnerException.Message,
            //            IPAddress = "191.168.1.1",
            //            ExceptionSolvedBy = "Admin",
            //            ExceptionOccuredAt = DateTime.Now,
            //        };
            //        expRepository.AddException(exceptionLog);
            //    }

        }
    }
}
