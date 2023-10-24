using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.ReportsInterface
{
    interface IMassecuiteSummary
    {
        func_massecuite_summary_Result massecuteToDateSummary(int unitCode, int seasonCode, DateTime reportDate);
    }
}
