using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IMasterStoppageType
    {
        bool AddMasterStoppageType(MasterStoppageType masterStoppageType);
        bool UpdateMasterStoppageType(MasterStoppageType masterStoppageType);
        bool DeleteMasterStoppageType(int id);
        List<MasterStoppageType> GetMasterStoppageTypeList();
        MasterStoppageType FindByCode(int id);
    }
}
