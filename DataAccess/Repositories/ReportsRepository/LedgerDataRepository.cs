using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces.ReportsInterface;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{
    public class LedgerDataRepository : ILedgerData
    {
        readonly SugarLabEntities Db;
        public LedgerDataRepository()
        {
            Db = new SugarLabEntities();
        }
        public List<ledger_data> GetLedgerDataForDates(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate)
        {
            return Db.ledger_data.Where(temp => temp.unit_code == unitCode
                                        && temp.season_code == seasonCode
                                        && temp.trans_date >= fromDate
                                        && temp.trans_date <= toDate
                                    ).ToList();
        }
        /// <summary>
        /// a function which returns ledger data one day earlier of the current entry date
        /// i.e. if entry date for the unit is 15-09-2019 then it will return ledger data 
        /// for 14-09-2019.
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="entryDate"></param>
        /// <returns></returns>
        public ledger_data GetLedgerDataForThePreviousDate(int unitCode, int seasonCode, DateTime entryDate)
        {
            DateTime previous_date = entryDate.AddDays(-1);
            ledger_data data = new ledger_data();
            try
            {
                data = Db.ledger_data.Where(temp => temp.unit_code == unitCode
                                        && temp.season_code == seasonCode
                                        && temp.trans_date == previous_date
                                        ).FirstOrDefault();
                return data;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return data;
            }
        }

        public ledger_data GetLedgerDataForTheDate(int unitCode, int seasonCode, DateTime entryDate)
        {
           
            ledger_data data = new ledger_data();
            try
            {
                data = Db.ledger_data.Where(temp => temp.unit_code == unitCode
                                        && temp.season_code == seasonCode
                                        && temp.trans_date == entryDate
                                        ).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return data;
            }
        }
       
        public func_ledger_data_period_summary_Result GetLedgerDataPeriodSummary(int unitCode, int seasonCode, DateTime startDate, DateTime EndDate)
        {
            func_ledger_data_period_summary_Result result = new func_ledger_data_period_summary_Result();
            result = Db.func_ledger_data_period_summary(unitCode, seasonCode, startDate, EndDate).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Asyncronus method to get the Ledgerdata Period Summary
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<func_ledger_data_period_summary_Result>  GetLedgerDataPeriodSummaryAsync(int unitCode, int seasonCode, DateTime startDate, DateTime EndDate)
        {
            func_ledger_data_period_summary_Result result = new func_ledger_data_period_summary_Result();
            result = await Task.FromResult(Db.func_ledger_data_period_summary(unitCode, seasonCode, startDate, EndDate).FirstOrDefault());
            return  result;
        }

        /// <summary>
        /// A function which returns the last processed dated data for the selected unit and season
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="seasonCode"></param>
        /// <returns>Ledger Data</returns>
        public async Task<ledger_data> GetLatestLedgerDataAsync(int unitCode, int seasonCode)
        {
            ledger_data result = new ledger_data();
            try
            {
                result = await Task.FromResult(Db.ledger_data.Where(r => r.unit_code == unitCode && r.season_code == seasonCode).OrderByDescending(x => x.trans_date).FirstOrDefault());
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                
            }
            return result;
        }
        
}
}
