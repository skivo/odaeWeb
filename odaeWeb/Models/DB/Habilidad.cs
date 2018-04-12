using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Habilidad
    {
        public Habilidad()
        {
            Codificacion = new HashSet<Codificacion>();
        }

        public int HabilidadId { get; set; }
        public string NombreHabilidad { get; set; }

        public ICollection<Codificacion> Codificacion { get; set; }
    }
}
