using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    interface IMasterMenu
    {
        bool AddMasterMenu(MasterMenu masterMenu);
        bool UpdateMasterMenu(MasterMenu masterMenu);
        bool DeleteMasterMenu(int id);
        List<MasterMenu> GetMasterMenuList();
        MasterMenu FindByCode(int id);
    }
}
