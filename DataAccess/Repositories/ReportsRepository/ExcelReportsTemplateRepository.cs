using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Repositories.ReportsRepository
{
    public class ExcelReportsTemplateRepository
    {
        public List<ExcelReportTemplate> GetExcepReportTemplate(int reportCode)
        {
            
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                List<ExcelReportTemplate> template = new List<ExcelReportTemplate>();
                template = Db.ExcelReportTemplates.Where(x =>  x.ReportCode == reportCode).ToList();
                return template;
            };
        }
    }
}
