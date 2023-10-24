using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface ICaneAnalyses
    {
        string Add(CaneAnalys caneAnalysis);

        string Delete(int unitCode,int Code);

        string Update(int unitCode, int Code);

        List<CaneAnalys> GetCaneAnalyisList(int unitCode);

        CaneAnalys GetCaneAnalysisByCode(int unitCode, int Code);


    }
}
