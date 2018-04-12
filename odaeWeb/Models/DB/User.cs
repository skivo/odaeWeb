using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class User
    {
        public User()
        {
            UserFaseCodificador = new HashSet<UserFaseCodificador>();
        }

        public string UserId { get; set; }
        public int ProfileId { get; set; }
        public bool UserActivo { get; set; }
        public byte[] Password { get; set; }
        public bool MustChangePassword { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }

        public Profile Profile { get; set; }
        public ICollection<UserFaseCodificador> UserFaseCodificador { get; set; }
    }
}
