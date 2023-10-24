using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterVehicle
    {
        bool AddMasterVehicle(MasterVehicle masterVehicle);
        bool UpdateMasterVehicle(MasterVehicle masterVehicle);
        bool DeleteMasterVehicle(int id);
        List<MasterVehicle> GetMasterVehicleList();
        MasterVehicle FindByCode(int id);
    }
}
