using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataAccess.CustomModels;
namespace DataAccess.Repositories.AutoReportGeneration
{
    public class ProcessedDatesForReportRepository
    {
        private SugarLabEntities _db;
        public ProcessedDatesForReportRepository()
        {
            _db = new SugarLabEntities();
        }

        /// <summary>
        /// Executes usp_ProcessedDatesForReport
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public ResponseStatusModel InsertProcessedDates(ProcessedDatesForReportParameterModel m)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            if (m == null)
            {
                response.status_code = 400;
                response.status_message = "Bad Request!";
            }
            else
            {
                using(_db)
                {
                    try
                    {
                        List<SqlParameter> outParams = new List<SqlParameter>();
                        outParams.Add(new SqlParameter("@status_code", SqlDbType.Int));
                        outParams.Add(new SqlParameter("@status_message", SqlDbType.NVarChar, 500));
                        

                        List<SqlParameter> p = new List<SqlParameter>();
                        p.Add(new SqlParameter("@unit_code",m.unit_code));
                        p.Add(new SqlParameter("@season_code", m.season_code));
                        p.Add(new SqlParameter("@process_date", m.process_date));
                        p.Add(new SqlParameter("@processed_by", m.processed_by));
                        
                        foreach(SqlParameter o in outParams)
                        {
                            o.Direction = ParameterDirection.Output;
                            p.Add(o);
                        }
                        
                        _db.Database.ExecuteNonQuery("EXEC usp_ProcessedDatesForReport @unit_code, @season_code, @process_date, @processed_by, @status_code OUTPUT, @status_message OUTPUT",p.ToArray());
                        response.status_code = (int)outParams[0].Value;
                        response.status_message = (string)outParams[1].Value;
                    }
                    catch (Exception ex)
                    {
                        response.status_code = 500;
                        response.status_message = ex.Message;
                    }
                }
            }
            return response;
        }
    }
}
