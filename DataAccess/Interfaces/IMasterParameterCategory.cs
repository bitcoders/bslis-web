
using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IMasterParameterCategory
    {
        bool AddMasterParameterCategory(MasterParameterCategory masterParameterCategory);
        bool UpdateMasterParameterCategory(MasterParameterCategory masterParameterCategory);
        bool DeleteMasterParameterCategory(int id);
        List<MasterParameterCategory> GetMasterParameterCategoryList();
        MasterParameterCategory FindByCode(int id);
    }
}
