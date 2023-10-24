using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    interface IMasterSubMenu
    {
        bool AddMasterSubMenu(MasterSubMenu masterSubMenu);
        bool UpdateMasterSubMenu(MasterSubMenu masterSubMenu);
        bool DeleteMasterSubMenu(int id);
        List<MasterSubMenu> GetMasterSubMenuList();
        MasterSubMenu FindByCode(int id);
    }
}
