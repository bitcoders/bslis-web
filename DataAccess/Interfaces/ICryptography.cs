using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface ICryptography
    {
        string GenerateSalt(int minSize, int maxSize);
        string GenerateHashedString(string plainText, string salt);
    }
}
