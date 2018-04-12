using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Codificador
    {
        public Codificador()
        {
            Codificacion = new HashSet<Codificacion>();
            UserFaseCodificador = new HashSet<UserFaseCodificador>();
        }

        public int CodificadorId { get; set; }
        public string NombreCodificador { get; set; }
        public string Email { get; set; }

        public ICollection<Codificacion> Codificacion { get; set; }
        public ICollection<UserFaseCodificador> UserFaseCodificador { get; set; }
    }
}
