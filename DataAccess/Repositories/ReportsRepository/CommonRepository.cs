using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ReportsRepository
{
    public class CommonRepository
    {
        public DateTime getMinProcessedDate (int unit_code, int season_code)
        {
            DateTime minDate = DateTime.Now.AddDays(-2);
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    minDate = Db.ledger_data.Where(@x => x.unit_code == unit_code && x.season_code == season_code)
                        .Min(@x => x.trans_date).GetValueOrDefault().Date;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return minDate;
        }

        public DateTime getMaxProcessedDate (int unit_code, int season_code)
        {
            DateTime maxDate = DateTime.Now.AddDays(-2); ;

            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    maxDate = Db.ledger_data.Where(@x => x.unit_code == unit_code && x.season_code == season_code)
                        .Max(@x => x.trans_date).GetValueOrDefault().Date;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return maxDate;
        }
    }
}
