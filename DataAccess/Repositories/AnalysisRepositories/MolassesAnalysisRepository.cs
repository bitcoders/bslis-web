using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Interfaces.ReportsInterface;
using System.Data.Entity.Validation;

namespace DataAccess.Repositories.AnalysisRepositories
{
    public class MolassesAnalysisRepository : IMolasses , IMolassesSummary
    {
        SugarLabEntities Db;
        readonly ExceptionRepository expRepository;
        public MolassesAnalysisRepository()
        {
            Db = new SugarLabEntities();
            expRepository = new ExceptionRepository();
        }

        public bool AddMolasses(MolassesAnalys molassesAnalysis)
        {
            try
            {
                Db.MolassesAnalyses.Add(molassesAnalysis);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool Edit(MolassesAnalys molassesAnalysis)
        {
            try
            {
                Db.Entry(molassesAnalysis).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MolassesAnalys GetMolassesDetailsById(int id, int unitCode, int seasonCode, DateTime EntryDate)
        {
            try
            {
                return Db.MolassesAnalyses.Where
                    (
                        x => x.id == id
                        && x.Unit_Code == unitCode
                        && x.season_code == seasonCode
                        && x.mo_entry_date == EntryDate
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<MolassesAnalys> GetMolassesListByDate(DateTime entryDate, int unitCode)
        {
            try
            {
                return Db.MolassesAnalyses.Where
                    (
                        x => x.Unit_Code == unitCode
                        && x.mo_entry_date == entryDate
                    ).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
        public func_molasses_period_summary_list_Result molassesTodateSummary(int unitCode, int seasonCode, DateTime reportDate)
        {
            func_molasses_period_summary_list_Result result = null;

            try
            {
                result = Db.func_molasses_period_summary_list(unitCode,seasonCode,reportDate, reportDate).FirstOrDefault();
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
                        FileName = "MolassesAnalysisRepository",
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
                            FileName = "MolassesAnalysisRepository",
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
