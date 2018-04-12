using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Nivel
    {
        public Nivel()
        {
            Codificacion = new HashSet<Codificacion>();
        }

        public int NivelId { get; set; }
        public string NivelDemandaCognitiva { get; set; }

        public ICollection<Codificacion> Codificacion { get; set; }
    }
}
