using System;
using System.Collections.Generic;
using DataAccess.Interfaces;
using System.Linq;

namespace DataAccess.Repositories
{
    public class MasterSeasonRepository : IMasterSeason
    {
        SugarLabEntities db;

        public MasterSeasonRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddSeason(MasterSeason masterSeason)
        {
            if(masterSeason == null)
            {
                return false;
            }
            try
            {
                db.MasterSeasons.Add(masterSeason);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool deleteSeason(int? id)
        {
            if(id == null)
            {
                return false;
            }
            try
            {
                MasterSeason masterSeason = db.MasterSeasons.Where(temp => temp.SeasonCode == id).FirstOrDefault();
                db.MasterSeasons.Remove(masterSeason);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public MasterSeason FindByCode(int? id)
        {
            MasterSeason masterSeason = null;
            if (id == null)
            {
                return null;
            }
            try
            {
                masterSeason = db.MasterSeasons.Where(temp => temp.SeasonCode == id).FirstOrDefault();
                return masterSeason;
                
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return masterSeason;
            }
        }

        public List<MasterSeason> GetMasterSeasonList()
        {
            return db.MasterSeasons.ToList();
        }

        public bool updateSeason(MasterSeason masterSeason)
        {
            if(masterSeason == null)
            {
                return false;
            }
            try
            {
                
                    MasterSeason ms = new MasterSeason();
                    ms = db.MasterSeasons.Where(temp => temp.SeasonCode == masterSeason.SeasonCode).FirstOrDefault();
                    ms.SeasonYear = masterSeason.SeasonYear;
                    ms.IsActive = masterSeason.IsActive;
                    db.SaveChanges();
                    return true;
               
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
    }
}
