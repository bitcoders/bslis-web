using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using System.Data.Entity.Validation;

namespace DataAccess.Repositories.AnalysisRepositories
{
    public class DataAdjustmentRepository : IDataAdjustment
    {
        readonly SugarLabEntities Db = null;
        readonly ExceptionRepository expRepository;
        readonly AuditRepository auditRepo = new AuditRepository();

        public DataAdjustmentRepository()
        {
            Db = new SugarLabEntities();
            expRepository = new ExceptionRepository();
        }

        public DataAdjustment GetDataAdjustmentByDate(DateTime EntryDate, int UnitCode = 0, int seasonCode = 0)
        {
            DataAdjustment dataAdjustment = null;
            if (UnitCode == 0 || seasonCode == 0 || EntryDate == null)
            {
                return dataAdjustment;
            }
            try
            {
               dataAdjustment = Db.DataAdjustments
                                .Where(x => x.a_unit_code == UnitCode && x.a_season_code == seasonCode && x.a_entry_date == EntryDate).FirstOrDefault();
                return dataAdjustment;
            }
            catch (DbEntityValidationException ex)
            {
                EntitiValidationException(ex);
                return dataAdjustment;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return dataAdjustment;
            }
        }

        public List<DataAdjustment> GetDataAdjustmentByUnit(int UnitCode = 0, int SeasonCode = 0)
        {
            List<DataAdjustment> dataAdjustment = new List<DataAdjustment>();
            try
            {
                dataAdjustment = Db.DataAdjustments.Where(x => x.a_unit_code == UnitCode && x.a_season_code == SeasonCode).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                EntitiValidationException(ex);
                return dataAdjustment;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return dataAdjustment;
            }
            return dataAdjustment;
        }
        public bool CreateDataAdjustment(DataAdjustment dataAdjustment)
        {
            if(dataAdjustment == null)
            {
                return false;
            }
            try
            {
                Db.DataAdjustments.Add(dataAdjustment);
                Db.SaveChanges();
                return true;
            }
            catch(DbEntityValidationException ex)
            {
                EntitiValidationException(ex);
                return false;
            }
            catch(Exception ex)
            {
                ExceptionLog(ex);
                return false;
            }
        }

        public bool EditDataAdjustment(DataAdjustment dataAdjustment)
        {
            if(dataAdjustment == null)
            {
                return false;
            }
            try
            {
                
                using (SugarLabEntities tempDb = new SugarLabEntities())
                {
                    tempDb.Configuration.ProxyCreationEnabled = false;

                    
                    DataAdjustment temp = new DataAdjustment();

                    temp = tempDb.DataAdjustments.Where(x => x.id == dataAdjustment.id && x.a_unit_code == dataAdjustment.a_unit_code && x.a_season_code == dataAdjustment.a_season_code).FirstOrDefault();

                    ///old Data
                    DataAdjustment oldData = new DataAdjustment()
                    {
                        id = temp.id,
                        a_unit_code = temp.a_unit_code,
                        a_season_code = temp.a_season_code,
                        a_entry_date = temp.a_entry_date,
                        a_sulpher = temp.a_sulpher,
                        a_lime = temp.a_lime,
                        a_phosphoric_acid = temp.a_phosphoric_acid,
                        a_color_reducer = temp.a_color_reducer,
                        a_megnafloe = temp.a_megnafloe,
                        a_lub_oil = temp.a_lub_oil,
                        a_viscosity_reducer = temp.a_viscosity_reducer,
                        a_biocide = temp.a_biocide,
                        a_lub_greace = temp.a_lub_greace,
                        a_boiler_chemical = temp.a_boiler_chemical,
                        a_estimated_sugar = temp.a_estimated_sugar,
                        a_estimated_molasses = temp.a_estimated_molasses,
                        a_washing_soda = temp.a_washing_soda,
                        a_hydrolic_soda = temp.a_hydrolic_soda,
                        a_de_scaling_chemical = temp.a_de_scaling_chemical,
                        a_seed_slurry = temp.a_seed_slurry,
                        a_anti_fomer = temp.a_anti_fomer,
                        a_chemical_for_brs_cleaning = temp.a_chemical_for_brs_cleaning,
                        a_is_active = temp.a_is_active,
                        crtd_dt = temp.crtd_dt,
                        crtd_by = temp.crtd_by,
                        updt_by = temp.updt_by,
                        updt_dt = temp.updt_dt,
                        EstimatedCaneForSyrupDiversion = temp.EstimatedCaneForSyrupDiversion
                    };

                    // New Data
                    temp.a_entry_date = dataAdjustment.a_entry_date;
                    temp.a_sulpher = dataAdjustment.a_sulpher;
                    temp.a_lime = dataAdjustment.a_lime;
                    temp.a_phosphoric_acid = dataAdjustment.a_phosphoric_acid;
                    temp.a_color_reducer = dataAdjustment.a_color_reducer;
                    temp.a_megnafloe = dataAdjustment.a_megnafloe;
                    temp.a_lub_oil = dataAdjustment.a_lub_oil;
                    temp.a_viscosity_reducer = dataAdjustment.a_viscosity_reducer;
                    temp.a_biocide = dataAdjustment.a_biocide;
                    temp.a_lub_greace = dataAdjustment.a_lub_greace;
                    temp.a_boiler_chemical = dataAdjustment.a_boiler_chemical;
                    temp.a_estimated_sugar = dataAdjustment.a_estimated_sugar;
                    temp.a_estimated_molasses = dataAdjustment.a_estimated_molasses;
                    temp.a_washing_soda = dataAdjustment.a_washing_soda;
                    temp.a_hydrolic_soda = dataAdjustment.a_hydrolic_soda;
                    temp.a_de_scaling_chemical = dataAdjustment.a_de_scaling_chemical;
                    temp.a_seed_slurry = dataAdjustment.a_seed_slurry;
                    temp.a_anti_fomer = dataAdjustment.a_anti_fomer;
                    temp.a_chemical_for_brs_cleaning = dataAdjustment.a_chemical_for_brs_cleaning;
                    temp.a_is_active = true;
                    temp.crtd_by = temp.crtd_by;
                    temp.crtd_dt = temp.crtd_dt;
                    temp.updt_by = dataAdjustment.updt_by;
                    temp.updt_dt = DateTime.Now;
                    temp.EstimatedCaneForSyrupDiversion = dataAdjustment.EstimatedCaneForSyrupDiversion;
                    if (tempDb.SaveChanges() > 0)
                    {
                        auditRepo.CreateAuditTrail(AuditActionType.Update, dataAdjustment.id.ToString(), oldData, temp);
                        return true;
                    }
                }

                
                return false;
            }
            catch(DbEntityValidationException ex)
            {
                EntitiValidationException(ex);
                return false;
            }
            catch(Exception ex)
            {
                ExceptionLog(ex);
                return false;
            }
            
        }


        public DataAdjustment GetDetailsById(int UnitCode, int SeasonCode, int Id)
        {
            DataAdjustment dj = new DataAdjustment();
            try
            {
                dj = Db.DataAdjustments.Where(d => d.a_unit_code == UnitCode && d.a_season_code == SeasonCode && d.id == Id).FirstOrDefault();
            }
            catch (DbEntityValidationException ex)
            {
                EntitiValidationException(ex);
                return dj;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return dj;
            }
            return dj;
        }
        private void ExceptionLog(Exception ex)
        {
            ExceptionLog exceptionLog = new ExceptionLog()
            {
                Code = "100",
                FileName = "Data Adjustment",
                StackTrace = ex.StackTrace.ToString(),
                ErrorCode = "100",
                InnerException = ex.InnerException.ToString(),
                IPAddress = "",
                ExceptionSolvedBy = "Admiin",
                ExceptionOccuredAt = DateTime.Now,
                ExceptionSolved = false,
            };
            expRepository.AddException(exceptionLog);
        }
        private void EntitiValidationException(DbEntityValidationException ex)
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
                            FileName = "Data Adjustment",
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
