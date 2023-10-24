using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IMasterUser
    {
        bool addMasterUser(MasterUser masterUser);
        bool updateUser(MasterUser masterUser);
        bool deleteUser(string code);
        List<MasterUser> GetMasterUserList();
        MasterUser FindByPk(string code);
        
    }
}
