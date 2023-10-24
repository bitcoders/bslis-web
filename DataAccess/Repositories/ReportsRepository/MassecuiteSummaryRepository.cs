using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using DataAccess.Interfaces.ReportsInterface;

namespace DataAccess.Repositories.ReportsRepository
{
    public class MassecuiteSummaryRepository : IMassecuiteSummary
    {
        readonly SugarLabEntities Db;
        readonly ExceptionRepository expRepository;
        public MassecuiteSummaryRepository()
        {
            Db = new SugarLabEntities();
            expRepository = new ExceptionRepository();
        }
        

        public func_massecuite_summary_Result massecuteToDateSummary(int unitCode, int seasonCode, DateTime reportDate)
        {
            func_massecuite_summary_Result result = new func_massecuite_summary_Result();

            try
            {
                result = Db.func_massecuite_summary(unitCode, seasonCode, reportDate).FirstOrDefault();
                return result;
            }
            catch(DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
                return result;
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
                return result;
            }
            
        }
        private void SaveExceptionLogs(Exception ex)
        {
            if (ex != null)
            {
                try
                {
                    ExceptionLog exceptionLog = new ExceptionLog()
                    {
                        Code = "501",
                        FileName = "MassecuiteSummaryRepository",
                        StackTrace = ex.StackTrace,
                        ErrorCode = "501",
                        InnerException = ex.Message,
                        IPAddress = "191.168.1.1",
                        ExceptionSolvedBy = "Admin",
                        ExceptionOccuredAt = DateTime.Now,
                    };
                    expRepository.AddException(exceptionLog);
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        }

        private void SaveEntityExceptionLog(System.Data.Entity.Validation.DbEntityValidationException ex)
        {
            if (ex != null)
            {
                List<string> exceptionList = new List<string>();
                exceptionList = ex.EntityValidationErrors.Select(e => string.Join(Environment.NewLine, e.ValidationErrors.Select(v => string.Format("{0} - {1}", v.PropertyName, v.ErrorMessage)))).ToList();

                try
                {
                    foreach (var err in exceptionList)
                    {
                        ExceptionLog exceptionLog = new ExceptionLog()
                        {
                            Code = "501",
                            FileName = "MassecuiteSummaryRepository",
                            StackTrace = err,
                            ErrorCode = "501",
                            InnerException = ex.Message,
                            IPAddress = "191.168.1.1",
                            ExceptionSolvedBy = "Admin",
                            ExceptionOccuredAt = DateTime.Now,
                        };
                        expRepository.AddException(exceptionLog);
                    }
                }
                catch (Exception x)
                {
                    throw x;
                }

            }
        }
    }
}
