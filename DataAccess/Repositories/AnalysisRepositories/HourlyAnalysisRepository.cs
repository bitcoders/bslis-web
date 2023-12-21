using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Core.Objects;

namespace DataAccess.Repositories.AnalysisRepositories
{
    public class HourlyAnalysisRepository : IHourlyAnalysis
    {
        readonly SugarLabEntities Db;
        readonly ExceptionRepository ExpRepository = new ExceptionRepository();
        readonly AuditRepository AuditRepo = new AuditRepository();
        public HourlyAnalysisRepository()
        {
            Db = new SugarLabEntities();
        }
        public bool CreateHourlyAnalysis(HourlyAnalys hourlyAnalysis)
        {
            if(hourlyAnalysis == null)
            {
                return false;
            }
            try
            {
                Db.HourlyAnalyses.Add(hourlyAnalysis);
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ExceptionLog exceptionLog = new ExceptionLog()
                {
                    Code = ex.HResult.GetTypeCode().ToString(),
                    FileName = ex.Source + ex.TargetSite.ToString(),
                    StackTrace = ex.StackTrace,
                    ErrorCode = ex.HResult.GetTypeCode().ToString(),
                    InnerException = ex.InnerException.ToString(),
                    IPAddress = null,
                    ExceptionOccuredAt = DateTime.Now,
                    ExceptionSolved = false,
                };
                Db.ExceptionLogs.Add(exceptionLog);
                return false;
            }
        }

        public (bool success, string message) DeleteHourlyAnalysis(int unit_code, string user_code, int lineId)
        {
            try
            {
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(Int32));
                ObjectParameter message = new ObjectParameter("message", typeof(string));  
                Db.usp_delete_hourlyAnalyses(unit_code, user_code, lineId, rowCount, message);
                int rowCountValue = (int)rowCount.Value;
                string messageValue = (string)message.Value;
                
                return (rowCountValue > 0, messageValue);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return (false,  ex.ToString());
            }
        }

        public bool EditHourlyAnalysis(HourlyAnalys hourlyAnalysis)
        {
            throw new NotImplementedException();
        }

        public List<HourlyAnalys> GetHourlyAnalysisDateWiseList(int UnitCode, int SeasonCode, DateTime FromDate, DateTime ToDate)
        {
            throw new NotImplementedException();
        }

        public List<HourlyAnalys> GetHourlyAnalysisList(int UnitCode, int SeasonCode, DateTime AnalysisDate)
        {
            List<HourlyAnalys> hourlyAnalyses = null;
            try
            {
                hourlyAnalyses = new List<HourlyAnalys>();
                hourlyAnalyses = Db.HourlyAnalyses.Where(temp => temp.unit_code == UnitCode && temp.season_code == SeasonCode && temp.entry_Date == AnalysisDate).OrderBy(x=>x.id).ToList();
                return hourlyAnalyses;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return hourlyAnalyses;
            }
            

        }

        /// <summary>
        /// Get Async list of all the analyses done by the unit in the crushing season.
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <param name="SeasonCode"></param>
        /// <returns></returns>
        public async Task<List<HourlyAnalys>> GetHourlyAnalysAllList(int UnitCode, int SeasonCode)
        {
            List<HourlyAnalys> hourlyAnalyses = null;
            try
            {
                hourlyAnalyses = new List<HourlyAnalys>();
                hourlyAnalyses = await Task.FromResult(Db.HourlyAnalyses.Where(temp => temp.unit_code == UnitCode && temp.season_code == SeasonCode).OrderBy(x => x.id).ToList());
                return hourlyAnalyses;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return hourlyAnalyses;
            }
        }


        /// <summary>
        /// The function will return details of Hourly Analyses which is latest i.e. the most last entry done by the Unit
        /// for the selected Crushing Season.
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <param name="SeasonCode"></param>
        /// <returns></returns>
        public async Task<HourlyAnalys> GetLatestHourlyAnalysesDetails(int UnitCode, int SeasonCode )
        {
            HourlyAnalys hourlyAnalys = new HourlyAnalys();
            try
            {
                hourlyAnalys = await Task.FromResult(Db.HourlyAnalyses.Where
                    (x => x.unit_code == UnitCode && x.season_code == SeasonCode).OrderByDescending(x=>x.id).FirstOrDefault());
                return hourlyAnalys;
            }
            catch(DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
                
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
            }
            return hourlyAnalys;
        }
        public bool UpdateHourlyAnalysis(HourlyAnalys hourlyAnalysis)
        {
            if(hourlyAnalysis == null)
            {
                return false;
            }
            //Db.Entry(hourlyAnalysis).State = System.Data.Entity.EntityState.Modified;
            try
            {
                HourlyAnalys temp = new HourlyAnalys();
                temp = Db.HourlyAnalyses.Where(x => x.id == hourlyAnalysis.id).FirstOrDefault();

                // old data
                HourlyAnalys OldHourly = new HourlyAnalys()
                {
                    id = temp.id,
                    unit_code = temp.unit_code,
                    season_code = temp.season_code,
                    entry_Date = temp.entry_Date,
                    entry_time = temp.entry_time,
                    new_mill_juice = temp.new_mill_juice,
                    old_mill_juice = temp.old_mill_juice,
                    juice_total = temp.juice_total,
                    new_mill_water = temp.new_mill_water,
                    old_mill_water = temp.old_mill_water,
                    water_total = temp.water_total,
                    sugar_bags_L31 = temp.sugar_bags_L31,
                    sugar_bags_L30 = temp.sugar_bags_L30,
                    sugar_bags_L_total = temp.sugar_bags_L_total,
                    sugar_bags_M31 = temp.sugar_bags_M31,
                    sugar_bags_M30 = temp.sugar_bags_M30,
                    sugar_bags_M_total = temp.sugar_bags_M_total,
                    sugar_bags_S31 = temp.sugar_bags_S31,
                    sugar_bags_S30 = temp.sugar_bags_S30,
                    sugar_raw = temp.sugar_raw,
                    sugar_bags_S_total = temp.sugar_bags_S_total,
                    sugar_Biss = temp.sugar_Biss,
                    sugar_bags_total = temp.sugar_bags_total,
                    cooling_trace = temp.cooling_trace,
                    cooling_pol = temp.cooling_pol,
                    cooling_ph = temp.cooling_ph,
                    standing_truck = temp.standing_truck,
                    standing_trippler = temp.standing_trippler,
                    standing_trolley = temp.standing_trolley,
                    standing_cart = temp.standing_cart,
                    un_crushed_cane = temp.un_crushed_cane,
                    crushed_cane = temp.crushed_cane,
                    updt_by = temp.updt_by,
                    updt_dt = temp.updt_dt,
                    cane_diverted_for_syrup = temp.cane_diverted_for_syrup,
                    diverted_syrup_quantity = temp.diverted_syrup_quantity,
                    export_sugar = temp.export_sugar,
                };

                // New Value
                temp.unit_code = hourlyAnalysis.unit_code; // this field will remain same
                temp.season_code = hourlyAnalysis.season_code; // this field will remain same
                temp.entry_Date = hourlyAnalysis.entry_Date; // this field will remain same
                temp.entry_time = hourlyAnalysis.entry_time; // this field will remain same
                temp.new_mill_juice = hourlyAnalysis.new_mill_juice;
                temp.old_mill_juice = hourlyAnalysis.old_mill_juice;
                temp.juice_total = hourlyAnalysis.juice_total;
                temp.new_mill_water = hourlyAnalysis.new_mill_water;
                temp.old_mill_water = hourlyAnalysis.old_mill_water;
                temp.water_total = hourlyAnalysis.water_total;
                temp.sugar_bags_L31 = hourlyAnalysis.sugar_bags_L31;
                temp.sugar_bags_L30 = hourlyAnalysis.sugar_bags_L30;
                temp.sugar_bags_L_total = hourlyAnalysis.sugar_bags_L_total;
                temp.sugar_bags_M31 = hourlyAnalysis.sugar_bags_M31;
                temp.sugar_bags_M30 = hourlyAnalysis.sugar_bags_M30;
                temp.sugar_bags_M_total = hourlyAnalysis.sugar_bags_M_total;
                temp.sugar_bags_S31 = hourlyAnalysis.sugar_bags_S31;
                temp.sugar_bags_S30 = hourlyAnalysis.sugar_bags_S30;
                temp.sugar_raw = hourlyAnalysis.sugar_raw;
                temp.sugar_bags_S_total = hourlyAnalysis.sugar_bags_S_total;
                temp.sugar_Biss = hourlyAnalysis.sugar_Biss;
                temp.sugar_bags_total = hourlyAnalysis.sugar_bags_total;
                temp.cooling_trace = hourlyAnalysis.cooling_trace;
                temp.cooling_pol = hourlyAnalysis.cooling_pol;
                temp.cooling_ph = hourlyAnalysis.cooling_ph;
                temp.standing_truck = hourlyAnalysis.standing_truck;
                temp.standing_trippler = hourlyAnalysis.standing_trippler;
                temp.standing_trolley = hourlyAnalysis.standing_trolley;
                temp.standing_cart = hourlyAnalysis.standing_cart;
                temp.un_crushed_cane = hourlyAnalysis.un_crushed_cane;
                temp.crushed_cane = hourlyAnalysis.crushed_cane;
                temp.cane_diverted_for_syrup = hourlyAnalysis.cane_diverted_for_syrup;
                temp.diverted_syrup_quantity = hourlyAnalysis.diverted_syrup_quantity;
                //temp.crtd_by = hourlyAnalysis.crtd_by;
                //temp.crtd_dt = hourlyAnalysis.crtd_dt;
                temp.updt_by = hourlyAnalysis.updt_by;
                temp.updt_dt = hourlyAnalysis.updt_dt;
                temp.export_sugar = hourlyAnalysis.export_sugar;
                
               
                Db.SaveChanges();
                AuditRepo.CreateAuditTrail(AuditActionType.Update, temp.id.ToString(), OldHourly, temp);
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            //Db.SaveChanges();
            //return true;
            
        }




        /// <summary>
        /// A method to get the last entry done for Hourly Analysis
        /// Using this method we can get the Next Entry time
        /// (at user entry form, user will be not able to change the entry time himself
        /// that is why we have to pick the next entry time programatically).
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <param name="SeasonCode"></param>
        /// <param name="EntryDate"></param>
        /// <returns></returns>
        public  HourlyAnalys GetLastAnalysisDetailsForEntryDate(int UnitCode, int SeasonCode, DateTime EntryDate)
        {
             HourlyAnalys hourlyAnalysis = new HourlyAnalys();
            //hourlyAnalysis = Db.HourlyAnalyses.OrderByDescending
            //    (temp => temp.unit_code == UnitCode
            //    && temp.season_code == SeasonCode
            //    && temp.entry_Date == EntryDate
            //    ).FirstOrDefault();

            //.Where(temp=> temp.unit_code == UnitCode && temp.season_code == SeasonCode && temp.entry_Date == EntryDate).FirstOrDefault();
            hourlyAnalysis =   Db.HourlyAnalyses.Where
                (temp => temp.unit_code == UnitCode
                && temp.season_code == SeasonCode
                && temp.entry_Date == EntryDate
                && temp.id == Db.HourlyAnalyses.Where(p => p.unit_code == UnitCode && p.season_code == SeasonCode && p.entry_Date == EntryDate)
                .Max(x => x.id)).FirstOrDefault();
            return hourlyAnalysis;
        }

        /// <summary>
        /// Returns HourlyAnalyses details by its ID and UnitCode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public HourlyAnalys GetHourlyAnalysisById(int id, int unitCode)
        {
            HourlyAnalys hourlyAnalysis = new HourlyAnalys();
            hourlyAnalysis = Db.HourlyAnalyses.Where(temp => temp.id == id && temp.unit_code == unitCode).FirstOrDefault();
            return hourlyAnalysis;
        }
        
        public List<func_hourly_data_for_period_Result> GetHourlyAnalysisSummaryForPeriod(int UnitCode, int SeasonCode)
        {
            List<func_hourly_data_for_period_Result> Result = new List<func_hourly_data_for_period_Result>();
            
            try
            {
                Result = Db.func_hourly_data_for_period(UnitCode, SeasonCode).ToList();
                return Result;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return Result;
            }
            
        }

        public func_hourly_data_for_period_Result GetHourlyAnalysisSummaryForDate(int unitCode, int seasonCode, DateTime reportDate)
        {
            func_hourly_data_for_period_Result result = null;
            try
            {
                result = Db.func_hourly_data_for_period(unitCode, seasonCode).Where(x => x.entry_date == reportDate && x.unit_code == unitCode).FirstOrDefault();
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

        /// <summary>
        /// A function to get the 'Cane Diversion Factor'. when a syrup is diverted to the distillery, than this factor will be calculated. the calculation is performed in a stored procedure.
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="entry_date"></param>
        /// <returns></returns>
        public usp_cane_diversion_factor_Result GetSyrupDiversionCaneFactor(int unit_code, int season_code, DateTime entry_date)
        {
            usp_cane_diversion_factor_Result result = new usp_cane_diversion_factor_Result();
            try
            {
                result = Db.usp_cane_diversion_factor(unit_code, season_code, entry_date).FirstOrDefault();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return result;
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
                        FileName = "HourlyAnalysisRepository",
                        StackTrace = ex.StackTrace,
                        ErrorCode = "501",
                        InnerException = ex.Message,
                        IPAddress = HttpContext.Current.Request.UserHostAddress,
                        ExceptionSolvedBy = "Admin",
                        ExceptionOccuredAt = DateTime.Now,
                    };
                    ExpRepository.AddException(exceptionLog);
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
                            FileName = "HourlyAnalysisRepository",
                            StackTrace = err,
                            ErrorCode = "501",
                            InnerException = ex.Message,
                            IPAddress = HttpContext.Current.Request.UserHostAddress,
                            ExceptionSolvedBy = "Admin",
                            ExceptionOccuredAt = DateTime.Now,
                        };
                        ExpRepository.AddException(exceptionLog);
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
