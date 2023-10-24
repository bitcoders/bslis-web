using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace DataAccess.Interfaces
{
    interface IStoppage
    {
        bool AddStoppage(Stoppage stoppage);
        bool UpdateStoppage(Stoppage stoppage);
        List<Stoppage> GetStoppagesList(int unitCode, int season_code, DateTime entryDate);
        List<Stoppage> GetStoppageListByType(int unitCode, DateTime entryDate, int stoppageHead);
        Stoppage GetStoppageDetailsById(int unitCode, int seasonCode, int Id);
    }
}
