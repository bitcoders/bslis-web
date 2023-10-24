using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PeriodicalStockRepository
    {
        // get list of entries order by date

        SugarLabEntities Db;
        readonly AuditRepository auditRepo = new AuditRepository();
        public PeriodicalStockRepository()
        {
            Db = new SugarLabEntities();
        }

        public List<PeriodicalStock> getPeriodicalStockList(int unitCode, int seasonCode)
        {

            List<PeriodicalStock> periodicalStocks = new List<PeriodicalStock>();

            try
            {
                
                using (Db)
                {
                    periodicalStocks = Db.PeriodicalStocks.Where(x => x.UnitCode == unitCode && x.SeasonCode == seasonCode && x.IsDeleted == false).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            

            return periodicalStocks;
        }

        public PeriodicalStock GetPeriodicalStock (System.Guid id)
        {
            PeriodicalStock periodicalStock = new PeriodicalStock();
            using(Db)
            {
                try
                {
                    periodicalStock = Db.PeriodicalStocks.Where(x => x.Id == id).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return periodicalStock;
        }

        // Add new Stock Data Entry

        public bool AddPerioducalStock(PeriodicalStock data)
        {
            bool result = false;
            if(data == null)
            {
                return false;
            }
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.PeriodicalStocks.Add(data);
                    if (Db.SaveChanges() == 1)
                    {
                        auditRepo.CreateAuditTrail(AuditActionType.Create,data.Id.ToString(), data, data);
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }

            }
            return result;
        }
            

        // Edit/Update new stock Data 

        public bool EditPerioducalStock(PeriodicalStock data)
        {
            bool result = false;
            PeriodicalStock oldData = new PeriodicalStock();
            PeriodicalStock newData = new PeriodicalStock();
            
            using(SugarLabEntities tempDb = new SugarLabEntities())
            {
                tempDb.Configuration.ProxyCreationEnabled = false;
                oldData = tempDb.PeriodicalStocks.Where(x => x.Id == data.Id).FirstOrDefault();
            }

            using(Db)
            {
                Db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                data.UnitCode = oldData.UnitCode;
                data.SeasonCode = oldData.SeasonCode;
                data.EntryDate = oldData.EntryDate;
                data.CreatedAt = oldData.CreatedAt;
                data.CreatedBy = oldData.CreatedBy;
                Db.SaveChanges();
                result = true;
            }

            using(SugarLabEntities tempDbTwo = new SugarLabEntities())
            {
                tempDbTwo.Configuration.ProxyCreationEnabled = false;
                newData = tempDbTwo.PeriodicalStocks.Where(x => x.Id == data.Id).FirstOrDefault();
            }
            auditRepo.CreateAuditTrail(AuditActionType.Update, data.Id.ToString(), oldData, newData);
            return result;
        }

        // Delete new stock data


    }
}
