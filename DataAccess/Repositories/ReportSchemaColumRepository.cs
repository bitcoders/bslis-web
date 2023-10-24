using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ReportSchemaColumRepository
    {
        public List<ReportSchemaColumn> GetReportDataColumnsList(int schemaCode)
        {
            List<ReportSchemaColumn> columns = new List<ReportSchemaColumn>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    columns = Db.ReportSchemaColumns.Where(x => x.IsActive == true & x.SchemaCode == schemaCode).ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return columns;
        }

        public ReportSchemaColumn GetReportDataColumns(int Code)
        {
           ReportSchemaColumn columnDtl = new ReportSchemaColumn();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    columnDtl = Db.ReportSchemaColumns.Where(x => x.Code == Code).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return columnDtl;
        }

        public bool AddSchemaColumn(ReportSchemaColumn reportSchemaColumn)
        {
            if(reportSchemaColumn == null)
            {
                return false;
            }
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    Db.ReportSchemaColumns.Add(reportSchemaColumn);
                    Db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }
    }
}
