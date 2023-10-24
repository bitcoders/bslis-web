using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Repositories.ReportsRepository;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class MasterUnitRepository :IMasterUnit
    {
        private SugarLabEntities db;
        public MasterUnitRepository()
        {
            db = new SugarLabEntities();
        }
        readonly AuditRepository AuditRepo = new AuditRepository();
        public bool AddMasterUnit(MasterUnit masterUnit)
        {
            if(masterUnit == null)
            {
                return false;
            }
            try
            {
                db.MasterUnits.Add(masterUnit);
                db.SaveChanges();
                return true;
            }
            catch(Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
        }

        public bool DeleteMasterUnit(int code)
        {
            MasterUnit masterUnit = db.MasterUnits.Where(i => i.Code == code).FirstOrDefault();
            if(masterUnit == null)
            {
                return false;
            }
            try
            {
                db.MasterUnits.Remove(masterUnit);
                db.SaveChanges();
            }
            catch(Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
            return true;
        }

        public MasterUnit FindUnitByPk(int code)
        {
            //MasterUnit masterUnit = new MasterUnit();
            try
            {
                //masterUnit = db.MasterUnits.Where(i => i.Code == code).FirstOrDefault();
                //return masterUnit; // db.MasterUnits.Where(i => i.Code == code).FirstOrDefault();
                MasterUnit unit = new MasterUnit();
                unit = db.MasterUnits.Where(temp => temp.Code == code).FirstOrDefault();
                return unit;
            }
            catch (Exception exception)
            {
                SaveExceptionLogs(exception);
                new Exception(exception.Message);
                return null;
            }
        }

        public List<MasterUnit> GetMasterUnitList()
        {
            try
            {
                return db.MasterUnits.ToList();
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public List<MasterUnit> GetActiveMasterUnitList()
        {
            try
            {
                return db.MasterUnits.Where(x=> x.IsActive == true).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMasterUnit(MasterUnit masterUnit)
        {
            if(masterUnit == null)
            {
                return false;
            }
            try
            {
                //db.Entry(masterUnit).State = System.Data.Entity.EntityState.Modified;

                using (SugarLabEntities Db = new SugarLabEntities())
                {

                    Db.Configuration.ProxyCreationEnabled = false;
                    MasterUnit unit = Db.MasterUnits.Where(x => x.Code == masterUnit.Code).FirstOrDefault();

                    if (unit == null)
                    {
                        return false;
                    }
                    // Old Values
                    MasterUnit OldValues = new MasterUnit
                    {
                        Id = unit.Id,
                        CompanyCode = unit.CompanyCode,
                        Code = unit.Code,
                        Name = unit.Name,
                        Address = unit.Address,
                        CrushingSeason = unit.CrushingSeason,
                        CrushingStartDate = unit.CrushingStartDate,
                        CrushingEndDate = unit.CrushingEndDate,
                        DayHours = unit.DayHours,
                        EntryDate = unit.EntryDate,
                        ProcessDate = unit.ProcessDate,
                        NewMillCapacity = unit.NewMillCapacity,
                        OldMillCapacity = unit.OldMillCapacity,
                        IsActive = unit.IsActive,
                        CreatedDate = unit.CreatedDate,
                        Createdy = unit.Createdy,
                        ReportStartTime = unit.ReportStartTime,
                        CrushingStartTime = unit.CrushingStartTime,
                        CrushingEndTime = unit.CrushingEndTime,
                        AllowedModificationDays = unit.AllowedModificationDays,
                        CrushingStartDateTime = unit.CrushingStartDateTime,
                        CrushingEndDateTime = unit.CrushingStartDateTime,
                        ReportStartHourMinute = unit.ReportStartHourMinute,
                        UpdatedBy = unit.UpdatedBy,
                        UpdatedDate = unit.UpdatedDate,
                    };
                    // New values
                    unit.Id = OldValues.Id;
                    unit.CompanyCode = OldValues.CompanyCode;
                    unit.Code = OldValues.Code;
                    unit.Name = masterUnit.Name;
                    unit.Address = masterUnit.Address;
                    unit.CrushingSeason = masterUnit.CrushingSeason;
                    unit.CrushingStartDate = masterUnit.CrushingStartDate;
                    unit.CrushingEndDate = masterUnit.CrushingEndDate;
                    unit.DayHours = masterUnit.DayHours;
                    unit.EntryDate = masterUnit.EntryDate;
                    unit.ProcessDate = masterUnit.ProcessDate;
                    unit.NewMillCapacity = masterUnit.NewMillCapacity;
                    unit.OldMillCapacity = masterUnit.OldMillCapacity;
                    unit.IsActive = masterUnit.IsActive;
                    unit.CreatedDate = OldValues.CreatedDate;
                    unit.Createdy = OldValues.Createdy;
                    unit.ReportStartTime = masterUnit.ReportStartTime;
                    unit.CrushingStartTime = masterUnit.CrushingStartTime;
                    unit.CrushingEndTime = masterUnit.CrushingEndTime;
                    unit.AllowedModificationDays = masterUnit.AllowedModificationDays;
                    unit.CrushingStartDateTime = masterUnit.CrushingStartDateTime;
                    unit.CrushingEndDateTime = masterUnit.CrushingEndDateTime;
                    unit.ReportStartHourMinute = masterUnit.ReportStartHourMinute;
                    unit.UpdatedDate = DateTime.Now;
                    unit.UpdatedBy = masterUnit.UpdatedBy;
                    Db.SaveChanges();
                    AuditRepo.CreateAuditTrail(AuditActionType.Update, unit.Id.ToString(), OldValues, unit);
                }
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateUnitDailyParameter(MasterUnit masterUnit)
        {
            if(masterUnit == null)
            {
                return false;
            }
            try
            {

                /// using ledger data, will check what is the latest processed data date for the selected unit,
                /// based on this date we will check either the unit is allowed to do back date entry or modification or not
                LedgerDataRepository ledgerDataRepository = new LedgerDataRepository();
                ledger_data ledger = new ledger_data();
                ledger = await ledgerDataRepository.GetLatestLedgerDataAsync(masterUnit.Code, masterUnit.CrushingSeason);

                MasterUnit Entity = db.MasterUnits.Where(x => x.Code == masterUnit.Code).FirstOrDefault();
                //db.Entry(masterUnit).State = System.Data.Entity.EntityState.Modified;
                if (ledger!= null)
                {
                    
                    DateTime ChangedDate = masterUnit.EntryDate;
                    int.TryParse(Entity.AllowedModificationDays.ToString(), out int AllowedMofificationDays);
                    double daysDifferece = Convert.ToDateTime(ledger.trans_date).Subtract(ChangedDate).TotalDays;
                    if (daysDifferece > AllowedMofificationDays)
                    {
                        return await Task.FromResult(false);
                    }
                }
               

                Entity.CrushingStartDate = masterUnit.CrushingStartDate;
                Entity.CrushingSeason = masterUnit.CrushingSeason;
                Entity.CrushingEndDate = masterUnit.CrushingEndDate;
                Entity.DayHours = masterUnit.DayHours;
                Entity.EntryDate = masterUnit.EntryDate;
                Entity.ProcessDate = masterUnit.ProcessDate;
                try
                {
                    db.SaveChanges();
                    return await Task.FromResult(true);
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return await Task.FromResult(false);
                }
                
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return await Task.FromResult(false);
            }
            
        }

        /// <summary>
        /// provide a comma saperated lit of unit codes to get the details of units
        /// </summary>
        /// <param name="unitCodes"></param>
        /// <returns></returns>
        public List<MasterUnit> GetMasterUnitDetailsByUnitCodes(string unitCodes)
        {
            List<MasterUnit> masterUnits = new List<MasterUnit>();
            try
            {
                List<int> unitCodeArray = unitCodes.Split(',').Select(int.Parse).ToList();
                
                masterUnits = db.MasterUnits.Where(x=> unitCodeArray.Contains(x.Code)).ToList();
                return masterUnits;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        private void SaveExceptionLogs(Exception ex)
        {
            if (ex != null)
            {
                ExceptionRepository expRepository = new ExceptionRepository();
                ExceptionLog exceptionLog = new ExceptionLog()
                {
                    Code = "501",
                    FileName = "Daily Analyses",
                    StackTrace = ex.StackTrace,
                    ErrorCode = "501",
                    InnerException = ex.InnerException.Message,
                    IPAddress = "191.168.1.1",
                    ExceptionSolvedBy = "Admin",
                    ExceptionOccuredAt = DateTime.Now,
                };
                expRepository.AddException(exceptionLog);
            }

        }
    }
}
