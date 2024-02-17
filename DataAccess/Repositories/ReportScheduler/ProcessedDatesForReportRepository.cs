using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataAccess.CustomModels;
using System.Linq;
namespace DataAccess.Repositories.ReportScheduler
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

        public List<ProcessedDatesForReport> GetProcessedDatesForreport(int unit_code, int season_code, int? report_status_code = null, DateTime? process_date = null , bool?  is_finalized_for_report = null )
        {
            try
            {
                using(_db)
                {
                    List<usp_select_ProcessedDatesForReport_Result> result = new List<usp_select_ProcessedDatesForReport_Result> ();
                    result = _db.usp_select_ProcessedDatesForReport(unit_code, season_code, report_status_code, process_date, is_finalized_for_report).ToList();
                    
                    if(result.Count > 0 )
                    {
                        List<ProcessedDatesForReport> dataList = new List<ProcessedDatesForReport>();
                        foreach(var x in result)
                        {
                            ProcessedDatesForReport data = new ProcessedDatesForReport()
                            {
                                Id = x.ID,
                                UnitCode = x.UnitCode,
                                SeasonCode = x.SeasonCode,
                                ProcessDate = x.ProcessDate,
                                FirstProcessedAt = x.FirstProcessedAt,
                                FirstProcessedBy = x.FirstProcessedBy,
                                RecentProcessedAt = x.RecentProcessedAt,
                                RecentProcessedBy = x.RecentProcessedBy,
                                ProcessCount = x.ProcessCount,
                                IsFinalizedForReport = x.IsFinalizedForReport,
                                DataFinalizedBy = x.DataFinalizedBy,
                                DataFinalizedAt = x.DataFinalizedAt,
                                ReportStatusCode = x.ReportStatusCode
                            };
                            dataList.Add(data);
                        }
                        return dataList;
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
            return null;
        }

        public ResponseStatusModel FinalizeReportById(int id, string finalized_by)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            try
            {
                using (_db)
                {
                    List<SqlParameter> outParams = new List<SqlParameter>();
                    outParams.Add(new SqlParameter("@status_code", SqlDbType.Int));
                    outParams.Add(new SqlParameter("@status_message", SqlDbType.NVarChar, 500));

                    List<SqlParameter> p = new List<SqlParameter>();
                    p.Add(new SqlParameter("@id",id));
                    p.Add(new SqlParameter("@finalized_by", finalized_by));

                    foreach (SqlParameter o in outParams)
                    {
                        o.Direction = ParameterDirection.Output;
                        p.Add(o);
                    }
                    try
                    {
                        _db.Database.ExecuteNonQuery("EXEC usp_finalize_report @id, @finalized_by, @status_code OUTPUT, @status_message OUTPUT", p.ToArray());
                        response.status_code = (int)outParams[0].Value;
                        response.status_message = (string)outParams[1].Value;
                    }catch(Exception ex)
                    {
                        new Exception(ex.Message);
                    }
                    
                }
            }
            catch(Exception ex)
            {
                response.status_code = 500;
                response.status_message = ex.Message;
            }
            return response;
        }
    }
}
