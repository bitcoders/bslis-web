using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.ReportsInterface
{
    interface ILedgerData
    {
        ledger_data GetLedgerDataForTheDate(int unitCode, int seasonCode, DateTime processDate);
        List<ledger_data> GetLedgerDataForDates(int unitCode, int seasonCode, DateTime fromDate, DateTime toDate);
    }
}
