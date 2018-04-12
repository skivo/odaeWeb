using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Escuela
    {
        public Escuela()
        {
            Estudiante = new HashSet<Estudiante>();
        }

        public short EscuelaId { get; set; }
        public string NombreEscuela { get; set; }
        public byte RegionId { get; set; }

        public Region Region { get; set; }
        public ICollection<Estudiante> Estudiante { get; set; }
    }
}
