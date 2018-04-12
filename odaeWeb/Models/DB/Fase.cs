using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Fase
    {
        public Fase()
        {
            Codificacion = new HashSet<Codificacion>();
            UserFaseCodificador = new HashSet<UserFaseCodificador>();
        }

        public int FaseId { get; set; }
        public string NombreFase { get; set; }
        public string DescripcionFase { get; set; }
        public bool FaseActiva { get; set; }

        public ICollection<Codificacion> Codificacion { get; set; }
        public ICollection<UserFaseCodificador> UserFaseCodificador { get; set; }
    }
}
