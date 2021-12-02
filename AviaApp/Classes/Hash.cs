using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AviaApp.Classes
{
    public static class Hash
    {
        public static string HashPassword(string password, string salt)
        {
            byte[] data = Encoding.ASCII.GetBytes(salt + password);
            data = MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
