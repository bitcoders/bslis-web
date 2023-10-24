using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repositories.AnalysisRepositories
{
   public class TwoHourlyAnalysisRepository : ITwoHourlyAnalysis
    {
        readonly SugarLabEntities db;
        public TwoHourlyAnalysisRepository()
        {
            db = new SugarLabEntities();
        }

        public bool AddTwoHourlyAnalysis(TwoHourlyAnalys twoHourlyAnalysis)
        {
            if(twoHourlyAnalysis == null)
            {
                return false;
            }
            try
            {
                db.TwoHourlyAnalyses.Add(twoHourlyAnalysis);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteTwoHourlyAnalysis(int id, int unitCode, int seasonCode)
        {
            throw new NotImplementedException();
        }

        public List<TwoHourlyAnalys> GetTwoHourlyAnalysis(int unitCode, int seasonCode, DateTime entryDate)
        {
            return db.TwoHourlyAnalyses.
                Where(a => a.Unit_Code == unitCode && a.season_code == seasonCode && a.Entry_Date == entryDate ).OrderBy(x=> x.Id).ToList();
        }

        public TwoHourlyAnalys GetTwoHourlyAnalysisDetails(int id, int unitCode, int seasonCode, DateTime EntryDate)
        {
            return db.TwoHourlyAnalyses.Where(a => a.Id == id && a.Unit_Code == unitCode && a.season_code == seasonCode  && a.Entry_Date == EntryDate ).FirstOrDefault();
        }

        public TwoHourlyAnalys GetTwoHourlyAnalysisDetailsByEntryTime(int unitCode, int seasonCode, DateTime entryDate, int entryTime)
        {
            return db.TwoHourlyAnalyses.
                Where
                    (
                        a => a.Unit_Code == unitCode
                        && a.season_code == seasonCode
                        && a.Entry_Date == entryDate
                        && a.entry_Time == entryTime
                    ).FirstOrDefault();
        }

        public bool UpdateTwoHourlyAnalysis(TwoHourlyAnalys twoHourlyAnalysis)
        {
            AuditRepository auditRepo = new AuditRepository();
            if(twoHourlyAnalysis == null)
            {
                return false;
            }
            try
            {
                var oldData = new object();
                var newData = new object();
                using (SugarLabEntities tempDb = new SugarLabEntities())
                {
                    tempDb.Configuration.ProxyCreationEnabled = false;
                     oldData = tempDb.TwoHourlyAnalyses.Where(x => x.Unit_Code == twoHourlyAnalysis.Unit_Code
                                                                && x.season_code == twoHourlyAnalysis.season_code
                                                                && x.Id == twoHourlyAnalysis.Id).FirstOrDefault();

                    
                }
                db.Entry(twoHourlyAnalysis).State = System.Data.Entity.EntityState.Modified;
                int savedRecords = db.SaveChanges();

                if(savedRecords > 0)
                {
                    using (SugarLabEntities tempDb = new SugarLabEntities())
                    {
                        tempDb.Configuration.ProxyCreationEnabled = false;
                        newData = tempDb.TwoHourlyAnalyses.Where(x => x.Unit_Code == twoHourlyAnalysis.Unit_Code
                                                                    && x.season_code == twoHourlyAnalysis.season_code
                                                                    && x.Id == twoHourlyAnalysis.Id).FirstOrDefault();


                    }
                    auditRepo.CreateAuditTrail(AuditActionType.Update, twoHourlyAnalysis.Id.ToString(), oldData, newData);
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                
            }
            return false;
        }


    }
}
