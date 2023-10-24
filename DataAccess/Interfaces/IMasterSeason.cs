using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterSeason
    {
        bool AddSeason(MasterSeason masterSeason);
        bool updateSeason(MasterSeason masterSeason);
        bool deleteSeason(int? id);
        List<MasterSeason> GetMasterSeasonList();
        MasterSeason FindByCode(int? id);
    }
}
