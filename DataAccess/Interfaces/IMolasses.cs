using System;
using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IMolasses
    {
        bool AddMolasses(MolassesAnalys molassesAnalysis);
        bool Edit(MolassesAnalys molassesAnalysis);
        List<MolassesAnalys> GetMolassesListByDate(DateTime entryDate, int unitCode);
        MolassesAnalys GetMolassesDetailsById(int id, int unitCode, int seasonCode, DateTime EntryDate);
    }
}
