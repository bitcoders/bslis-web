using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.CustomModels;
using DataAccess.CustomModels.Reports;

namespace DataAccess.Repositories
{
    public class DashboardRepository
    {
        public List<usp_dashboard_select_hourlyData_Result> GetHourlyDataSummary(string user_code, int company_code, int unit_code, int season_code, DateTime entry_date)
        {
            using(SugarLabEntities _db = new SugarLabEntities())
            {
                try
                {
                    List<SqlParameter> sqlParam = new List<SqlParameter>()
                {
                    new SqlParameter("company_code", SqlDbType.Int){Value = company_code},
                    new SqlParameter("unit_code",SqlDbType.Int){Value = unit_code},
                    new SqlParameter("season_code",SqlDbType.Int){Value = season_code},
                    new SqlParameter("entry_date",SqlDbType.DateTime){Value = entry_date}
                };
                    List<usp_dashboard_select_hourlyData_Result> dModel = _db.Database.SqlQuery<usp_dashboard_select_hourlyData_Result>("exec usp_dashboard_select_HourlyData @company_code, @unit_code, @season_code, @entry_date", sqlParam.ToArray()).ToList();
                    return dModel;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Get data for dashboard for processed dates.
        /// </summary>
        /// <param name="user_code"></param>
        /// <param name="company_code"></param>
        /// <param name="season_code"></param>
        /// <param name="history_days"></param>
        /// <returns></returns>
        public List<DashboardProcessedData> GetProcessedDataSummary(string user_code, int company_code, int season_code, int history_days)
        {
            using(SugarLabEntities _db = new SugarLabEntities())
            {
                try
                {
                    List<SqlParameter> sqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("company_code", SqlDbType.Int){Value = company_code},
                        new SqlParameter("season_code",SqlDbType.Int){Value = season_code},
                        new SqlParameter("history_days",SqlDbType.Int){Value = history_days}
                    };
                    var result = _db.Database.SqlQuery<DashboardProcessedData>("EXEC usp_dashboard_select_processedData @company_code , @season_code, @history_days ", sqlParam.ToArray()).ToList();
                    List<DashboardProcessedData> dModel = new List<DashboardProcessedData>();
                    if (result.Count > 0)
                    {
                        foreach(var i in result)
                        {
                            DashboardProcessedData temp = new DashboardProcessedData()
                            {
                                id = i.id,
                                entry_date = i.entry_date,
                                unit_code = i.unit_code,
                                unit_name = i.unit_name,
                                company_code = i.company_code,
                                company_name = i.company_name,
                                cane_crushed = i.cane_crushed,
                                estimated_sugar_percent_cane = i.estimated_sugar_percent_cane,
                                estimated_recovery_on_syrup = i.estimated_recovery_on_syrup,
                                estimated_sugar_percent_on_b_heavy = i.estimated_sugar_percent_on_b_heavy,
                                estimated_sugar_percent_on_c_heavy = i.estimated_sugar_percent_on_c_heavy,
                                estimated_sugar_percent_on_raw_sugar = i.estimated_sugar_percent_on_raw_sugar,
                                total_losses_percent_cane = i.total_losses_percent_cane,
                                unknwon_loss_percent_cane = i.unknwon_loss_percent_cane,
                                steam_percent_cane = i.steam_percent_cane,
                                total_bagasse_percent_cane = i.total_bagasse_percent_cane,
                                total_sugar_bagged = i.total_sugar_bagged,
                                estimated_molasses_percent_cane = i.estimated_molasses_percent_cane,
                                fiber_percent_cane = i.fiber_percent_cane,
                                pol_in_cane = i.pol_in_cane,
                            };
                            dModel.Add(temp);
                        }
                    }
                    
                    return dModel;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message); return null;
                }
            }
        }
    }
}
