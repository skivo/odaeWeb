using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Profile
    {
        public Profile()
        {
            User = new HashSet<User>();
        }

        public int ProfileId { get; set; }
        public string ProfileName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
