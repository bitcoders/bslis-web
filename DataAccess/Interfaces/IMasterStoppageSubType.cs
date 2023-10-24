
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    interface IMasterStoppageSubType
    {
        bool AddMasterStoppageSubType(MasterStoppageSubType masterStoppageSubType);
        bool UpdateMasterStoppageSubType(MasterStoppageSubType masterStoppageSubType);
        bool DeleteMasterStoppageSubType(int id);
        List<MasterStoppageSubType> GetMasterStoppageSubTypeList();
        List<MasterStoppageSubType> GetMasterStoppageSubListByMasterHead(int id);
        MasterStoppageSubType FindByCode(int id);
    }
}
