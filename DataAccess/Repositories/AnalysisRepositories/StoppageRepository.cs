using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;


namespace DataAccess.Repositories.AnalysisRepositories
{
    public class StoppageRepository : IStoppage
    {
        readonly SugarLabEntities Db;
        
        ExceptionRepository expRepository = new ExceptionRepository();
        AuditRepository AuditRepo = new AuditRepository();
        public StoppageRepository()
        {
            Db = new SugarLabEntities();
        }
        public bool AddStoppage(Stoppage stoppage)
        {
            if(stoppage == null)
            {
                return false;
            }
            try
            {
                Db.Stoppages.Add(stoppage);
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public List<Stoppage> GetStoppageListByType(int unitCode, DateTime entryDate, int stoppageHead)
        {
            try
            {
                return Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.s_date == entryDate
                                        && x.s_head_code == stoppageHead
                                        && x.is_deleted == false
                                    ).ToList();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<Stoppage> GetStoppagesList(int unitCode, int season_code, DateTime entryDate)
        {
            try
            {
                return Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.s_date == entryDate
                                        && x.is_deleted == false
                                        && x.season_code == season_code
                                    ).OrderBy(x => x.id).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }


        // below function is used to close the stoppage (set the end time of stoppage)
        public bool UpdateStoppage(Stoppage stoppage)
        {
            try
            {
                Db.Entry(stoppage).State = System.Data.Entity.EntityState.Modified;
                var data = Db.Stoppages
                    .Where(x => x.id == stoppage.id 
                            && x.unit_code == stoppage.unit_code 
                            && x.season_code == stoppage.season_code)
                            .FirstOrDefault();
                data.unit_code = stoppage.unit_code;
                data.s_date = stoppage.s_date;
                data.season_code = stoppage.season_code;
                data.s_start_time = stoppage.s_start_time;
                data.s_end_calendar_date = DateTime.Now.Date;
                data.s_end_time = stoppage.s_end_time;
                data.s_duration = stoppage.s_duration;
                data.s_net_duration = stoppage.s_net_duration;
                data.s_updt_dt = DateTime.Now.Date;
                data.s_crtd_by = data.s_crtd_by;
                data.is_deleted = stoppage.is_deleted;
                try
                {
                    Db.SaveChanges();
                }
                catch(Exception ex)
                {
                    SaveExceptionLogs(ex);
                    return false;
                }
                
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        
        /// <summary>
        /// To change the reason of selected stoppage
        /// </summary>
        /// <param name="stoppage"></param>
        /// <returns></returns>
        public bool UpdateStoppageReason(Stoppage stoppage)
        {
            try
            {
                //Db.Entry(stoppage).State = System.Data.Entity.EntityState.Modified;
                var data = Db.Stoppages.Where(x => x.id == stoppage.id
                            && x.unit_code == stoppage.unit_code
                            && x.season_code == stoppage.season_code).FirstOrDefault();

                // oldrecord
                Stoppage oldRecord = new Stoppage()
                {
                    id = data.id,
                    unit_code = data.unit_code,
                    season_code = data.season_code,
                    s_date = data.s_date,
                    s_start_calendar_date = data.s_start_calendar_date,
                    s_start_time = data.s_start_time,
                    s_end_calendar_date = data.s_end_calendar_date,
                    s_end_time = data.s_end_time,
                    s_duration = data.s_duration,
                    s_net_duration = data.s_net_duration,
                    s_mill_code = data.s_mill_code,
                    s_head_code = data.s_head_code,
                    s_head_name = data.s_head_name,
                    s_sub_head_code = data.s_sub_head_code,
                    s_sub_head_name = data.s_sub_head_name,
                    s_comment = data.s_comment,
                    s_crtd_by = data.s_crtd_by,
                    s_crtd_dt = data.s_crtd_dt,
                    s_updt_by = data.s_updt_by,
                    s_updt_dt = data.s_updt_dt,
                    is_deleted = data.is_deleted,

                };
                // new record
                    data.id = data.id;
                    data.unit_code = data.unit_code;
                    data.season_code = data.season_code;
                    data.s_date = data.s_date;
                    data.s_start_calendar_date = data.s_start_calendar_date;
                    data.s_start_time = data.s_start_time;
                    data.s_end_calendar_date = data.s_end_calendar_date;
                    data.s_end_time = data.s_end_time;
                    data.s_duration = data.s_duration;
                    data.s_net_duration = data.s_net_duration;
                    data.s_mill_code = data.s_mill_code;
                    data.s_head_code = stoppage.s_head_code;
                    data.s_head_name = stoppage.s_head_name;
                    data.s_sub_head_code = stoppage.s_sub_head_code;
                    data.s_sub_head_name = stoppage.s_sub_head_name;
                    data.s_comment = stoppage.s_comment;
                    data.s_crtd_by = data.s_crtd_by;
                    data.s_crtd_dt = data.s_crtd_dt;
                    data.s_updt_by = stoppage.s_updt_by;
                    data.s_updt_dt = DateTime.Now;
                    data.is_deleted = data.is_deleted;
                
                    Db.SaveChanges();
                AuditRepo.CreateAuditTrail(AuditActionType.Update, stoppage.id.ToString(), oldRecord, data);
                return true;
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
            }
            return false;
        }


        public bool UpdateStoppageduration(int unitCode, int seasonCode, Stoppage stoppage)
        {
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                //Db.Entry(stoppage).State = System.Data.Entity.EntityState.Modified;
                var data = Db.Stoppages.Where(x => x.unit_code == unitCode
                                                && x.season_code == seasonCode
                                                && x.id == stoppage.id
                                                && x.is_deleted == false
                                                ).FirstOrDefault();
                if(data == null)
                {
                    return false;
                }
                try
                {
                    // Old Values
                    Stoppage OldStoppage = new Stoppage()
                    {
                        id = data.id,
                        unit_code = data.unit_code,
                        s_date = data.s_date,
                        season_code = data.season_code,
                        s_start_calendar_date = data.s_start_calendar_date,
                        s_start_time = data.s_start_time,
                        s_end_calendar_date = data.s_end_calendar_date,
                        s_end_time = data.s_end_time,
                        s_duration = data.s_duration,
                        s_net_duration = data.s_net_duration,
                        s_mill_code = data.s_mill_code,
                        s_head_code = data.s_head_code,
                        s_head_name = data.s_head_name,
                        s_sub_head_code = data.s_sub_head_code,
                        s_sub_head_name = data.s_sub_head_name,
                        s_comment = data.s_comment,
                        s_crtd_by = data.s_crtd_by,
                        s_crtd_dt = data.s_crtd_dt,
                        s_updt_by = stoppage.s_updt_by,
                        s_updt_dt = DateTime.Now,
                        sent_sms_count = data.sent_sms_count,
                        is_deleted = data.is_deleted
                    };

                    // New Values
                    data.s_start_time = stoppage.s_start_time;
                    data.s_end_time = stoppage.s_end_time;
                    data.s_mill_code = stoppage.s_mill_code;
                    data.s_duration = stoppage.s_duration;
                    data.s_net_duration = stoppage.s_net_duration;
                    data.s_head_code = stoppage.s_head_code;
                    data.s_head_name = stoppage.s_head_name;
                    data.s_sub_head_code = stoppage.s_sub_head_code;
                    data.s_sub_head_name = stoppage.s_sub_head_name;
                    data.s_comment = stoppage.s_comment;
                    data.s_crtd_by = data.s_crtd_by;
                    data.s_crtd_dt = data.s_crtd_dt;
                    data.is_deleted = data.is_deleted;
                    Db.SaveChanges();
                    AuditRepo.CreateAuditTrail(AuditActionType.Update, data.id.ToString(), OldStoppage, data);
                    return true;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
               

            }
        }
        public Stoppage GetStoppageDetailsById(int unitCode, int seasonCode, int Id)
        {
            try
            {
                return Db.Stoppages.Where(x => x.unit_code == unitCode
                                        && x.season_code == seasonCode
                                        && x.id == Id
                                        && x.is_deleted == false
                                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }

        }

        public proc_stoppageSummaryForDate_Result GetStoppageSummaryForDay(int unitCode, int seasonCode, DateTime reportDate)
        {
            
            proc_stoppageSummaryForDate_Result stoppageSummaryForDate = new proc_stoppageSummaryForDate_Result();
            try
            {
                stoppageSummaryForDate = Db.proc_stoppageSummaryForDate(unitCode, seasonCode, reportDate).FirstOrDefault();
                return stoppageSummaryForDate;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return stoppageSummaryForDate;
            }
        }

        public bool DeleteStoppage (int unitCode, int id, string updateBy)
        {
            Stoppage data = new Stoppage();
            data = Db.Stoppages.Where(x => x.id == id).FirstOrDefault();
            if (data.unit_code == unitCode)
            {
                try
                {


                    // Old Values
                    Stoppage OldStoppage = new Stoppage()
                    {
                        id = data.id,
                        unit_code = data.unit_code,
                        s_date = data.s_date,
                        season_code = data.season_code,
                        s_start_calendar_date = data.s_start_calendar_date,
                        s_start_time = data.s_start_time,
                        s_end_calendar_date = data.s_end_calendar_date,
                        s_end_time = data.s_end_time,
                        s_duration = data.s_duration,
                        s_net_duration = data.s_net_duration,
                        s_mill_code = data.s_mill_code,
                        s_head_code = data.s_head_code,
                        s_head_name = data.s_head_name,
                        s_sub_head_code = data.s_sub_head_code,
                        s_sub_head_name = data.s_sub_head_name,
                        s_comment = data.s_comment,
                        s_crtd_by = data.s_crtd_by,
                        s_crtd_dt = data.s_crtd_dt,
                        s_updt_by = updateBy,
                        s_updt_dt = DateTime.Now,
                        sent_sms_count = data.sent_sms_count,
                        is_deleted = data.is_deleted
                    };

                    // New Values
                    data.id = data.id;
                    data.unit_code = data.unit_code;
                    data.s_date = data.s_date;
                    data.season_code = data.season_code;
                    data.s_start_calendar_date = data.s_start_calendar_date;
                    data.s_start_time = data.s_start_time;
                    data.s_end_calendar_date = data.s_end_calendar_date;
                    data.s_end_time = data.s_end_time;
                    data.s_duration = data.s_duration;
                    data.s_net_duration = data.s_net_duration;
                    data.s_mill_code = data.s_mill_code;
                    data.s_head_code = data.s_head_code;
                    data.s_head_name = data.s_head_name;
                    data.s_sub_head_code = data.s_sub_head_code;
                    data.s_sub_head_name = data.s_sub_head_name;
                    data.s_comment = data.s_comment;
                    data.s_crtd_by = data.s_crtd_by;
                    data.s_crtd_dt = data.s_crtd_dt;
                    data.s_updt_by = updateBy;
                    data.s_updt_dt = DateTime.Now;
                    data.sent_sms_count = data.sent_sms_count;
                    data.is_deleted = true;
                    Db.SaveChanges();
                    AuditRepo.CreateAuditTrail(AuditActionType.Update, data.id.ToString(), OldStoppage, data);
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// a function which returns an integer value, as user wants a rounded figure.
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="decimalRoundAt"></param>
        /// <returns></returns>
        public int CustomRound(decimal minutes, decimal decimalRoundAt)
        {
            decimal decimalpoints = Math.Abs(minutes - Math.Floor(minutes));
            if(decimalpoints >= decimalRoundAt)
            {
                return (int)Math.Round(minutes);
            }
            else
            {
                return (int)Math.Floor(minutes);
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
                        FileName = "Stoppages",
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
                            FileName = "Daily Analyses",
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
