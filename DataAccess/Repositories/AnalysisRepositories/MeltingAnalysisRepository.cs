using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Interfaces.ReportsInterface;
using System.Data.Entity.Validation;
namespace DataAccess.Repositories.AnalysisRepositories
{
    public class MeltingAnalysisRepository : IMeltings, IMeltingSummary
    {
        SugarLabEntities Db;
        readonly ExceptionRepository expRepository;
        public MeltingAnalysisRepository()
        {
            Db = new SugarLabEntities();
            expRepository = new ExceptionRepository();
        }
        public bool AddMeltingAnalysis(MeltingAnalys meltingAnalysis)
        {
            try
            {
                Db.MeltingAnalyses.Add(meltingAnalysis);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool EditMelingAnalysis(MeltingAnalys meltingAnalysis)
        {
            try
            {
                Db.Entry(meltingAnalysis).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public List<MeltingAnalys> GetMeltingAnalysesListByDate(DateTime entryDate, int unitCode)
        {
            try
            {
                return Db.MeltingAnalyses.Where
                    (
                        x => x.unit_code == unitCode
                        && x.entry_date == entryDate
                    ).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
    

        public MeltingAnalys GetMeltingAnalysisById(int Id, int unitCode, int seasonCode,  DateTime entryDate)
        {
            try
            {
                return Db.MeltingAnalyses.Where
                    (
                        x => x.id == Id
                        && x.unit_code == unitCode
                        && x.season_code == seasonCode
                        && x.entry_date == entryDate
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public func_melting_period_summary_list_Result GetMeltingsTodateSummary(int unitCode, int seasonCode, DateTime reportDate)
        {
            func_melting_period_summary_list_Result result = new func_melting_period_summary_list_Result();

            try
            {
                result = Db.func_melting_period_summary_list(unitCode
                                            ,seasonCode
                                            ,reportDate, reportDate).FirstOrDefault();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
                return result;
            }
            catch (Exception ex)
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
                        FileName = "MeltingAnalysisRepository",
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
                            FileName = "MeltingAnalysisRepository",
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
