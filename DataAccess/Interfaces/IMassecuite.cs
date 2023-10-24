using System;
using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IMassecuite
    {
        bool AddMassecuite(MassecuiteAnalys massecuiteAnalysis);
        bool Edit(MassecuiteAnalys massecuiteAnalysis);
        MassecuiteAnalys GetMassecuiteDetailsById(int Id, int unitCode, int CrushingSeason);
        List<MassecuiteAnalys> GetMassecuiteDetails(int unitCode, int seasonCode, DateTime entryDate);
    }
}
