using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.CustomModels;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class CaneAnalysisRepository : ICaneAnalyses
    {
        /// <summary>
        /// Add a new pre-season cane analysis data
        /// </summary>
        /// <param name="caneAnalysis"></param>
        /// <returns></returns>
        public string Add(CaneAnalys caneAnalysis)
        {
            if(caneAnalysis == null)
            {
                return "Invalid input provided!";
            }
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.CaneAnalyses.Add(caneAnalysis);
                    Db.SaveChanges();
                    return "Success: analysis saved sucessfully.";
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Delete(int unitCode, int Code)
        {
            throw new NotImplementedException();
        }

        public List<CaneAnalys> GetCaneAnalyisList(int unitCode)
        {
            throw new NotImplementedException();
        }

        public CaneAnalys GetCaneAnalysisByCode(int unitCode, int Code)
        {
            throw new NotImplementedException();
        }

        public string Update(int unitCode, int Code)
        {
            throw new NotImplementedException();
        }

        public List<CaneAnalysisViewModel> GetCaneAnalysisViewModelList(int unitCode, int seasonCode)
        {
            List<CaneAnalysisViewModel> models = new List<CaneAnalysisViewModel>();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@unitCode", unitCode));
            p.Add(new SqlParameter("@seasonCode", seasonCode));
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {

                    models = Db.Database.SqlQuery<CaneAnalysisViewModel>("Select ca.id, ca.UnitCode, ca.SampleDate, ca.AnalysisDate" +
                                    " , ca.ZoneCode, z.Name" +
                                    " , ca.VillageCode, v.Name villageName, g.Code, g.Name growerName, g.RelativeName, ca.VarietyCode, vr.Name varietyName , ct.name CaneType, LandPosition" +
                                    " ,  FieldCondition, JuicePercent, Brix, Pol, Purity, PolInCaneToday, RecoveryByWCapToday, RecoveryByMolPurityToday, PreviousSeasonHarvestingDate, SeasonCode" +
                                    " from CaneAnalyses ca, Zones z, Villages v, Growers g, CaneVarieties vr, CaneTypes ct" +
                                    " where ca.unitCode = z.unitCode and ca.zoneCode = z.code" +
                                    " and ca.unitCode = v.unitCode and ca.villageCode = v.code" +
                                    " and  ca.unitCode = g.unitCode and ca.growerCode = g.code and ca.VillageCode = g.VillageCode" +
                                    " and ca.varietyCode = vr.code" +
                                    " and ca.CaneType = ct.code" +
                                    " and ca.unitCode = @unitCode and ca.seasonCode = @seasonCode"
                        , p.ToArray()
                        ).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return models;
        }
    }
}
