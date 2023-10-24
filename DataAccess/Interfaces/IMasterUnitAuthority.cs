using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Interfaces
{
    interface IMasterUnitAuthority
    {
        bool addMasterUnitAuthorities(MasterUnitAuthority masterUnitAuthority);
        bool updateMasterUnitAuthorities(MasterUnitAuthority masterUnitAuthority);
        bool deleteMasterUnitAuthorities(int id);
        List<MasterUnitAuthority> GetMasterUnitAuthoritiesList();
        MasterUnitAuthority FindByCode(int code);
    }
}
