using odaeWeb.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace odaeWeb.Helpers
{
    public sealed class Tools
    {
        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        public static string getToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
