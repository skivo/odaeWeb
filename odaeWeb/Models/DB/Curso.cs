using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Curso
    {
        public Curso()
        {
            Objetivo = new HashSet<Objetivo>();
        }

        public int CursoId { get; set; }
        public string NombreCurso { get; set; }

        public ICollection<Objetivo> Objetivo { get; set; }
    }
}
