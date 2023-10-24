using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Interfaces;

namespace DataAccess.Repositories.AnalysisRepositories
{
    public class DailyAnalysisRepository : IDailyAnalysis
    {
        SugarLabEntities Db;
        ExceptionRepository expRepository  = new ExceptionRepository();
        readonly AuditRepository auditRepo = new AuditRepository();
        string current_ip= "127.0.0.1";
        public DailyAnalysisRepository()
        {
           current_ip  = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            Db = new SugarLabEntities();
        }
        public bool AddDailyAnalysis(DailyAnalys dailyAnalysis)
        {
            try
            {
                Db.DailyAnalyses.Add(dailyAnalysis);
                Db.SaveChanges();
                return true;
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
                return false;
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
                new Exception(ex.Message);
                return false;
            }
        }
        public bool EditDailyAnalysis(DailyAnalys dailyAnalysis)
        {
            try
            {
                DailyAnalys oldData = new DailyAnalys();
                DailyAnalys newData = new DailyAnalys();
                using (SugarLabEntities tempDb = new SugarLabEntities())
                {
                    tempDb.Configuration.ProxyCreationEnabled = false;
                    oldData = tempDb.DailyAnalyses.Where(x => x.id == dailyAnalysis.id).FirstOrDefault();
                }
                Db.Entry(dailyAnalysis).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                using (SugarLabEntities tempDbTwo = new SugarLabEntities())
                {
                    tempDbTwo.Configuration.ProxyCreationEnabled = false;
                    newData = tempDbTwo.DailyAnalyses.Where(x => x.id == dailyAnalysis.id).FirstOrDefault();
                }
                auditRepo.CreateAuditTrail(AuditActionType.Update, dailyAnalysis.id.ToString(), oldData, newData);
                return true;
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException entityException)
            {
                SaveEntityExceptionLog(entityException);
                SaveExceptionLogs(entityException);
                
                throw entityException;

            }
            //catch (Exception ex)
            //{
            //    SaveExceptionLogs(ex);
            //    throw ex;
            //    //return false;
            //}

        }

        public DailyAnalys GetDailyAnalysisById(int id, int unitCode)
        {
            DailyAnalys dailyAnalysis = new DailyAnalys();
            try
            {
                dailyAnalysis = Db.DailyAnalyses.Where(i => i.id == id && i.unit_code == unitCode).FirstOrDefault();
                return dailyAnalysis;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return dailyAnalysis;
            }

        }
        public DailyAnalys GetDailyAnalysisDetailsByDate(int unitCode, int seasonCode, DateTime entryDate)
        {
            try
            {
                return Db.DailyAnalyses.Where(
                    i => i.unit_code == unitCode
                    && i.season_code == seasonCode
                    && i.entry_date == entryDate.Date
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
                new Exception(ex.Message);
                return null;
            }
        }

        public List<DailyAnalys> GetDailyAnalysisByDateRangeList(int unit_code, int season_code, DateTime fromDate, DateTime tillDate)
        {
            try
            {
                return Db.DailyAnalyses.Where(
                    i => i.unit_code == unit_code
                    && i.season_code == season_code
                    && i.entry_date >= fromDate
                    && i.entry_date <= tillDate
                    ).ToList();
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
                new Exception(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns the summarized data for daily analyses
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="entryDate"></param>
        /// <returns></returns>
        //public VU_DailyAnalysesSummary dailyAnalysesSummaryReport(int unitCode, int seasonCode, DateTime entryDate)
        //{
        //    VU_DailyAnalysesSummary dailyAnalysesSummary = null;
        //    try
        //    {
        //        dailyAnalysesSummary = Db.VU_DailyAnalysesSummary.Where(x => x.unit_code == unitCode
        //            && x.season_code == seasonCode
        //            && x.entry_date == entryDate
        //    ).FirstOrDefault();
        //        return dailyAnalysesSummary;
        //    }
        //    catch(Exception ex)
        //    {
        //        SaveExceptionLogs(ex);
        //        return dailyAnalysesSummary;
        //    }
        //}

        public func_DailyAnalysesSummaryForDates_Result dailyAnalysesSummaryReport(int unitCode, int seasonCode, DateTime entryDate)
        {
            func_DailyAnalysesSummaryForDates_Result dailyAnalysesSummary = null;
            try
            {
                dailyAnalysesSummary = Db.func_DailyAnalysesSummaryForDates(unitCode, seasonCode,entryDate,entryDate).FirstOrDefault();
                return dailyAnalysesSummary;
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
                return dailyAnalysesSummary;
            }
        }
        public func_DailyAnalysesSummaryForDates_Result dailyAnalysesSummaryFromCrushingStartDate(int unitCode, int seasonCode, DateTime reportDate)
        {
            
            var MasterUnitDetails = Db.MasterUnits.Where(x => x.Code == unitCode && x.CrushingSeason == seasonCode).FirstOrDefault();
            func_DailyAnalysesSummaryForDates_Result dailyAnalysesSummary = null;
            try
            {
                dailyAnalysesSummary = Db.func_DailyAnalysesSummaryForDates(unitCode, seasonCode, MasterUnitDetails.CrushingStartDate, reportDate).FirstOrDefault();
                return dailyAnalysesSummary;
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
                return dailyAnalysesSummary;
            }
        }

        /// <summary>
        /// For Previous date only by its unit code.
        /// After passing unit code, function will it self pick current entry date, and based on it 
        /// it will subtract one day by this return data for previous day.
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public DailyAnalys dailyAnalysesForPreviousDate(int unitCode)
        {
            var MasterUnitDetails = Db.MasterUnits.Where(x => x.Code == unitCode).FirstOrDefault();
            DailyAnalys dailyAnalysisData = new DailyAnalys();
            DateTime previousDate = MasterUnitDetails.EntryDate.AddDays(-1);
            int seasonCode = MasterUnitDetails.CrushingSeason;
            try
            {
                dailyAnalysisData = Db.DailyAnalyses.Where(x => x.unit_code == unitCode
                && x.entry_date == previousDate
                && x.season_code == seasonCode
                ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex); 
            }
            return dailyAnalysisData;
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
                        FileName = "Daily Analyses",
                        StackTrace = ex.StackTrace,
                        ErrorCode = "501",
                        InnerException = ex.Message,
                        IPAddress = current_ip,
                        ExceptionSolvedBy = "Admin",
                        ExceptionOccuredAt = DateTime.Now,
                    };
                    expRepository.AddException(exceptionLog);
                }
                catch(Exception x)
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
                            FileName = "Daily Analyses",
                            StackTrace = err,
                            ErrorCode = "501",
                            InnerException = ex.Message,
                            IPAddress = current_ip,
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
