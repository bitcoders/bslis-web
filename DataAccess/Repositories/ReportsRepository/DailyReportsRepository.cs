using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{
    public abstract class DailyReportsRepository
    {
        public abstract string GeneratePdf(int unitCode, int seasonCode, DateTime reportDate, string path);
    }
}
