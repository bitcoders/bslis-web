using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data;
using DataAccess.CustomModels;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.Web.UI.WebControls;

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
        public bool CreateHourlyAnalysis(HourlyAnalysesViewModel m)
        {
            if (m == null)
            {
                return false;
            }
            try
            {
                ObjectParameter insertedRowsParam = new ObjectParameter("inserted_rows", System.Data.SqlDbType.Int);
                ObjectParameter errorMessageParam = new ObjectParameter("error_message", System.Data.SqlDbType.NVarChar);

                Db.usp_insert_hourlyAnalyses(m.hourlyAnalysesModel.unit_code, m.hourlyAnalysesModel.season_code, m.hourlyAnalysesModel.entry_Date, m.hourlyAnalysesModel.entry_time, m.hourlyAnalysesModel.new_mill_juice, m.hourlyAnalysesModel.old_mill_juice
                    , m.hourlyAnalysesModel.new_mill_water, m.hourlyAnalysesModel.old_mill_water, m.hourlyAnalysesModel.sugar_bags_L31, m.hourlyAnalysesModel.sugar_bags_L30
                    , m.hourlyAnalysesModel.sugar_bags_M31, m.hourlyAnalysesModel.sugar_bags_M30
                    , m.hourlyAnalysesModel.sugar_bags_S31, m.hourlyAnalysesModel.sugar_bags_S30, m.hourlyAnalysesModel.sugar_Biss, m.hourlyAnalysesModel.sugar_raw
                    , m.hourlyAnalysesModel.cooling_trace, m.hourlyAnalysesModel.cooling_pol, m.hourlyAnalysesModel.cooling_ph, m.hourlyAnalysesModel.standing_truck, m.hourlyAnalysesModel.standing_trolley, m.hourlyAnalysesModel.standing_trippler
                    , m.hourlyAnalysesModel.standing_cart, m.hourlyAnalysesModel.un_crushed_cane, m.hourlyAnalysesModel.crushed_cane, m.hourlyAnalysesModel.cane_diverted_for_syrup, m.hourlyAnalysesModel.diverted_syrup_quantity
                    , m.hourlyAnalysesModel.export_sugar, m.hourlyAnalysesModel.crtd_by, m.MillControlModel.imbibition_water_temp, m.MillControlModel.exhaust_steam_temp, m.MillControlModel.mill_biocide_dosing, m.MillControlModel.mill_washing, m.MillControlModel.mill_steaming, m.MillControlModel.sugar_bags_temp, m.MillControlModel.molasses_inlet_temp, m.MillControlModel.molasses_outlet_temp, m.MillControlModel.mill_hydraulic_pressure_one, m.MillControlModel.mill_hydraulic_pressure_two, m.MillControlModel.mill_hydraulic_pressure_three, m.MillControlModel.mill_hydraulic_pressure_four
                     , m.MillControlModel.mill_hydraulic_pressure_five
                     , m.MillControlModel.mill_load_one
                     , m.MillControlModel.mill_load_two
                     , m.MillControlModel.mill_load_three
                     , m.MillControlModel.mill_load_four
                     , m.MillControlModel.mill_load_five
                     , m.MillControlModel.mill_rpm_one
                     , m.MillControlModel.mill_rpm_two
                     , m.MillControlModel.mill_rpm_three
                     , m.MillControlModel.mill_rpm_four
                     , m.MillControlModel.mill_rpm_five
                    , insertedRowsParam, errorMessageParam);

                int insertedRows = (int)insertedRowsParam.Value;
                string errorMessage = errorMessageParam.Value.ToString();

                if (insertedRows > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
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
                return (false, ex.ToString());
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

        /// <summary>
        /// Get list of data from 'HourlyAnalyses' table only
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <param name="SeasonCode"></param>
        /// <param name="AnalysisDate"></param>
        /// <returns></returns>
        public List<HourlyAnalys> GetHourlyAnalysisList(int UnitCode, int SeasonCode, DateTime AnalysisDate)
        {
            List<HourlyAnalys> hourlyAnalyses = null;
            try
            {
                hourlyAnalyses = new List<HourlyAnalys>();
                hourlyAnalyses = Db.HourlyAnalyses.Where(temp => temp.unit_code == UnitCode && temp.season_code == SeasonCode && temp.entry_Date == AnalysisDate).OrderBy(x => x.id).ToList();
                return hourlyAnalyses;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return hourlyAnalyses;
            }
        }

        /// <summary>
        /// Get List of hourly Analyses data along with Mill Control Data for the date
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="entry_date"></param>
        /// <returns></returns>
        public List<HourlyAnalysesViewModel> GetHourlyAnalysesWithMillControlDataList(int unit_code, DateTime entry_date)
        {
            List<HourlyAnalysesViewModel> list = new List<HourlyAnalysesViewModel>();

            try
            {
                var data = Db.usp_select_hourlyAnalyses(unit_code, entry_date).ToList();

                if (data != null)
                {
                    foreach (var d in data)
                    {

                        HourlyAnalysesViewModel temp = new HourlyAnalysesViewModel();

                        HourlyAnalys h = new HourlyAnalys()
                        {
                            id = d.id,
                            unit_code = d.unit_code,
                            season_code = d.season_code,
                            entry_Date = d.entry_Date,
                            entry_time = d.entry_time,
                            new_mill_juice = d.new_mill_juice,
                            old_mill_juice = d.old_mill_juice,
                            juice_total = d.juice_total,
                            new_mill_water = d.new_mill_water,
                            old_mill_water = d.old_mill_water,
                            water_total = d.water_total,
                            sugar_bags_L31 = d.sugar_bags_L31,
                            sugar_bags_L30 = d.sugar_bags_L30,
                            sugar_bags_L_total = d.sugar_bags_L_total,
                            sugar_bags_M31 = d.sugar_bags_M31,
                            sugar_bags_M30 = d.sugar_bags_M30,
                            sugar_bags_M_total = d.sugar_bags_M_total,
                            sugar_bags_S31 = d.sugar_bags_S31,
                            sugar_bags_S30 = d.sugar_bags_S30,
                            sugar_bags_S_total = d.sugar_bags_L30,
                            sugar_Biss = d.sugar_Biss,
                            sugar_raw = d.sugar_raw,
                            sugar_bags_total = d.sugar_bags_total,
                            cooling_trace = d.cooling_trace,
                            cooling_pol = d.cooling_pol,
                            cooling_ph = d.cooling_ph,
                            standing_truck = d.standing_truck,
                            standing_trippler = d.standing_trippler,
                            standing_trolley = d.standing_trolley,
                            standing_cart = d.standing_cart,
                            un_crushed_cane = d.un_crushed_cane,
                            cane_diverted_for_syrup = d.cane_diverted_for_syrup,
                            diverted_syrup_quantity = d.diverted_syrup_quantity,
                            export_sugar = d.export_sugar
                        };

                        HourlyAnalysesMillControlData mc = new HourlyAnalysesMillControlData()
                        {
                            Id = (int)d.MillDataID,
                            HourlyAnalysesNo = (long)d.HourlyAnalysesNo,
                            entry_date = d.mill_data_entry_date,
                            entry_time = d.mill_data_entry_time,
                            imbibition_water_temp = d.imbibition_water_temp,
                            exhaust_steam_temp = d.exhaust_steam_temp,
                            mill_biocide_dosing = d.mill_biocide_dosing,
                            mill_washing = d.mill_washing,
                            mill_steaming = d.mill_steaming,
                            sugar_bags_temp = d.sugar_bags_temp,
                            molasses_inlet_temp = d.molasses_inlet_temp,
                            molasses_outlet_temp = d.molasses_outlet_temp,
                            mill_hydraulic_pressure_one = d.mill_hydraulic_pressure_one,
                            mill_hydraulic_pressure_two = d.mill_hydraulic_pressure_two,
                            mill_hydraulic_pressure_three = d.mill_hydraulic_pressure_three,
                            mill_hydraulic_pressure_four = d.mill_hydraulic_pressure_four
                            ,mill_hydraulic_pressure_five = d.mill_hydraulic_pressure_five
                            , mill_load_one = d.mill_load_one
                            , mill_load_two = d.mill_load_two
                            , mill_load_three = d.mill_load_three
                            , mill_load_four = d.mill_load_four
                            , mill_load_five = d.mill_load_five
                            , mill_rpm_one = d.mill_rpm_one
                            , mill_rpm_two = d.mill_rpm_two
                            , mill_rpm_three = d.mill_rpm_three
                            , mill_rpm_four = d.mill_rpm_four
                            , mill_rpm_five = d.mill_rpm_five
                        };

                        temp.hourlyAnalysesModel = h;
                        temp.MillControlModel = mc;
                        list.Add(temp);
                    }
                }
            }
            catch (Exception e)
            {
                new Exception(e.Message);
            }

            return list;
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
        public async Task<HourlyAnalys> GetLatestHourlyAnalysesDetails(int UnitCode, int SeasonCode)
        {
            HourlyAnalys hourlyAnalys = new HourlyAnalys();
            try
            {
                hourlyAnalys = await Task.FromResult(Db.HourlyAnalyses.Where
                    (x => x.unit_code == UnitCode && x.season_code == SeasonCode).OrderByDescending(x => x.id).FirstOrDefault());
                return hourlyAnalys;
            }
            catch (DbEntityValidationException ex)
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
            if (hourlyAnalysis == null)
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

                try
                {
                    Db.SaveChanges();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                
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
        public HourlyAnalys GetLastAnalysisDetailsForEntryDate(int UnitCode, int SeasonCode, DateTime EntryDate)
        {
            HourlyAnalys hourlyAnalysis = new HourlyAnalys();
            //hourlyAnalysis = Db.HourlyAnalyses.OrderByDescending
            //    (temp => temp.unit_code == UnitCode
            //    && temp.season_code == SeasonCode
            //    && temp.entry_Date == EntryDate
            //    ).FirstOrDefault();

            //.Where(temp=> temp.unit_code == UnitCode && temp.season_code == SeasonCode && temp.entry_Date == EntryDate).FirstOrDefault();
            hourlyAnalysis = Db.HourlyAnalyses.Where
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
            using(Db)
            {
                hourlyAnalysis = Db.HourlyAnalyses.Where(temp => temp.id == id && temp.unit_code == unitCode).FirstOrDefault();
            }
            
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
            catch (Exception ex)
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
            catch (Exception ex)
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

        public HourlyAnalysesViewModel GetMillControlDataById(int id, int unit_code)
        {
            try
            {
                HourlyAnalysesMillControlData d = new HourlyAnalysesMillControlData();
                using (Db)
                {
                    var r = Db.usp_select_hourlyAnalysesMillControlData(id, unit_code).FirstOrDefault();
                    if (r != null)
                    {
                        d.Id = r.mill_data_id;
                        d.HourlyAnalysesNo = Convert.ToInt32(r.HourlyAnalysesNo);
                        d.unit_code = r.mill_data_unit_code;
                        d.season_code = r.mill_data_season_code;
                        d.entry_date = r.mill_data_entry_date;
                        d.entry_time = r.mill_data_entry_time;
                        d.imbibition_water_temp = r.imbibition_water_temp;
                        d.exhaust_steam_temp = r.exhaust_steam_temp;
                        d.mill_biocide_dosing = r.mill_biocide_dosing;
                        d.mill_washing = r.mill_washing;
                        d.mill_steaming = r.mill_steaming;
                        d.sugar_bags_temp = r.sugar_bags_temp;
                        d.molasses_inlet_temp = r.molasses_outlet_temp;
                        d.molasses_outlet_temp = r.molasses_outlet_temp;
                        d.mill_hydraulic_pressure_one = r.mill_hydraulic_pressure_one;
                        d.mill_hydraulic_pressure_two = r.mill_hydraulic_pressure_two;
                        d.mill_hydraulic_pressure_three = r.mill_hydraulic_pressure_three;
                        d.mill_hydraulic_pressure_four = r.mill_hydraulic_pressure_four;
                        d.mill_hydraulic_pressure_five = r.mill_hydraulic_pressure_five;
                        d.mill_load_one = r.mill_load_one;
                        d.mill_load_two = r.mill_load_two;
                        d.mill_load_three = r.mill_load_three;
                        d.mill_load_four = r.mill_load_four;
                        d.mill_load_five = r.mill_load_five;

                        d.mill_rpm_one = r.mill_rpm_one;
                        d.mill_rpm_two = r.mill_rpm_two;
                        d.mill_rpm_three = r.mill_rpm_three;
                        d.mill_rpm_four = r.mill_rpm_four;
                        d.mill_rpm_five = r.mill_rpm_five;
                    }
                    HourlyAnalys h = new HourlyAnalys()
                    {
                        cooling_trace = r.cooling_trace,
                        cooling_ph = r.cooling_ph,
                        cooling_pol = r.cooling_pol
                    };
                    HourlyAnalysesViewModel model = new HourlyAnalysesViewModel()
                    {
                        MillControlModel = d,
                        hourlyAnalysesModel = h
                    };
                    return model;
                }
            }
            catch (Exception e)
            {
                new Exception(e.Message);
                return null;
            }

        }

        /// <summary>
        /// Get list of Mill control data fromm  only 'HourlyAnalysesMillControlData' table.
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="entry_date"></param>
        /// <returns></returns>
        public List<HourlyAnalysesMillControlData> GetMillControlDataByUnit(int unit_code, int season_code, DateTime entry_date)
        {
            List<HourlyAnalysesMillControlData> list = new List<HourlyAnalysesMillControlData>();
            try
            {
                using (Db)
                {
                    List<usp_selectAll_hourlyAnalysesMillControlData_Result> result = Db.usp_selectAll_hourlyAnalysesMillControlData(unit_code, entry_date, season_code).ToList();
                    if (result.Count > 0)
                    {

                        foreach (var r in result)
                        {
                            try
                            {
                                HourlyAnalysesMillControlData d = new HourlyAnalysesMillControlData
                                {
                                    Id = r.Id,
                                    HourlyAnalysesNo = Convert.ToInt32(r.HourlyAnalysesNo),
                                    unit_code = r.unit_code,
                                    season_code = r.season_code,
                                    entry_date = r.entry_date,
                                    entry_time = r.entry_time,
                                    imbibition_water_temp = r.imbibition_water_temp,
                                    exhaust_steam_temp = r.exhaust_steam_temp,
                                    mill_biocide_dosing = r.mill_biocide_dosing,
                                    mill_washing = r.mill_washing,
                                    mill_steaming = r.mill_steaming,
                                    sugar_bags_temp = r.sugar_bags_temp,
                                    molasses_inlet_temp = r.molasses_outlet_temp,
                                    molasses_outlet_temp = r.molasses_outlet_temp,
                                    mill_hydraulic_pressure_one = r.mill_hydraulic_pressure_one,
                                    mill_hydraulic_pressure_two = r.mill_hydraulic_pressure_two,
                                    mill_hydraulic_pressure_three = r.mill_hydraulic_pressure_three,
                                    mill_hydraulic_pressure_four = r.mill_hydraulic_pressure_four
                                };
                                list.Add(d);
                            }
                            catch
                            {
                                continue;
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                new Exception(e.Message);
            }
            return list;
        }


        public bool UpdateMillcontrolData(HourlyAnalysesViewModel model)
        {
            if(model == null) { return false; }
            else 
            {
                try
                {
                    using(Db)
                    {
                        Db.usp_update_mill_control_data(model.MillControlModel.unit_code
                            , model.MillControlModel.season_code
                            , model.MillControlModel.Id
                            , model.MillControlModel.imbibition_water_temp
                            , model.MillControlModel.exhaust_steam_temp
                            , model.MillControlModel.mill_biocide_dosing
                            , model.MillControlModel.mill_washing
                            , model.MillControlModel.mill_steaming
                            , model.MillControlModel.sugar_bags_temp
                            , model.MillControlModel.molasses_inlet_temp
                            , model.MillControlModel.molasses_outlet_temp
                            , model.hourlyAnalysesModel.cooling_trace
                            , model.hourlyAnalysesModel.cooling_pol
                            , model.hourlyAnalysesModel.cooling_ph
                            , model.MillControlModel.mill_hydraulic_pressure_one
                            , model.MillControlModel.mill_hydraulic_pressure_two
                            , model.MillControlModel.mill_hydraulic_pressure_three
                            , model.MillControlModel.mill_hydraulic_pressure_four
                            , model.MillControlModel.mill_hydraulic_pressure_five
                            , model.MillControlModel.mill_load_one
                            , model.MillControlModel.mill_load_two
                            , model.MillControlModel.mill_load_three
                            , model.MillControlModel.mill_load_four
                            , model.MillControlModel.mill_load_five
                            , model.MillControlModel.mill_rpm_one
                            , model.MillControlModel.mill_rpm_two
                            , model.MillControlModel.mill_rpm_three
                            , model.MillControlModel.mill_rpm_four
                            , model.MillControlModel.mill_rpm_five
                            );
                    }
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
}
