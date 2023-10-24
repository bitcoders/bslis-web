using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    interface IDataAdjustment
    {
        DataAdjustment GetDataAdjustmentByDate( DateTime EntryDate, int UnitCode, int SeasonCode);
        List<DataAdjustment> GetDataAdjustmentByUnit(int UnitCode = 0, int SeasonCode = 0);
        bool CreateDataAdjustment(DataAdjustment dataAdjustment);

        bool EditDataAdjustment(DataAdjustment dataAdjustment);
    }
}
