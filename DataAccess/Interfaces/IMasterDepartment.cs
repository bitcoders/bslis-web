using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterDepartment
    {
        bool addMasterDepartment(MasterDepartment masterDepartment);
        bool updateDepartment(MasterDepartment masterDepartment);
        bool deleteDepartment(int id);
        List<MasterDepartment> GetMasterDepartmentList();
        MasterDepartment FindByCode(int id);
    }
}
