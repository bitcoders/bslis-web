using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterUnit
    {
        bool AddMasterUnit(MasterUnit masterUnit);
        bool UpdateMasterUnit(MasterUnit masterUnit);
        bool DeleteMasterUnit(int code);
        List<MasterUnit> GetMasterUnitList();
        MasterUnit FindUnitByPk(int code);
        List<MasterUnit> GetMasterUnitDetailsByUnitCodes(string unitCodes);
    }
}
