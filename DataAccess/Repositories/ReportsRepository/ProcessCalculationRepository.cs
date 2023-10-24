using System;
using DataAccess.CustomModels;
namespace DataAccess.Repositories.ReportsRepository
{
    public class ProcessCalculationRepository
    {
        readonly SugarLabEntities Db;

        ValidationRepository validationRepository = new ValidationRepository();
        public ProcessCalculationRepository()
        {
            Db = new SugarLabEntities();
        }
        public bool ProcessCalculation(int unitCode, int seasonCode, string ProcessDate)
        {
            try
            {
                if(validationRepository.analysesDataValidationBeforProcess(unitCode,seasonCode,ProcessDate)== true)
                {
                    //Db.proc_daily_calculation(unitCode, seasonCode, Convert.ToDateTime(ProcessDate));
                    //Db.proc_ProcessedAdditionalData(unitCode, seasonCode, Convert.ToDateTime(ProcessDate));
                    Db.DataProcessing(unitCode, seasonCode, Convert.ToDateTime(ProcessDate));
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// process Daily Calculation for date range
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ProcessCalculationForDateRange(ProcessCalculationModel model)
        {
            if(model == null)
            {
                return false;
            }
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    Db.proc_daily_calculation_for_dates(model.UnitCode, model.SeasonCode, Convert.ToDateTime(model.FromDate), Convert.ToDateTime(model.ToDate));
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
    }
}
