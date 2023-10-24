using System;


namespace DataAccess.Interfaces.ReportsInterface
{
    interface IMeltingSummary
    {
        func_melting_period_summary_list_Result GetMeltingsTodateSummary(int unitCode, int seasonCode, DateTime reportDate);
    }
}
