using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    interface IMeltings
    {
        bool AddMeltingAnalysis(MeltingAnalys meltingAnalysis);
        bool EditMelingAnalysis(MeltingAnalys meltingAnalysis);
        List<MeltingAnalys> GetMeltingAnalysesListByDate(DateTime entryDate, int unitCode);
        MeltingAnalys GetMeltingAnalysisById(int Id, int unitCode, int seasonCode,  DateTime entryDate);
    }
}
