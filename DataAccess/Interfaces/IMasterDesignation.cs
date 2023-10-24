using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterDesignation
    {
        bool addMasterDesignation(MasterDesignation masterDesignation);
        bool updateDesignation(MasterDesignation masterDesignation);
        bool deleteDesignation(int id);
        List<MasterDesignation> GetMasterDesignationList();
        MasterDesignation FindByPk(int id);
    }
}
