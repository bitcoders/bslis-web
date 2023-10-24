
using System.Collections.Generic;


namespace DataAccess.Interfaces
{
    interface IMasterParameterSubCategory
    {
        bool AddMasterParameterSubCategory(MasterParameterSubCategory masterParameterSubCategory);
        bool UpdateMasterParameterSubCategory(MasterParameterSubCategory masterParameterSubCategory);
        bool DeleteMasterParameterSubCategory(int id);
        List<MasterParameterSubCategory> GetMasterParameterSubCategoryList();
        MasterParameterSubCategory FindByCode(int id);
        List<MasterParameterSubCategory> GetMasterParameterCategoriesByParameterMasterCode(int code);
    }
}
