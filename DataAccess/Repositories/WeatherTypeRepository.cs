using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class WeatherTypeRepository
    {
        /// <summary>
        /// Create or add a new weather type
        /// </summary>
        /// <param name="weatherType"></param>
        /// <returns></returns>
        public bool AddWeatherType(WeatherType weatherType)
        {
            
            try
            {
                using (SugarLabEntities db = new SugarLabEntities())
                {
                    WeatherType wt = new WeatherType()
                    {
                        Code = weatherType.Code,
                        Text = weatherType.Text,
                        Description = weatherType.Description,
                        IsActive = weatherType.IsActive
                    };
                    db.WeatherTypes.Add(wt);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public bool UpdateWeatherType(WeatherType weatherType)
        {
            WeatherType wt = new WeatherType();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    wt = Db.WeatherTypes.Where(x => x.Code == weatherType.Code).FirstOrDefault();
                    if (wt == null)
                    {
                        return false;
                    }
                    wt.Text = weatherType.Text;
                    wt.Description = weatherType.Description;
                    Db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            
        }

        public bool DisableWeatherType(string weatherTypeCode)
        {
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    WeatherType wt = new WeatherType();
                    wt = Db.WeatherTypes.Where(x => x.Code == weatherTypeCode).FirstOrDefault();
                    if (wt == null)
                    {
                        return false;
                    }
                    if(wt.IsActive == true)
                    {
                        wt.IsActive = false;
                    }
                    else
                    {
                        wt.IsActive = true;
                    }
                    Db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        public List<WeatherType> GetWeatherTypes()
        {
            List<WeatherType> weatherTypes = new List<WeatherType>();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    weatherTypes = Db.WeatherTypes.ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return weatherTypes;
        }

        public WeatherType GetWeatherTypeByCode(string code)
        {
            WeatherType weatherTypes = new WeatherType();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    weatherTypes = Db.WeatherTypes.Where(x=>x.Code == code).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return weatherTypes;
        }
    }
}
