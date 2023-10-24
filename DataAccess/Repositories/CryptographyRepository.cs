using System;
using System.Text;
using DataAccess.Interfaces;
using System.Security.Cryptography;

namespace DataAccess.Repositories
{
    public class CryptographyRepository : ICryptography
    {
        public string GenerateHashedString(string plainText, string salt)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes((plainText + salt).ToString()));
                StringBuilder sb = new StringBuilder();
                for(int i =0; i< bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public string GenerateSalt(int minSize = 20, int maxSize = 30)
        {
            //generate a random number for the size of salt
            Random random = new Random();
            int saltSize = random.Next(minSize, maxSize);
            byte[] saltBytes = new byte[saltSize];

            //initialize a random number generator
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // fill the salt with cryptographically strong byte value            
                rng.GetNonZeroBytes(saltBytes);
                StringBuilder sb = new StringBuilder();
                for(int i =0; i< saltBytes.Length; i++)
                {
                    sb.Append(saltBytes[i].ToString("x2"));
                }
                return sb.ToString();
            
        }
    }
}
