using System;

namespace DataAccess.Interfaces.ReportsInterface
{
    interface IMolassesSummary
    {
        func_molasses_period_summary_list_Result molassesTodateSummary(int unitCode, int seasonCode, DateTime reportDate);
    }
}
