using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Objetivo
    {
        public Objetivo()
        {
            Codificacion = new HashSet<Codificacion>();
        }

        public int CursoId { get; set; }
        public int EjeId { get; set; }
        public int ObjetivoId { get; set; }
        public string ObjetivoAprendizaje { get; set; }

        public Curso Curso { get; set; }
        public Eje Eje { get; set; }
        public ICollection<Codificacion> Codificacion { get; set; }
    }
}
