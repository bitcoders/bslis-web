using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IReportDetails
    {
        
        bool AddReportDetail(ReportDetail reportDetail);
        bool EditReportDetail(ReportDetail reportDetail);
        bool DeleteReportDetail(int id);
        List<ReportDetail> getReportDetailsList();
        ReportDetail GetReportDetails(int id);
    }
}
