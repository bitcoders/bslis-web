using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.AnalysisRepositories;

namespace DataAccess.Repositories
{
    
    public class ReportSchemaRepository
    {
        AuditRepository auditRepository = new AuditRepository();
        // get list of Report schema
        public List<ReportSchema> GetReportSchemaList()
        {
            List<ReportSchema> result = new List<ReportSchema>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.ReportSchemas.ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return result;
            };
        }

        // get ReportSchema by its code

        public ReportSchema GetReportSchemaByCode(int id)
        {
            ReportSchema result = new ReportSchema();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.ReportSchemas.Where(x => x.Code == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return result;
            };
        }
        // Add
        public bool AddReportSchema(ReportSchema reportSchema)
        {
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                if(reportSchema == null)
                {
                    return false;
                }
                try
                {
                    Db.ReportSchemas.Add(reportSchema);
                    Db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return false;
            };
        }
        // Edit 
        public bool EditReportSchema (ReportSchema reportSchema)
        {
            if(reportSchema == null)
            {
                return false;
            }
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    ReportSchema data = Db.ReportSchemas.Find(reportSchema.Code);
                    if(data == null)
                    {
                        return false;
                    }
                    ReportSchema oldData = new ReportSchema()
                    {
                        Code = data.Code,
                        SysObjectName = data.SysObjectName,
                        SysObjectDescripton = data.SysObjectDescripton,
                        SearchKeywords = data.SearchKeywords,
                        IsActive = data.IsActive,
                        SchemaType = data.SchemaType,
                        CreatedDate = data.CreatedDate,
                        CreatedBy = data.CreatedBy,
                        UpdatedBy = data.UpdatedBy,
                        UpdatedDate = data.UpdatedDate
                    };

                    // assign new data
                    data.Code = reportSchema.Code;
                    data.SysObjectName = reportSchema.SysObjectName;
                    data.SysObjectDescripton = reportSchema.SysObjectDescripton;
                    data.IsActive = reportSchema.IsActive;
                    data.SchemaType = reportSchema.SchemaType;
                    data.CreatedDate = data.CreatedDate;
                    data.CreatedBy = data.CreatedBy;
                    data.UpdatedBy = reportSchema.UpdatedBy;
                    data.UpdatedDate = DateTime.Now;

                    Db.SaveChanges();
                    auditRepository.CreateAuditTrail(AuditActionType.Update, data.Code.ToString(), oldData, data);
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }
        // Delete -- update is active status
    }
}
