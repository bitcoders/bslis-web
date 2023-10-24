using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using System.Data.Entity.Validation;
using DataAccess.Repositories;

namespace DataAccess.Repositories
{
    public class ReportDetailsRepository : IReportDetails
    {
        SugarLabEntities Db;
        ExceptionRepository expRepository;
        AuditRepository auditRepository;
        public ReportDetailsRepository()
        {
            Db = new SugarLabEntities();
            expRepository = new ExceptionRepository();
            auditRepository = new AuditRepository();
        }
        public bool AddReportDetail(ReportDetail reportDetail)
        {
            try
            {
                Db.ReportDetails.Add(reportDetail);
                Db.SaveChanges();
                return true;
            }
            catch(DbEntityValidationException ex)
            {
                SaveEntityExceptionLog(ex);
                return false;
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
                return false;
            }
        }

        public bool DeleteReportDetail(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditReportDetail(ReportDetail reportDetail)
        {
            var report = Db.ReportDetails.Where(x => x.Code == reportDetail.Code).FirstOrDefault();
            try
            {
                //oldValue;
                ReportDetail oldValues = new ReportDetail()
                {
                    Code = report.Code,
                    Name = report.Name,
                    Description = report.Description,
                    ReportSchemaCode = report.ReportSchemaCode,
                    ReportCategory = report.ReportCategory,
                    CreatedAt = report.CreatedAt,
                    CreatedBy = report.CreatedBy
                };
                // NewValue
                report.Code = report.Code;
                report.Name = reportDetail.Name;
                report.Description = reportDetail.Description;
                report.ReportSchemaCode = reportDetail.ReportSchemaCode;
                report.CreatedBy = report.CreatedBy;
                report.CreatedAt = report.CreatedAt;
                report.ReportCategory = report.ReportCategory;
                Db.SaveChanges();
                auditRepository.CreateAuditTrail(AuditActionType.Update,report.Code.ToString(), oldValues, report);
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }

        public ReportDetail GetReportDetails(int id)
        {
            ReportDetail entity = new ReportDetail();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                Db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    entity = Db.ReportDetails.Where(x => x.Code == id).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return entity;
        }

        public List<ReportDetail> getReportDetailsList()
        {
            List<ReportDetail> reportDetails = null;
            try
            {
                reportDetails = Db.ReportDetails.Where(x => x.IsActive == true).ToList();
                return  reportDetails;
            }
            catch(Exception ex)
            {
                SaveExceptionLogs(ex);
                return reportDetails;
            }
        }


        /// <summary>
        /// get list of report details which is based on Excel File Based template.
        /// </summary>
        /// <returns>List<ReportDetail></returns>
        public List<ReportDetail> GetExcelTemplateReportDetails(bool IsAdminOnly = false)
        {
            List<ReportDetail> reportDetails = null;
            try
            {
                reportDetails = Db.ReportDetails.Where(x => x.IsActive == true 
                && x.IsTemplateBased == true
                && x.AdminOnly == IsAdminOnly
                ).ToList();
                return reportDetails;
            }
            catch (Exception ex)
            {
                SaveExceptionLogs(ex);
                return reportDetails;
            }
        }
        private void SaveExceptionLogs(Exception ex)
        {
            if (ex != null)
            {
                try
                {
                    ExceptionLog exceptionLog = new ExceptionLog()
                    {
                        Code = "501",
                        FileName = "Daily Analyses",
                        StackTrace = ex.StackTrace,
                        ErrorCode = "501",
                        InnerException = ex.Message,
                        IPAddress = "191.168.1.1",
                        ExceptionSolvedBy = "Admin",
                        ExceptionOccuredAt = DateTime.Now,
                    };
                    expRepository.AddException(exceptionLog);
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        }

        private void SaveEntityExceptionLog(System.Data.Entity.Validation.DbEntityValidationException ex)
        {
            if (ex != null)
            {
                List<string> exceptionList = new List<string>();
                exceptionList = ex.EntityValidationErrors.Select(e => string.Join(Environment.NewLine, e.ValidationErrors.Select(v => string.Format("{0} - {1}", v.PropertyName, v.ErrorMessage)))).ToList();

                try
                {
                    foreach (var err in exceptionList)
                    {
                        ExceptionLog exceptionLog = new ExceptionLog()
                        {
                            Code = "501",
                            FileName = "Daily Analyses",
                            StackTrace = err,
                            ErrorCode = "501",
                            InnerException = ex.Message,
                            IPAddress = "191.168.1.1",
                            ExceptionSolvedBy = "Admin",
                            ExceptionOccuredAt = DateTime.Now,
                        };
                        expRepository.AddException(exceptionLog);
                    }
                }
                catch (Exception x)
                {
                    throw x;
                }

            }
        }
    }
}
