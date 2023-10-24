using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataAccess.Interfaces;
namespace DataAccess.Repositories
{
    class ExceptionRepository : IExceptionLog
    {
        readonly SugarLabEntities Db;

        public ExceptionRepository()
        {
            Db = new SugarLabEntities();
        }

        public bool AddException(ExceptionLog exception)
        {
            if(exception == null)
            {
                return false;
            }
            try
            {
                Db.ExceptionLogs.Add(exception);
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteException(int Id)
        {
            try
            {
                ExceptionLog exLog = Db.ExceptionLogs.Where(temp => temp.Id == Id).FirstOrDefault();
                Db.ExceptionLogs.Remove(exLog);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public ExceptionLog GetExceptionById(int id)
        {
            ExceptionLog exLog = null;
            try
            {
                return Db.ExceptionLogs.Where(temp => temp.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return exLog;
            }
        }

        public List<ExceptionLog> GetEXceptionsListByCode(int ExceptionCode)
        {
            return Db.ExceptionLogs.ToList();
        }

        public bool UpdateException(ExceptionLog exception)
        {
            throw new NotImplementedException();
        }
    }
}
