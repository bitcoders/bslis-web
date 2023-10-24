using System;
using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IKeySample
    {
        bool AddKeySample(KeySampleAnalys keySampleAnalys);
        bool EditKeySample(KeySampleAnalys keySampleAnalys);
        List<KeySampleAnalys> GetKeySampleAnalysListByDate(DateTime entryDateint,int unitCode);
        KeySampleAnalys GetKeySampleAnalysById(int unitCode, int seasonCode, int Id, DateTime dateTime);

    }
}
