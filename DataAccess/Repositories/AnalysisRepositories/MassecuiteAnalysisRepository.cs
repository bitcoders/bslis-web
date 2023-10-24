using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Interfaces;
namespace DataAccess.Repositories.AnalysisRepositories
{
    public class MassecuiteAnalysisRepository: IMassecuite
    {
       readonly public SugarLabEntities Db;
       public MassecuiteAnalysisRepository()
        {
            Db = new SugarLabEntities();
        }

        public bool AddMassecuite(MassecuiteAnalys massecuiteAnalysis)
        {
            try
            {
                Db.MassecuiteAnalyses.Add(massecuiteAnalysis);
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool Edit(MassecuiteAnalys massecuiteAnalysis)
        {
            try
            {
                Db.Entry(massecuiteAnalysis).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public List<MassecuiteAnalys> GetMassecuiteDetails(int unitCode, int seasonCode, DateTime entryDate)
        {
            try
            {
                return Db.MassecuiteAnalyses.Where
                    (
                        x => x.unit_code == unitCode
                        && x.season_code == seasonCode
                        && x.m_entry_date == entryDate
                    ).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public MassecuiteAnalys GetMassecuiteDetailsById(int Id, int unitCode, int CrushingSeason)
        {
            try
            {
                return Db.MassecuiteAnalyses.Where
                    (
                        x => x.id == Id
                        && x.unit_code == unitCode
                        && x.season_code == CrushingSeason
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
    }
}
