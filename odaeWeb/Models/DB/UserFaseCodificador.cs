using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class UserFaseCodificador
    {
        public string UserId { get; set; }
        public int CodificadorId { get; set; }
        public int FaseId { get; set; }

        public Codificador Codificador { get; set; }
        public Fase Fase { get; set; }
        public User User { get; set; }
    }
}
