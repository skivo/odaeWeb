using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace odaeWeb.Helpers
{
    public static class Cripto
    {
        public static byte[] HashPassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            return new SHA256Managed().ComputeHash(bytes);
        }
    }
}
