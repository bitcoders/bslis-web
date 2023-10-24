using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Interfaces;

namespace DataAccess.Repositories.AnalysisRepositories
{
    public class KeySampleAnalysRepository : IKeySample
    {
        SugarLabEntities Db;
        public KeySampleAnalysRepository()
        {
            Db = new SugarLabEntities();
        }
        public bool AddKeySample(KeySampleAnalys keySampleAnalys)
        {
            try
            {
                Db.KeySampleAnalyses.Add(keySampleAnalys);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool EditKeySample(KeySampleAnalys keySampleAnalys)
        {
            try
            {
                Db.Entry(keySampleAnalys).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public KeySampleAnalys GetKeySampleAnalysById(int unitCode, int seasonCode, int Id, DateTime entryDate)
        {
            try
            {
                return Db.KeySampleAnalyses.Where
                    (
                        x => x.id == Id
                        && x.unit_code == unitCode
                        && x.season_code == seasonCode
                        && x.entry_date == entryDate
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<KeySampleAnalys> GetKeySampleAnalysListByDate(DateTime entryDate, int unitCode)
        {
            try
            {
                return Db.KeySampleAnalyses.Where
                    (
                        x => x.unit_code == unitCode
                        && x.entry_date == entryDate
                    ).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
    }
}
