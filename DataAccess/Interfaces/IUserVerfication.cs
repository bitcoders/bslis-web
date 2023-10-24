using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IUserVerfication
    {
        bool AddUserVerification(UserVerification UserVerification);
        bool EditUserVerification(UserVerification UserVerification);
        bool DeleteUserVerfication(int id);
        List<UserVerification> GetUserVerificationList();
        UserVerification FindByPk(int id);
    }
}
