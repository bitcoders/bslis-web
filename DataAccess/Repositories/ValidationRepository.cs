using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;
using DataAccess.CustomModels;
namespace DataAccess.Repositories
{
    public class ValidationRepository
    {
        
        DateTime ? season_start_date, season_end_date;
        TimeSpan season_start_time, season_end_time;
        double report_start_time;
        int day_available_hours;

        

        /// <summary>
        /// Check either user have rights to do analyses entry for the season or not
        /// </summary>
        /// <param name="user_code"></param>
        /// <param name="season_code"></param>
        /// <returns></returns>
        public bool EntryAllowedForSeason(string user_code, string season_code)
        {
            bool allowed = false;
            MasterUser user;
            
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                 user = Db.MasterUsers.Where(x => x.Code == user_code).FirstOrDefault();
            }
            if(user != null)
            {
                string[] allowedSeasons = user.EntryAllowedSeasons.Trim().Split(',');
                if(allowedSeasons.Contains(season_code))
                {
                    allowed = true;
                }
            }
            return allowed;
        }
        /// <summary>
        /// Check either user have rights to update analyses entry for the season or not
        /// </summary>
        /// <param name="user_code"></param>
        /// <param name="season_code"></param>
        /// <returns></returns>
        public bool UpdationAllowedForSeason(string user_code, string season_code)
        {
            bool allowed = false;
            MasterUser user;

            using (SugarLabEntities Db = new SugarLabEntities())
            {
                user = Db.MasterUsers.Where(x => x.Code == user_code).FirstOrDefault();
            }
            if (user != null)
            {
                string[] allowedSeasons = user.ModificationAllowedForSeasons.Trim().Split(',');
                if (allowedSeasons.Contains(season_code))
                {
                    allowed = true;
                }
            }
            return allowed;
        }

        /// <summary>
        /// Check either user have rights to view analyses entry for the season or not
        /// </summary>
        /// <param name="user_code"></param>
        /// <param name="season_code"></param>
        /// <returns></returns>
        public bool ViewAllowedForSeason(string user_code, string season_code)
        {
            bool allowed = false;
            MasterUser user;

            using (SugarLabEntities Db = new SugarLabEntities())
            {
                user = Db.MasterUsers.Where(x => x.Code == user_code).FirstOrDefault();
            }
            if (user != null)
            {
                string[] allowedSeasons = user.ViewAllowedForSeasons.Trim().Split(',');
                if (allowedSeasons.Contains(season_code))
                {
                    allowed = true;
                }
            }
            return allowed;
        }


        /// <summary>
        /// Check all entries are done in hourly Analayses before performing the final calcuation (process)
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="process_date"></param>
        /// <returns></returns>
        public ValidationModel validateHourlyDataBeforeProcess(int unit_code, int season_code, string process_date)
        {
            season_details(unit_code, season_code,process_date);
            List<HourlyAnalys> hourlyAnalys = new List<HourlyAnalys>();
            ValidationModel validationModel = new ValidationModel();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                DateTime pDate = Convert.ToDateTime(process_date);
                hourlyAnalys = Db.HourlyAnalyses.Where(@h => h.unit_code == unit_code && h.season_code == season_code && h.entry_Date == pDate).ToList();
                
               
                if(hourlyAnalys == null)
                {
                    validationModel.validated = false;
                    validationModel.validationMessage = "'Hourly Entry' not found!";
                }
                if(hourlyAnalys.Count == day_available_hours)
                {
                    validationModel.validated = true;
                    validationModel.validationMessage = "'Hourly Entry' checked OK (" + hourlyAnalys.Count + "/" + day_available_hours.ToString() + ")!";
                }
                else
                {
                    validationModel.validated = false;
                    validationModel.validationMessage = "'Hourly Entry' incomplete (" + hourlyAnalys.Count + "/" + day_available_hours.ToString() + ")!";
                }
            }
            return validationModel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="process_date"></param>
        /// <returns></returns>
        public ValidationModel validateTwoHourlyDataBeforeProcess(int unit_code, int season_code, string process_date)
        {
            season_details(unit_code, season_code, process_date);
            List<TwoHourlyAnalys> twoHourlyAnalys = new List<TwoHourlyAnalys>();
            ValidationModel validationModel = new ValidationModel();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                
                DateTime pDate = Convert.ToDateTime(process_date);
                twoHourlyAnalys = Db.TwoHourlyAnalyses.Where(@t => t.Unit_Code == unit_code && t.season_code == season_code && t.Entry_Date == pDate.Date).ToList();
                if(twoHourlyAnalys != null)
                {
                    
                    bool totalHoursEvenodd = (day_available_hours % 2) == 0 ? true : false;
                    // if available hours in day are even number then there will be even hours like 20/2 = 10
                    // otherwise we are required to add 1 hour eg 21/2 = 10 but actual entries are 11 
                    // that is why such complex if condition is present here
                    if (twoHourlyAnalys.Count == 12 || twoHourlyAnalys.Count == (totalHoursEvenodd == true ? (day_available_hours / 2) :(day_available_hours/2)+1 ))
                    {
                        validationModel.validated = true;
                        validationModel.validationMessage = "Two hourly analyses entries are OK!";
                    }
                    else
                    {
                        validationModel.validated = false;
                        validationModel.validationMessage = "Two hourly analyses entries are incomplete!";
                    }
                }
                else
                { 
                    validationModel.validated = false;
                    validationModel.validationMessage = "Two hourly analyses entries not found for the date!";
                }
            }
            return validationModel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="process_date"></param>
        /// <returns></returns>
        public ValidationModel validateDailyAnalysesBeforeProcess(int unit_code, int season_code, string process_date)
        {
            season_details(unit_code, season_code, process_date);
            ValidationModel validationModel = new ValidationModel();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                DateTime pDate = Convert.ToDateTime(process_date);
                DailyAnalys dailyAnalys = Db.DailyAnalyses.Where(@d => d.unit_code == unit_code && d.season_code == season_code && d.entry_date == pDate.Date).FirstOrDefault();
                if(dailyAnalys != null)
                {
                    validationModel.validated = true;
                    validationModel.validationMessage = "Daily analyses OK!";
                }
                else
                {
                    validationModel.validated = false;
                    validationModel.validationMessage = "Daily analyses not found for the date!";
                }
            }
            return validationModel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="process_date"></param>
        /// <returns></returns>
        public ValidationModel validateStoppageDataBeforeProcess(int unit_code, int season_code, string process_date)
        {
            season_details(unit_code, season_code, process_date);
            ValidationModel validationModel = new ValidationModel();
            List<Stoppage> stoppages = new List<Stoppage>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                DateTime pDate = Convert.ToDateTime(process_date);
                stoppages = Db.Stoppages.Where(@s => s.unit_code == unit_code && s.season_code == season_code && s.s_date == pDate.Date).ToList();
                if(stoppages.Count > 0)
                {
                    foreach(var i in stoppages)
                    {
                        if(i.s_end_time != null)
                        {
                            validationModel.validated = true;
                            validationModel.validationMessage = "Stoppage entries OK!";
                        }
                        else
                        {
                            validationModel.validated = false;
                            validationModel.validationMessage = "Stoppage entries are incomplete!";
                            break;
                        }
                    }
                }
                if(stoppages.Count == 0)
                {
                    validationModel.validated = true;
                    validationModel.validationMessage = "Stoppage entries OK!";
                }
            }
            return validationModel;
        }


        public bool analysesDataValidationBeforProcess(int unit_code, int season_code, string process_date)
        {
            bool validated = false;
            ValidationRepository validationRepo = new ValidationRepository();
            List<ValidationModel> validationModels = new List<ValidationModel>();
            try
            {
                validationModels.Add(validationRepo.validateHourlyDataBeforeProcess(unit_code, season_code, process_date));
                validationModels.Add(validationRepo.validateTwoHourlyDataBeforeProcess(unit_code, season_code, process_date));
                validationModels.Add(validationRepo.validateDailyAnalysesBeforeProcess(unit_code, season_code, process_date));
                validationModels.Add(validationRepo.validateStoppageDataBeforeProcess(unit_code, season_code, process_date));

                foreach(var x in validationModels)
                {
                    if(x.validated == false)
                    {
                        validated = false;
                        break;
                    }
                    else
                    {
                        validated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return validated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="season_code"></param>
        /// <param name="process_date"></param>
        public void season_details (int unit_code, int season_code, string process_date)
        {
            UnitSeason unitSeason = new UnitSeason();
            MasterUnit masterUnit = new MasterUnit();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                unitSeason = Db.UnitSeasons.Where(@u => u.Code == unit_code && u.Season == season_code).FirstOrDefault();
                masterUnit = Db.MasterUnits.Where(@m => m.Code == unit_code && m.CrushingSeason == season_code).FirstOrDefault();
                season_start_date = unitSeason.CrushingStartDateTime;
                season_end_date = unitSeason.CrushingEndDateTime;
                season_start_time = unitSeason.CrushingStartDateTime.Value.TimeOfDay;
                season_end_time = unitSeason.CrushingEndDateTime.Value.TimeOfDay;
                report_start_time = unitSeason.ReportStartHourMinute.Value.Hours;

                if(unitSeason.CrushingStartDateTime.Value.Date != Convert.ToDateTime(process_date).Date && unitSeason.CrushingEndDateTime != Convert.ToDateTime(process_date).Date) 
                {
                    day_available_hours = 24;
                }

                else if(unitSeason.CrushingStartDateTime.Value.Date == Convert.ToDateTime(process_date))
                {
                    // i.e. its first crushing date of season
                    DateTime tempCrushingStartDateTime = season_start_date.Value.Date.AddHours(season_start_date.Value.Hour);
                    DateTime nextCurhingDate = (unitSeason.CrushingStartDateTime.Value.Date.AddDays(1)).AddHours(unitSeason.ReportStartHourMinute.Value.Hours);
                    day_available_hours = (nextCurhingDate - tempCrushingStartDateTime).Hours;
                }
                else if(unitSeason.CrushingEndDateTime.Value.Date == Convert.ToDateTime(process_date))
                {
                    DateTime secondLastCrushing = (unitSeason.CrushingEndDateTime).Value.AddDays(-1).Date.AddHours(unitSeason.ReportStartHourMinute.Value.Hours);
                    day_available_hours = (unitSeason.CrushingEndDateTime - secondLastCrushing).Value.Hours;
                }
                else
                {
                    day_available_hours = 24;
                }
                
                //else if(unitSeason.CrushingStartDateTime.Value.Date == Convert.ToDateTime(process_date).Date)
                //{
                //    if(unitSeason.CrushingStartDateTime.Value.Hour > unitSeason.ReportStartHourMinute.Value.Hours)
                //    {
                //        day_available_hours = 24 - (unitSeason.CrushingEndDateTime.Value.Hour - unitSeason.ReportStartHourMinute.Value.Hours);
                //    }
                //    else
                //    {
                //        day_available_hours = (unitSeason.ReportStartHourMinute.Value.Hours- unitSeason.CrushingEndDateTime.Value.Hour);
                //    }
                //}
                //else
                //{
                //    if (unitSeason.CrushingEndDateTime.Value.Hour > unitSeason.ReportStartHourMinute.Value.Hours)
                //    {
                //        day_available_hours = 24- (unitSeason.CrushingEndDateTime.Value.Hour - unitSeason.ReportStartHourMinute.Value.Hours);
                //    }
                //    else
                //    {
                //        day_available_hours = (unitSeason.ReportStartHourMinute.Value.Hours - unitSeason.CrushingEndDateTime.Value.Hour);
                //    }
                //}
            }
        }
    }
}
